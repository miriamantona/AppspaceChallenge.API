namespace AppspaceChallenge.API.Model.TMBD
{
  public class Movie
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public List<int> Genre_ids { get; set; }
    public DateTime Release_date { get; set; }
    public string Original_language { get; set; }
    public string Overview { get; set; }
  }
}
