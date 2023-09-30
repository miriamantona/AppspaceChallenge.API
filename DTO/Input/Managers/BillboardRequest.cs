namespace AppspaceChallenge.API.DTO.Input.Managers
{
  public class BillboardRequest: ManagerBaseRequest
  {
    public DateTime From { get; set; }
    public int NumberOfWeeks { get; set; }
    public int NumberOfScreens { get; set; }
  }

}
