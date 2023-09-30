using AppspaceChallenge.API.DTO.Input;
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

    [HttpPost("GetIntelligentBillboard", Name = "GetIntelligentBillboard")]
    public async Task<IntelligentBillboard> GetIntelligentBillboard(IntelligentBillboardRequest request)
    {
      return await _intelligentBillBoardManager.CreateIntelligentBillboard(request);
    }
  }
}
