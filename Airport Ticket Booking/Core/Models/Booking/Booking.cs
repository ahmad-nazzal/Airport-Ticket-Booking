using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Core.Models.Booking
{
    public class Booking
    {
        public required Guid Id { get; init; } = Guid.NewGuid();
        public required Guid FlightId { get; init; }
        public required Guid PassengerId { get; init; }
        public required Guid CabinId { get; init; }
        public required DateTime BookingDate { get; init; } = DateTime.UtcNow;
        public bool IsCancelled { get; private set; } = false;
        public int TotalPrice { get; set; }



    }
}
