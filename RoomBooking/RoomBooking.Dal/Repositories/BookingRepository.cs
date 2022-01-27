using BookingBooking.Domain.Interfaces.Dal;
using Microsoft.EntityFrameworkCore;
using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
            var booking = await _ctx.Bookings.Where(x => x.Id == id).Include(x=>x.User).Include(x=>x.Room).SingleOrDefaultAsync();
            _ctx.Bookings.Include(x => x.User);
            _ctx.Bookings.Include(x => x.Room);
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

        //public Task<IEnumerable<Booking>> GetBookingsAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> InsertBookingAsync(Booking booking)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> PutBookingAsync(Booking booking)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
