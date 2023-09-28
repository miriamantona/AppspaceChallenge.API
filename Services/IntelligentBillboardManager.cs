using AppspaceChallenge.API.DTO;
using AppspaceChallenge.API.Repositories;
using TMBD = AppspaceChallenge.API.Model.TMBD;
using Constants = AppspaceChallenge.API.Constants;
using AppspaceChallenge.API.Constants;

namespace AppspaceChallenge.API.Services
{
  public class IntelligentBillBoardManager : IIntelligentBillBoardManager
  {
    private readonly IMoviesRepository _moviesRepository;
    private readonly IGenresRepository _genresRepository;

    public IntelligentBillBoardManager(IMoviesRepository moviesRepository)
    {
      _moviesRepository = moviesRepository;
    }

    public async Task<IntelligentBillboard> CreateIntelligentBillboard(DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms)
    {
      IntelligentBillboard billboard = new IntelligentBillboard();

      var movies = await _moviesRepository.GetMoviesFromTMDB(from, to);

      for (DateTime date = from; date <= to; date = from.AddDays(6 - (int)from.DayOfWeek))
      {
        WeeklyPlanning weeklyPlanning = new WeeklyPlanning();

        for (DateTime currentDay = date; currentDay < from.AddDays(6 - (int)from.DayOfWeek); currentDay = currentDay.AddDays(1))
        {
          switch (currentDay.DayOfWeek)
          {
            case DayOfWeek.Sunday:
              weeklyPlanning.Sunday = new DailyPlanning()
              {
                Date = currentDay,
                BigRoomMovies = SearchMoviesForBigRooms(currentDay, screensInBigRooms, movies),
                SmallRoomMovies = SearchMoviesForSmallRooms(currentDay, screensInSmallRooms, movies)
              };
              break;
            case DayOfWeek.Monday:
              weeklyPlanning.Monday = new DailyPlanning()
              {
                Date = currentDay,
                BigRoomMovies = SearchMoviesForBigRooms(currentDay, screensInBigRooms, movies),
                SmallRoomMovies = SearchMoviesForSmallRooms(currentDay, screensInSmallRooms, movies)
              };
              break;
            case DayOfWeek.Tuesday:
              weeklyPlanning.Tuesday = new DailyPlanning()
              {
                Date = currentDay,
                BigRoomMovies = SearchMoviesForBigRooms(currentDay, screensInBigRooms, movies),
                SmallRoomMovies = SearchMoviesForSmallRooms(currentDay, screensInSmallRooms, movies)
              };
              break;
            case DayOfWeek.Wednesday:
              weeklyPlanning.Wednesday = new DailyPlanning()
              {
                Date = currentDay,
                BigRoomMovies = SearchMoviesForBigRooms(currentDay, screensInBigRooms, movies),
                SmallRoomMovies = SearchMoviesForSmallRooms(currentDay, screensInSmallRooms, movies)
              };
              break;
            case DayOfWeek.Thursday:
              weeklyPlanning.Thursday = new DailyPlanning()
              {
                Date = currentDay,
                BigRoomMovies = SearchMoviesForBigRooms(currentDay, screensInBigRooms, movies),
                SmallRoomMovies = SearchMoviesForSmallRooms(currentDay, screensInSmallRooms, movies)
              };
              break;
            case DayOfWeek.Friday:
              weeklyPlanning.Friday = new DailyPlanning()
              {
                Date = currentDay,
                BigRoomMovies = SearchMoviesForBigRooms(currentDay, screensInBigRooms, movies),
                SmallRoomMovies = SearchMoviesForSmallRooms(currentDay, screensInSmallRooms, movies)
              };
              break;
            case DayOfWeek.Saturday:
              weeklyPlanning.Saturday = new DailyPlanning()
              {
                Date = currentDay,
                BigRoomMovies = SearchMoviesForBigRooms(currentDay, screensInBigRooms, movies),
                SmallRoomMovies = SearchMoviesForSmallRooms(currentDay, screensInSmallRooms, movies)
              };
              break;
            default:
              break;
          }
        }

        billboard.WeeklyPlannings.Add(weeklyPlanning);
      }
      return null;
    }

    private List<string> SearchMoviesForBigRooms(DateTime currentDay, int screensInBigRooms, IEnumerable<TMBD.Movie> movies)
    {
      var query = (from m in movies  
                  where m.Release_date <= currentDay &&
                  Constants.Genres.BlockbusterGenres()
                      .Any(blockbusterGenre => m.Genre_ids.Contains(Genres.GenreInfo[blockbusterGenre].TMDBId))
                  select m.Title).Take(screensInBigRooms);

      var result = query.ToList();
      return result;
    }

    private List<string> SearchMoviesForSmallRooms(DateTime currentDay, int screensInSmallRooms, IEnumerable<TMBD.Movie> movies)
    {
      var query = (from m in movies
                   where m.Release_date <= currentDay &&
                   Constants.Genres.MinorityGenres()
                       .Any(minorityGenre => m.Genre_ids.Contains(Genres.GenreInfo[minorityGenre].TMDBId))
                   select m.Title).Take(screensInSmallRooms);

      var result = query.ToList();
      return result;
    }

    public IEnumerable<Model.BeezyCinema.Movie> GetIntelligentBillboardWithSuccessfullMovies(int cinemaId, DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms)
    {
      var result = _moviesRepository.GetMoviesWithBiggestSeatsSold(cinemaId);
      return result;
    }
  }
}
