using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Validation
{
    public class FlightDateValidation: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date && date < DateTime.UtcNow)
            {
                return new ValidationResult(ErrorMessage ?? "Date must be today or in the future.");
            }
            return ValidationResult.Success;
        }

    }
}
