using TMBD = AppspaceChallenge.API.Model.TMBD;

namespace AppspaceChallenge.API.Responses
{
  public class GenresListResponse
  {
    public List<TMBD.Genre> Genres { get; set; }
  }
}
