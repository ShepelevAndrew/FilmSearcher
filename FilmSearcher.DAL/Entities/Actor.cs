namespace FilmSearcher.DAL.Entities
{
    public class Actor
    {
        public int ActorId { get; set; }
        public string ProfilePictureURL { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }

        public List<ActorMovie> ActorsMovies { get; set; }
    }
}
