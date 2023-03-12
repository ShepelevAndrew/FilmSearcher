using FilmSearcher.DAL.Entities;

namespace FilmSearcher.Web.Models
{
    public class MovieViewModel
    {
        public List<Actor> ActorsData { get; set; }
        public Movie Movie { get; set; }
    }
}
