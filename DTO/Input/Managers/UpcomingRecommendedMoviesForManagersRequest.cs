namespace AppspaceChallenge.API.DTO.Input.Managers
{
  public class UpcomingRecommendedMoviesForManagersRequest: ManagerBaseRequest
  {
    public DateTime to { get; set; }
    public int AgeRate { get; set; }
    public string Genre { get; set; }
  }
}
