using RoomBooking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Dal
{
    public interface IUsers
    {
        List<User> GetAll();
    }
}
