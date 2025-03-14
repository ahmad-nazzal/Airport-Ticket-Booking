using Airport_Ticket_Booking.Core.Models.Cabin;
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
        private readonly CsvHandler<Cabin> _csvHandler;
        public CabinRebository(string filePath)
        {
            _csvHandler = new CsvHandler<Cabin>(filePath);
            _cabins = _csvHandler.ReadFromCsv();
        }
        public void AddCabin(Cabin cabin)
        {
            _cabins.Add(cabin);
            _csvHandler.WriteToCsv(_cabins);
        }
        public void DeleteCabin(Guid cabinId)
        {
            var cabin = _cabins.FirstOrDefault(cabin => cabin.Id == cabinId);
            if (cabin != null)
            {
                _cabins.Remove(cabin);
                _csvHandler.WriteToCsv(_cabins);
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
                _csvHandler.WriteToCsv(_cabins);
            }
        }
    }
}
