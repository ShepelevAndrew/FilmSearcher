using FilmSearcher.DAL.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearcher.BLL.Models
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
