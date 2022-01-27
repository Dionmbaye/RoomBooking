using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int StartSlot { get; set; }
        public int EndSlot { get; set; }

        public  Room Room { get; set; } = null!;
        public  User User { get; set; } = null!;
    }
}
