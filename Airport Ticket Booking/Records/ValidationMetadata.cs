using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Records
{
    public record ValidationMetadata
    {
        public required string PropertyName { get; init; } 
        public required string DataType { get; init; }
        public List<string> Constraints { get; init; } = new();
    }
}
