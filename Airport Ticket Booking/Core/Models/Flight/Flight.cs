using Airport_Ticket_Booking.Core.Models.Cabin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Core.Models.Flight
{
    public class Flight
    {
        public required Guid Id { get; init; } = Guid.NewGuid();
        public required string FlightName { get; init; }
        public required string DepartureCountry{ get; init; }
        public string? DestinationCountry{ get; init; }
        public string? DepartureAirport{ get; init; }
        public string? ArrivalAirport{ get; init; }
        public DateTime DepartureDate { get; init; }
        public DateTime ArrivalDate { get; init; }
        public List<Cabin.Cabin> Cabins { get; set; } = new();

    }
}
