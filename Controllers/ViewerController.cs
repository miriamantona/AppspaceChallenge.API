using Viewers = AppspaceChallenge.API.DTO.Input.Viewers;
using AppspaceChallenge.API.DTO.Output;
using Microsoft.AspNetCore.Mvc;

namespace AppspaceChallenge.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ViewerController : ControllerBase
  {
    // Use case 4
    [HttpPost("GetAllTimeRecomendedMovies", Name = "GetAllTimeRecomendedMovies")]
    public async Task<ICollection<MovieRecommendation>> GetAllTimeRecomendedMovies(Viewers.AllTimeRecomendedMoviesRequest request)
    {
      throw new NotImplementedException();
    }

    // Use case 5
    [HttpPost("GetRecommendedUpcomingMoviesFromNow", Name = "GetRecommendedUpcomingMoviesFromNow")]
    public async Task<ICollection<MovieRecommendation>> GetRecommendedUpcGetRecommendedUpcomingMoviesFromNowomingMoviesFromNowForViewers(Viewers.UpcomingRecommendedMoviesForViewersRequest request)
    {
      throw new NotImplementedException();
    }

    // Use case 6
    [HttpPost("GetAllTimeRecommendedTVShows", Name = "GetAllTimeRecommendedTVShows")]
    public async Task<ICollection<TVShowRecommendation>> GetAllTimeRecommendedTVShows(Viewers.AllTimeRecommendedTVShowsRequest request)
    {
      throw new NotImplementedException();
    }

    // Use case 7
    [HttpPost("GetAllTimeRecommendedDocumentaries", Name = "GetAllTimeRecommendedDocumentaries")]
    public async Task<ICollection<DocumentaryRecommendation>> GetAllTimeRecommendedDocumentaries(Viewers.AllTimeRecommendedDocumentariesRequest request)
    {
      throw new NotImplementedException();
    }
  }
}
