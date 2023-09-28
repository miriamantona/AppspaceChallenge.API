using AppspaceChallenge.API.DBContext;
using AppspaceChallenge.API.Responses;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BeezyCinema = AppspaceChallenge.API.Model.BeezyCinema;
using TMDB = AppspaceChallenge.API.Model.TMBD;

namespace AppspaceChallenge.API.Repositories
{
  public class MoviesRepository : IMoviesRepository
  {
    private readonly HttpClient _httpClient;
    private readonly IGenresRepository _genresRepository;
    private readonly BeezyCinemaDBContext _dbContext;



    public MoviesRepository(IGenresRepository genresRepository, BeezyCinemaDBContext dbContext)
    {
      _httpClient = new HttpClient();
      _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIxNThkYjIwYjRjOGRjOGY3MmNjZWI0MjZlNjVkMjUxZiIsInN1YiI6IjY1MTJhNTIwM2E0YTEyMDExY2Y0ZmFlOSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.j7Ra6SCrwScimg1cJGjoGUICUnlEgTCw8dxxZhKMoJY");

      _genresRepository = genresRepository;
      _dbContext = dbContext;
    }

    public async Task<IEnumerable<TMDB.Movie>> GetMoviesFromTMDB(DateTime from, DateTime to, int minimumPages)
    {
      List<TMDB.Movie> allMovies = new List<TMDB.Movie>();

      for (var currentPage = 1; currentPage <= minimumPages; currentPage++)
      {
        string url = $"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page={currentPage}&release_date.gte={from:yyyy-MM-dd}&release_date.lte={to:yyyy-MM-dd}&sort_by=popularity.desc";

        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
          throw new Exception("Error en la solicitud: " + response.ReasonPhrase);

        string jsonResult = await response.Content.ReadAsStringAsync();
        var movieList = JsonConvert.DeserializeObject<MovieListResponse>(jsonResult);

        if (movieList == null || !movieList.results.Any())
          return Enumerable.Empty<TMDB.Movie>();

        var movies = movieList.results;
        var genres = await _genresRepository.GetGenres();

        if (genres == null)
          return Enumerable.Empty<TMDB.Movie>();

        foreach (var movie in movies)
        {
          var genreNames = movie.Genre_ids
              .Select(id => genres.FirstOrDefault(g => g.Id == id).Name)
              .ToList();

          movie.Genres = genreNames;
        }
        allMovies.AddRange(movies);
      }
      return allMovies;
    }

    public IEnumerable<BeezyCinema.Movie> GetMoviesWithBiggestSeatsSold(int cinemaId)
    {
      var query = from cinema in _dbContext.Cinemas
                  join room in _dbContext.Rooms on cinema.Id equals room.CinemaId
                  join session in _dbContext.Sessions on room.Id equals session.RoomId
                  join movie in _dbContext.Movies on session.MovieId equals movie.Id
                  where cinema.Id == cinemaId &&
                        session.SeatsSold > 0.75 * (
                            from s in _dbContext.Sessions
                            join r in _dbContext.Rooms on s.RoomId equals r.Id
                            join c in _dbContext.Cinemas on r.CinemaId equals c.Id
                            where c.Id == cinemaId
                            select (double?)s.SeatsSold ?? 0
                        ).Max()
                  select movie;


      var result = query.ToList();
      return result;
    }
  }
}
