using Airport_Ticket_Booking.Models.Cabin;
using Airport_Ticket_Booking.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Models.Flight
{
    public class Flight
    {
        public required Guid Id { get; init; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public required string FlightName { get; init; }

        [Required, StringLength(100)]
        public required string DepartureCountry { get; init; }

        [Required, StringLength(100)]
        public required string DestinationCountry { get; init; }

        [Required, StringLength(100)]
        public required string DepartureAirport { get; init; }

        [Required, StringLength(100)]
        public required string ArrivalAirport { get; init; }

        [Required]
        [DataType(DataType.DateTime)]
        [FlightDateValidation(ErrorMessage = "Departure date must be today or in the future.")]
        public required DateTime DepartureDate { get; init; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime ArrivalDate { get; init; }

        public override string ToString()
        {
            return $"Flight Name: {FlightName}, Departure Country: {DepartureCountry}, Destination Country: {DestinationCountry}, Departure Airport: {DepartureAirport}, Arrival Airport: {ArrivalAirport}, Departure Date: {DepartureDate}, Arrival Date: {ArrivalDate}";
        }

    }
}
