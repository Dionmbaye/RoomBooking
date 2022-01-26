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
        private readonly IRoomRepository _RoomRepository;

        public RoomService(IRoomRepository RoomRepository) =>
            _RoomRepository = RoomRepository;


        public async Task<IEnumerable<Room>> GetRoomsAsync() =>
            await _RoomRepository.GetRoomsAsync();

        public async Task<Room?> GetRoomAsync(int id) => await _RoomRepository.GetRoomAsync(id);

        public async Task<bool> DeleteRoomAsync(int id)
        {
            return await _RoomRepository.DeleteRoomAsync(id);
        }

        public async Task<bool> PutRoomAsync(Room Room)
        {
            return await _RoomRepository.PutRoomAsync(Room);
        }

        public Task<bool> InsertRoomAsync(Room Room)
        {
            return _RoomRepository.InsertRoomAsync(Room);
        }
    }
}
