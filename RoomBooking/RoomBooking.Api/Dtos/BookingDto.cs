using FoolProof.Core;
using RoomBooking.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Api.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [BookingDtoDateValidationAttribute(ErrorMessage ="Date must be equals or greater than now")]
        public DateTime Date { get; set; }
        [Required]
        [BookingDtoStartSlotValidationAttribute(ErrorMessage = "StartSlot must be equals or greater than now")]
        public int StartSlot { get; set; }
        [Required]
        [GreaterThan("StartSlot", ErrorMessage = "EndSlot must be greater than StartSlot")]
        public int EndSlot { get; set; }
        [Required]
        public Room Room { get; set; } = null!;
        [Required]
        public User User { get; set; } = null!;
    }
}
