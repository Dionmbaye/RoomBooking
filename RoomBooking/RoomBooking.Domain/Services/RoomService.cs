using RoomBooking.Domain.Interfaces.Dal;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Services
{
    public class RoomService:IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository RoomRepository) =>
            _roomRepository = RoomRepository;


        public async Task<IEnumerable<Room>> GetRoomsAsync() =>
            await _roomRepository.GetRoomsAsync();

        public async Task<Room?> GetRoomAsync(int id) => await _roomRepository.GetRoomAsync(id);

        public async Task<bool> DeleteRoomAsync(int id)
        {
            return await _roomRepository.DeleteRoomAsync(id);
        }

        public async Task<bool> PutRoomAsync(Room Room)
        {
            return await _roomRepository.PutRoomAsync(Room);
        }

        public Task<bool> InsertRoomAsync(Room Room)
        {
            return _roomRepository.InsertRoomAsync(Room);
        }
    }
}
