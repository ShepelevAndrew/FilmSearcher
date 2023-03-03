using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;
using FilmSearcher.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearcher.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ISearchService<Movie> _searchService;

        public MovieController(IMovieService movieService, ISearchService<Movie> searchService)
        {
            _movieService = movieService;
            _searchService = searchService;;
        }

        [HttpGet]
        public async Task<IActionResult> Movies()
        {
            var movies = await _movieService.GetAllAsync();

            #region sort
            var sortMovies = movies.OrderByDescending(m => m.StartDate).ToList();

            for (int j = 0; j < sortMovies.Count; j++)
            {
                for (int i = 0; i < sortMovies.Count; i++)
                {
                    if (sortMovies[i].StartDate > DateTime.Now)
                    {
                        var temp = sortMovies[i];
                        if (i + 1 < sortMovies.Count)
                        {
                            if (sortMovies[i + 1].EndDate > DateTime.Now)
                            {
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

        [HttpGet]
        public async Task<JsonResult> Search(string search)
        {
            var movies = search == null 
                ? null
                : await _searchService.Search(search);

            return Json(movies);
        }

        [HttpGet]
        public async Task<JsonResult> Actors(int id)
        {
            var actors = _movieService.GetActorsById(id).ToList();

            foreach(var actor in actors)
            {
                actor.ActorsMovies = null;
            }

            return Json(actors);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Owner")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> Create(Movie movie/*, List<Actor> actors*/)
        {
            await _movieService.AddAsync(movie);
           /* foreach(var actor in actors)
                await _actorMovieService.AddAsync(movie.MovieId, actor);*/
            return RedirectToAction(nameof(Movies));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null) return View("NotFound");

            await _movieService.DeleteAsync(id);
            return RedirectToAction(nameof(Movies));
        }

        [Authorize(Roles = "Moderator, Admin, Owner")]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null) return View("NotFound");

            var actors = await _movieService.GetActorsAsync();

            var viewModel = new MovieViewModel()
            {
                Movie = movie,
                Actors = actors.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin, Owner")]
        public async Task<IActionResult> Edit([Bind("")] MovieViewModel model)
        {
            /*if(!ModelState.IsValid)
            {
                return View();
            }*/

            await _movieService.UpdateAsync(model.Movie);
            await _movieService.AddActorsByIdAsync(model.Movie.MovieId, model.Actors);

            return RedirectToAction(nameof(Movies));
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null) return View("NotFound");

            var actors = _movieService.GetActorsById(id).ToList();

            var viewModel = new MovieViewModel()
            {
                Movie = movie,
                Actors = actors,
            };

            return View(viewModel);
        }
    }
}
