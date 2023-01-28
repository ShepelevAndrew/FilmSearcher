namespace FilmSearcher.Models
{
    public class Producer
    {
        public int ProducerId { get; set; }
        public string ProfilePictureURL { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
