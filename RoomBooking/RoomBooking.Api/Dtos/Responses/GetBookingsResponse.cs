namespace RoomBooking.Api.Dtos.Responses
{
    public class GetBookingsResponse
    {
        public IEnumerable<BookinDto> Bookings { get; set; } = new List<BookinDto>();
    }
}
