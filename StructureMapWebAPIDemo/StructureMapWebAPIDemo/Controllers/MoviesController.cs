using System.Web.Http;
using StructureMapWebAPIDemo.Lib.Repositories;

namespace StructureMapWebAPIDemo.Controllers
{
    [System.Web.Mvc.RoutePrefix("movies")]
    public class MoviesController : ApiController
    {
        private readonly IMovieRepository _movieRepo;
        
        public MoviesController(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }
        
        [HttpGet]
        [Route("all")]
        public IHttpActionResult All()
        {
            
            var allMovies = _movieRepo.GetAllMovies();
            
            return Ok(allMovies);
        }
        
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var movie = _movieRepo.GetById(id);
            
            if(movie == null)
            {
                return NotFound();
            }
            
            return Ok(movie);
        }
    }
}