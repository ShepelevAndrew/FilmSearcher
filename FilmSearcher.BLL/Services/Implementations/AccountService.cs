using FilmSearcher.BLL.Helpers;
using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Domain.Enum;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;
using System.Security.Claims;

namespace FilmSearcher.BLL.Services.Implementations
{
    public class AccountService : IAccountService
    {

        private readonly IBaseRepository<User> _userRepository;

        public AccountService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ClaimsIdentity> Register(User model)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.ToList().FirstOrDefault(x => x.Name == model.Name);

            if (user != null)
            {
                
            }

            user = new User()
            {
                Name = model.Name,
                Role = Role.User,
                Password = HashPasswordHelper.HashPassword(model.Password),
            };

            await _userRepository.AddAsync(user);

            var result = Authenticate(user);

            return result;
        }

        public async Task<ClaimsIdentity> Login(User model)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(x => x.Name == model.Name);

            if (user == null)
            {
                return new ClaimsIdentity();
            }

            if (user.Password != HashPasswordHelper.HashPassword(model.Password))
            {
                return new ClaimsIdentity();
            }
            var result = Authenticate(user);

            return result;
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public Task<bool> ChangePassword(User model)
        {
            throw new NotImplementedException();
        }
    }
}
