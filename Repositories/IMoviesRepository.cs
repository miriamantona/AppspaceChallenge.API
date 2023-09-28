using BeezyCinema = AppspaceChallenge.API.Model.BeezyCinema;
using TMDB = AppspaceChallenge.API.Model.TMBD;

namespace AppspaceChallenge.API.Repositories
{
  public interface IMoviesRepository
  {
    Task<IEnumerable<TMDB.Movie>> GetMoviesFromTMDB(DateTime from, DateTime to);

    IEnumerable<BeezyCinema.Movie> GetMoviesWithBiggestSeatsSold(int cinemaId);
  }
}
