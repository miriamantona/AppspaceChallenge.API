using AppspaceChallenge.API.DTO.Input.Managers;
using AppspaceChallenge.DataAccess.Constants;
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
    private readonly IKeywordsRepository _keywordsRepository;
    private readonly IDetailsRepository _detailsRepository;

    private List<TMBD.Movie> movies;
    private IEnumerable<BeezyCinema.Movie> sucessfulMoviesInCity;
    private IList<string> assignedMovies;
    private const int resultsPerPage = 20;

    public IntelligentBillBoardManager(IMoviesRepository moviesRepository,
      IKeywordsRepository keywordsRepository,
      IDetailsRepository detailsRepository)
    {
      _moviesRepository = moviesRepository;
      _keywordsRepository = keywordsRepository;
      _detailsRepository = detailsRepository;
    }

    public async Task<DTOOutput.IntelligentBillboard> CreateIntelligentBillboard(IntelligentBillboardRequest request)
    {
      DTOOutput.IntelligentBillboard billboard = new DTOOutput.IntelligentBillboard();
      assignedMovies = new List<string>();
      movies = new List<TMBD.Movie>();

      if (request.IncludeSuccessfulMoviesInCity && request.CityId.HasValue && request.CityId > 0)
      {
        sucessfulMoviesInCity = _moviesRepository.GetMoviesWithBiggestSeatsSold(request.CityId.Value);
      }

      int numberOfWeeks = CompleteWeeks(request.From, request.To);
      int minimumResults = numberOfWeeks * (request.ScreensInBigRooms + request.ScreensInSmallRooms);
      int minimumPages = (int)Math.Ceiling((double)minimumResults / resultsPerPage);      

      await GetMoviesFromTMDB(request.From, request.To, minimumPages);

      for (DateTime date = request.From; date <= request.To; date = date.AddDays(8 - (int)date.DayOfWeek))
      {
        DTOOutput.IntelligentWeeklyPlanning weeklyPlanning = new DTOOutput.IntelligentWeeklyPlanning()
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

    private async Task GetMoviesFromTMDB(DateTime from, DateTime to, int minimumPages)
    {
      for (var currentPage = 1; currentPage <= minimumPages; currentPage++)
      {
        var result = await _moviesRepository.GetMoviesFromTMDB(from, to, currentPage);
        movies.AddRange(result);
      }

      if (movies == null || !movies.Any())
        throw new Exception("No movies found");
    }

    static int CompleteWeeks(DateTime from, DateTime to)
    {
      int days = (int)(to - from).TotalDays + 1;
      int extraDays = (int)from.DayOfWeek;
      int totalDays = days + extraDays;

      return totalDays / 7;
    }

    private async Task<IList<DTOOutput.MovieRecommendation>> SearchMoviesForBigRooms(DateTime currentDay, IntelligentBillboardRequest request, int minimumPages)
    {
      IList<DTOOutput.MovieRecommendation> recommendedMovies = new List<DTOOutput.MovieRecommendation>();

      // Check first if the are sucessful movies in the city.
      if (sucessfulMoviesInCity != null && sucessfulMoviesInCity.Any())
      {
        var successfulMoviesForBigRooms = (from m in sucessfulMoviesInCity
                                           where !assignedMovies.Any(assignedMovie => assignedMovie == m.OriginalTitle)
                                           select m).Take(request.ScreensInBigRooms);

        foreach (var movie in successfulMoviesForBigRooms)
        {
          recommendedMovies.Add(await PrepareMovieFromBeezyCinemaToDTO(movie));
          this.assignedMovies.Add(movie.OriginalTitle);
        }
      }

      if (recommendedMovies.Count < request.ScreensInBigRooms)
      {
        // A movie is considered for a big room if more than half of its genres are classified as blockbusters.

        var moviesForBigRooms = (from m in movies
                                 let blockbusterGenres = m.Genre_ids.Count(g => Genres.BlockbusterGenres.Any(bg => bg.Value.TMDBId == g))
                                 where m.Release_date <= currentDay &&
                                       blockbusterGenres > m.Genre_ids.Count / 2 &&
                                       !assignedMovies.Any(assignedMovie => assignedMovie == m.Title)
                                 select m).Take(request.ScreensInBigRooms - recommendedMovies.Count);

        if (moviesForBigRooms == null || !moviesForBigRooms.Any())
        {
          movies.AddRange(await _moviesRepository.GetMoviesFromTMDB(request.From, request.To, minimumPages++));
          await SearchMoviesForBigRooms(currentDay, request, minimumPages++);
        }

        foreach (var movie in moviesForBigRooms)
        {
          recommendedMovies.Add(await PrepareMovieFromTMDBToDTO(movie));
          this.assignedMovies.Add(movie.Title);
        }
      }
      return recommendedMovies;
    }

    private async Task<IList<DTOOutput.MovieRecommendation>> SearchMoviesForSmallRooms(DateTime currentDay, IntelligentBillboardRequest request, int minimumPages)
    {
      // A movie is considered for a small room if more than half of its genres are classified as minority genres.

      IList<DTOOutput.MovieRecommendation> recommendedMovies = new List<DTOOutput.MovieRecommendation>();

      var moviesForSmalRooms = (from m in movies
                                let minorityGenres = m.Genre_ids.Count(g => Genres.MinorityGenres.Any(bg => bg.Value.TMDBId == g))
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
        recommendedMovies.Add(await PrepareMovieFromTMDBToDTO(movie));
        this.assignedMovies.Add(movie.Title);
      }

      return recommendedMovies;
    }

    private async Task<DTOOutput.MovieRecommendation> PrepareMovieFromTMDBToDTO(TMBD.Movie movie)
    {
      return new DTOOutput.MovieRecommendation
      {
        Title = movie.Title,
        Overview = movie.Overview,
        ReleaseDate = movie.Release_date,
        Language = movie.Original_language,
        Genres = movie.Genre_ids.Select(id => Genres.GetGenreByTMDBId(id)).ToList(),
        Keywords = await _keywordsRepository.GetKeywordNames(movie.Id),
        Website = (await _detailsRepository.GetDetails(movie.Id)).Homepage
      };
    }

    private async Task<DTOOutput.MovieRecommendation> PrepareMovieFromBeezyCinemaToDTO(BeezyCinema.Movie movie)
    {
      var movieTMDB = await _moviesRepository.GetMovieFromTMDB(movie.OriginalTitle);

      return new DTOOutput.MovieRecommendation
      {
        Title = movie.OriginalTitle,
        Overview = movieTMDB.Overview,
        ReleaseDate = movie.ReleaseDate,
        Language = movie.OriginalLanguage,
        Genres = movie.MovieGenres.Select(g => Genres.GetGenreByBeezyCinemaId(g.GenreId)).ToList(),
        Keywords = await _keywordsRepository.GetKeywordNames(movieTMDB.Id),
        Website = (await _detailsRepository.GetDetails(movieTMDB.Id)).Homepage
      };
    }
  }
}
