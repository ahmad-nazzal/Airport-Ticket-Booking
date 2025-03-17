using Airport_Ticket_Booking.Models.Booking;
using Airport_Ticket_Booking.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Services.BookingService
{
    public interface IBookingService
    {
        public void BookFlight(Guid flightId, Guid passengerId, Guid cabinId);
        public void CancelBooking(Guid bookingId);
        public void ChangeFlight(Guid bookingId, Guid newFlightId, Guid newCabinId);
        public void ChangeCabin(Guid bookingId, Guid newCabinId);
        public List<Booking> GetPassengerBookings(Guid passengerId);
        public List<Booking> GetFlightBookings(Guid flightId);
        public List<Booking> GetAllBookings();
        public List<Booking> FilterBookings(BookingFilter filter);
    }
}
