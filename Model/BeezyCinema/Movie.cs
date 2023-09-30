namespace AppspaceChallenge.API.Model.BeezyCinema
{
  public class Movie
  {
    public int Id { get; set; }
    public string OriginalTitle { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string OriginalLanguage { get; set; }
    public bool Adult { get; set; }
    public ICollection<MovieGenre> MovieGenres { get; set; }
  }
}
