using AutoMapper;
using FilmSearcher.BLL.Models;
using FilmSearcher.DAL.Entities;

namespace FilmSearcher.BLL.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<MovieDTO, Movie>();
        }
    }
}
