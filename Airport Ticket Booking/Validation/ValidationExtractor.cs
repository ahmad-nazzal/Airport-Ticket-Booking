using Airport_Ticket_Booking.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Validation
{
    public static class ValidationExtractor
    {
        public static List<ValidationMetadata> GetValidationRules<T>()
        {
            var validationDetails = new List<ValidationMetadata>();

            foreach (var property in typeof(T).GetProperties())
            {
                var constraints = new List<string>();
                var dataType = property.PropertyType.Name;

                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    switch (attribute)
                    {
                        case RequiredAttribute:
                            constraints.Add("Required");
                            break;
                        case StringLengthAttribute stringLength:
                            constraints.Add($"Max Length: {stringLength.MaximumLength}");
                            break;
                        case RangeAttribute range:
                            constraints.Add($"Range: {range.Minimum} - {range.Maximum}");
                            break;
                        case DataTypeAttribute dataTypeAttr:
                            constraints.Add($"Type: {dataTypeAttr.DataType}");
                            break;
                        case FlightDateValidation:
                            constraints.Add("Date must be today or in the future");
                            break;
                    }
                }

                validationDetails.Add(new ValidationMetadata
                {
                    PropertyName = property.Name,
                    DataType = dataType,
                    Constraints = constraints
                });
            }

            return validationDetails;
        }
    }
}
