using FilmSearcher.DAL.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearcher.BLL.Models
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory Category { get; set; }
        public double Score { get; set; }
        public bool IsInBookmark { get; set; }

        public int ProducerId { get; set; }
        public int CinemaId { get; set; }
    }
}
