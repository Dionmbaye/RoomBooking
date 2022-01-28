using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RoomBooking.Api;
using RoomBooking.Api.Controllers;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task Should_Get_Rooms()
        {

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

    }
}
