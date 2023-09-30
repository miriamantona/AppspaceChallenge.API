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
    private readonly BeezyCinemaDBContext _dbContext;

    public MoviesRepository(BeezyCinemaDBContext dbContext)
    {
      _httpClient = new HttpClient();
      _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIxNThkYjIwYjRjOGRjOGY3MmNjZWI0MjZlNjVkMjUxZiIsInN1YiI6IjY1MTJhNTIwM2E0YTEyMDExY2Y0ZmFlOSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.j7Ra6SCrwScimg1cJGjoGUICUnlEgTCw8dxxZhKMoJY");

      _dbContext = dbContext;
    }

    public async Task<IEnumerable<TMDB.Movie>> GetMoviesFromTMDB(DateTime from, DateTime to, int page)
    {
      string url = $"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page={page}&release_date.gte={from:yyyy-MM-dd}&release_date.lte={to:yyyy-MM-dd}&sort_by=popularity.desc";

      HttpResponseMessage response = await _httpClient.GetAsync(url);

      if (!response.IsSuccessStatusCode)
        throw new Exception("Error en la solicitud: " + response.ReasonPhrase);

      string jsonResult = await response.Content.ReadAsStringAsync();
      var movieList = JsonConvert.DeserializeObject<MovieListResponse>(jsonResult);

      if (movieList == null || !movieList.results.Any())
        return Enumerable.Empty<TMDB.Movie>();

      return movieList.results;
    }

    public async Task<TMDB.Movie> GetMovieFromTMDB(string title)
    {
      string url = $"https://api.themoviedb.org/3/search/movie?query={title}&include_adult=false&language=en-US&page=1\r\n";

      HttpResponseMessage response = await _httpClient.GetAsync(url);

      if (!response.IsSuccessStatusCode)
        throw new Exception("Error en la solicitud: " + response.ReasonPhrase);

      string jsonResult = await response.Content.ReadAsStringAsync();
      var movieList = JsonConvert.DeserializeObject<MovieListResponse>(jsonResult);

      if (movieList == null || !movieList.results.Any())
        return null;

      var query = (from m in movieList.results
                   where m.Title == title
                   select m);

      var result = query.FirstOrDefault();
      return result;
    }

    public IEnumerable<BeezyCinema.Movie> GetMoviesWithBiggestSeatsSold(int cityId)
    {
      // A succesful movie is considered to have more than 75% seats sold in a city.

      var query = from cinema in _dbContext.Cinemas
                  join room in _dbContext.Rooms on cinema.Id equals room.CinemaId
                  join session in _dbContext.Sessions on room.Id equals session.RoomId
                  join movie in _dbContext.Movies.Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre) on session.MovieId equals movie.Id
                  where cinema.CityId == cityId &&
                        session.SeatsSold > 0.75 * (
                            from s in _dbContext.Sessions
                            join r in _dbContext.Rooms on s.RoomId equals r.Id
                            join c in _dbContext.Cinemas on r.CinemaId equals c.Id
                            where c.CityId == cityId
                            select (double?)s.SeatsSold ?? 0
                        ).Max()
                  select movie;

      var result = query.Distinct().ToList();
      return result;
    }
  }
}
