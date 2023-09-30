using System.Collections.ObjectModel;

namespace AppspaceChallenge.API.DTO.Output
{
  public class Billboard
  {
    public ICollection<WeeklyPlanning> WeeklyPlannings { get; set; }

    public Billboard()
    {
      WeeklyPlannings = new Collection<WeeklyPlanning>();
    }
  }
}
