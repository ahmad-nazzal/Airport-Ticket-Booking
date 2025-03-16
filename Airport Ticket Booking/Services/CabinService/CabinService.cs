using Airport_Ticket_Booking.Infrastructure.Repositories.CabinRepository;
using Airport_Ticket_Booking.Models.Cabin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Services.CabinService
{
    public class CabinService: ICabinService
    {
        private readonly ICabinRepository _cabinRepository;
        public CabinService(ICabinRepository cabinRepository)
        {
            _cabinRepository = cabinRepository;
        }
        public void AddCabin(Cabin cabin)
        {
            _cabinRepository.AddCabin(cabin);
        }
        public void DeleteCabin(Guid cabinId)
        {
            _cabinRepository.DeleteCabin(cabinId);
        }
        public Cabin GetCabinById(Guid cabinId)
        {
            var cabin = _cabinRepository.GetCabinById(cabinId);
            if (cabin == null)
            {
                Console.WriteLine("Cabin Not Found");
            }
            return cabin;
        }
        public List<Cabin> GetAllCabins()
        {
            return _cabinRepository.GetAllCabins();
        }
        public void UpdateCabin(Cabin cabin)
        {
            _cabinRepository.UpdateCabin(cabin);
        }
        public List<Cabin> GetCabinsByFlightId(Guid flightId)
        {
            return _cabinRepository.GetAllCabins().Where(cabin => cabin.FlightId == flightId).ToList();
        }
    }
}
