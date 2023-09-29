using AppspaceChallenge.API.Constants;
using AppspaceChallenge.API.DTO.Input;
using AppspaceChallenge.API.DTO.Output;
using AppspaceChallenge.API.Repositories;
using TMBD = AppspaceChallenge.API.Model.TMBD;

namespace AppspaceChallenge.API.Services
{
  public class IntelligentBillBoardManager : IIntelligentBillBoardManager
  {
    private readonly IMoviesRepository _moviesRepository;

    private IEnumerable<TMBD.Movie> movies;
    private IList<TMBD.Movie> assignedMovies;
    private const int resultsPerPage = 20;

    public IntelligentBillBoardManager(IMoviesRepository moviesRepository)
    {
      _moviesRepository = moviesRepository;
    }

    public async Task<IntelligentBillboard> CreateIntelligentBillboard(IntelligentBillboardRequest request)
    {
      IntelligentBillboard billboard = new IntelligentBillboard();
      int numberOfDays = (request.To - request.From).Days + 1;
      int minimumResults = numberOfDays * (request.ScreensInBigRooms + request.ScreensInSmallRooms);
      int minimumPages = (int)Math.Ceiling((double)minimumResults / resultsPerPage);

      movies = await _moviesRepository.GetMoviesFromTMDB(request.From, request.To, minimumPages);
      assignedMovies = new List<TMBD.Movie>();

      for (DateTime date = request.From; date <= request.To; date = date.AddDays(8 - (int)date.DayOfWeek))
      {
        WeeklyPlanning weeklyPlanning = new WeeklyPlanning();

        for (DateTime currentDay = date; currentDay < date.AddDays(8 - (int)date.DayOfWeek) && currentDay <= request.To; currentDay = currentDay.AddDays(1))
        {
          switch (currentDay.DayOfWeek)
          {
            case DayOfWeek.Monday:
              weeklyPlanning.Monday = AssignMoviesToDailyPlanning(currentDay, request.ScreensInBigRooms, request.ScreensInSmallRooms);
              break;
            case DayOfWeek.Tuesday:
              weeklyPlanning.Tuesday = AssignMoviesToDailyPlanning(currentDay, request.ScreensInBigRooms, request.ScreensInSmallRooms);
              break;
            case DayOfWeek.Wednesday:
              weeklyPlanning.Wednesday = AssignMoviesToDailyPlanning(currentDay, request.ScreensInBigRooms, request.ScreensInSmallRooms);
              break;
            case DayOfWeek.Thursday:
              weeklyPlanning.Thursday = AssignMoviesToDailyPlanning(currentDay, request.ScreensInBigRooms, request.ScreensInSmallRooms);
              break;
            case DayOfWeek.Friday:
              weeklyPlanning.Friday = AssignMoviesToDailyPlanning(currentDay, request.ScreensInBigRooms, request.ScreensInSmallRooms);
              break;
            case DayOfWeek.Saturday:
              weeklyPlanning.Saturday = AssignMoviesToDailyPlanning(currentDay, request.ScreensInBigRooms, request.ScreensInSmallRooms);
              break;
            case DayOfWeek.Sunday:
              weeklyPlanning.Sunday = AssignMoviesToDailyPlanning(currentDay, request.ScreensInBigRooms, request.ScreensInSmallRooms);
              break;
            default:
              break;
          }
        }

        billboard.WeeklyPlannings.Add(weeklyPlanning);
      }
      return billboard;
    }

    public IEnumerable<Model.BeezyCinema.Movie> GetIntelligentBillboardWithSuccessfullMovies(IntelligentBillboardWithSuccessfullMoviesRequest request)
    {
      var result = _moviesRepository.GetMoviesWithBiggestSeatsSold(request.CinemaId);
      return result;
    }

    private DailyPlanning AssignMoviesToDailyPlanning(DateTime currentDay, int screensInBigRooms, int screensInSmallRooms)
    {
      return new DailyPlanning()
      {
        Date = currentDay,
        BigRoomMovies = SearchMoviesForBigRooms(currentDay, screensInBigRooms),
        SmallRoomMovies = SearchMoviesForSmallRooms(currentDay, screensInSmallRooms)
      };
    }

    private IList<string> SearchMoviesForBigRooms(DateTime currentDay, int screensInBigRooms)
    {
      var moviesForBigRooms = (from m in movies
                               where m.Release_date <= currentDay &&
                               Constants.Genres.BlockbusterGenres()
                                   .Any(blockbusterGenre => m.Genre_ids.Contains(Genres.GenreInfo[blockbusterGenre].TMDBId)) &&
                               !assignedMovies.Any(assignedMovie => assignedMovie.Title == m.Title)
                               select m).Take(screensInBigRooms).ToList();

      foreach (var movie in moviesForBigRooms)
      {
        this.assignedMovies.Add(movie);
      }

      return moviesForBigRooms.Select(m => m.Title).ToList();
    }

    private IList<string> SearchMoviesForSmallRooms(DateTime currentDay, int screensInSmallRooms)
    {
      var moviesForSmallRooms = (from m in movies
                               where m.Release_date <= currentDay &&
                               Constants.Genres.MinorityGenres()
                                   .Any(minorityGenre => m.Genre_ids.Contains(Genres.GenreInfo[minorityGenre].TMDBId)) &&
                               !assignedMovies.Any(assignedMovies => assignedMovies.Title == m.Title)
                               select m).Take(screensInSmallRooms).ToList();

      foreach (var movie in moviesForSmallRooms)
      {
        this.assignedMovies.Add(movie);
      }

      return moviesForSmallRooms.Select(m => m.Title).ToList();
    }
  }
}
