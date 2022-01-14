using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain
{
    public interface IUsers
    {
        List<User> GetAll();
    }
}
