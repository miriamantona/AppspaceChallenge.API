using AppspaceChallenge.API.DTO.Input;
using AppspaceChallenge.API.DTO.Output;
using AppspaceChallenge.API.Responses;
using AppspaceChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;
using BeezyCinema = AppspaceChallenge.API.Model.BeezyCinema;

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

    [HttpGet("GetIntelligentBillboardWithSuccessfullMovies", Name = "GetIntelligentBillboardWithSuccessfullMovies")]
    public IEnumerable<BeezyCinema.Movie> GetIntelligentBillboardWithSuccessfullMovies(IntelligentBillboardWithSuccessfullMoviesRequest request)
    {
      var result = _intelligentBillBoardManager.GetIntelligentBillboardWithSuccessfullMovies(request);
      return result;
    }
  }
}
