using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Api.Dtos.Responses;
using RoomBooking.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using RoomBooking.Api.Dtos;
using RoomBooking.Domain.Models;

namespace RoomBooking.Api.Controllers
{
    [ApiController]
    [Route("Bookings")]
    public class BookingController : Controller
    {

        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        //Get all rooms
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetBookingsResponse))]
        public async Task<IActionResult> GetBookingsAsync()
        {
            var bookings = await _bookingService.GetBookingsAsync();
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

        //Get Booking by Id
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetBookingByIdResponse))]
        public async Task<IActionResult> GetBookingAsync([FromRoute] int id)
        {
            var booking = await _bookingService.GetBookingAsync(id);
            var response = new GetBookingByIdResponse();
            
            if (booking == null)
            {
                return NotFound();
            }
            else
            {
                response.Booking = _mapper.Map<BookingDto>(booking);
                return Ok(response);
            }
        }

        //Delete Booking whith Id
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteBookingAsync([FromRoute] int id)
        {
            if (await _bookingService.DeleteBookingAsync(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        //Post Booking
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(InsertBookingResponse))]
        public async Task<IActionResult> PostBooking([FromBody] BookingDto booking)
        {
            if (ModelState.IsValid)
            {
                var bookingModel = _mapper.Map<Booking>(booking);
                var slots = await _bookingService.BookRoom(bookingModel);
                
                if (slots == null)
                {
                    return BadRequest();
                }
                else
                {
                    if (slots.Count() == 0)
                    {
                        return NoContent();
                    }
                    else
                    {
                        var response = new InsertBookingResponse
                    {
                        Slots = _mapper.Map<List<SlotDto>>(slots)
                    };

                        return Conflict(response);
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
