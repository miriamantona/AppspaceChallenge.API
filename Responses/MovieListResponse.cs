using TMBD = AppspaceChallenge.API.Model.TMBD;

namespace AppspaceChallenge.API.Responses
{
  public class MovieListResponse
  {
    public List<TMBD.Movie> results { get; set; }
  }
}
