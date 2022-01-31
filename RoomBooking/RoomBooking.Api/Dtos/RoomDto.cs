using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Api.Dtos
{
    public class RoomDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
