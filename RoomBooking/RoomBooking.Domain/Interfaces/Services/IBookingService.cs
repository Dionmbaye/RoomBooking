using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Interfaces.Services
{
    public interface IBookingService
    {
        public Task<IEnumerable<Booking>> GetBookingsAsync();
        public Task<Booking?> GetBookingAsync(int id);
        public Task<bool> DeleteBookingAsync(int id);
        public Task<bool> PutBookingAsync(Booking Booking);
        public Task<IEnumerable<Slot>?> InsertBookingAsync(Booking Booking);
    }
}
