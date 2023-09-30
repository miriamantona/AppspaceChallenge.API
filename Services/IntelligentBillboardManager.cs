using AppspaceChallenge.DataAccess.Repositories;
using BeezyCinema = AppspaceChallenge.DataAccess.Model.BeezyCinema;
using DTOInput = AppspaceChallenge.API.DTO.Input;
using DTOOutput = AppspaceChallenge.API.DTO.Output;
using TMBD = AppspaceChallenge.DataAccess.Model.TMBD;

namespace AppspaceChallenge.API.Services
{
  public class IntelligentBillBoardManager : IIntelligentBillBoardManager
  {
    private readonly IMoviesRepository _moviesRepository;
    private readonly IGenresRepository _genresRepository;
    private readonly IKeywordsRepository _keywordsRepository;
    private readonly IDetailsRepository _detailsRepository;

    private List<TMBD.Movie> movies;
    private IEnumerable<BeezyCinema.Movie> sucessfulMoviesInCity;
    private IList<string> assignedMovies;
    private const int resultsPerPage = 20;
    private IEnumerable<TMBD.Genre> genres;

    public IntelligentBillBoardManager(IMoviesRepository moviesRepository,
      IGenresRepository genresRepository,
      IKeywordsRepository keywordsRepository,
      IDetailsRepository detailsRepository)
    {
      _moviesRepository = moviesRepository;
      _genresRepository = genresRepository;
      _keywordsRepository = keywordsRepository;
      _detailsRepository = detailsRepository;
    }

    public async Task<DTOOutput.IntelligentBillboard> CreateIntelligentBillboard(DTOInput.IntelligentBillboardRequest request)
    {
      int numberOfWeeks = CompleteWeeks(request.From, request.To);
      int minimumResults = numberOfWeeks * (request.ScreensInBigRooms + request.ScreensInSmallRooms);
      int minimumPages = (int)Math.Ceiling((double)minimumResults / resultsPerPage);
      movies = new List<TMBD.Movie>();

      for (var currentPage = 1; currentPage <= minimumPages; currentPage++)
      {
        var result = await _moviesRepository.GetMoviesFromTMDB(request.From, request.To, currentPage);
        movies.AddRange(result);
      }

      if (request.IncludeSuccessfulMoviesInCity && request.CityId.HasValue && request.CityId > 0)
      {
        sucessfulMoviesInCity = _moviesRepository.GetMoviesWithBiggestSeatsSold(request.CityId.Value);
      }

      if (movies == null || !movies.Any())
        throw new Exception("No movies found");

      genres = await _genresRepository.GetGenres();

      if (movies == null || !movies.Any())
        throw new Exception("No genres found");

      DTOOutput.IntelligentBillboard billboard = new DTOOutput.IntelligentBillboard();
      assignedMovies = new List<string>();

      for (DateTime date = request.From; date <= request.To; date = date.AddDays(8 - (int)date.DayOfWeek))
      {
        DTOOutput.WeeklyPlanning weeklyPlanning = new DTOOutput.WeeklyPlanning()
        {
          From = date,
          To = date.AddDays(7 - (int)date.DayOfWeek),
          BigRoomMovies = await SearchMoviesForBigRooms(date, request, minimumPages),
          SmallRoomMovies = await SearchMoviesForSmallRooms(date, request, minimumPages)
        };
        billboard.WeeklyPlannings.Add(weeklyPlanning);
      }
      return billboard;
    }
    static int CompleteWeeks(DateTime from, DateTime to)
    {
      int days = (int)(to - from).TotalDays + 1;
      int extraDays = (int)from.DayOfWeek;
      int totalDays = days + extraDays;

      return totalDays / 7;
    }

