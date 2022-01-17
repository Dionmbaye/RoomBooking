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
    }
}
