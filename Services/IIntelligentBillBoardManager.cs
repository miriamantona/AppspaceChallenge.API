using AppspaceChallenge.API.DTO.Input.Managers;
using AppspaceChallenge.API.DTO.Output;

namespace AppspaceChallenge.API.Services
{
  public interface IIntelligentBillBoardManager
  {
    Task<IntelligentBillboard> CreateIntelligentBillboard(IntelligentBillboardRequest request);
  }
}
