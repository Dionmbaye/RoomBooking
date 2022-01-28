using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RoomBooking.Domain.Interfaces.Dal;
using RoomBooking.Domain.Models;
using RoomBooking.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Test.Domain
{
    [TestClass]
    public class RoomServiceTests
    {
        private readonly RoomService _roomService;
        private readonly IRoomRepository _roomRepository;

        public RoomServiceTests()
        {
            _roomRepository = Substitute.For<IRoomRepository>();
            _roomService = new RoomService(_roomRepository);
        }

        [TestMethod]
        public async Task Should_Get_Rooms()
        {
            _roomRepository.GetRoomsAsync().Returns(new List<Room>
            {
                new Room
                {
                    Id = 1,
                    Name = "Test1"
                }
            });

            var rooms = await _roomService.GetRoomsAsync();

            Assert.IsNotNull(rooms);
        }

        [TestMethod]
        public async Task Should_Get_Room_Where_Id_Equals_1()
        {
            _roomRepository.GetRoomAsync(1).Returns(
                new Room
                {
                    Id = 1,
                    Name = "Test1"
                }
            );

            var room = await _roomService.GetRoomAsync(1);

            Assert.IsNotNull(room);
        }

        [TestMethod]
        public async Task Should_Put_Room()
        {
            var room = new Room
            {
                Id = 1,
                Name = "Test1"
            };

            _roomRepository.PutRoomAsync(room).Returns(true);

            var response = await _roomService.PutRoomAsync(room);

            Assert.AreEqual(true, response);
        }

        [TestMethod]
        public async Task Should_Insert_Room()
        {
            var room = new Room
            {
                Id = 1,
                Name = "Test1"
            };

            _roomRepository.InsertRoomAsync(room).Returns(true);

            var response = await _roomService.InsertRoomAsync(room);

            Assert.AreEqual(true, response);
        }
    }
}
