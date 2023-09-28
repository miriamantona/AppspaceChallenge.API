using AppspaceChallenge.API.DTO.Input;
using AppspaceChallenge.API.DTO.Output;

namespace AppspaceChallenge.API.Services
{
  public interface IIntelligentBillBoardManager
  {
    Task<IntelligentBillboard> CreateIntelligentBillboard(IntelligentBillboardRequest request);
    IEnumerable<Model.BeezyCinema.Movie> GetIntelligentBillboardWithSuccessfullMovies(int cinemaId, DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms);    
  }
}
