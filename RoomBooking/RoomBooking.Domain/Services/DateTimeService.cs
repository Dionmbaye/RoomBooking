using RoomBooking.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Services
{
    public class DateTimeService : IDateTimeService
    {
       

        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }

        public int GetHourNow()
        {
           return DateTime.Now.Hour;
        }
    }
}
