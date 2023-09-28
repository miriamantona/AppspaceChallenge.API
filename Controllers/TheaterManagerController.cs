using AppspaceChallenge.API.DTO;
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

    [HttpGet("GetIntelligentBillboard", Name = "GetIntelligentBillboard")]
    public async Task<IntelligentBillboard> GetIntelligentBillboard(DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms)
    {
      return await _intelligentBillBoardManager.CreateIntelligentBillboard(from, to, screensInBigRooms, screensInSmallRooms);
    }

    [HttpGet("GetIntelligentBillboardWithSuccessfullMovies", Name = "GetIntelligentBillboardWithSuccessfullMovies")]
    public IEnumerable<BeezyCinema.Movie> GetIntelligentBillboardWithSuccessfullMovies(int cinemaId, DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms)
    {
      var result = _intelligentBillBoardManager.GetIntelligentBillboardWithSuccessfullMovies(cinemaId, from, to, screensInBigRooms, screensInSmallRooms);
      return result;
    }
  }
}
