using Airport_Ticket_Booking.Core.Models.Passenger;
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
        public void DeletePassenger(int passengerId);
        public Passenger GetPassengerById(int passengerId);
        public List<Passenger> GetAllPassengers();
        public void UpdatePassenger(Passenger passenger);
    }
}
