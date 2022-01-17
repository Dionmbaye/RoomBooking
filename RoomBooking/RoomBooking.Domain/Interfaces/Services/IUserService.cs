using RoomBooking.Domain.Models;

namespace RoomBooking.Domain.Interfaces.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsersAsync();
    }
}
