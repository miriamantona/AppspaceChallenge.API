using Managers = AppspaceChallenge.API.DTO.Input.Managers;
using AppspaceChallenge.API.DTO.Output;
using AppspaceChallenge.API.Services;
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

    // Use case 8
    [HttpPost("GetUpcomingRecommendedMovies", Name = "GetUpcomingRecommendedMovies")]
    public async Task<ICollection<MovieRecommendation>> GetUpcomingRecommendedMovies(Managers.UpcomingRecommendedMoviesForManagersRequest request)
    {
      throw new NotImplementedException();
    }

    // Use case 9
    [HttpPost("GetBillboard", Name = "GetBillboard")]
    public async Task<ICollection<Billboard>> GetBillboard(Managers.BillboardRequest request)
    {
      throw new NotImplementedException();
    }

    // Use case 10
    [HttpPost("GetIntelligentBillboard", Name = "GetIntelligentBillboard")]
    public async Task<IntelligentBillboard> GetIntelligentBillboard(Managers.IntelligentBillboardRequest request)
    {
      return await _intelligentBillBoardManager.CreateIntelligentBillboard(request);
    }

  }
}
