using RoomBooking.Domain.Models;

namespace RoomBooking.Api.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int StartSlot { get; set; }
        public int EndSlot { get; set; }

        public Room Room { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
