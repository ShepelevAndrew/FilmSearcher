using FilmSearcher.DAL.Domain.Enum;

namespace FilmSearcher.DAL.Entities
{
    public class User
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public List<MovieUser> MoviesUsers { get; set; }

        public static object FindFirstValue(string nameIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
