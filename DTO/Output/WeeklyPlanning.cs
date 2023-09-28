namespace AppspaceChallenge.API.DTO.Output
{
  public class WeeklyPlanning
  {
    public DailyPlanning Monday { get; set; }
    public DailyPlanning Tuesday { get; set; }
    public DailyPlanning Wednesday { get; set; }
    public DailyPlanning Thursday { get; set; }
    public DailyPlanning Friday { get; set; }
    public DailyPlanning Saturday { get; set; }
    public DailyPlanning Sunday { get; set; }
  }
}
