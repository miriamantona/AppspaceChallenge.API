namespace AppspaceChallenge.API.DTO.Input
{
  public class IntelligentBillboardRequest
  {
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int ScreensInBigRooms { get; set; }
    public int ScreensInSmallRooms { get; set; }
  }
}
