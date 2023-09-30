namespace AppspaceChallenge.API.DTO.Input
{
  public class GetAllTimeRecomendedMoviesRequest
  {
    public List<string> PreferredKeywords { get; set; }
    public List<string> PreferredGenres { get; set; }
  }
}
