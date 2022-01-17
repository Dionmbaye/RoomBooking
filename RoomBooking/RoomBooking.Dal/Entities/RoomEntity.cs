using System;
using System.Collections.Generic;

namespace RoomBooking.Dal
{
    public partial class RoomEntity
    {
        public RoomEntity()
        {
            Bookings = new HashSet<BookingEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<BookingEntity> Bookings { get; set; }
    }
}
