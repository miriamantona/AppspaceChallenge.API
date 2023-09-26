using AppspaceChallenge.API.Models;
using AppspaceChallenge.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AppspaceChallenge.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TheaterManagerController : ControllerBase
  {
    private readonly IMoviesRepository _repository;

    public TheaterManagerController(IMoviesRepository repository)
    {
      _repository = repository;
    }

    [HttpGet(Name = "GetIntelligentBillboard")]
    public async Task<IEnumerable<Movie>> Get(DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms)
    {
      return await _repository.GetMovies(from, to);
    }

    /*
    [HttpGet(Name = "GetIntelligentBillboardWithSuccessfullMovies")]
    public IEnumerable<IntelligentBillboard> Get(int cinemaId, DateTime from, DateTime to, int screensInBigRooms, int screensInSmallRooms)
    {
      return null;
    }*/
  }
}
