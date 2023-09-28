using System;

namespace AppspaceChallenge.API.DTO
{
  public class WeeklyPlanning
  {
    public DailyPlanning Sunday { get; set; }
    public DailyPlanning Monday { get; set; }
    public DailyPlanning Tuesday { get; set; }
    public DailyPlanning Wednesday { get; set; }
    public DailyPlanning Thursday { get; set; }
    public DailyPlanning Friday { get; set; }
    public DailyPlanning Saturday { get; set; }
  }
}
