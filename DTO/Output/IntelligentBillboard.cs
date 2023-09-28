namespace AppspaceChallenge.API.DTO.Output
{
  public class IntelligentBillboard
  {
    public List<WeeklyPlanning> WeeklyPlannings { get; set; }

    public IntelligentBillboard()
    {
      WeeklyPlannings = new List<WeeklyPlanning>();
    }
  }
}
