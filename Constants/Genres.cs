namespace AppspaceChallenge.API.Constants
{
  public static class Genres
  {
    public static Dictionary<string, (int BeezyCinemaId, int TMDBId)> BlockbusterGenres = new Dictionary<string, (int BeezyCinemaId, int TMDBId)>
    {
      { "Action", (1, 28) },
      { "Adventure", (2, 12) },
      { "Animation", (3, 16) },
      { "Comedy", (4, 35) },
      { "Fantasy", (9, 14) },
      { "Science Fiction", (15, 878) }
    };

    public static Dictionary<string, (int BeezyCinemaId, int TMDBId)> MinorityGenres = new Dictionary<string, (int BeezyCinemaId, int TMDBId)>
    {
        { "Crime", (5, 80) },
        { "Documentary", (6, 99) },
        { "Drama", (7, 18) },
        { "Family", (8, 10751) },
        { "History", (10, 36) },
        { "Horror", (11, 27) },
        { "Music", (12, 10402) },
        { "Mystery", (13, 9648) },
        { "Romance", (14, 10749) },
        { "TV Movie", (16, 10770) },
        { "Thriller", (17, 53) },
        { "War", (18, 10752) },
        { "Western", (19, 37) }
     };
    public static string GetGenreByTMDBId(int tmdbId)
    {
      foreach (var genre in BlockbusterGenres)
      {
        if (genre.Value.TMDBId == tmdbId)
        {
          return genre.Key;
        }
      }

      foreach (var genre in MinorityGenres)
      {
        if (genre.Value.TMDBId == tmdbId)
        {
          return genre.Key;
        }
      }

      throw new ArgumentException("No genre found.");
    }
  }
}
