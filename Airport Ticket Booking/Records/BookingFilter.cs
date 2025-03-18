using Airport_Ticket_Booking.Models.Cabin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Records
{
    public record BookingFilter
    {
        public Guid? FlightId = null;
        public decimal? MinPrice = null;
        public decimal? MaxPrice = null;
        public string? DepartureCountry = null;
        public string? DestinationCountry = null;
        public DateTime? DepartureDate = null;
        public string? DepartureAirport = null;
        public string? ArrivalAirport = null;
        public CabinClass? CabinClass = null;
        public Guid? PassengerId = null;
    }
}