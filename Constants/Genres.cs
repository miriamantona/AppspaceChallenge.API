namespace AppspaceChallenge.API.Constants
{
  public static class Genres
  {
    public static Dictionary<string, (int BeezyCinemaId, int TMDBId)> GenreInfo = new Dictionary<string, (int BeezyCinemaId, int TMDBId)>
    {
        { "Action", (1, 28) },
        { "Adventure", (2, 12) },
        { "Animation", (3, 16) },
        { "Comedy", (4, 35) },
        { "Crime", (5, 80) },
        { "Documentary", (6, 99) },
        { "Drama", (7, 18) },
        { "Family", (8, 10751) },
        { "Fantasy", (9, 14) },
        { "History", (10, 36) },
        { "Horror", (11, 27) },
        { "Music", (12, 10402) },
        { "Mystery", (13, 9648) },
        { "Romance", (14, 10749) },
        { "Science Fiction", (15, 878) },
        { "TV Movie", (16, 10770) },
        { "Thriller", (17, 53) },
        { "War", (18, 10752) },
        { "Western", (19, 37) }
     };

    public static List<string> BlockbusterGenres()
    {
      List<string> blockbusterGenres = new List<string>
        {
            "Action",
            "Adventure",
            "Animation",
            "Comedy",
            "Fantasy",
            "Science Fiction"
        };

      List<string> resultGenres = GenreInfo.Keys
          .Where(genre => blockbusterGenres.Contains(genre))
          .ToList();

      return resultGenres;
    }

    public static List<string> MinorityGenres()
    {
      List<string> minorityGenres = new List<string>
        {
            "Drama",
            "Family",
            "Crime",
            "History",
            "Horror",
            "Romance",
            "Mystery",
            "Music",
            "Documentary",
            "TV Movie",
            "Thriller",
            "Western",
            "War"
        };

      List<string> resultGenres = GenreInfo.Keys
          .Where(genre => minorityGenres.Contains(genre))
          .ToList();

      return resultGenres;
    }
  }
}
