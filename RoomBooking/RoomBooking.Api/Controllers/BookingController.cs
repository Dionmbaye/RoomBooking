﻿using AutoMapper;
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
        private readonly IDateTimeService _dateTimeService;

        public BookingController(IBookingService bookingService, IMapper mapper, IDateTimeService dateTimeService)
        {
            _bookingService = bookingService;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
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
            response.Booking = _mapper.Map<BookingDto>(booking);
            if (booking == null)
            {
                return NotFound();
            }
            else
            {
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
    }
}
