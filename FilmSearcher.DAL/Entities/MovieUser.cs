namespace FilmSearcher.DAL.Entities
{
    public class MovieUser
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public bool InBookmark { get; set; }
        public int? MovieScore { get; set; }
    }
}
