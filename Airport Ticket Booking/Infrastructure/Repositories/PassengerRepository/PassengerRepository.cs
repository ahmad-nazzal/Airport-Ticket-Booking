using Airport_Ticket_Booking.Models.Passenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Infrastructure.Repositories.PassengerRepository
{
    public class PassengerRepository: IPassengerRepository
    {
        private readonly List<Passenger> _passengers;
        private readonly CsvHandler<Passenger> _csvHandler;
        public PassengerRepository(string filePath)
        {
            _csvHandler = new CsvHandler<Passenger>(filePath);
            _passengers = _csvHandler.ReadFromCsv();
        }
        public void AddPassenger(Passenger passenger)
        {
            _passengers.Add(passenger);
            _csvHandler.WriteToCsv(_passengers);
        }
        public void DeletePassenger(Guid passengerId)
        {
            var passenger = _passengers.FirstOrDefault(passenger => passenger.Id == passengerId);
            if (passenger != null)
            {
                _passengers.Remove(passenger);
                _csvHandler.WriteToCsv(_passengers);
            }
        }
        public Passenger GetPassengerById(Guid passengerId)
        {
            return _passengers.FirstOrDefault(passenger => passenger.Id == passengerId);
        }
        public List<Passenger> GetAllPassengers()
        {
            return _passengers;
        }
        public void UpdatePassenger(Passenger passenger)
        {
            var existingPassenger = _passengers.FirstOrDefault(p => p.Id == passenger.Id);
            if (existingPassenger != null)
            {
                existingPassenger = passenger;
                _csvHandler.WriteToCsv(_passengers);
            }
        }
    }
}
