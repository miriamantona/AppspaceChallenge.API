using AppspaceChallenge.API.Model.TMBD;
using AppspaceChallenge.API.Responses;
using Newtonsoft.Json;

namespace AppspaceChallenge.API.Repositories
{
  public class GenresRepository : IGenresRepository
  {
    private readonly HttpClient _httpClient;

    public GenresRepository()
    {
      _httpClient = new HttpClient();
      _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIxNThkYjIwYjRjOGRjOGY3MmNjZWI0MjZlNjVkMjUxZiIsInN1YiI6IjY1MTJhNTIwM2E0YTEyMDExY2Y0ZmFlOSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.j7Ra6SCrwScimg1cJGjoGUICUnlEgTCw8dxxZhKMoJY");
    }

    public async Task<IEnumerable<Genre>> GetGenres()
    {
      string url = "https://api.themoviedb.org/3/genre/movie/list";

      HttpResponseMessage response = await _httpClient.GetAsync(url);

      if (!response.IsSuccessStatusCode)
        throw new Exception("Error en la solicitud: " + response.ReasonPhrase);

      string jsonResult = await response.Content.ReadAsStringAsync();
      GenresListResponse result = JsonConvert.DeserializeObject<GenresListResponse>(jsonResult);

      if (result != null && result.Genres.Any())
      {
        return result.Genres;
      }
      else
      {
        return Enumerable.Empty<Genre>();
      }
    }
  }
}
