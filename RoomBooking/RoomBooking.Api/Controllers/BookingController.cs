using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Api.Dtos.Responses;
using RoomBooking.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using RoomBooking.Api.Dtos;

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
    }
}
