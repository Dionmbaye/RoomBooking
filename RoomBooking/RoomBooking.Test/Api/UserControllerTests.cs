﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RoomBooking.Api.Controllers;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;

namespace RoomBooking.Test.Api
{
    [TestClass]
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly IUserService _userService;

        public UserControllerTests()
        {
            _userService = Substitute.For<IUserService>();
            _userController = new UserController(_userService);
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
        public void Should_Delete_User_Where_Id_Equal_1()
        {
            var command = Substitute.For<UserController>();
            command.DeleteUser(1);

            command.Received().DeleteUser(1);
        }
    }
}
