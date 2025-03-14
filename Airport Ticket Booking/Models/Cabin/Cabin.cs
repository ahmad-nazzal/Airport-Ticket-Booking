using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Models.Cabin
{
    public class Cabin
    {
        public required Guid Id { get; init; } = Guid.NewGuid();
        public required CabinClass CabinName { get; init; }
        public required decimal Price { get; set; }
        public required int TotalSeats { get; init; }
        public int AvailableSeats { get; private set; }
        public required Guid FlightId { get; init; }
    }
}
