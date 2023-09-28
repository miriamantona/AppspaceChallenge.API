namespace AppspaceChallenge.API.Model.BeezyCinema
{
  public class Cinema
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime OpenSince { get; set; }
    public int CityId { get; set; }
  }
}
