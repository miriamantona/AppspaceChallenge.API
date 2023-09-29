namespace AppspaceChallenge.API.DTO.Input
{
  public class IntelligentBillboardWithSuccessfullMoviesRequest
  {
    public int CinemaId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int ScreensInBigRooms { get; set; }
    public int ScreensInSmallRooms { get; set; }
  }
}
