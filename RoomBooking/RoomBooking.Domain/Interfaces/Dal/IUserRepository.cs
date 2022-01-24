using RoomBooking.Domain.Models;

namespace RoomBooking.Domain.Interfaces.Dal
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUserAsync(int id);
        public void DeleteUser(int id);
    }
}
