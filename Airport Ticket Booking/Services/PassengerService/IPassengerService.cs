using Airport_Ticket_Booking.Models.Passenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Services.PassengerService
{
    public interface IPassengerService
    {
        public Passenger GetPassengerById(Guid passengerId);
        public List<Passenger> GetAllPassengers();
        public void Refund(Guid passengerId, decimal amount);
        public void DeductAccountBalance(Guid passengerId, decimal amount);
    }
}
