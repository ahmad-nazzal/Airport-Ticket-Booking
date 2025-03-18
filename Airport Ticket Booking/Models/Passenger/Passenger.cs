using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Airport_Ticket_Booking.Models.Passenger
{
    public class Passenger
    {
        public required Guid Id { get; init; } = Guid.NewGuid();
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public required decimal AccountBalance { get; set; }

        public override string ToString()
        {
            return $"First Name: {FirstName}, Last Name: {LastName}, Email: {Email}, Account Balance: {AccountBalance}";
        }
    }
}
