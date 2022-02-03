using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomBooking.Dal;
using RoomBooking.Dal.Repositories;
using RoomBooking.Domain.Models;

namespace RoomBooking.Test.Dal
{
    [TestClass]
    public class UserRepositoryTests
    {
        private UserRepository? _userRepository;
        private DbContextOptions<KataHotelContext>? _options;

        [TestMethod]
        public async Task Should_Get_Users()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
                .UseInMemoryDatabase("when_requesting_users")
                .Options;
            IEnumerable<User> users = null;

            using (var ctx = new KataHotelContext(_options))
            {
                var fakeUserEntity = new UserEntity
                {
                    Id = 1,
                    FirstName = "Test 1",
                    LastName = "Test 2"
                };
                ctx.Users.Add(fakeUserEntity);
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new KataHotelContext(_options))
            {
                _userRepository = new UserRepository(ctx);
                users = await _userRepository.GetUsersAsync();
            }

            Assert.IsNotNull(users);
            Assert.AreEqual(users.Count(), 1);
        }

        [TestMethod]
        public async Task Should_Get_User_Where_id_equals_1()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
                .UseInMemoryDatabase("when_requesting_user")
                .Options;
            User user = null;

            using (var ctx = new KataHotelContext(_options))
            {
                var fakeUserEntity = new UserEntity
                {
                    Id = 1,
                    FirstName = "Test 1",
                    LastName = "Test 2"
                };
                ctx.Users.Add(fakeUserEntity);
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new KataHotelContext(_options))
            {
                _userRepository = new UserRepository(ctx);
                user = await _userRepository.GetUserAsync(1);
            }

            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, 1);

        }

        public UserRepository Get_userRepository()
        {
            return _userRepository;
        }

        [TestMethod]
        public async Task Should_Delete_User_Where_Id_Equal_1()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_delete_user")
               .Options;
            User? user = new User();
            using (var ctx = new KataHotelContext(_options))
            {
                var fakeUserEntity = new UserEntity
                {
                    Id = 1,
                    FirstName = "Test 1",
                    LastName = "Test 2"
                };
                ctx.Users.Add(fakeUserEntity);
                ctx.SaveChanges();
            }

            using (var ctx = new KataHotelContext(_options))
            {
                _userRepository = new UserRepository(ctx);
                await _userRepository.DeleteUserAsync(1);
                user = await _userRepository.GetUserAsync(1);
            }

            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task Should_Put_User()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_put_user")
               .Options;
            var updated = false;

            using (var ctx = new KataHotelContext(_options))
            {
                var fakeUser = new User
                {
                    Id = 1,
                    FirstName = "New Test 1",
                    LastName = "New Test 2"
                };

                _userRepository = new UserRepository(ctx);
                updated = await _userRepository.PutUserAsync(fakeUser);
            }

            Assert.AreEqual(true, updated);
        }

        [TestMethod]
        public async Task Should_Insert_User()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_put_user")
               .Options;
            var inserted = false;

            using (var ctx = new KataHotelContext(_options))
            {
                var fakeUser = new User
                {
                    Id = 1,
                    FirstName = "New Test 1",
                    LastName = "New Test 2"
                };

                _userRepository = new UserRepository(ctx);
                inserted = await _userRepository.InsertUserAsync(fakeUser);
            }

            Assert.AreEqual(true, inserted);
        }

        [TestMethod]
        public async Task Should_Get_User_Bookings()
        {
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_get_booking_for_user")
               .Options;
            IEnumerable<Booking> booking = null;
            using (var ctx = new KataHotelContext(_options))
            {
                ctx.Bookings.Add(new BookingEntity
                {
                    Id = 1,
                    StartSlot = 2,
                    EndSlot = 4,
                    Date = DateTime.Now,
                    Room = new RoomEntity
                    {
                        Id = 1,
                        Name = "Room2"
                    },
                    UserId = 1,
                    RoomId = 1,
                    User = new UserEntity
                    {
                        Id = 1,
                        FirstName = "New Test 1",
                        LastName = "New Test 2"
                    }

                });
                await ctx.SaveChangesAsync();

                _userRepository = new UserRepository(ctx);
                booking = await _userRepository.GetUserBookings(1);
            }

            Assert.IsNotNull(booking);
            Assert.AreEqual(booking.Count(), 1);
        }
    }
}
