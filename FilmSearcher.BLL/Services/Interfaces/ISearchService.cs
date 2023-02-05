using FilmSearcher.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearcher.BLL.Services.Interfaces
{
    public interface ISearchService<T> where T : class
    {
        Task<IEnumerable<T>> Search(string text);
    }
}
