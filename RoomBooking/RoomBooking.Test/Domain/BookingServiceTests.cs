using BookingBooking.Domain.Interfaces.Dal;
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
    public class BookingServiceTests
    {
        private readonly BookingService _bookingService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingServiceTests()
        {
            _bookingRepository = Substitute.For<IBookingRepository>();
            _roomRepository = Substitute.For<IRoomRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _bookingService = new BookingService(_bookingRepository, _roomRepository, _userRepository);
        }
        [TestMethod]
        public async Task Should_Get_Bookings()
        {
            _bookingRepository.GetBookingsAsync().Returns(new List<Booking>
            {
                new Booking
                {
                    Id = 1,
                    Date = DateTime.Now.Date,
                    EndSlot = 10,
                    StartSlot=6,
                    Room=new Room{Id=1, Name="Test"},
                    User=new User{Id=1, FirstName="Test", LastName="Test"},

                }
            });

            var rooms = await _bookingService.GetBookingsAsync();

            Assert.IsNotNull(rooms);
        }

        [TestMethod]
        public async Task Should_Get_Booking_Where_Id_Equals_1()
        {
            _bookingRepository.GetBookingAsync(1).Returns(
                new Booking
                {
                    Id = 1,
                    Date= DateTime.Now,
                    User=new User { Id=1, FirstName="Test", LastName="Test"},
                    Room=new Room { Id=1, Name= "Test"},
                    StartSlot=6,
                    EndSlot=10
                }
            );

            var booking = await _bookingService.GetBookingAsync(1);

            Assert.IsNotNull(booking);
        }

        [TestMethod]
        public async Task Should_Put_Booking()
        {
            var booking = new Booking
            {
                Id = 1,
                Date = DateTime.Now,
                User = new User { Id = 1, FirstName = "Test", LastName = "Test" },
                Room = new Room { Id = 1, Name = "Test" },
                StartSlot = 6,
                EndSlot = 10
            };

            _bookingRepository.PutBookingAsync(booking).Returns(true);

            var response = await _bookingService.PutBookingAsync(booking);

            Assert.AreEqual(true, response);
        }

        [TestMethod]
        public async Task Should_Book_For_Room()
        {
            var booking = new Booking
            {
                Id = 1,
                Date = DateTime.Now,
                User = new User { Id = 1, FirstName = "Test", LastName = "Test" },
                Room = new Room { Id = 1, Name = "Test" },
                StartSlot = 6,
                EndSlot = 10
            };

            _bookingRepository.InsertBookingAsync(booking).Returns(true);
            _roomRepository.GetRoomsAsync().Returns(new List<Room> { new Room { Id = 1, Name="Test" } });
            _userRepository.GetUsersAsync().Returns(new List<User> { new User { Id = 1, FirstName = "Test", LastName = "Test" }, });

            var response = await _bookingService.BookRoom(booking);

            Assert.IsNotNull(response);
        }
    }
}
