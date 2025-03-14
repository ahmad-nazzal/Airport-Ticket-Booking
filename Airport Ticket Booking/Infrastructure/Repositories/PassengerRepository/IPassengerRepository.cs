using Airport_Ticket_Booking.Models.Passenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Infrastructure.Repositories.PassengerRepository
{
    public interface IPassengerRepository
    {
        public void AddPassenger(Passenger passenger);
        public void DeletePassenger(Guid passengerId);
        public Passenger GetPassengerById(Guid passengerId);
        public List<Passenger> GetAllPassengers();
        public void UpdatePassenger(Passenger passenger);
    }
}
