using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RoomBooking.Api.Dtos;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Test.Dto
{
    [TestClass]
    public class BookingDtoTests
    {

        [TestMethod]
        public void Should_Accept_Valid_Booking()
        {
            BookingDto booking = GetTestBooking();

            var validationContext = new ValidationContext(booking);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(booking, validationContext, errors, true);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(errors.Count, 0);
        }

        [TestMethod]
        public void Should_Return_Invalid_StartSlot_Error()
        {
            BookingDto booking = GetTestBooking();
            booking.StartSlot = booking.StartSlot - 2;

            var validationContext = new ValidationContext(booking);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(booking, validationContext, errors, true);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors.Single().MemberNames.Count(), 1);
            Assert.AreEqual(errors.Single().MemberNames.Single(), nameof(BookingDto.StartSlot));

        }

        [TestMethod]
        public void Should_Return_Invalid_Date_Error()
        {
            BookingDto booking = GetTestBooking();
            booking.Date = new DateTime(2012, 12, 25, 10, 30, 50);

            var validationContext = new ValidationContext(booking);
            var errors = new List<ValidationResult>();
            var result = Validator.TryValidateObject(booking, validationContext, errors, true);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors.Single().MemberNames.Count(), 1);
            Assert.AreEqual(errors.Single().MemberNames.Single(), nameof(BookingDto.Date));

        }

        [TestMethod]
        public void Should_Return_Invalid_StartSlot_GreaterThan_End_Error()
        {
            BookingDto booking = GetTestBooking();
            booking.StartSlot = booking.EndSlot - 5;

            var validationContext = new ValidationContext(booking);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(booking, validationContext, errors, true);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors.Single().MemberNames.Count(), 1);
            Assert.AreEqual(errors.Single().MemberNames.Single(), nameof(BookingDto.StartSlot));

        }

        private static BookingDto GetTestBooking()
        {
            var room = new Room { Id = 1, Name = "Test" };
            var user = new User { FirstName = "Test1", LastName = "Test2", Id = 1 };
            var _dateTimeService = Substitute.For<IDateTimeService>();
            _dateTimeService.GetDateTimeNow().Returns(new DateTime(2021, 12, 25, 10, 30, 50));
            _dateTimeService.GetHourNow().Returns(10);
            var BookingDto = new BookingDto(_dateTimeService)
            {
                Id = 1,
                Date = new DateTime(2021, 12, 25, 10, 30, 50),
                StartSlot = 11,
                EndSlot = 14,
                Room = room,
                User = user
            };
            return BookingDto;
        }

    }
}
