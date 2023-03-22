using FilmSearcher.BLL.Models;
using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Entities;
using FilmSearcher.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmSearcher.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ISearchService<Movie> _searchService;

        public MovieController(IMovieService movieService, ISearchService<Movie> searchService)
        {
            _movieService = movieService;
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Movies()
        {
            var currentUserId = "0";
            if(User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
                currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var movies = await _movieService.GetAllAsync(int.Parse(currentUserId));

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
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            await _movieService.AddAsync(model.Movie);
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

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin, Owner")]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null) return View("NotFound");

            return View(movie);
        }

        [HttpGet]
        public async Task<JsonResult> MovieJson(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            return Json(movie);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin, Owner")]
        public async Task<IActionResult> Edit([FromBody] MovieViewModel model)
        {
            await _movieService.UpdateAsync(model.Movie);
            await _movieService.AddActorsByIdAsync(model.Movie.MovieId, model.ActorsData);

            return RedirectToRoute(new { controller = nameof(Movie), action = nameof(Details), id = model.Movie.MovieId });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null) return View("NotFound");

            var actors = _movieService.GetActorsById(id).ToList();

            var movieUser = await _movieService.GetMovieUserByMovieId(id);

            var viewModel = new MovieViewModel()
            {
                Movie = movie,
                ActorsData = actors,
                MovieUser = movieUser
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Bookmark(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var movieUser = new MovieUser()
            {
                MovieId = id,
                UserId = int.Parse(currentUserId),
            };

            await _movieService.AddOrRemoveInBookmarkAsync(movieUser);

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Score(int id, int score)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var movieUser = new MovieUser()
            {
                MovieId = id,
                UserId = int.Parse(currentUserId),
                MovieScore = score,
            };

            await _movieService.AddOrUpdateScore(movieUser);

            return Ok();
        }
    }
}