using AppspaceChallenge.API.Responses;
using Newtonsoft.Json;
using TMBD = AppspaceChallenge.API.Model.TMBD;

namespace AppspaceChallenge.API.Repositories
{
  public class KeywordsRepository : IKeywordsRepository
  {
    private readonly HttpClient _httpClient;

    public KeywordsRepository()
    {
      _httpClient = new HttpClient();
      _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIxNThkYjIwYjRjOGRjOGY3MmNjZWI0MjZlNjVkMjUxZiIsInN1YiI6IjY1MTJhNTIwM2E0YTEyMDExY2Y0ZmFlOSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.j7Ra6SCrwScimg1cJGjoGUICUnlEgTCw8dxxZhKMoJY");
    }

    public async Task<IEnumerable<TMBD.Keyword>> GetKeywords(int movieId)
    {
      string url = $"https://api.themoviedb.org/3/movie/{movieId}/keywords";
      HttpResponseMessage response = await _httpClient.GetAsync(url);

      if (!response.IsSuccessStatusCode)
        throw new Exception("Error getting keywords: " + response.ReasonPhrase);

      string jsonResult = await response.Content.ReadAsStringAsync();
      var result = JsonConvert.DeserializeObject<KeywordListResponse>(jsonResult);

      if (result == null || !result.Keywords.Any())
        return Enumerable.Empty<TMBD.Keyword>();

      if (result != null && result.Keywords.Any())
      {
        return result.Keywords;
      }
      else
      {
        return Enumerable.Empty<TMBD.Keyword>();
      }
    }

    public async Task<ICollection<string>> GetKeywordNames(int movieId)
    {
      IList<string> keywordNames = new List<string>();
      var keywords = await this.GetKeywords(movieId);
      foreach (var keyword in keywords)
      {
        keywordNames.Add(keyword.Name);
      }
      return keywordNames;
    }
  }
}
