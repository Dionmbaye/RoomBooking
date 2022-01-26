using AutoMapper;
using RoomBooking.Api.Dtos;
using RoomBooking.Domain.Models;

namespace RoomBooking.Api
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //For user
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            //For Room
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();
        }
    }
}
