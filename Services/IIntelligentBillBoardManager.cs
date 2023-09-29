using AppspaceChallenge.API.DTO.Input;
using AppspaceChallenge.API.DTO.Output;
using BeezyCinema = AppspaceChallenge.API.Model.BeezyCinema;
using AppspaceChallenge.API.Responses;

namespace AppspaceChallenge.API.Services
{
  public interface IIntelligentBillBoardManager
  {
    Task<IntelligentBillboard> CreateIntelligentBillboard(IntelligentBillboardRequest request);
    IEnumerable<BeezyCinema.Movie> GetIntelligentBillboardWithSuccessfullMovies(IntelligentBillboardWithSuccessfullMoviesRequest request);    
  }
}
