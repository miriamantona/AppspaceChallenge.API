namespace AppspaceChallenge.API.DTO.Input
{
  public class GetRecommendedUpcomingMoviesFromNowRequest
  {
    public DateTime to { get; set; }
    public List<string> PreferredKeywords { get; set; }
    public List<string> PreferredGenres { get; set; }
  }
}
