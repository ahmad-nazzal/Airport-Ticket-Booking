using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Infrastructure
{
    public interface IDataHandler<T>
    {
        public void SaveData(List<T> data);
        public List<T> LoadData();
    }
}
