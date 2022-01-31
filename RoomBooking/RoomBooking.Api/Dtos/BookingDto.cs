using RoomBooking.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Api.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int StartSlot { get; set; }
        [Required]
        public int EndSlot { get; set; }

        public Room Room { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
