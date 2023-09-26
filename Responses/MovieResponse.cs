namespace AppspaceChallenge.API.Responses
{
  public class MovieListResponse
  {
    public List<MovieResponse> Results { get; set; }
  }

  public class MovieResponse
  {
    public string Title { get; set; }
    public string Overview { get; set; }
    public List<int> Genre_ids { get; set; }
  }
}
