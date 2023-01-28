using FilmSearcher.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearcher.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        public async Task<IActionResult> Actors()
        {
            var actors = await _actorService.GetAll();
            return View(actors);
        }
    }
}
