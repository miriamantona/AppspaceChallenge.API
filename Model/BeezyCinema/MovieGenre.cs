namespace AppspaceChallenge.API.Model.BeezyCinema
{
  public class MovieGenre
  {
    public int MovieId { get; set; }
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    public Movie Movie { get; set; }
  }
}
