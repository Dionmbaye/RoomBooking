using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using RoomBooking.Api;
using RoomBooking.Api.Controllers;
using RoomBooking.Api.Dtos;
using RoomBooking.Api.Dtos.Responses;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Test.Api
{
    [TestClass]
    public class BookingControllerTests
    {
        private readonly BookingController _bookingController;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingControllerTests()
        {
            _bookingService = Substitute.For<IBookingService>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingConfig());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _bookingController = new BookingController(_bookingService, _mapper);
        }

        [TestMethod]
        public async Task Should_Get_Bookings()
        {
            ConfigureService();

            var room = new Room { Id = 1, Name = "Test" };
            var user = new User { FirstName = "Test1", LastName = "Test2", Id = 1 };
            _bookingService.GetBookingsAsync().Returns(new List<Booking>
            {
                new Booking
                {
                    Id = 1,
                    Date = DateTime.Now,
                    StartSlot = 6,
                    EndSlot = 10,
                    Room = room,
                    User = user,
                }
             });

            var bookings = await _bookingController.GetBookingsAsync();

            Assert.IsNotNull(bookings);
        }

        [TestMethod]
        public async Task Should_Get_Booking_Where_Id_Equals_1()
        {
            ConfigureService();

            var room = new Room { Id = 1, Name = "Test" };
            var user = new User { FirstName = "Test1", LastName = "Test2", Id = 1 };

            _bookingService.GetBookingAsync(1).Returns(
                new Booking
                {
                    Id = 1,
                    Date = DateTime.Now,
                    StartSlot = 6,
                    EndSlot = 10,
                    Room = room,
                    User = user,
                }
            );

            var booking = await _bookingController.GetBookingAsync(1);

            Assert.IsNotNull(booking);
        }

        [TestMethod]
        public async Task Should_Book_For_Room_1()
        {
            var room = new Room { Id = 1, Name = "Test" };
            var user = new User { FirstName = "Test1", LastName = "Test2", Id = 1 };

            var book = new BookingDto
            {
                Id = 1,
                Date = DateTime.Now,
                StartSlot = 6,
                EndSlot = 10,
                Room = room,
                User = user,
            };
            
            var response = await _bookingController.PostBooking(book);
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NoContentResult));

        }

        [TestMethod]
        public async Task Should_Return_BadRequest_When_Book_For_Room_1()
        {
            var book = new BookingDto
            {
                Id = 1,
                Date = DateTime.Now,
                StartSlot = 6,
                EndSlot = 10
            };

            _bookingService.BookRoom(new Booking { 
                Id = book.Id, 
                Date = book.Date, 
                StartSlot = book.StartSlot, 
                EndSlot = book.EndSlot }).ReturnsNullForAnyArgs();
            var response = await _bookingController.PostBooking(book);
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

        }

        [TestMethod]
        public async Task Should_Return_Slots_When_Book_For_Room_1()
        {
            //Arrange
            var book = new BookingDto
            {
                Id = 1,
                Date = DateTime.Now,
                StartSlot = 6,
                EndSlot = 20
            };

            _bookingService.BookRoom(new Booking { Id = book.Id, 
                Date = book.Date, 
                StartSlot = book.StartSlot, 
                EndSlot = book.EndSlot })
                .ReturnsForAnyArgs(new List<Slot> { new Slot { Date = DateTime.Now, End = 6, Start = 1 },
                    new Slot{Date=DateTime.Now, Start=20, End=24 } });

            //Act
            var response = await _bookingController.PostBooking(book);
            var x = ((ConflictObjectResult)response).Value;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(ConflictObjectResult));
            Assert.AreEqual(2, ((InsertBookingResponse)x).Slots.Count());

        }



        private static void ConfigureService()
        {
            var sc = new ServiceCollection();
            sc.AddScoped((_) => Substitute.For<IDateTimeService>());
            var serviceProvider = sc.BuildServiceProvider();
            ServiceLocator.SetLocatorProvider(serviceProvider);
        }
    }
}
