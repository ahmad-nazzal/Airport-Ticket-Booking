using Airport_Ticket_Booking.Models.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Infrastructure.Repositories.BookingRepository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CsvHandler<Booking> _csvHandler;
        private readonly List<Booking> _bookings;
        public BookingRepository(string filePath)
        {
            _csvHandler = new CsvHandler<Booking>(filePath);
            _bookings = _csvHandler.ReadFromCsv();
        }
        public void AddBooking(Booking booking)
        {
            _bookings.Add(booking);
            _csvHandler.WriteToCsv(_bookings);
        }
        public void DeleteBooking(Guid bookingId)
        {
            var booking = _bookings.FirstOrDefault(booking => booking.Id == bookingId);
            if (booking != null)
            {
                _bookings.Remove(booking);
                _csvHandler.WriteToCsv(_bookings);
            }
        }
        public Booking GetBookingById(Guid bookingId)
        {
            return _bookings.FirstOrDefault(booking => booking.Id == bookingId);
        }
        public List<Booking> GetAllBookings()
        {
            return _bookings;
        }
        public void UpdateBooking(Booking booking)
        {
            var existingBooking = _bookings.FirstOrDefault(b => b.Id == booking.Id);
            if (existingBooking != null)
            {
                existingBooking = booking;
                _csvHandler.WriteToCsv(_bookings);
            }
        }



    }
}
