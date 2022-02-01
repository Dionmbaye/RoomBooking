using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Api.Dtos.Responses
{
    public class GetBookingByIdResponse
    {
        public BookingDto Booking { get; set; } = new BookingDto();
    }
}
