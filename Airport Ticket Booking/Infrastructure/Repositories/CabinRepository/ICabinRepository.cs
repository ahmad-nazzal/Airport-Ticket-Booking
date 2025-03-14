using Airport_Ticket_Booking.Core.Models.Cabin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Infrastructure.Repositories.CabinRepository
{
    public interface ICabinRepository
    {
        public void AddCabin(Cabin cabin);
        public void DeleteCabin(Guid cabinId);
        public Cabin GetCabinById(Guid cabinId);
        public List<Cabin> GetAllCabins();
        public void UpdateCabin(Cabin cabin);
    }
}
