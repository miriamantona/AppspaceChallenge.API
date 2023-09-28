using AppspaceChallenge.API.Model.TMBD;

namespace AppspaceChallenge.API.Repositories
{
  public interface IGenresRepository
  {
    Task<IEnumerable<Genre>> GetGenres();
  }
}
