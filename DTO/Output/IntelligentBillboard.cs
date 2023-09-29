using System.Collections.ObjectModel;

namespace AppspaceChallenge.API.DTO.Output
{
  public class IntelligentBillboard
  {
    public ICollection<WeeklyPlanning> WeeklyPlannings { get; set; }

    public IntelligentBillboard()
    {
      WeeklyPlannings = new Collection<WeeklyPlanning>();
    }
  }
}
