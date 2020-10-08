using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcIntroduction.Models
{
    public class MovieRepository
    {
        
        public IEnumerable<Movie> GetAll()
        {
            return _movies;
        }

        public Movie GetByTitle(string title)
        {
            return _movies.First(m => m.Title == title);
        }

        public void Add(Movie movie)
        {
            _movies.Add(movie);
        }

        public void Save(Movie movie)
        {
            // ...
        }


        private static List<Movie> _movies = new List<Movie>()
        {
            new Movie() { Title="Star Wars", Rating=4 },
            new Movie() { Title="The Kings Speech", Rating=4},
            new Movie() { Title="My Blue Heaven", Rating=1}
        };
    }
}