using FilmSearcher.BLL.Services.Implementations;
using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace FilmSearcher.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IBaseRepository<Movie> _movieService;
        private readonly ISearchService<Movie> _searchService;

        public MovieController(IBaseRepository<Movie> movieService, ISearchService<Movie> searchService)
        {
            _movieService = movieService;
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Movies(string search = "")
        {
            var movies = search == ""
                ? await _movieService.GetAllAsync()
                : await _searchService.Search(search);

            #region sort
            var sortMovies = movies.OrderByDescending(m => m.StartDate).ToList();

            for(int j = 0; j < sortMovies.Count; j++) { 
                for(int i = 0; i < sortMovies.Count; i++)
                {
                    if (sortMovies[i].StartDate > DateTime.Now)
                    {
                        var temp = sortMovies[i];
                        if (i + 1 < sortMovies.Count)
                        {
                            if (sortMovies[i + 1].EndDate > DateTime.Now) { 
                                sortMovies[i] = sortMovies[i + 1];
                                sortMovies[i + 1] = temp;
                            }
                        }
                    } 
                }
            }
            #endregion

            return View(sortMovies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("")] Movie movie)
        {
            /*if(!ModelState.IsValid)
            {
                return View();
            }*/

            await _movieService.AddAsync(movie);
            return RedirectToAction(nameof(Movies));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null) return View("NotFound");

            await _movieService.DeleteAsync(id);
            return RedirectToAction(nameof(Movies));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null) return View("NotFound");

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("")] Movie movie)
        {
            /*if(!ModelState.IsValid)
            {
                return View();
            }*/

            await _movieService.UpdateAsync(id, movie);
            return RedirectToAction(nameof(Movies));
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null) return View("NotFound");

            return View(movie);
        }
    }
}
