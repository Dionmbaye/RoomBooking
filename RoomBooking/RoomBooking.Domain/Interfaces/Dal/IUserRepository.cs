using RoomBooking.Domain.Models;

namespace RoomBooking.Domain.Interfaces.Dal
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUserAsync(int id);
        public Task<bool> DeleteUserAsync(int id);
    }
}
