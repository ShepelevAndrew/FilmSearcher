using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearcher.Web.Controllers
{
    public class ActorController : Controller
    {
        private readonly ICrudService<Actor> _actorService;
        private readonly ISearchService<Actor> _searchService;
        
        public ActorController(ICrudService<Actor> actorService, ISearchService<Actor> search)
        {
            _actorService = actorService;
            _searchService = search;
        }

        [HttpGet]
        public async Task<IActionResult> Actors(string searchString = "")
        {
            var actors = searchString == "" 
                ? await _actorService.GetAllAsync() 
                : await _searchService.Search(searchString);

            return View(actors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Actor actor)
        {
            /*if(!ModelState.IsValid)
            {
                return View();
            }*/

            await _actorService.AddAsync(actor);
            return RedirectToAction(nameof(Actors));
        }

        public async Task<IActionResult> Details(int id)
        {
            var actor = await _actorService.GetByIdAsync(id);

            if (actor == null) return View("NotFound");

            return View(actor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _actorService.GetByIdAsync(id);

            if (actor == null) return View("NotFound");

            return View(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            /*if(!ModelState.IsValid)
            {
                return View();
            }*/

            await _actorService.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Actors));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _actorService.GetByIdAsync(id);

            if (actor == null) return View("NotFound");

            await _actorService.DeleteAsync(id);
            return RedirectToAction("Actors");
        }
    }
}
