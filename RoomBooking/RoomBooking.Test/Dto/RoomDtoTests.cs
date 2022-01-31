using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomBooking.Api.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Test.Dto
{
    [TestClass]
    public class RoomDtoTests
    {

        [TestMethod]
        public void Should_Accept_Valid_Room()
        {
            RoomDto room = new RoomDto { Name = "Test", Id = 1 };

            var validationContext = new ValidationContext(room);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(room, validationContext, errors, true);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(errors.Count, 0);

        }
        [TestMethod]
        public void Should_Return_Name_Field_Validation_Error()
        {
            RoomDto room = new RoomDto { Name = "", Id = 1 };

            var validationContext = new ValidationContext(room);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(room, validationContext, errors, true);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors.Single().MemberNames.Count(), 1);
            Assert.AreEqual(errors.Single().MemberNames.Single(), nameof(RoomDto.Name));

        }

        [TestMethod]
        public void Should_Return_Length_Name_Field_Validation_Error()
        {
            RoomDto room = new RoomDto { Name = "UIUIUJUUIUIUIUIUIUIUUUIUDIUIUIUIUIUIUIUUIIUIUIUIUIUIUIUIUIUIUUIUIUIIUUUIUIUIUIUIUIUIUUUIUUUIUIUIUUIUUIUUIUIIUUIUIUIU", Id = 1 };

            var validationContext = new ValidationContext(room);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(room, validationContext, errors, true);


            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors.Single().MemberNames.Count(), 1);
            Assert.AreEqual(errors.Single().MemberNames.Single(), nameof(RoomDto.Name));

        }
    }
}
