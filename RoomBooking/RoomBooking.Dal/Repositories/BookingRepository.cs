using BookingBooking.Domain.Interfaces.Dal;
using Microsoft.EntityFrameworkCore;
using RoomBooking.Domain.Models;

namespace RoomBooking.Dal.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly KataHotelContext _ctx;

        public BookingRepository(KataHotelContext ctx) =>
            _ctx = ctx;
        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _ctx.Bookings.SingleOrDefaultAsync(x => x.Id == id);

            if (booking != null)
            {
                _ctx.Bookings.Remove(booking);
                await _ctx.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Booking?> GetBookingAsync(int id)
        {
            var booking = await _ctx.Bookings.Include(x=>x.User).Include(x=>x.Room).SingleOrDefaultAsync(x => x.Id == id);
            if (booking != null)
            {
                return new Booking
                {
                    Date=booking.Date,
                    EndSlot=booking.EndSlot,
                    StartSlot=booking.StartSlot,
                    Room=new Room { Id=booking.Room.Id, Name=booking.Room.Name },
                    User=new User { Id=booking.User.Id, FirstName=booking.User.FirstName, LastName=booking.User.LastName,},
                    Id = booking.Id
                };
            }
            return null;
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            var bookings = await _ctx.Bookings.Include(x => x.User).Include(x => x.Room).ToListAsync();
            return bookings.Select(u => new Booking
            {
                Id = u.Id,
                Date = u.Date,
                EndSlot=u.EndSlot,
                Room=new Room { Id = u.Room.Id, Name = u.Room.Name },
                StartSlot=u.StartSlot,
                User= new User { Id=u.User.Id, LastName=u.User.FirstName, FirstName=u.User.LastName,}
            });
        }

        public async Task<bool> InsertBookingAsync(Booking booking)
        {
            BookingEntity book =new BookingEntity
            {
                Date = booking.Date,
                EndSlot = booking.EndSlot,
                StartSlot = booking.StartSlot,
                Id = booking.Id,
                RoomId=booking.Room.Id,
                UserId=booking.User.Id
            };

            try
            {
                _ctx.Bookings.Add(book).Property(e => e.Id);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PutBookingAsync(Booking booking)
        {
            var bookingEntity = await _ctx.Bookings.SingleOrDefaultAsync(x => x.Id == booking.Id);
            if (bookingEntity != null)
            {
                bookingEntity.Date = booking.Date;
                bookingEntity.EndSlot = booking.EndSlot;
                bookingEntity.StartSlot = booking.StartSlot;
                bookingEntity.Id = booking.Id;
                bookingEntity.RoomId = booking.Room.Id;
                bookingEntity.UserId = booking.User.Id;
                
                await _ctx.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
