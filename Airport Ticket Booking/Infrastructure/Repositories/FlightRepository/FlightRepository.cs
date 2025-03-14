using Airport_Ticket_Booking.Core.Models.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Infrastructure.Repositories.FlightRepository
{
    public class FlightRepository: IFlightRepository
    {
        private readonly List<Flight> _flights;
        private readonly CsvHandler<Flight> _csvHandler;
        public FlightRepository(string filePath)
        {
            _csvHandler = new CsvHandler<Flight>(filePath);
            _flights = _csvHandler.ReadFromCsv();
        }
        public void AddFlight(Flight flight)
        {
            _flights.Add(flight);
            _csvHandler.WriteToCsv(_flights);
        }
        public void DeleteFlight(Guid flightId)
        {
            var flight = _flights.FirstOrDefault(flight => flight.Id == flightId);
            if (flight != null)
            {
                _flights.Remove(flight);
                _csvHandler.WriteToCsv(_flights);
            }
        }
        public Flight GetFlightById(Guid flightId)
        {
            return _flights.FirstOrDefault(flight => flight.Id == flightId);
        }
        public List<Flight> GetAllFlights()
        {
            return _flights;
        }
        public void UpdateFlight(Flight flight)
        {
            var existingFlight = _flights.FirstOrDefault(f => f.Id == flight.Id);
            if (existingFlight != null)
            {
                existingFlight = flight;
                _csvHandler.WriteToCsv(_flights);
            }
        }
    }
}
