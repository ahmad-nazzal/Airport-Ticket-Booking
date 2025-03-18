using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Models.Booking
{
    public class Booking
    {
        public required Guid Id { get; init; } = Guid.NewGuid();
        public required Guid FlightId { get; set; }
        public required Guid PassengerId { get; init; }
        public required Guid CabinId { get; set; }
        public DateTime BookingDate { get; init; } = DateTime.UtcNow;
        public bool IsCancelled { get; set; } = false;
        public decimal TotalPrice { get; set; } = 0;

        public override string ToString()
        {
            return $"Booking ID: {Id}, Flight ID: {FlightId}, Passenger ID: {PassengerId}, Cabin ID: {CabinId}, Booking Date: {BookingDate}, Is Cancelled: {IsCancelled}, Total Price: {TotalPrice}";
        }
    }
}
