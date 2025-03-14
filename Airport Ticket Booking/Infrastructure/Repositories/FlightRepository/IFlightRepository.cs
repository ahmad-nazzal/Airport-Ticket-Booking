using Airport_Ticket_Booking.Core.Models.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Infrastructure.Repositories.FlightRepository
{
    public interface IFlightRepository
    {
        public void AddFlight(Flight flight);
        public void DeleteFlight(Guid flightId);
        public Flight GetFlightById(Guid flightId);
        public List<Flight> GetAllFlights();
        public void UpdateFlight(Flight flight);

    }
}
