namespace AppspaceChallenge.API.DTO.Output
{
  public interface IRecommendation
  {
    string Title { get; }
    string Overview { get; }
    ICollection<string> Genres { get; }
    string Language { get; }
    DateTime? ReleaseDate { get; }
    string Website { get; }
    ICollection<string> Keywords { get; }
  }
}
