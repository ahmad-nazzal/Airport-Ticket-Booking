using Airport_Ticket_Booking.Core.Models.Cabin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Infrastructure.Repositories.CabinRepository
{
    ;public interface ICabinRebository
    {
        public void AddCabin(Cabin cabin);
        public void DeleteCabin(int cabinId);
        public Cabin GetCabinById(int cabinId);
        public List<Cabin> GetAllCabins();
        public void UpdateCabin(Cabin cabin);
    }
}
