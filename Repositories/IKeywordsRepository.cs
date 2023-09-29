using TMBD = AppspaceChallenge.API.Model.TMBD;

namespace AppspaceChallenge.API.Repositories
{
  public interface IKeywordsRepository
  {
    Task<IEnumerable<TMBD.Keyword>> GetKeywords(int movieId);
    Task<ICollection<string>> GetKeywordNames(int movieId);
  }
}
