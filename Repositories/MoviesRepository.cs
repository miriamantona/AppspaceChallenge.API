using AppspaceChallenge.API.Models;
using AppspaceChallenge.API.Responses;
using Newtonsoft.Json;

namespace AppspaceChallenge.API.Repositories
{
  public class MoviesRepository: IMoviesRepository
  {
    private readonly HttpClient _httpClient;

    public MoviesRepository()
    {
      _httpClient = new HttpClient();
      _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIxNThkYjIwYjRjOGRjOGY3MmNjZWI0MjZlNjVkMjUxZiIsInN1YiI6IjY1MTJhNTIwM2E0YTEyMDExY2Y0ZmFlOSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.j7Ra6SCrwScimg1cJGjoGUICUnlEgTCw8dxxZhKMoJY");
    }

    public async Task<IEnumerable<Movie>> GetMovies(DateTime from, DateTime to)
    {
      string url = "https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc";

      HttpResponseMessage response = await _httpClient.GetAsync(url);

      if (response.IsSuccessStatusCode)
      {
        string jsonResult = await response.Content.ReadAsStringAsync();
        var movieListResponse = JsonConvert.DeserializeObject<MovieListResponse>(jsonResult);

        // Ahora, puedes acceder a la lista de películas usando movieListResponse.Results
        List<Movie> movies = movieListResponse.Results.Select(movieResponse => new Movie
        {
          Title = movieResponse.Title,
          Overview = movieResponse.Overview
          //GenreIds = movieResponse.Genre_ids
        }).ToList();

        return movies;
      }
      else
      {
        throw new Exception("Error en la solicitud: " + response.ReasonPhrase);
      }
    }
  }
}
