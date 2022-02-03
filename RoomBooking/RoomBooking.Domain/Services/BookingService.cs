using BookingBooking.Domain.Interfaces.Dal;
using RoomBooking.Domain.Interfaces.Dal;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }



        public async Task<bool> DeleteBookingAsync(int id)
        {
            return await _bookingRepository.DeleteBookingAsync(id);
        }

        public async Task<Booking?> GetBookingAsync(int id)
        {
            return await _bookingRepository.GetBookingAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync() =>
            await _bookingRepository.GetBookingsAsync();


        public async Task<IEnumerable<Slot>?> BookRoom(Booking booking)
        {
            IEnumerable<Booking> allBookings = await _bookingRepository.GetBookingsAsync();
            IEnumerable<Room> allRooms = await _roomRepository.GetRoomsAsync();
            IEnumerable<User> allUsers = await _userRepository.GetUsersAsync();

            if (!UserExists(allUsers.ToList(), booking.User) || !RoomExists(allRooms.ToList(), booking.Room))
            {
                return null;
            }
            else
            {
                if(allBookings.Where(i=>i.Date.Date==booking.Date.Date).Any())
                {
                    if(IsOverlap(allBookings.Where(x=>x.Date.Date==booking.Date.Date).ToList(), booking))
                    {
                        return GetAllFreeSlots(allBookings.Where(x => x.Date.Date == booking.Date.Date).ToList());
                    }
                    else
                    {
                        await _bookingRepository.InsertBookingAsync(booking);
                        return new List<Slot>();
                    }
                }
                else
                {
                    await _bookingRepository.InsertBookingAsync(booking);
                    return new List<Slot>();
                }
            }
        }

        public async Task<bool> PutBookingAsync(Booking booking)
        {
            return await _bookingRepository.PutBookingAsync(booking);
        }

        private static bool IsOverlap(List<Booking> bookings, Booking booking)
        {
            bookings=bookings.OrderBy(x=>x.StartSlot).ToList();
            foreach (Booking b in bookings)
            {
                if((booking.StartSlot<b.EndSlot && booking.StartSlot>b.StartSlot) || (booking.EndSlot<=b.EndSlot && booking.EndSlot>b.StartSlot))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool UserExists(List<User> users, User user)
        {
            return users.Exists(x => x.Id == user.Id && x.FirstName==user.FirstName && x.LastName==user.LastName);
        }

        private static bool RoomExists(List<Room> rooms, Room room)
        {
            return rooms.Exists(x => x.Id == room.Id && x.Name == room.Name);
        }

        private static List<Slot> GetAllFreeSlots(List<Booking> bookings)
        {
            bookings = bookings.OrderBy(x => x.StartSlot).ToList();
            List<Slot> freeSlots = new List<Slot>();
            var start = 1;
            for (var b=0; b<bookings.Count(); b++)
            {
                if(start<bookings[b].StartSlot)
                {
                    freeSlots.Add(new Slot { Start=start, End=bookings[b].StartSlot, Date= bookings[b].Date});
                    start=bookings[b].EndSlot;
                }

                if(b== bookings.Count()-1 && bookings[b].EndSlot<24)
                {
                    freeSlots.Add(new Slot { Start = bookings[b].EndSlot, End = 24, Date = bookings[b].Date });
                }
            }
            return freeSlots;
        }
    }
}
