namespace AppspaceChallenge.API.DTO.Input.Viewers
{
  public class UpcomingRecommendedMoviesForViewersRequest
  {
    public DateTime to { get; set; }
    public List<string> PreferredKeywords { get; set; }
    public List<string> PreferredGenres { get; set; }
  }
}
