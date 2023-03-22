using AutoMapper;
using FilmSearcher.BLL.Models;
using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Domain.Enum;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;

namespace FilmSearcher.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMovieUserRepository _movieUserRepository;
        private readonly IMapper _mapper;

        public UserService(IBaseRepository<User> userService, IMovieUserRepository movieUserRepository, IMapper mapper)
        {
            _userRepository = userService;
            _movieUserRepository = movieUserRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if(user != null) {
                var userDto = _mapper.Map<UserDTO>(user);

                return userDto;
            }

            return null;
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

        public IEnumerable<MovieDTO> GetMoviesByUserId(int Id)
        {
            var movies = _movieUserRepository.GetMoviesByUserId(Id);
            var moviesDto = new List<MovieDTO>();

            foreach (var movie in movies)
            {
                MovieDTO movieDTO = new()
                {
                    MovieId = movie.MovieId,
                    Name = movie.Name,
                    Description = movie.Description,
                    ImageURL = movie.ImageURL,
                    StartDate = movie.StartDate,
                    EndDate = movie.EndDate,
                    Category = movie.Category,
                };

                moviesDto.Add(movieDTO);
            }

            return moviesDto;
        }
    }
}
