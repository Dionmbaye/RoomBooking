using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RoomBooking.Domain.Interfaces.Dal;
using RoomBooking.Domain.Models;
using RoomBooking.Domain.Services;

namespace RoomBooking.Test.Domain
{
    [TestClass]
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly IUserRepository _userRepository;

        public UserServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _userService = new UserService(_userRepository);
        }

        [TestMethod]
        public async Task Should_Get_Users()
        {
            _userRepository.GetUsersAsync().Returns(new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Test1",
                    LastName =  "Test2"
                }
            });

            var users = await _userService.GetUsersAsync();

            Assert.IsNotNull(users);
        }

        [TestMethod]
        public async Task Should_Get_User_Where_Id_Equals_1()
        {
            _userRepository.GetUserAsync(1).Returns(
                new User
                {
                    Id = 1,
                    FirstName = "Test1",
                    LastName =  "Test2"
                }
            );

            var user= await _userService.GetUserAsync(1);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task Should_Put_User()
        {
            var user = new User
            {
                Id = 1,
                FirstName = "Test1",
                LastName = "Test2"
            };

            _userRepository.PutUserAsync(user).Returns(true);

            var response = await _userService.PutUserAsync(user);

            Assert.AreEqual(true, response);
        }

        [TestMethod]
        public async Task Should_Insert_User()
        {
            var user = new User
            {
                Id = 1,
                FirstName = "Test1",
                LastName = "Test2"
            };

            _userRepository.InsertUserAsync(user).Returns(true);

            var response = await _userService.InsertUserAsync(user);

            Assert.AreEqual(true, response);
        }
    }
}
