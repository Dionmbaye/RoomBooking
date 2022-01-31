using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Api.Dtos
{
    public class BookingDtoStartSlotValidationAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Convert.ToInt32(value) >= DateTime.Now.Hour;
        }
    }
}
