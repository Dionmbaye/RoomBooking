using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Api.Dtos;
using RoomBooking.Api.Dtos.Responses;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace RoomBooking.Api.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        //Get all users
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetUsersResponse))]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();
            if (users == null)
            {
                return NotFound();
            }
            var response = new GetUsersResponse
            {
                Users = _mapper.Map<List<UserDto>>(users)
            };
            return users.Count() > 0 ? Ok(response) : NoContent();
        }

        //Get all bookings for user
        [HttpGet]
        [Route("{id}/Bookings")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetBookingsResponse))]
        public async Task<IActionResult> GetUserBookingsAsync([FromRoute] int id)
        {
            
            var bookings = await _userService.GetUserBookingsAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            var response = new GetBookingsResponse
            {
                Bookings = _mapper.Map<List<BookingDto>>(bookings)
            };
            return bookings.Count() > 0 ? Ok(response) : NoContent();
        }

        //Get User by Id
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetUserByIdResponse))]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var user = await _userService.GetUserAsync(id);
            var response = new GetUserByIdResponse();
            
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                response.User = _mapper.Map<UserDto>(user);
                return Ok(response);
            }

        }

        //Delete User whith Id
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int id)
        {
            if (await _userService.DeleteUserAsync(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        //Update User with Id and User
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutUser([FromRoute]int id, [FromBody]UserDto user)
        {

            if (ModelState.IsValid)
            {
                if (id != user.Id)
                {
                    BadRequest();
                }
                try
                {
                    var userModel = _mapper.Map<User>(user);
                    var response = await _userService.PutUserAsync(userModel);
                    return Ok();
                }
                catch
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //Post User
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostUser([FromBody]UserDto user)
        {
            if (ModelState.IsValid)
            {
                var userModel = _mapper.Map<User>(user);
                var response = await _userService.InsertUserAsync(userModel);
                if (response)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

    }
}
