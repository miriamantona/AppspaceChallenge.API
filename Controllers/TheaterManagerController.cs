using AppspaceChallenge.API.DTO.Input;
using AppspaceChallenge.API.DTO.Output;
using AppspaceChallenge.API.Services;
using AppspaceChallenge.DataAccess.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AppspaceChallenge.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TheaterManagerController : ControllerBase
  {
    private readonly IIntelligentBillBoardManager _intelligentBillBoardManager;

    public TheaterManagerController(IIntelligentBillBoardManager intelligentBillBoardManager)
    {
      _intelligentBillBoardManager = intelligentBillBoardManager;
    }

    [HttpPost("GetIntelligentBillboard", Name = "GetIntelligentBillboard")]
    public async Task<IntelligentBillboard> GetIntelligentBillboard(IntelligentBillboardRequest request)
    {
      return await _intelligentBillBoardManager.CreateIntelligentBillboard(request);
    }

    [HttpPost("GetAllTimeRecomendedMovies", Name = "GetAllTimeRecomendedMovies")]
    public async Task<ICollection<MovieRecommendation>> GetAllTimeRecomendedMovies(GetAllTimeRecomendedMoviesRequest request)
    {
      throw new NotImplementedException();
    }

    [HttpPost("GetRecommendedUpcomingMoviesFromNow", Name = "GetRecommendedUpcomingMoviesFromNow")]
    public async Task<ICollection<MovieRecommendation>> GetRecommendedUpcomingMoviesFromNow(GetRecommendedUpcomingMoviesFromNowRequest request)
    {
      throw new NotImplementedException();
    }
   
    [HttpPost("GetAllTimeRecommendedTVShows", Name = "GetAllTimeRecommendedTVShows")]
    public async Task<ICollection<TVShowRecommendation>> GetAllTimeRecommendedTVShows(GetAllTimeRecommendedTVShowsRequest request)
    {
      throw new NotImplementedException();
    }

    [HttpPost("GetAllTimeRecommendedDocumentaries", Name = "GetAllTimeRecommendedDocumentaries")]
    public async Task<ICollection<DocumentaryRecommendation>> GetAllTimeRecommendedDocumentaries(GetAllTimeRecommendedDocumentariesRequest request)
    {
      throw new NotImplementedException();
    }
  }
}
