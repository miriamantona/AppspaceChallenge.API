using Newtonsoft.Json;

namespace AppspaceChallenge.API.Models
{
  public class Movie
  {
    public string Title { get; set; }
    public string Overview { get; set; }
    public List<string> Genres { get; set; }
  }
}
