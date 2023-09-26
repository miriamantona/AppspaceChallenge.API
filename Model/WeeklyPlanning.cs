using System;

namespace AppspaceChallenge.API.Models
{
  public class WeeklyPlanning
  {
    public DateTime Date { get; set; }
    public List<Movie> BigRoomMovies { get; set; }
    public List<Movie> SmallRoomMovies { get; set; }
  }
}
