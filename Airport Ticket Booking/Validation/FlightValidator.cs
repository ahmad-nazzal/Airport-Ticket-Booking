using Airport_Ticket_Booking.Models.Flight;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Validation
{
    public static class FlightValidator
    {
        public static List<string> ValidateFlight(Flight flight)
        {
            var errors = new List<string>();
            var context = new ValidationContext(flight, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(flight, context, results, validateAllProperties: true))
            {
                foreach (var validationResult in results)
                {
                    errors.Add(validationResult.ErrorMessage!);
                }
            }

            return errors;
        }

        public static void DisplayValidationRules()
        {
            var validationRules = ValidationExtractor.GetValidationRules<Flight>();

            foreach (var rule in validationRules)
            {
                Console.WriteLine($"Property: {rule.PropertyName}");
                Console.WriteLine($"Data Type: {rule.DataType}");
                Console.WriteLine("Constraints: ");
                foreach (var constraint in rule.Constraints)
                {
                    Console.WriteLine($"- {constraint}");
                }
                Console.WriteLine();
            }
        }
    }
}
