namespace AppspaceChallenge.API.DTO.Input.Managers
{
  public class ManagerBaseRequest
  {
    public bool IncludeSuccessfulMoviesInCity { get; set; }
    public int? CityId { get; set; }
  }
}
