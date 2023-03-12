using FilmSearcher.BLL.Models;
using FilmSearcher.DAL.Entities;

namespace FilmSearcher.Web.Models
{
    public class UserViewModel
    {
        public UserDTO User { get; set; }
        public List<MovieDTO> Movies { get; set;}
    }
}
