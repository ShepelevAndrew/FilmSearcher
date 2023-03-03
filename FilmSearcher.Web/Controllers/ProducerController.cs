using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearcher.Web.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IBaseRepository<Producer> _producerService;

        public ProducerController(IBaseRepository<Producer> producerService)
        {
            _producerService = producerService;
        }

        public async Task<IActionResult> Producers()
        {
            var producers = await _producerService.GetAllAsync();
            return View(producers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("")] Producer producer)
        {
            /*if(!ModelState.IsValid)
            {
                return View();
            }*/

            await _producerService.AddAsync(producer);
            return RedirectToAction(nameof(Producers));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _producerService.GetByIdAsync(id);
            if (producer == null) return View("NotFound");

            await _producerService.DeleteAsync(id);
            return RedirectToAction(nameof(Producers));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _producerService.GetByIdAsync(id);

            if (producer == null) return View("NotFound");

            return View(producer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("")] Producer producer)
        {
            /*if(!ModelState.IsValid)
            {
                return View();
            }*/

            await _producerService.UpdateAsync(producer);
            return RedirectToAction(nameof(Producers));
        }

        public async Task<IActionResult> Details(int id)
        {
            var producer = await _producerService.GetByIdAsync(id);

            if (producer == null) return View("NotFound");

            return View(producer);
        }
    }
}
