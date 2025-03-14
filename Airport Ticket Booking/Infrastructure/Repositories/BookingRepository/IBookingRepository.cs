using Airport_Ticket_Booking.Core.Models.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Infrastructure.Repositories.BookingRepository
{
    public interface IBookingRepository
    {
        public void AddBooking(Booking booking);
        public void DeleteBooking(int bookingId);
        public Booking GetBookingById(int booking);
        public List<Booking> GetAllBookings();
        public void UpdateBooking(Booking booking);
    }
}
