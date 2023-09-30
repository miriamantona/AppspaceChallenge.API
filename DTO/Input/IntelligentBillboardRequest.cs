namespace AppspaceChallenge.API.DTO.Input
{
  public class IntelligentBillboardRequest
  {
    //Add validation filters!
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int ScreensInBigRooms { get; set; }
    public int ScreensInSmallRooms { get; set; }
    public bool IncludeSuccessfulMoviesInCity { get; set; }
    public int? CityId { get; set; }
  }
}
