using AutoMapper;
using RoomBooking.Api.Dtos;
using RoomBooking.Domain.Models;

namespace RoomBooking.Api
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
