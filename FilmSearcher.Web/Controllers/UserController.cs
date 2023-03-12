using FilmSearcher.BLL.Models;
using FilmSearcher.BLL.Services.Implementations;
using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Entities;
using FilmSearcher.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmSearcher.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetUsers();

            return View(users);
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUser(id);

            return RedirectToAction("Users");
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Update(UserDTO user)
        {
            await _userService.UpdateUser(user);

            return RedirectToAction("Users");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(id == int.Parse(currentUserId))
            {
                var user = await _userService.GetUserById(id);
                var movies = _userService.GetMoviesByUserId(id);

                var model = new UserViewModel
                {
                    User = user,
                    Movies = movies.ToList()
                };

                return View(model);
            }

            return View(null);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> InBookmark()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var movies = _userService.GetMoviesByUserId(int.Parse(currentUserId));

            return Json(movies);
        }
    }
}
