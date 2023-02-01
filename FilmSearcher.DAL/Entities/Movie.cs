using FilmSearcher.DAL.Domain;

namespace FilmSearcher.DAL.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory Category { get; set; }

        //Relationships
        public List<ActorMovie> ActorsMovies { get; set; }

        //Cinema
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        //Producer
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
    }
}
