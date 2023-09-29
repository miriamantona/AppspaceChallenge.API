using AppspaceChallenge.API.Model.TMBD;
using Newtonsoft.Json;

namespace AppspaceChallenge.API.Repositories
{
  public class DetailsRepository : IDetailsRepository
  {
    private readonly HttpClient _httpClient;

    public DetailsRepository()
    {
      _httpClient = new HttpClient();
      _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIxNThkYjIwYjRjOGRjOGY3MmNjZWI0MjZlNjVkMjUxZiIsInN1YiI6IjY1MTJhNTIwM2E0YTEyMDExY2Y0ZmFlOSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.j7Ra6SCrwScimg1cJGjoGUICUnlEgTCw8dxxZhKMoJY");
    }

    public async Task<Details> GetDetails(int movieId)
    {
      string url = $"https://api.themoviedb.org/3/movie/{movieId}?language=en-US\");";

      HttpResponseMessage response = await _httpClient.GetAsync(url);

      if (!response.IsSuccessStatusCode)
        throw new Exception("Error en la solicitud: " + response.ReasonPhrase);

      string jsonResult = await response.Content.ReadAsStringAsync();
      var result = JsonConvert.DeserializeObject<Details>(jsonResult);

      if (result != null)
      {
        return result;
      }
      else
      {
        return new Details();
      }
    }
  }
}
