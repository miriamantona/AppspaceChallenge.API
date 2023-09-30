namespace AppspaceChallenge.API.DTO.Output
{
  public class IntelligentWeeklyPlanning
  {
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public ICollection<MovieRecommendation> BigRoomMovies { get; set; }
    public ICollection<MovieRecommendation> SmallRoomMovies { get; set; }
  }
}
