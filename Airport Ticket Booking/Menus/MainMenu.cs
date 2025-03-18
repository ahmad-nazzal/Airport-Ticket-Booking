using Airport_Ticket_Booking.Models.Cabin;
using Airport_Ticket_Booking.Records;
using Airport_Ticket_Booking.Services.BookingService;
using Airport_Ticket_Booking.Services.CabinService;
using Airport_Ticket_Booking.Services.FlightService;
using Airport_Ticket_Booking.Services.PassengerService;
using Airport_Ticket_Booking.Validation;
using System;

namespace Airport_Ticket_Booking.Menus
{
    public class MainMenu : BaseMenu
    {
        private readonly IBookingService _bookingService;
        private readonly IFlightService _flightService;
        private readonly IPassengerService _passengerService;
        private readonly ICabinService _cabinService;

        public MainMenu(IBookingService bookingService, IFlightService flightService, IPassengerService passengerService, ICabinService cabinService)
        {
            _bookingService = bookingService;
            _flightService = flightService;
            _passengerService = passengerService;
            _cabinService = cabinService;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                DisplayOptions("Airport Ticket Booking System", new string[] { "Passenger", "Manager", "Exit" });
                int choice = GetUserChoice(3);

                switch (choice)
                {
                    case 1:
                        ShowPassengerMenu();
                        break;
                    case 2:
                        ShowManagerMenu();
                        break;
                    case 3:
                        Console.WriteLine("Exiting the system...");
                        return;
                }
            }
        }

        private void ShowPassengerMenu()
        {
            while (true)
            {
                DisplayOptions("Passenger Menu", new string[] { "Book a Flight", "Search for Flights", "Manage Bookings", "Go Back" });
                int choice = GetUserChoice(4);

                switch (choice)
                {
                    case 1:
                        BookFlight();
                        break;
                    case 2:
                        SearchFlights();
                        break;
                    case 3:
                        ManageBookings();
                        break;
                    case 4:
                        return;
                }
            }
        }

        private void ShowManagerMenu()
        {
            while (true)
            {
                DisplayOptions("Manager Menu", new string[] { "Filter Bookings", "Batch Flight Upload (CSV)", "Go Back" });
                int choice = GetUserChoice(3);

                switch (choice)
                {
                    case 1:
                        FilterBookings();
                        break;
                    case 2:
                        ImportFlights();
                        break;
                    case 3:
                        return;
                }
            }
        }

        private void BookFlight()
        {
            var passengers = _passengerService.GetAllPassengers();
            var flights = _flightService.GetAllFlights();
            var cabins = _cabinService.GetAllCabins();

            var selectedPassenger = SelectFromList("Passenger", passengers);
            if (selectedPassenger == null) return;

            var selectedFlight = SelectFromList("Flight", flights);
            if (selectedFlight == null) return;

            var selectedCabin = SelectFromList("Cabin", cabins);
            if (selectedCabin == null) return;

            _bookingService.BookFlight(selectedFlight.Id, selectedPassenger.Id, selectedCabin.Id);
            Console.WriteLine("Flight booked successfully!");
            Console.ReadKey();
        }

        private void SearchFlights()
        {
            Console.Clear();
            Console.WriteLine("===== Search Flights =====");

            FlightFilter filter = new FlightFilter();

            Console.Write("Enter Departure Country (or press Enter to skip): ");
            string departureCountry = Console.ReadLine();
            filter.DepartureCountry = string.IsNullOrWhiteSpace(departureCountry) ? null : departureCountry;

            Console.Write("Enter Destination Country (or press Enter to skip): ");
            string destinationCountry = Console.ReadLine();
            filter.DestinationCountry = string.IsNullOrWhiteSpace(destinationCountry) ? null : destinationCountry;

            Console.Write("Enter Departure Airport (or press Enter to skip): ");
            string departureAirport = Console.ReadLine();
            filter.DepartureAirport = string.IsNullOrWhiteSpace(departureAirport) ? null : departureAirport;

            Console.Write("Enter Arrival Airport (or press Enter to skip): ");
            string arrivalAirport = Console.ReadLine();
            filter.ArrivalAirport = string.IsNullOrWhiteSpace(arrivalAirport) ? null : arrivalAirport;

            Console.Write("Enter Departure Date (YYYY-MM-DD) (or press Enter to skip): ");
            string departureDateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(departureDateInput) && DateTime.TryParse(departureDateInput, out DateTime departureDate))
            {
                filter.DepartureDate = departureDate;
            }

