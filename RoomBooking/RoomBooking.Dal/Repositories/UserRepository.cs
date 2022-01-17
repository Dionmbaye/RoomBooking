using Microsoft.EntityFrameworkCore;
using RoomBooking.Domain.Interfaces.Dal;
using RoomBooking.Domain.Models;

namespace RoomBooking.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KataHotelContext _ctx;

        public UserRepository(KataHotelContext ctx) =>
            _ctx = ctx;

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _ctx.Users.ToListAsync();
            return users.Select(u => new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            });
        }
    }
}