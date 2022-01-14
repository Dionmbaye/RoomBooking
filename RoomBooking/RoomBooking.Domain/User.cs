namespace RoomBooking.Domain
{
    public class User:IUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Id { get; set; }
    }
}