            Console.Write("Enter Minimum Price (or press Enter to skip): ");
            string minPriceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(minPriceInput) && decimal.TryParse(minPriceInput, out decimal minPrice))
            {
                filter.MinPrice = minPrice;
            }

            Console.Write("Enter Maximum Price (or press Enter to skip): ");
            string maxPriceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(maxPriceInput) && decimal.TryParse(maxPriceInput, out decimal maxPrice))
            {
                filter.MaxPrice = maxPrice;
            }

            Console.Write("Enter Cabin Class (Economy/Business/First) (or press Enter to skip): ");
            string cabinClassInput = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(cabinClassInput))
            {
                if (Enum.TryParse<CabinClass>(cabinClassInput, true, out CabinClass cabinClass))
                {
                    filter.CabinClass = cabinClass;
                }
                else
                {
                    Console.WriteLine("Invalid cabin class. Please enter Economy, Business, or First.");
                    Console.ReadKey();
                    return;
                }
            }

            var flights = _flightService.SearchFlights(filter);

            if (flights.Count == 0)
            {
                Console.WriteLine("No flights found matching the criteria.");
                Console.ReadKey();
                return;
            }

            var selectedFlight = SelectFromList("Flight", flights);

            if (selectedFlight != null)
            {
                Console.WriteLine(selectedFlight);
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        public void ManageBookings()
        {
            Console.Clear();
            Console.WriteLine("===== Manage Bookings =====");

            var passenger = SelectFromList("Passenger", _passengerService.GetAllPassengers());

            if (passenger == null) return;

            var bookings = _bookingService.GetPassengerBookings(passenger.Id);
            if (bookings == null || !bookings.Any())
            {
                Console.WriteLine("No bookings found.");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                var selectedBooking = SelectFromList("Booking", bookings);

                if (selectedBooking == null) return;

                string[] options = { "Change Flight", "Change Cabin", "Cancel Booking", "Go Back" };
                DisplayOptions("Booking Options", options);
                int choice = GetUserChoice(options.Length);

                switch (choice)
                {
                    case 1:
                        ChangeFlight(selectedBooking.Id);
                        break;
                    case 2:
                        ChangeCabin(selectedBooking.Id, selectedBooking.FlightId);
                        break;
                    case 3:
                        CancelBooking(selectedBooking.Id);
                        break;
                    case 4:
                        return;
                }
            }
        }

        private void ChangeFlight(Guid bookingId)
        {
            Console.Clear();
            Console.WriteLine("===== Change Flight =====");

            var flights = _flightService.GetAllFlights();
            var selectedFlight = SelectFromList("Flights",flights);

            if (selectedFlight == null) return;

            var cabins = _cabinService.GetCabinsByFlightId(selectedFlight.Id);
            var selectedCabin = SelectFromList("Cabin", cabins);
            if (selectedCabin == null) return;

            _bookingService.ChangeFlight(bookingId, selectedFlight.Id, selectedCabin.Id);
            Console.WriteLine("Flight changed successfully!");
            Console.ReadKey();
        }

        private void ChangeCabin(Guid bookingId, Guid flightId)
        {
            Console.Clear();
            Console.WriteLine("===== Change Cabin =====");

            var cabins = _cabinService.GetCabinsByFlightId(flightId);
            var selectedCabin = SelectFromList("Cabins",cabins);

            if (selectedCabin == null) return;

            _bookingService.ChangeCabin(bookingId, selectedCabin.Id);
            Console.WriteLine("Cabin changed successfully!");
            Console.ReadKey();
        }

        private void CancelBooking(Guid bookingId)
        {
            Console.Write("Are you sure you want to cancel this booking? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                _bookingService.CancelBooking(bookingId);
                Console.WriteLine("Booking canceled.");
            }
            else
            {
                Console.WriteLine("Cancellation aborted.");
            }
            Console.ReadKey();
        }
        public void FilterBookings()
        {
            Console.Clear();
            Console.WriteLine("===== Filter Bookings =====");

            BookingFilter filter = new BookingFilter();

            var flight = SelectFromList("Flight", _flightService.GetAllFlights());
            filter.FlightId = flight?.Id;

            Console.Write("Enter Minimum Price (or press Enter to skip): ");
            string minPriceInput = Console.ReadLine();
            filter.MinPrice = decimal.TryParse(minPriceInput, out decimal minPrice) ? minPrice : (decimal?)null;

            Console.Write("Enter Maximum Price (or press Enter to skip): ");
            string maxPriceInput = Console.ReadLine();
            filter.MaxPrice = decimal.TryParse(maxPriceInput, out decimal maxPrice) ? maxPrice : (decimal?)null;

            Console.Write("Enter Departure Country (or press Enter to skip): ");
            filter.DepartureCountry = Console.ReadLine();

            Console.Write("Enter Destination Country (or press Enter to skip): ");
            filter.DestinationCountry = Console.ReadLine();

            Console.Write("Enter Departure Date (YYYY-MM-DD) (or press Enter to skip): ");
            string departureDateInput = Console.ReadLine();
            filter.DepartureDate = DateTime.TryParse(departureDateInput, out DateTime departureDate) ? departureDate : (DateTime?)null;

            Console.Write("Enter Departure Airport (or press Enter to skip): ");
            filter.DepartureAirport = Console.ReadLine();

            Console.Write("Enter Arrival Airport (or press Enter to skip): ");
            filter.ArrivalAirport = Console.ReadLine();

            Console.Write("Enter Cabin Class (Economy/Business/First) (or press Enter to skip): ");
            string cabinClassInput = Console.ReadLine();
            filter.CabinClass = string.IsNullOrWhiteSpace(cabinClassInput) ? null : Enum.Parse<CabinClass>(cabinClassInput, true);

            Console.Write("Enter Passenger ID (or press Enter to skip): ");
            string passengerIdInput = Console.ReadLine();
            filter.PassengerId = Guid.TryParse(passengerIdInput, out Guid passengerId) ? passengerId : (Guid?)null;

            var filteredBookings = _bookingService.FilterBookings(filter);

            if (filteredBookings == null || filteredBookings.Count == 0)
            {
                Console.WriteLine("\nNo bookings found matching the criteria.");
            }
            else
            {
                Console.WriteLine("\n===== Filtered Bookings =====");
                foreach (var booking in filteredBookings)
                {
                    Console.WriteLine(booking);
                }
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void ImportFlights()
        {
            var filePath = @"D:\\Trainings\\Foothill-trainig\\Tasks\\Airport-Ticket-Booking\\Airport Ticket Booking\\CsvData\importedFlights.csv";
            var errors = _flightService.ImportFlightsFromCsv(filePath);
            if (errors.Count > 0)
            {
                Console.WriteLine("Errors found in the CSV file:");
                foreach (var error in errors)
                {
                    Console.WriteLine($"- {error}");
                }
                FlightValidator.DisplayValidationRules();
            }
            else
            {
                Console.WriteLine("Flights imported successfully!");
            }
            Console.ReadKey();
        }
    }
}
