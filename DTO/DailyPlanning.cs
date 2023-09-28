namespace AppspaceChallenge.API.DTO
{
  public class DailyPlanning
  {
    public DateTime Date { get; set; }
    public List<string> BigRoomMovies { get; set; }
    public List<string> SmallRoomMovies { get; set; }
  }
}
