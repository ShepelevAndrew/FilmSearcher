using FilmSearcher.BLL.Services.Implementation;
using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearcher.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly ICrudService<Movie> _movieService;

        public MovieController(ICrudService<Movie> movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Movies()
        {
            var movies = await _movieService.GetAllAsync();
            return View(movies);
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
