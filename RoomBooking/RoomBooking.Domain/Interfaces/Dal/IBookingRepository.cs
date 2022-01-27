using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingBooking.Domain.Interfaces.Dal
{
    public interface IBookingRepository
    {
        //public Task<IEnumerable<Booking>> GetBookingsAsync();
        //public Task<Booking?> GetBookingAsync(int id);
        public Task<bool> DeleteBookingAsync(int id);
        //public Task<bool> PutBookingAsync(Booking booking);
        //public Task<bool> InsertBookingAsync(Booking booking);
    }
}
