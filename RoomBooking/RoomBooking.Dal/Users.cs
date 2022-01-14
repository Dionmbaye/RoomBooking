using RoomBooking.Domain;

namespace RoomBooking.Dal
{
    public class Users : IUsers
    {
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            users.Add(new User { FirstName = "Data", LastName = "Toto", Id = 1 });
            return users;
        }
    }
}