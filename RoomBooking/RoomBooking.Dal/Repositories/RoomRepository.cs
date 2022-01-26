using Microsoft.EntityFrameworkCore;
using RoomBooking.Domain.Interfaces.Dal;
using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Dal.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly KataHotelContext _ctx;

        public RoomRepository(KataHotelContext ctx) =>
            _ctx = ctx;

        public async Task<bool> DeleteRoomAsync(int id)
        {
            
            var room = await _ctx.Rooms.SingleOrDefaultAsync(x => x.Id == id);

            if (room != null)
            {
                _ctx.Rooms.Remove(room);
                await _ctx.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Room?> GetRoomAsync(int id)
        {
            var room = await _ctx.Rooms.SingleOrDefaultAsync(x => x.Id == id);
            if (room != null)
            {
                return new Room
                {
                    Name = room.Name,
                    Id = room.Id
                };
            }
            return null;
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            var rooms = await _ctx.Rooms.ToListAsync();
            return rooms.Select(u => new Room
            {
                Id = u.Id,
                Name = u.Name
            });
        }

        public async Task<bool> InsertRoomAsync(Room room)
        {
            RoomEntity RoomEntity = new RoomEntity
            {
                Name = room.Name,
                Id = 0
            };

            try
            {
                _ctx.Rooms.Add(RoomEntity).Property(e => e.Id);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PutRoomAsync(Room room)
        {
            var roomEntity = await _ctx.Rooms.SingleOrDefaultAsync(x => x.Id == room.Id);
            if (roomEntity != null)
            {
                roomEntity.Name = room.Name;
                await _ctx.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
