namespace RoomBooking.Api.Dtos.Responses
{
    public class GetUsersResponse
    {
        public IEnumerable<UserDto> Users { get; set; } = new List<UserDto>();
    }
}
