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
        private BookingRepository? _BookingRepository;
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
                _BookingRepository = new BookingRepository(ctx);
                booking = await _BookingRepository.GetBookingAsync(1);
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
                _BookingRepository = new BookingRepository(ctx);
                await _BookingRepository.DeleteBookingAsync(1);
                booking = await _BookingRepository.GetBookingAsync(1);
            }

            //Assert
            Assert.IsNull(booking);
        }
       
    }
}
