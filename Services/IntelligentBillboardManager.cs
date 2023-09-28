using AppspaceChallenge.API.DTO;
using AppspaceChallenge.API.Repositories;
using TMBD = AppspaceChallenge.API.Model.TMBD;
using Constants = AppspaceChallenge.API.Constants;
using AppspaceChallenge.API.Constants;
using AppspaceChallenge.API.Model.TMBD;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Collections.Generic;


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

    public async Task<IntelligentBillboard> CreateIntelligentBillboard(DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms)//DTO???
    {
      IntelligentBillboard billboard = new IntelligentBillboard();
      int numberOfDays = (to - from).Days + 1;
      int minimumResults = numberOfDays * (screensInBigRooms + screensInSmallRooms);
      int minimumPages = (int)Math.Ceiling((double)minimumResults / resultsPerPage);

      movies = await _moviesRepository.GetMoviesFromTMDB(from, to, minimumPages);
      assignedMovies = new List<TMBD.Movie>();

      for (DateTime date = from; date <= to; date = date.AddDays(8 - (int)date.DayOfWeek))
      {
        WeeklyPlanning weeklyPlanning = new WeeklyPlanning();

        for (DateTime currentDay = date; currentDay < date.AddDays(8 - (int)date.DayOfWeek) && currentDay <= to; currentDay = currentDay.AddDays(1))
        {
          switch (currentDay.DayOfWeek)
          {
            case DayOfWeek.Monday:
              weeklyPlanning.Monday = AssignMoviesToDailyPlanning(currentDay, screensInBigRooms, screensInSmallRooms);
              break;
            case DayOfWeek.Tuesday:
              weeklyPlanning.Tuesday = AssignMoviesToDailyPlanning(currentDay, screensInBigRooms, screensInSmallRooms);
              break;
            case DayOfWeek.Wednesday:
              weeklyPlanning.Wednesday = AssignMoviesToDailyPlanning(currentDay, screensInBigRooms, screensInSmallRooms);
              break;
            case DayOfWeek.Thursday:
              weeklyPlanning.Thursday = AssignMoviesToDailyPlanning(currentDay, screensInBigRooms, screensInSmallRooms);
              break;
            case DayOfWeek.Friday:
              weeklyPlanning.Friday = AssignMoviesToDailyPlanning(currentDay, screensInBigRooms, screensInSmallRooms);
              break;
            case DayOfWeek.Saturday:
              weeklyPlanning.Saturday = AssignMoviesToDailyPlanning(currentDay, screensInBigRooms, screensInSmallRooms);
              break;
            case DayOfWeek.Sunday:
              weeklyPlanning.Sunday = AssignMoviesToDailyPlanning(currentDay, screensInBigRooms, screensInSmallRooms);
              break;
            default:
              break;
          }
        }

        billboard.WeeklyPlannings.Add(weeklyPlanning);
      }
      return billboard;
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


    public IEnumerable<Model.BeezyCinema.Movie> GetIntelligentBillboardWithSuccessfullMovies(int cinemaId, DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms)
    {
      var result = _moviesRepository.GetMoviesWithBiggestSeatsSold(cinemaId);
      return result;
    }
  }
}
