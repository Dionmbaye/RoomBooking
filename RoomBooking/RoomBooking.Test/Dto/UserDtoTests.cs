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
    public class UserDtoTests
    {
        [TestMethod]
        public void Should_Accept_Valid_User()
        {
            UserDto user = GetTestUser();

            var validationContext = new ValidationContext(user);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(user, validationContext, errors, true);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(errors.Count, 0);

        }

        [TestMethod]
        public void Should_Return_FirstName_Field_Validation_Error()
        {
            UserDto user = GetTestUser();
            user.FirstName = "";
            var validationContext = new ValidationContext(user);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(user, validationContext, errors, true);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors.Single().MemberNames.Count(), 1);
            Assert.AreEqual(errors.Single().MemberNames.Single(), nameof(UserDto.FirstName));

        }

        [TestMethod]
        public void Should_Return_Length_FirstName_Field_Validation_Error()
        {
            UserDto user = GetTestUser();
            user.FirstName = "UIUIUJUUIUIUIUIUIUIUUUIUDIUIUIUIUIUIUIUUIIUIUIUIUIUIUIUIUIUIUUIUIUIIUUUIUIUIUIUIUIUIUUUIUUUIUIUIUUIUUIUUIUIIUUIUIUIU";
           
            var validationContext = new ValidationContext(user);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(user, validationContext, errors, true);


            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors.Single().MemberNames.Count(), 1);
            Assert.AreEqual(errors.Single().MemberNames.Single(), nameof(UserDto.FirstName));

        }

        [TestMethod]
        public void Should_Return_LastName_Field_Validation_Error()
        {
            UserDto user = GetTestUser();
            user.LastName = "";
            var validationContext = new ValidationContext(user);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(user, validationContext, errors, true);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors.Single().MemberNames.Count(), 1);
            Assert.AreEqual(errors.Single().MemberNames.Single(), nameof(UserDto.LastName));

        }

        [TestMethod]
        public void Should_Return_Length_LastName_Field_Validation_Error()
        {
            UserDto user = GetTestUser();
            user.LastName = "UIUIUJUUIUIUIUIUIUIUUUIUDIUIUIUIUIUIUIUUIIUIUIUIUIUIUIUIUIUIUUIUIUIIUUUIUIUIUIUIUIUIUUUIUUUIUIUIUUIUUIUUIUIIUUIUIUIU";

            var validationContext = new ValidationContext(user);
            var errors = new List<ValidationResult>();

            var result = Validator.TryValidateObject(user, validationContext, errors, true);


            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors.Single().MemberNames.Count(), 1);
            Assert.AreEqual(errors.Single().MemberNames.Single(), nameof(UserDto.LastName));

        }


        private static UserDto GetTestUser()
        {
            var UserDto = new UserDto
            {
                Id = 1,
                FirstName = "Test1",
                LastName = "Test2"
            };
            return UserDto;
        }

    }
}
