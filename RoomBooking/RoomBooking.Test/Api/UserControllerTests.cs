using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RoomBooking.Api;
using RoomBooking.Api.Controllers;
using RoomBooking.Api.Dtos;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;

namespace RoomBooking.Test.Api
{
    [TestClass]
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserControllerTests()
        {
            _userService = Substitute.For<IUserService>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingConfig());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _userController = new UserController(_userService, _mapper);
        }

        [TestMethod]
        public async Task Should_Get_Users()
        {
            _userService.GetUsersAsync().Returns(new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Test1",
                    LastName =  "Test2"
                }
            });

            var users = await _userController.GetUsersAsync();

            Assert.IsNotNull(users);
        }

        [TestMethod]
        public async Task Should_Get_User_Where_Id_Equals_1()
        {
            _userService.GetUserAsync(1).Returns(
            new User
            {
                Id = 1,
                FirstName = "Test1",
                LastName = "Test2"
            }
          );

            var user = await _userController.GetUserAsync(1);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task Should_Return_NotFound_Where_Id_Equals_0()
        {
            var user = await _userController.GetUserAsync(0);

            Assert.IsNotNull(user);
            Assert.IsInstanceOfType(user, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Should_Call_DeleUser_Where_Id_Equal_1()
        {
            UserController userController = new UserController(_userService, _mapper);

            await userController.DeleteUserAsync(1);
            await _userService.Received().DeleteUserAsync(1);
        }

        [TestMethod]
        public async Task Should_Update_User_Where_Id_Equal_1()
        {
            UserController userController = new UserController(_userService, _mapper);

            _userService.GetUserAsync(1).Returns(
               new User
               {
                   Id = 1,
                   FirstName = "Test1",
                   LastName = "Test2"
               }
            );
            UserDto userUpdate = new UserDto { FirstName = "Sophie", Id = 1, LastName = "Anne" };
            var user=new User { LastName = userUpdate.LastName, Id = userUpdate.Id, FirstName=userUpdate.FirstName };
           
            var response = await _userController.PutUser(1, userUpdate);

            Assert.IsNotNull(response);
        }

    }
}
