using AppspaceChallenge.API.DTO;
using AppspaceChallenge.API.Model.BeezyCinema;

namespace AppspaceChallenge.API.Services
{
  public interface IIntelligentBillBoardManager
  {
    Task<IntelligentBillboard> CreateIntelligentBillboard(DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms);
    IEnumerable<Model.BeezyCinema.Movie> GetIntelligentBillboardWithSuccessfullMovies(int cinemaId, DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms);    
  }
}
