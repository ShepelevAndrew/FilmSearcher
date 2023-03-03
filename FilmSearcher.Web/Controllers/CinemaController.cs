using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearcher.Web.Controllers
{
    public class CinemaController : Controller
    {
        private readonly IBaseRepository<Cinema> _cinemaService;

        public CinemaController(IBaseRepository<Cinema> movieService)
        {
            _cinemaService = movieService;
        }

        public async Task<IActionResult> Cinemas()
        {
            var cinemas = await _cinemaService.GetAllAsync();
            return View(cinemas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("")] Cinema cinema)
        {
            /*if(!ModelState.IsValid)
            {
                return View();
            }*/

            await _cinemaService.AddAsync(cinema);
            return RedirectToAction(nameof(Cinemas));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _cinemaService.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");

            await _cinemaService.DeleteAsync(id);
            return RedirectToAction(nameof(Cinemas));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _cinemaService.GetByIdAsync(id);

            if (cinema == null) return View("NotFound");

            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("")] Cinema cinema)
        {
            /*if(!ModelState.IsValid)
            {
                return View();
            }*/

            await _cinemaService.UpdateAsync(cinema);
            return RedirectToAction(nameof(Cinemas));
        }

        public async Task<IActionResult> Details(int id)
        {
            var cinema = await _cinemaService.GetByIdAsync(id);

            if (cinema == null) return View("NotFound");

            return View(cinema);
        }
    }
}
