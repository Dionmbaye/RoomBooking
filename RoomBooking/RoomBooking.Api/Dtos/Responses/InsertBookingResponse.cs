namespace RoomBooking.Api.Dtos.Responses
{
    public class InsertBookingResponse
    {
        public IEnumerable<SlotDto> Slots { get; set; } = new List<SlotDto>();
    }
}
