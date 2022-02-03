using RoomBooking.Domain.Interfaces.Dal;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;

namespace RoomBooking.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; 

        public UserService(IUserRepository userRepository) =>
            _userRepository = userRepository;


        public async Task<IEnumerable<User>> GetUsersAsync() =>
            await _userRepository.GetUsersAsync();

        public async Task<User?> GetUserAsync(int id) => await _userRepository.GetUserAsync(id);

        public async Task<bool> DeleteUserAsync(int id)
        {
           return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<bool> PutUserAsync(User user)
        {
            return await _userRepository.PutUserAsync(user);
        }

        public Task<bool> InsertUserAsync(User user)
        {
            return _userRepository.InsertUserAsync(user);
        }

        public Task<IEnumerable<Booking>> GetUserBookingsAsync(int id)
        {
            return _userRepository.GetUserBookings(id);
        }
    }
}
