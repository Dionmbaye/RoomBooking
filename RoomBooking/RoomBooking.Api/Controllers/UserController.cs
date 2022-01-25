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

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetUserByIdResponse))]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var user = await _userService.GetUserAsync(id);
            var response = new GetUserByIdResponse();
            response.User = _mapper.Map<UserDto>(user);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }

        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteUserAsync(int id)
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

        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutUser(int id, UserDto user)
        {


            if (id != user.Id)
            {
                BadRequest();
            }
            try
            {
                var userModel = _mapper.Map<User>(user);
                var response =await _userService.PutUserAsync(userModel);
                return Ok();
            }
            catch
            {
                return NotFound();
            }

        }

    }
}
