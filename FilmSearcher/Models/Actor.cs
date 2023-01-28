using System.ComponentModel.DataAnnotations;

namespace FilmSearcher.Models
{
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }

        public string ProfilePictureURL { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }

        public List<ActorMovie> ActorMovies { get; set; }
    }
}
