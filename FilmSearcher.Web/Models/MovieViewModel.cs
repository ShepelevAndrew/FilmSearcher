using FilmSearcher.DAL.Entities;

namespace FilmSearcher.Web.Models
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
