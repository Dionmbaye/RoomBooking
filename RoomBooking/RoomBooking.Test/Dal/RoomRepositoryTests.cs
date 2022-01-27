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
        private RoomRepository? _roomRepository;
        private DbContextOptions<KataHotelContext>? _options;

        public RoomRepository Get_RoomRepository()
        {
            return _roomRepository;
        }

        [TestMethod]
        public async Task Should_Get_Rooms()
        {
            //Arrange
            _options = new DbContextOptionsBuilder<KataHotelContext>()
                .UseInMemoryDatabase("when_requesting_Rooms")
                .Options;
            IEnumerable<Room>? rooms = null;
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

            //Act
            using (var ctx = new KataHotelContext(_options))
            {
                _roomRepository = new RoomRepository(ctx);
                rooms = await _roomRepository.GetRoomsAsync();
            }

            //Assert
            Assert.IsNotNull(rooms);
            Assert.AreEqual(rooms.Count(), 1);
        }

        [TestMethod]
        public async Task Should_Get_Room_Where_id_equals_1()
        {
            //Arrange
            _options = new DbContextOptionsBuilder<KataHotelContext>()
                .UseInMemoryDatabase("when_requesting_Room")
                .Options;
            Room? room = null;
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

            //Act
            using (var ctx = new KataHotelContext(_options))
            {
                _roomRepository = new RoomRepository(ctx);
                room = await _roomRepository.GetRoomAsync(1);
            }

            //Assert
            Assert.IsNotNull(room);
            Assert.AreEqual(room.Id, 1);

        }

        [TestMethod]
        public async Task Should_Delete_Room_Where_Id_Equal_1()
        {
            //Arrange
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

            //Act
            using (var ctx = new KataHotelContext(_options))
            {
                _roomRepository = new RoomRepository(ctx);
                await _roomRepository.DeleteRoomAsync(1);
                Room = await _roomRepository.GetRoomAsync(1);
            }

            //Assert
            Assert.IsNull(Room);
        }

        [TestMethod]
        public async Task Should_Put_Room()
        {
            //Arrange
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_put_Room")
               .Options;
            var updated = false;

            //Act
            using (var ctx = new KataHotelContext(_options))
            {
                var fakeRoom = new Room
                {
                    Id = 1,
                    Name = "Test 1"
                };

                _roomRepository = new RoomRepository(ctx);
                updated = await _roomRepository.PutRoomAsync(fakeRoom);
            }

            //Assert
            Assert.AreEqual(true, updated);
        }

        [TestMethod]
        public async Task Should_Insert_Room()
        {
            //Arrange
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_put_Room")
               .Options;
            var inserted = false;

            //Act
            using (var ctx = new KataHotelContext(_options))
            {
                var fakeRoom = new Room
                {
                    Id = 1,
                    Name = "Test 1"
                };

                _roomRepository = new RoomRepository(ctx);
                inserted = await _roomRepository.InsertRoomAsync(fakeRoom);
            }

            //Assert
            Assert.AreEqual(true, inserted);
        }
    }
}
