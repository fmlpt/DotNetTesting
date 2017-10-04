using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMapWebAPIDemo.Lib.DataModel;

namespace StructureMapWebAPIDemo.Lib.Repositories
{
    public interface IMovieRepository
    {
        List<Movie> GetAllMovies();
        Movie GetById(int id);
    }

    public class MovieRepository : IMovieRepository
    {
        public List<Movie> GetAllMovies()
        {
            return new List<Movie>()
            {
                new Movie()
                {
                    Id = 1,
                    Title = "The Incredibles",
                    ReleaseDate = new DateTime(2004, 11, 5),
                    RunningTimeMinutes = 116,
                    Director = "Brad Bird"
                },
                new Movie()
                {
                    Id = 2,
                    Title = "Wreck-It Ralph",
                    ReleaseDate = new DateTime(2012, 11, 2),
                    RunningTimeMinutes = 120,
                    Director = "Rich Moore"
                },
                new Movie()
                {
                    Id = 3,
                    Title = "Inside Out",
                    ReleaseDate = new DateTime(2015, 6, 19),
                    RunningTimeMinutes = 102,
                    Director = "Pete Doctor & Ronnie Del Carmen"
                }
            };
        }

        public Movie GetById(int id)
        {
            var allMovies = GetAllMovies();

            if (allMovies.Any(x => x.Id == id))
            {
                return allMovies.First(x => x.Id == id);
            }

            return null;
        }
    }
}