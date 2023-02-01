using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearcher.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
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
    }
}
