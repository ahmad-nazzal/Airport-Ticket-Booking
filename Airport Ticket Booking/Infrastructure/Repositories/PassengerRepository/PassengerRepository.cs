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
        private readonly IDataHandler<Passenger> _dataHandler;
        public PassengerRepository(string filePath, IDataHandler<Passenger> dataHandler)
        {
            _dataHandler = dataHandler;
            _passengers = _dataHandler.LoadData();
        }
        public void AddPassenger(Passenger passenger)
        {
            _passengers.Add(passenger);
            _dataHandler.SaveData(_passengers);
        }
        public void DeletePassenger(Guid passengerId)
        {
            var passenger = _passengers.FirstOrDefault(passenger => passenger.Id == passengerId);
            if (passenger != null)
            {
                _passengers.Remove(passenger);
                _dataHandler.SaveData(_passengers);
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
                _dataHandler.SaveData(_passengers);
            }
        }
    }
}
