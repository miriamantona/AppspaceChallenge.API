using AppspaceChallenge.API.DTO;
using AppspaceChallenge.API.Model.TMBD;
using AppspaceChallenge.API.Repositories;
using System.Globalization;
using System.Linq.Expressions;

namespace AppspaceChallenge.API.Services
{
  public class IntelligentBillBoardManager : IIntelligentBillBoardManager
  {
    private readonly IMoviesRepository _moviesRepository;
    private readonly IGenresRepository _genresRepository;

    public IntelligentBillBoardManager(IMoviesRepository moviesRepository)
    {
      _moviesRepository = moviesRepository;
    }

    public async Task<IntelligentBillboard> CreateIntelligentBillboard(DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms)
    {
      var movies = await _moviesRepository.GetMoviesFromTMDB(from, to);

      IntelligentBillboard intelligentBillboard = new IntelligentBillboard
      {
        WeeklyPlannings = new List<WeeklyPlanning>()
      };

      Calendar calendar = CultureInfo.InvariantCulture.Calendar;
      List<string> assignedMovies = new List<string>();

      return intelligentBillboard;
    }

    public IEnumerable<Model.BeezyCinema.Movie> GetIntelligentBillboardWithSuccessfullMovies(int cinemaId, DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms)
    {
      var result = _moviesRepository.GetMoviesWithBiggestSeatsSold(cinemaId);
      return result;
    }
  }
}
