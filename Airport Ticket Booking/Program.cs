// See https://aka.ms/new-console-template for more information
using Airport_Ticket_Booking.Models.Booking;
using Airport_Ticket_Booking.Infrastructure;
using Airport_Ticket_Booking.Infrastructure.Repositories.BookingRepository;

Console.WriteLine("Hello, World!");

var filePath = @"D:\\Trainings\\Foothill-trainig\\Tasks\\Airport-Ticket-Booking\\Airport Ticket Booking\\CsvData\bookings.csv";
var dataHandler = new CsvHandler<Booking>(filePath);
var bookingRepository = new BookingRepository(filePath, dataHandler);
var bookings = bookingRepository.GetAllBookings();
foreach (var booking in bookings)
{
    Console.WriteLine(booking.Id);
    Console.WriteLine(booking.FlightId);
    Console.WriteLine(booking.PassengerId);
    Console.WriteLine(booking.CabinId);
    Console.WriteLine(booking.BookingDate);
    Console.WriteLine(booking.IsCancelled);
    Console.WriteLine(booking.TotalPrice);
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();

}
Console.WriteLine("-----------------");
var existingBooking = bookingRepository.GetBookingById(bookings[0].Id);
existingBooking.TotalPrice = 10124;
bookingRepository.UpdateBooking(existingBooking);
foreach (var booking in bookings)
{
    Console.WriteLine(booking.Id);
    Console.WriteLine(booking.FlightId);
    Console.WriteLine(booking.PassengerId);
    Console.WriteLine(booking.CabinId);
    Console.WriteLine(booking.BookingDate);
    Console.WriteLine(booking.IsCancelled);
    Console.WriteLine(booking.TotalPrice);
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();

}
Console.ReadLine();