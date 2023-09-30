namespace AppspaceChallenge.API.DTO.Input.Viewers
{
  public class AllTimeRecomendedMoviesRequest
  {
    public List<string> PreferredKeywords { get; set; }
    public List<string> PreferredGenres { get; set; }
  }
}