    private async Task<IList<DTOOutput.SuggestedMovie>> SearchMoviesForBigRooms(DateTime currentDay, DTOInput.IntelligentBillboardRequest request, int minimumPages)
    {
      IList<DTOOutput.SuggestedMovie> suggestedMovies = new List<DTOOutput.SuggestedMovie>();

      // Check first if the are sucessful movies in the city.
      if (sucessfulMoviesInCity != null && sucessfulMoviesInCity.Any())
      {
        var successfulMoviesForBigRooms = (from m in sucessfulMoviesInCity
                                           where !assignedMovies.Any(assignedMovie => assignedMovie == m.OriginalTitle)
                                           select m).Take(request.ScreensInBigRooms);

        foreach (var movie in successfulMoviesForBigRooms)
        {
          suggestedMovies.Add(await PrepareMovieFromBeezyCinemaToDTO(movie));
          this.assignedMovies.Add(movie.OriginalTitle);
        }
      }

      if (suggestedMovies.Count < request.ScreensInBigRooms)
      {
        // A movie is considered for a big room if more than half of its genres are classified as blockbusters.

        var moviesForBigRooms = (from m in movies
                                 let blockbusterGenres = m.Genre_ids.Count(g => Constants.Genres.BlockbusterGenres.Any(bg => bg.Value.TMDBId == g))
                                 where m.Release_date <= currentDay &&
                                       blockbusterGenres > m.Genre_ids.Count / 2 &&
                                       !assignedMovies.Any(assignedMovie => assignedMovie == m.Title)
                                 select m).Take(request.ScreensInBigRooms - suggestedMovies.Count);

        if (moviesForBigRooms == null || !moviesForBigRooms.Any())
        {
          movies.AddRange(await _moviesRepository.GetMoviesFromTMDB(request.From, request.To, minimumPages++));
          await SearchMoviesForBigRooms(currentDay, request, minimumPages++);
        }

        foreach (var movie in moviesForBigRooms)
        {
          suggestedMovies.Add(await PrepareMovieFromTMDBToDTO(movie));
          this.assignedMovies.Add(movie.Title);
        }
      }
      return suggestedMovies;
    }

    private async Task<IList<DTOOutput.SuggestedMovie>> SearchMoviesForSmallRooms(DateTime currentDay, DTOInput.IntelligentBillboardRequest request, int minimumPages)
    {
      // A movie is considered for a small room if more than half of its genres are classified as minority genres.

      IList<DTOOutput.SuggestedMovie> suggestedMovies = new List<DTOOutput.SuggestedMovie>();

      var moviesForSmalRooms = (from m in movies
                                let minorityGenres = m.Genre_ids.Count(g => Constants.Genres.MinorityGenres.Any(bg => bg.Value.TMDBId == g))
                                where m.Release_date <= currentDay &&
                                      minorityGenres > m.Genre_ids.Count / 2 &&
                                      !assignedMovies.Any(assignedMovie => assignedMovie == m.Title)
                                select m).Take(request.ScreensInSmallRooms);

      if (moviesForSmalRooms == null || !moviesForSmalRooms.Any())
      {
        movies.AddRange(await _moviesRepository.GetMoviesFromTMDB(request.From, request.To, minimumPages++));
        await SearchMoviesForSmallRooms(currentDay, request, minimumPages++);
      }

      foreach (var movie in moviesForSmalRooms)
      {
        suggestedMovies.Add(await PrepareMovieFromTMDBToDTO(movie));
        this.assignedMovies.Add(movie.Title);
      }

      return suggestedMovies;
    }

    private async Task<DTOOutput.SuggestedMovie> PrepareMovieFromTMDBToDTO(TMBD.Movie movie)
    {
      return new DTOOutput.SuggestedMovie
      {
        Title = movie.Title,
        Overview = movie.Overview,
        ReleaseDate = movie.Release_date,
        Language = movie.Original_language,
        Genres = movie.Genre_ids.Select(id => genres.FirstOrDefault(g => g.Id == id).Name).ToList(),
        Keywords = await _keywordsRepository.GetKeywordNames(movie.Id),
        Website = (await _detailsRepository.GetDetails(movie.Id)).Homepage
      };
    }

    private async Task<DTOOutput.SuggestedMovie> PrepareMovieFromBeezyCinemaToDTO(BeezyCinema.Movie movie)
    {
      var movieTMDB = await _moviesRepository.GetMovieFromTMDB(movie.OriginalTitle);

      return new DTOOutput.SuggestedMovie
      {
        Title = movie.OriginalTitle,
        Overview = movieTMDB.Overview,
        ReleaseDate = movie.ReleaseDate,
        Language = movie.OriginalLanguage,
        Genres = movie.MovieGenres.Select(g => g.Genre.Name).ToList(),
        Keywords = await _keywordsRepository.GetKeywordNames(movieTMDB.Id),
        Website = (await _detailsRepository.GetDetails(movieTMDB.Id)).Homepage
      };
    }
  }
}
