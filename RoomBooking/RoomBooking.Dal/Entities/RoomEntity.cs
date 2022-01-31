using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBooking.Dal
{
    [Index(nameof(Name), IsUnique = true)]
    public partial class RoomEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public RoomEntity()
        {
            Bookings = new HashSet<BookingEntity>();
        }

        public string Name { get; set; } = null!;

        public virtual ICollection<BookingEntity> Bookings { get; set; }
    }
}
