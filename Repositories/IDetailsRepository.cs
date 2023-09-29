using AppspaceChallenge.API.Model.TMBD;

namespace AppspaceChallenge.API.Repositories
{
  public interface IDetailsRepository
  {
    Task<Details> GetDetails(int movieId);
  }
}
