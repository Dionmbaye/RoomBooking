using BookingBooking.Domain.Interfaces.Dal;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository) =>
            _bookingRepository = bookingRepository;

        public async Task<bool> DeleteBookingAsync(int id)
        {
            return await _bookingRepository.DeleteBookingAsync(id);
        }

        public async Task<Booking?> GetBookingAsync(int id)
        {
            return await _bookingRepository.GetBookingAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()=>
            await _bookingRepository.GetBookingsAsync();


        public Task<bool> InsertBookingAsync(Booking Booking)
        {
            return _bookingRepository.InsertBookingAsync(Booking);
        }

        public async Task<bool> PutBookingAsync(Booking Booking)
        {
            return await _bookingRepository.PutBookingAsync(Booking);
        }
    }
}
