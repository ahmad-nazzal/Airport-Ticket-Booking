using Airport_Ticket_Booking.Infrastructure.Repositories.BookingRepository;
using Airport_Ticket_Booking.Infrastructure.Repositories.FlightRepository;
using Airport_Ticket_Booking.Models.Booking;
using Airport_Ticket_Booking.Services.CabinService;
using Airport_Ticket_Booking.Services.FlightService;
using Airport_Ticket_Booking.Services.PassengerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Services.BookingService
{
    public class BookingService: IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IFlightService _flightService;
        private readonly ICabinService _cabinService;
        private readonly IPassengerService _passengerService;

        public BookingService(IBookingRepository bookingRepository, IFlightService flightService, ICabinService cabinService, IPassengerService passengerService)
        {
            _bookingRepository = bookingRepository;
            _flightService = flightService;
            _cabinService = cabinService;
            _passengerService = passengerService;
        }

        public void BookFlight(Guid flightId, Guid passengerId, Guid cabinId)
        {
            var flight = _flightService.GetFlightById(flightId);
            if (flight == null)
            {
                Console.WriteLine("Flight Not Found");
                return;
            }

            var passenger = _passengerService.GetPassengerById(passengerId);
            if (passenger == null)
            {
                Console.WriteLine("Passenger Not Found");
                return;
            }

            var cabin = _cabinService.GetCabinById(cabinId);
            if (cabin == null)
            {
                Console.WriteLine("Cabin Not Found");
                return;
            }

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                FlightId = flightId,
                PassengerId = passengerId,
                CabinId = cabinId,
                TotalPrice = cabin.Price,
            };

            if(!_passengerService.DeductAccountBalance(passengerId, cabin.Price))
            {
                Console.WriteLine("Insufficient Balance");
                return;
            }

            _bookingRepository.AddBooking(booking);
        }

        public void CancelBooking(Guid bookingId)
        {
            var booking = _bookingRepository.GetBookingById(bookingId);
            if (booking == null)
            {
                Console.WriteLine("Booking Not Found");
                return;
            }
            if (booking.IsCancelled)
            {
                Console.WriteLine("It is already Cancelled");
                return;
            }
            if (!_passengerService.Refund(booking.PassengerId, booking.TotalPrice))
            {
                Console.WriteLine("Refund Failed");
                return;
            }

            booking.IsCancelled = true;
            _bookingRepository.UpdateBooking(booking);
        }

        public void ChangeCabin(Guid bookingId, Guid newCabinId)
        {
            var booking = _bookingRepository.GetBookingById(bookingId);
            if (booking == null)
            {
                Console.WriteLine("Booking Not Found");
                return;
            }

            var newCabin = _cabinService.GetCabinById(newCabinId);
            if (newCabin == null)
            {
                Console.WriteLine("Cabin Not Found");
                return;
            }

            var oldCabin = _cabinService.GetCabinById(booking.CabinId);

            if(!_passengerService.Refund(booking.PassengerId, oldCabin.Price) || !_passengerService.DeductAccountBalance(booking.PassengerId, newCabin.Price))
            {
                Console.WriteLine("Transaction Failed");
                return;
            }            

            booking.CabinId = newCabinId;
            booking.TotalPrice = newCabin.Price;
            _bookingRepository.UpdateBooking(booking);
        }

        public void ChangeFlight(Guid bookingId, Guid newFlightId, Guid newCabinId)
        {
            var booking = _bookingRepository.GetBookingById(bookingId);
            if (booking == null)
            {
                Console.WriteLine("Booking Not Found");
                return;
            }

            var newFlight = _flightService.GetFlightById(newFlightId);
            if (newFlight == null)
            {
                Console.WriteLine("Flight Not Found");
                return;
            }

            var newCabin = _cabinService.GetCabinById(newCabinId);
            if (newCabin == null)
            {
                Console.WriteLine("new Cabin Not Found");
                return;
            }

            var oldCabin = _cabinService.GetCabinById(booking.CabinId);
            if (!_passengerService.Refund(booking.PassengerId, oldCabin.Price) || !_passengerService.DeductAccountBalance(booking.PassengerId, newCabin.Price))
            {
                Console.WriteLine("Transaction Failed");
                return;
            }

            booking.FlightId = newFlightId;
            _bookingRepository.UpdateBooking(booking);
        }

        public List<Booking> FilterBookings(Guid change)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings();
        }

        public List<Booking> GetFlightBookings(Guid flightId)
        {
            var bookings =  _bookingRepository.GetAllBookings().Where(booking => booking.FlightId == flightId).ToList() ?? new List<Booking>();
            return bookings;
        }

        public List<Booking> GetPassengerBookings(Guid passengerId)
        {
            var booking = _bookingRepository.GetAllBookings().Where(booking => booking.PassengerId == passengerId).ToList() ?? new List<Booking>();
            return booking;
        }
    }
}
