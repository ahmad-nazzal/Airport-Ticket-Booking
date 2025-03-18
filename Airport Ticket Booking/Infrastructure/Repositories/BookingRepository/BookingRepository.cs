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
        private readonly IDataHandler<Booking> _dataHandler;
        private readonly List<Booking> _bookings;
        public BookingRepository(IDataHandler<Booking> dataHandler)
        {
            _dataHandler = dataHandler;
            _bookings = _dataHandler.LoadData();
        }
        public void AddBooking(Booking booking)
        {
            _bookings.Add(booking);
            _dataHandler.SaveData(_bookings);
        }
        public void DeleteBooking(Guid bookingId)
        {
            var booking = _bookings.FirstOrDefault(booking => booking.Id == bookingId);
            if (booking != null)
            {
                _bookings.Remove(booking);
                _dataHandler.SaveData(_bookings);
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
                _dataHandler.SaveData(_bookings);
            }
        }



    }
}
