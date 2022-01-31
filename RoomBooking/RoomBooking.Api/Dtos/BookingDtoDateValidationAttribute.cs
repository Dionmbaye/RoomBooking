using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Api.Dtos
{
    public class BookingDtoDateValidationAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = Convert.ToDateTime(value);
            return date >= DateTime.Now.AddSeconds(-2);
        }
    }
}
