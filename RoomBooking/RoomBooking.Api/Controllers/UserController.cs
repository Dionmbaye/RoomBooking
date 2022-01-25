using System.Net;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Api.Dtos;
using RoomBooking.Api.Dtos.Responses;
using RoomBooking.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RoomBooking.Api.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) =>
            _userService = userService;

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetUsersResponse))]
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                if (users == null)
                {
                    return NotFound();
                }
                var response = new GetUsersResponse
                {
                    Users = users.Select(u => new UserDto
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    })
                };
                return users.Count() > 0 ? Ok(response) : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetUserByIdResponse))]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            try
            {
                var user = await _userService.GetUserAsync(id);
                var response = new GetUserByIdResponse();
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    response.User.FirstName = user.FirstName;
                    response.User.LastName = user.LastName;
                    response.User.Id = user.Id;

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }

    }
}
