namespace AppspaceChallenge.API.DTO.Output
{
  public class WeeklyPlanning
  {
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public ICollection<MovieRecommendation> Movies { get; set; }
  }
}
