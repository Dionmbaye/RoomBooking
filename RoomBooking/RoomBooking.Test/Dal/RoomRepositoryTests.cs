using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomBooking.Dal;
using RoomBooking.Dal.Repositories;
using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Test.Dal
{
    [TestClass]
    public class RoomRepositoryTests
    {
        private RoomRepository? _RoomRepository;
        private DbContextOptions<KataHotelContext>? _options;

        [TestMethod]
        public async Task Should_Get_Rooms()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
                .UseInMemoryDatabase("when_requesting_Rooms")
                .Options;

            using (var ctx = new KataHotelContext(_options))
            {
                var fakeRoomEntity = new RoomEntity
                {
                    Id = 1,
                    Name = "Test 1"
                };
                ctx.Rooms.Add(fakeRoomEntity);
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new KataHotelContext(_options))
            {
                _RoomRepository = new RoomRepository(ctx);
                var Rooms = await _RoomRepository.GetRoomsAsync();

                Assert.IsNotNull(Rooms);
                Assert.AreEqual(Rooms.Count(), 1);
            }
        }

        [TestMethod]
        public async Task Should_Get_Room_Where_id_equals_1()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
                .UseInMemoryDatabase("when_requesting_Room")
                .Options;

            using (var ctx = new KataHotelContext(_options))
            {
                var fakeRoomEntity = new RoomEntity
                {
                    Id = 1,
                    Name = "Test 1"
                };
                ctx.Rooms.Add(fakeRoomEntity);
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new KataHotelContext(_options))
            {
                _RoomRepository = new RoomRepository(ctx);
                var Room = await _RoomRepository.GetRoomAsync(1);

                Assert.IsNotNull(Room);
                Assert.AreEqual(Room.Id, 1);
            }

        }

        public RoomRepository Get_RoomRepository()
        {
            return _RoomRepository;
        }

        [TestMethod]
        public async Task Should_Delete_Room_Where_Id_Equal_1()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_delete_Room")
               .Options;
            Room? Room = new Room();
            using (var ctx = new KataHotelContext(_options))
            {
                var fakeRoomEntity = new RoomEntity
                {
                    Id = 1,
                    Name = "Test 1"
                };
                ctx.Rooms.Add(fakeRoomEntity);
                ctx.SaveChanges();
            }

            using (var ctx = new KataHotelContext(_options))
            {
                _RoomRepository = new RoomRepository(ctx);
                await _RoomRepository.DeleteRoomAsync(1);
                Room = await _RoomRepository.GetRoomAsync(1);
            }

            Assert.IsNull(Room);
        }

        [TestMethod]
        public async Task Should_Put_Room()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_put_Room")
               .Options;
            var updated = false;

            using (var ctx = new KataHotelContext(_options))
            {
                var fakeRoom = new Room
                {
                    Id = 1,
                    Name = "Test 1"
                };

                _RoomRepository = new RoomRepository(ctx);
                updated = await _RoomRepository.PutRoomAsync(fakeRoom);
            }

            Assert.AreEqual(true, updated);
        }

        [TestMethod]
        public async Task Should_Insert_Room()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_put_Room")
               .Options;
            var inserted = false;

            using (var ctx = new KataHotelContext(_options))
            {
                var fakeRoom = new Room
                {
                    Id = 1,
                    Name = "Test 1"
                };

                _RoomRepository = new RoomRepository(ctx);
                inserted = await _RoomRepository.InsertRoomAsync(fakeRoom);
            }

            Assert.AreEqual(true, inserted);
        }
    }
}
