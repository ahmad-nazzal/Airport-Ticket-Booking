using Airport_Ticket_Booking.Models.Booking;
using Airport_Ticket_Booking.Models.Flight;
using Airport_Ticket_Booking.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Services.FlightService
{
    public interface IFlightService
    {
        public void DeleteFlight(Guid flightId);
        public Flight GetFlightById(Guid flightId);
        public List<Flight> GetAllFlights();
        public void AddFlight(Flight flight);
        public List<Flight> SearchFlights(FlightFilter filter);
        public void ImportFlightsFromCsv(string filePath);
        public List<string> ValidateFlightData(Flight flight);
    }
}
