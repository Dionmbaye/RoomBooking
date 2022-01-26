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
        private readonly RoomService _RoomService;
        private readonly IRoomRepository _RoomRepository;

        public RoomServiceTests()
        {
            _RoomRepository = Substitute.For<IRoomRepository>();
            _RoomService = new RoomService(_RoomRepository);
        }

        [TestMethod]
        public async Task Should_Get_Rooms()
        {
            _RoomRepository.GetRoomsAsync().Returns(new List<Room>
            {
                new Room
                {
                    Id = 1,
                    Name = "Test1"
                }
            });

            var rooms = await _RoomService.GetRoomsAsync();

            Assert.IsNotNull(rooms);
        }

        [TestMethod]
        public async Task Should_Get_Room_Where_Id_Equals_1()
        {
            _RoomRepository.GetRoomAsync(1).Returns(
                new Room
                {
                    Id = 1,
                    Name = "Test1"
                }
            );

            var room = await _RoomService.GetRoomAsync(1);

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

            _RoomRepository.PutRoomAsync(room).Returns(true);

            var response = await _RoomService.PutRoomAsync(room);

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

            _RoomRepository.InsertRoomAsync(room).Returns(true);

            var response = await _RoomService.InsertRoomAsync(room);

            Assert.AreEqual(true, response);
        }
    }
}
