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

        public async Task<bool> DeleteUserAsync(int id)
        {
            var c = _ctx.Users;
            var user=await _ctx.Users.SingleOrDefaultAsync(x => x.Id == id);
            
            if (user != null)
            {
                _ctx.Users.Remove(user);
                await _ctx.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<User?> GetUserAsync(int id)
        {
            var user = await _ctx.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                return new User
                {
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName
                };
            }
            return null;
        }

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

        public async Task<bool> PutUserAsync(User user)
        {
            var userEntity =  _ctx.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            if(userEntity != null)
            {
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                _ctx.Entry(userEntity).State= EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return true;
            }      
            return false;
        }
    }
}