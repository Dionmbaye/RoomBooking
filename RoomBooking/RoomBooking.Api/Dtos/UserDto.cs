using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Api.Dtos
{
    public class UserDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
