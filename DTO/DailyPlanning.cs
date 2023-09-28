namespace AppspaceChallenge.API.DTO
{
  public class DailyPlanning
  {
    public DateTime Date { get; set; }
    public ICollection<string> BigRoomMovies { get; set; }
    public ICollection<string> SmallRoomMovies { get; set; }
  }
}
