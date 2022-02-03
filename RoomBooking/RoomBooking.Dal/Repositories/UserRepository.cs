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

        public async Task<IEnumerable<Booking>> GetUserBookings(int id)
        {
           var bookings= await _ctx.Bookings.Include(x => x.Room)
                .Include(x => x.User)
                .Where(x=>x.UserId==id)
                .ToListAsync();

            return bookings.Select(u => new Booking
            {
                Id = u.Id,
                Date = u.Date,
                EndSlot = u.EndSlot,
                Room = new Room { Id = u.Room.Id, Name = u.Room.Name },
                StartSlot = u.StartSlot,
                User = new User { Id = u.User.Id, LastName = u.User.FirstName, FirstName = u.User.LastName, }
                });
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

        public async Task<bool> InsertUserAsync(User user)
        {
            UserEntity userEntity = new UserEntity
            {
                FirstName=user.FirstName,
                LastName=user.LastName,
                Id=0
            };
            
            try
            {
                _ctx.Users.Add(userEntity).Property(e => e.Id);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PutUserAsync(User user)
        {
            var userEntity = await _ctx.Users.SingleOrDefaultAsync(x => x.Id == user.Id);
            if(userEntity != null)
            {
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                await _ctx.SaveChangesAsync();
                return true;
            }      
            return false;
        }
    }
}