using Airport_Ticket_Booking.Models.Cabin;
using Airport_Ticket_Booking.Models.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Infrastructure.Repositories.CabinRepository
{
    public class CabinRebository : ICabinRepository
    {
        private readonly List<Cabin> _cabins;
        private readonly IDataHandler<Cabin> _dataHandler;
        public CabinRebository(string filePath, IDataHandler<Cabin> dataHandler)
        {
            _dataHandler = dataHandler;
            _cabins = _dataHandler.LoadData();
        }
        public void AddCabin(Cabin cabin)
        {
            _cabins.Add(cabin);
            _dataHandler.SaveData(_cabins);
        }
        public void DeleteCabin(Guid cabinId)
        {
            var cabin = _cabins.FirstOrDefault(cabin => cabin.Id == cabinId);
            if (cabin != null)
            {
                _cabins.Remove(cabin);
                _dataHandler.SaveData(_cabins);
            }
        }
        public Cabin GetCabinById(Guid cabinId)
        {
            return _cabins.FirstOrDefault(cabin => cabin.Id == cabinId);
        }
        public List<Cabin> GetAllCabins()
        {
            return _cabins;
        }
        public void UpdateCabin(Cabin cabin)
        {
            var existingCabin = _cabins.FirstOrDefault(c => c.Id == cabin.Id);
            if (existingCabin != null)
            {
                existingCabin = cabin;
                _dataHandler.SaveData(_cabins);
            }
        }
    }
}
