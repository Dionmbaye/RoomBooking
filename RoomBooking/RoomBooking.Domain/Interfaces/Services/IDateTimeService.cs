using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Interfaces.Services
{
    public interface IDateTimeService
    {
        public DateTime GetDateTimeNow();
        public int GetHourNow();
    }
}
