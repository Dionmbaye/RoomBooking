using System;
using System.Collections.Generic;

namespace RoomBooking.Dal
{
    public partial class UserEntity
    {
        public UserEntity()
        {
            Bookings = new HashSet<BookingEntity>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual ICollection<BookingEntity> Bookings { get; set; }
    }
}
