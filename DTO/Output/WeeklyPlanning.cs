namespace AppspaceChallenge.API.DTO.Output
{
  public class WeeklyPlanning
  {
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public ICollection<MovieRecommendation> BigRoomMovies { get; set; }
    public ICollection<MovieRecommendation> SmallRoomMovies { get; set; }
  }
}
