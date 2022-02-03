using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RoomBooking.Api;
using RoomBooking.Api.Controllers;
using RoomBooking.Api.Dtos;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Test.Api
{
    [TestClass]
    public class RoomControllerTests
    {
        private readonly RoomController _roomController;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomControllerTests()
        {
            _roomService = Substitute.For<IRoomService>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingConfig());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _roomController = new RoomController(_roomService, _mapper);
        }

        [TestMethod]
        public async Task Should_Get_Rooms()
        {
            _roomService.GetRoomsAsync().Returns(new List<Room>
            {
                new Room
                {
                    Id = 1,
                    Name ="Test 1"
                }
            });

            var users = await _roomController.GetRoomsAsync();

            Assert.IsNotNull(users);
        }

        [TestMethod]
        public async Task Should_Get_Room_Where_Id_Equals_1()
        {
            _roomService.GetRoomAsync(1).Returns(
            new Room
            {
                Id = 1,
                Name = "Test1"
            }
          );

            var room = await _roomController.GetRoomAsync(1);

            Assert.IsNotNull(room);
        }

        [TestMethod]
        public async Task Should_Return_NotFound_Where_Id_Equals_0()
        {
            var user = await _roomController.GetRoomAsync(0);

            Assert.IsNotNull(user);
            Assert.IsInstanceOfType(user, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Should_Call_DeleBooking_Where_Id_Equal_1()
        {
            RoomController roomController = new RoomController(_roomService, _mapper);

            await roomController.DeleteRoomAsync(1);
            await _roomService.Received().DeleteRoomAsync(1);
        }

        [TestMethod]
        public async Task Should_Update_Room_Where_Id_Equal_1()
        {
            RoomController userController = new RoomController(_roomService, _mapper);

            _roomService.GetRoomAsync(1).Returns(
               new Room
               {
                   Id = 1,
                   Name = "Test"
               }
            );
            RoomDto userUpdate = new RoomDto { Id = 1, Name = "Test" };
            var room = new Room { Id = userUpdate.Id, Name = userUpdate.Name };

            var response = await _roomController.PutRoom(1, userUpdate);

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task Should_Create_New_Room()
        {
            RoomController roomController = new RoomController(_roomService, _mapper);

            RoomDto roomUpdate = new RoomDto { Name = "Test Room", Id = 1};

            var response = await _roomController.PostRoom(roomUpdate);

            Assert.IsNotNull(response);
        }
    }
}

