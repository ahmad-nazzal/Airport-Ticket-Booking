using Airport_Ticket_Booking.Models.Cabin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Services.CabinService
{
    public interface ICabinService
    {
        public Cabin GetCabinById(Guid cabinId);
        public List<Cabin> GetAllCabins();
        public void AddCabin(Cabin cabin);
        public void UpdateCabin(Cabin cabin);
        public void DeleteCabin(Guid cabinId);
        public List<Cabin> GetCabinsByFlightId(Guid flightId);
    }
}
