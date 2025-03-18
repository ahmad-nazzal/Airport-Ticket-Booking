using Airport_Ticket_Booking.Infrastructure;
using Airport_Ticket_Booking.Infrastructure.Repositories.BookingRepository;
using Airport_Ticket_Booking.Infrastructure.Repositories.CabinRepository;
using Airport_Ticket_Booking.Infrastructure.Repositories.FlightRepository;
using Airport_Ticket_Booking.Infrastructure.Repositories.PassengerRepository;
using Airport_Ticket_Booking.Menus;
using Airport_Ticket_Booking.Models.Booking;
using Airport_Ticket_Booking.Models.Cabin;
using Airport_Ticket_Booking.Models.Flight;
using Airport_Ticket_Booking.Models.Passenger;
using Airport_Ticket_Booking.Services.BookingService;
using Airport_Ticket_Booking.Services.CabinService;
using Airport_Ticket_Booking.Services.FlightService;
using Airport_Ticket_Booking.Services.PassengerService;

class Program
{
    static void Main(string[] args)
    {
        var cabinFilePath = @"D:\\Trainings\\Foothill-trainig\\Tasks\\Airport-Ticket-Booking\\Airport Ticket Booking\\CsvData\cabins.csv";
        IDataHandler<Cabin> cabinDataHandler = new CsvHandler<Cabin>(cabinFilePath);
        ICabinRepository cabinRepository = new CabinRepository(cabinDataHandler);
        ICabinService cabinService = new CabinService(cabinRepository);

        var flightFilePath = @"D:\\Trainings\\Foothill-trainig\\Tasks\\Airport-Ticket-Booking\\Airport Ticket Booking\\CsvData\flights.csv";
        IDataHandler<Flight> flightDataHandler = new CsvHandler<Flight>(flightFilePath);
        IFlightRepository flightRepository = new FlightRepository(flightDataHandler);
        IFlightService flightService = new FlightService(flightRepository, cabinService);

        var passengerFilePath = @"D:\\Trainings\\Foothill-trainig\\Tasks\\Airport-Ticket-Booking\\Airport Ticket Booking\\CsvData\passengers.csv";
        IDataHandler<Passenger> passengerDataHandler = new CsvHandler<Passenger>(passengerFilePath);
        IPassengerRepository passengerRepository = new PassengerRepository(passengerDataHandler);
        IPassengerService passengerService = new PassengerService(passengerRepository);

        var bookingFilePath = @"D:\\Trainings\\Foothill-trainig\\Tasks\\Airport-Ticket-Booking\\Airport Ticket Booking\\CsvData\bookings.csv";
        IDataHandler<Booking> bookingDataHandler = new CsvHandler<Booking>(bookingFilePath);
        IBookingRepository bookingRepository = new BookingRepository(bookingDataHandler);
        IBookingService bookingService = new BookingService(bookingRepository, flightService, cabinService, passengerService);

        var menu = new MainMenu(bookingService, flightService, passengerService, cabinService);
        menu.ShowMainMenu();
    }
}
