using AutoMapper;
using FilmSearcher.BLL.Models;
using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Domain.Enum;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmSearcher.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IBaseRepository<User> userService, IMapper mapper)
        {
            _userRepository = userService;
            _mapper = mapper;
        }

        public async Task Create(UserDTO model)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.ToList().FirstOrDefault(u => u.Name == model.Name);

            if (user == null)
            {
                user = _mapper.Map<User>(model);
                await _userRepository.AddAsync(user);
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);

            return true;
        }

        public async Task<bool> UpdateUser(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _userRepository.UpdateAsync(user);

            return true;
        }

        public Dictionary<int, string> GetRoles()
        {
            var roles = ((Role[])Enum.GetValues(typeof(Role)))
                    .ToDictionary(k => (int)k, t => Enum.GetName(typeof(Role), t));

            return roles;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();

            var usersDto = _mapper.Map<IEnumerable<UserDTO>>(users);

            return usersDto;
        }
    }
}
