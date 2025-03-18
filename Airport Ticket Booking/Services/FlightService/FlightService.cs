using Airport_Ticket_Booking.Infrastructure.Repositories.FlightRepository;
using Airport_Ticket_Booking.Models.Flight;
using Airport_Ticket_Booking.Records;
using Airport_Ticket_Booking.Services.CabinService;
using Airport_Ticket_Booking.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Services.FlightService
{
    public class FlightService: IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly ICabinService _cabinService;
        public FlightService(IFlightRepository flightRepository, ICabinService cabinService)
        {
            _flightRepository = flightRepository;
            _cabinService = cabinService;
        }

        public void AddFlight(Flight flight)
        {
            _flightRepository.AddFlight(flight);
        }

        public void DeleteFlight(Guid flightId)
        {
            _flightRepository.DeleteFlight(flightId);
        }

        public List<Flight> GetAllFlights()
        {
            return _flightRepository.GetAllFlights();
        }

        public Flight GetFlightById(Guid flightId)
        {
            return _flightRepository.GetFlightById(flightId);
        }


        public List<Flight> SearchFlights(FlightFilter filter)
        {
            return _flightRepository.GetAllFlights()
                .Where(flight =>
                    (filter.MinPrice == null || _cabinService.GetCabinsByFlightId(flight.Id).Any(cabin=> cabin.Price >= filter.MinPrice)) &&
                    (filter.MaxPrice == null || _cabinService.GetCabinsByFlightId(flight.Id).Any(cabin => cabin.Price <= filter.MaxPrice)) &&
                    (string.IsNullOrEmpty(filter.DepartureCountry) || flight.DepartureCountry.Equals(filter.DepartureCountry, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(filter.DestinationCountry) || flight.DestinationCountry.Equals(filter.DestinationCountry, StringComparison.OrdinalIgnoreCase)) &&
                    (filter.DepartureDate == null || flight.DepartureDate.Date == filter.DepartureDate.Value.Date) &&
                    (string.IsNullOrEmpty(filter.DepartureAirport) || flight.DepartureAirport.Equals(filter.DepartureAirport, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(filter.ArrivalAirport) || flight.ArrivalAirport.Equals(filter.ArrivalAirport, StringComparison.OrdinalIgnoreCase)) &&
                    (filter.CabinClass == null || _cabinService.GetCabinsByFlightId(flight.Id).Any(cabin => cabin.CabinName == filter.CabinClass))
                )
                .ToList();
        }
        public List<string> ImportFlightsFromCsv(string filePath)
        {
            var errorsList = new List<string>();

            if (!File.Exists(filePath))
            {
                errorsList.Add("File not found.");
                return errorsList;
            }

            var lines = File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(',');

                if (columns.Length < 7)
                {
                    errorsList.Add($"Row {i + 1}: Invalid data format.");
                    continue;
                }

                try
                {
                    var flight = new Flight
                    {
                        Id = Guid.NewGuid(),
                        FlightName = columns[0].Trim(),
                        DepartureCountry = columns[1].Trim(),
                        DestinationCountry = columns[2].Trim(),
                        DepartureAirport = columns[3].Trim(),
                        ArrivalAirport = columns[4].Trim(),
                        DepartureDate = DateTime.ParseExact(columns[5].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        ArrivalDate = DateTime.ParseExact(columns[6].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    };

                    var validationErrors = FlightValidator.ValidateFlight(flight);
                    if (validationErrors.Any())
                    {
                        errorsList.Add($"Row {i + 1} Errors: {string.Join(", ", validationErrors)}");
                    }
                    else
                    {
                        _flightRepository.AddFlight(flight);
                    }
                }
                catch (Exception ex)
                {
                    errorsList.Add($"Row {i + 1}: Error processing data - {ex.Message}");
                }
            }

            return errorsList;
        }
    }
}
