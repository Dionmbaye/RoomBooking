using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Interfaces.Dal
{
    public interface IRoomRepository
    {
        public Task<IEnumerable<Room>> GetRoomsAsync();
        public Task<Room?> GetRoomAsync(int id);
        public Task<bool> DeleteRoomAsync(int id);
        public Task<bool> PutRoomAsync(Room room);
        public Task<bool> InsertRoomAsync(Room room);
    }
}
