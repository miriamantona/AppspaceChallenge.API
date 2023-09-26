using AppspaceChallenge.API.Models;

namespace AppspaceChallenge.API.Repositories
{
  public interface IMoviesRepository
  {
    Task<IEnumerable<Movie>> GetMovies(DateTime from, DateTime to);
  }
}
