using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomBooking.Dal;
using RoomBooking.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomBooking.Domain.Models;

namespace RoomBooking.Test.Dal
{
    [TestClass]
    public class BookingRepositoryTests
    {
        private BookingRepository? _bookingRepository;
        private DbContextOptions<KataHotelContext>? _options;

        [TestMethod]
        public async Task Should_Get_Booking_Where_id_equals_2()
        {
            //Arrange
            _options = new DbContextOptionsBuilder<KataHotelContext>()
              .UseInMemoryDatabase("when_Get_Booking")
              .Options;
            Booking? booking = new Booking();

            using (var ctx = new KataHotelContext(_options))
            {
                var room = new RoomEntity { Id = 1, Name = "Test" };
                ctx.Rooms.Add(room);
                var user = new UserEntity { FirstName = "Test1", LastName = "Test2", Id = 1 };
                ctx.Users.Add(user);
                var fakeBookingEntity = new BookingEntity
                {
                    Id = 1,
                    Date = DateTime.Now,
                    StartSlot = 6,
                    EndSlot = 10,
                    Room = room,
                    RoomId = 1,
                    User = user,
                    UserId = 1
                };


                ctx.Bookings.Add(fakeBookingEntity);
                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new KataHotelContext(_options))
            {
                _bookingRepository = new BookingRepository(ctx);
                booking = await _bookingRepository.GetBookingAsync(1);
            }

            //Assert
            Assert.IsNotNull(booking);
            Assert.AreEqual(booking.Id, 1);


        }

        [TestMethod]
        public async Task Should_Delete_Bookin_Where_Id_Equal_1()
        {
            //Arrange
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_delete_Booking")
               .Options;
            Booking? booking = new Booking();
            var room = new RoomEntity { Id = 1, Name = "Test" };
            var user = new UserEntity { FirstName = "Test1", LastName = "Test2", Id = 1 };

            using (var ctx = new KataHotelContext(_options))
            {
                
                ctx.Rooms.Add(room);
                ctx.Users.Add(user);
                var fakeBookingEntity = new BookingEntity
                {
                    Id = 1,
                    Date = DateTime.Now,
                    StartSlot = 6,
                    EndSlot=10,
                    Room=room,
                    RoomId=1,
                    User=user,
                    UserId=1
                };
                
                ctx.Bookings.Add(fakeBookingEntity);
                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new KataHotelContext(_options))
            {
                _bookingRepository = new BookingRepository(ctx);
                await _bookingRepository.DeleteBookingAsync(1);
                booking = await _bookingRepository.GetBookingAsync(1);
            }

            //Assert
            Assert.IsNull(booking);
        }

        [TestMethod]
        public async Task Should_Insert_Booking()
        {
            //Arrange
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_insert_booking")
               .Options;
            var inserted = false;
            Booking? booking = new Booking();
            var room = new Room { Id = 1, Name = "Test" };
            var user = new User { FirstName = "Test1", LastName = "Test2", Id = 1 };

            //Act
            using (var ctx = new KataHotelContext(_options))
            {
                var fakeBooking = new Booking
                {
                    Id = 1,
                    Date = DateTime.Now,
                    StartSlot = 6,
                    EndSlot = 10,
                    Room = room,
                    User = user,
                };

                _bookingRepository = new BookingRepository(ctx);
                inserted = await _bookingRepository.InsertBookingAsync(fakeBooking);
            }

            //Assert
            Assert.AreEqual(true, inserted);
        }

        [TestMethod]
        public async Task Should_Put_Booking()
        {
            //Arrange
            _options = new DbContextOptionsBuilder<KataHotelContext>()
               .UseInMemoryDatabase("when_put_Booking")
               .Options;
            var updated = false;
            Booking? booking = new Booking();
            var room = new Room { Id = 1, Name = "Test" };
            var user = new User { FirstName = "Test1", LastName = "Test2", Id = 1 };
            var fakeBooking = new Booking
            {
                Id = 1,
                Date = DateTime.Now,
                StartSlot = 6,
                EndSlot = 10,
                Room = room,
                User = user,
            };

            //Act
            using (var ctx = new KataHotelContext(_options))
            {
                BookingEntity book = new BookingEntity
                {
                    Id = 1,
                    Date = DateTime.Now,
                    StartSlot = 6,
                    EndSlot = 10,
                    Room = new RoomEntity { Id = room.Id, Name = room.Name },
                    User = new UserEntity { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, }
                };

                ctx.Bookings.Add(book);
                await ctx.SaveChangesAsync();

                _bookingRepository = new BookingRepository(ctx);
                updated = await _bookingRepository.PutBookingAsync(fakeBooking);
            }

            //Assert
            Assert.AreEqual(true, updated);
        }

        [TestMethod]
        public async Task Should_Get_Bookings()
        {
            //Arrange
            _options = new DbContextOptionsBuilder<KataHotelContext>()
                .UseInMemoryDatabase("when_requesting_Bookings")
                .Options;
            IEnumerable<Booking>? booking = null;
            var room = new Room { Id = 1, Name = "Test" };
            var user = new User { FirstName = "Test1", LastName = "Test2", Id = 1 };
            using (var ctx = new KataHotelContext(_options))
            {
                BookingEntity book = new BookingEntity
                {
                    Id = 1,
                    Date = DateTime.Now,
                    StartSlot = 6,
                    EndSlot = 10,
                    Room = new RoomEntity { Id = room.Id, Name = room.Name },
                    User = new UserEntity { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName },
                    RoomId= room.Id,
                    UserId= user.Id
                };
                ctx.Bookings.Add(book);
                await ctx.SaveChangesAsync();
            }

            //Act
            using (var ctx = new KataHotelContext(_options))
            {
                _bookingRepository = new BookingRepository(ctx);
                booking = await _bookingRepository.GetBookingsAsync();
            }

            //Assert
            Assert.IsNotNull(booking);
            Assert.AreEqual(booking.Count(), 1);
        }



    }
}
