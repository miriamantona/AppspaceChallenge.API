namespace AppspaceChallenge.API.DTO
{
  public class IntelligentBillboard
  {
    public List<WeeklyPlanning> WeeklyPlannings { get; set; }

    public IntelligentBillboard()
    {
      this.WeeklyPlannings = new List<WeeklyPlanning>();
    }
  }
}
