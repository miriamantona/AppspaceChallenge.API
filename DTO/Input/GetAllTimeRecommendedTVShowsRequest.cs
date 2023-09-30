namespace AppspaceChallenge.API.DTO.Input
{
  public class GetAllTimeRecommendedTVShowsRequest
  {
    public List<string> PreferredKeywords { get; set; }
    public List<string> PreferredGenres { get; set; }
  }
}
