namespace AppspaceChallenge.API.Model.BeezyCinema
{
  public class Room
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Size { get; set; }
    public int Seats { get; set; }
    public int CinemaId { get; set; }
  }
}
