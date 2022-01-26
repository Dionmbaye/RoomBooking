using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBooking.Dal
{
    public partial class BookingEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int StartSlot { get; set; }
        public int EndSlot { get; set; }

        public virtual RoomEntity Room { get; set; } = null!;
        public virtual UserEntity User { get; set; } = null!;
    }
}
