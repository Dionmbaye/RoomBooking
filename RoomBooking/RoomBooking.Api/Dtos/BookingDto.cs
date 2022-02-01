using FoolProof.Core;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Api.Dtos
{
    public class BookingDto: IValidatableObject
    {
        private readonly IDateTimeService _dateTimeService;
        public BookingDto(IDateTimeService dateTimeService)
        {
            this._dateTimeService = dateTimeService;
        }

        public BookingDto()
        {
        }
        public int Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        public int StartSlot { get; set; }
        [Required]
        [GreaterThan("StartSlot", ErrorMessage = "EndSlot must be greater than StartSlot")]
        public int EndSlot { get; set; }
        [Required]
        public Room Room { get; set; } = null!;
        [Required]
        public User User { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var datenow = _dateTimeService.GetDateTimeNow();
            var hournow = _dateTimeService.GetHourNow();
            if (Date<datenow)
            {
                yield return new ValidationResult("Date must be equals or greater than now", new List<string> { "Date" });
            }
            if(StartSlot<hournow)
            {
                yield return new ValidationResult("Date must be equals or greater than now", new List<string> { "StartSlot" });
            }
        }
    }
}
