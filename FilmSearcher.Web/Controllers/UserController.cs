using FilmSearcher.BLL.Models;
using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearcher.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUser(id);

            return RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserDTO user)
        {
            await _userService.UpdateUser(user);

            return RedirectToAction("Users");
        }
    }
}
