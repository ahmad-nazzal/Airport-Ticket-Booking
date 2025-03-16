using Airport_Ticket_Booking.Infrastructure.Repositories.PassengerRepository;
using Airport_Ticket_Booking.Models.Passenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Services.PassengerService
{
    public class PassengerService: IPassengerService
    {
        private readonly IPassengerRepository _passengerRepository;
        public PassengerService(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }
        public Passenger GetPassengerById(Guid passengerId)
        {
            var passenger = _passengerRepository.GetPassengerById(passengerId);
            if (passenger == null)
            {
                Console.WriteLine("Passenger Not Found");
            }
            return passenger;
        }
        public List<Passenger> GetAllPassengers()
        {
            return _passengerRepository.GetAllPassengers();
        }
    }
}
