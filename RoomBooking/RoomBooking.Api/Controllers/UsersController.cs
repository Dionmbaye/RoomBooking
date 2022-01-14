using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Domain;
using RoomBooking.Dal;

namespace RoomBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
       
        [HttpGet]
        public IEnumerable<User> Get()
        {
            Users us= new Users();
            return us.GetAll().ToArray();

        }
    }
}
