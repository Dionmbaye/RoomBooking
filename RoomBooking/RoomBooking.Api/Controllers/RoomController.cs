using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Api.Dtos;
using RoomBooking.Api.Dtos.Responses;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RoomBooking.Api.Controllers
{
    [ApiController]
    [Route("Rooms")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        //Get all rooms
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetRoomsResponse))]
        public async Task<IActionResult> GetRoomsAsync()
        {
            var rooms = await _roomService.GetRoomsAsync();
            if (rooms == null)
            {
                return NotFound();
            }
            var response = new GetRoomsResponse
            {
                Rooms = _mapper.Map<List<RoomDto>>(rooms)
            };
            return rooms.Count() > 0 ? Ok(response) : NoContent();
        }

        //Get Room by Id
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetRoomByIdResponse))]
        public async Task<IActionResult> GetRoomAsync([FromRoute] int id)
        {
            var room = await _roomService.GetRoomAsync(id);
            var response = new GetRoomByIdResponse();
            
            if (room == null)
            {
                return NotFound();
            }
            else
            {
                response.Room = _mapper.Map<RoomDto>(room);
                return Ok(response);
            }

        }

        //Delete Room whith Id
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteRoomAsync([FromRoute] int id)
        {
            if (await _roomService.DeleteRoomAsync(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        //Update Room with Id and Room
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutRoom([FromRoute] int id, [FromBody] RoomDto room)
        {

            if (ModelState.IsValid)
            {
                if (id != room.Id)
                {
                    BadRequest();
                }
                try
                {
                    var roomModel = _mapper.Map<Room>(room);
                    var response = await _roomService.PutRoomAsync(roomModel);
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

        //Post Room
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostRoom([FromBody] RoomDto room)
        {
            if (ModelState.IsValid)
            {
                var roomModel = _mapper.Map<Room>(room);
                var response = await _roomService.InsertRoomAsync(roomModel);
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
