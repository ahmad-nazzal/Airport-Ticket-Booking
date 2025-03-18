# Airport Ticket Booking System

## Project Overview

The **Airport Ticket Booking System** is a .NET console application designed to manage the process of booking flights for passengers and allowing managers to oversee and manage flight bookings. This system is a simulation of an airport booking system, where passengers can search, book, and manage flights, while managers can filter and import flights, as well as validate imported data.

The application uses a file system for data storage, ensuring that all the flight and booking data is easily accessible and stored for persistent use.

## Features

### Passenger Features
Passengers have the ability to:
1. **Book a Flight:**
   - Passengers can search for flights based on different parameters (e.g., price, destination, departure airport).
   - Choose a flight class (Economy, Business, or First Class), with varying prices based on the selected class.
   - **Balance Management**: When booking a flight, the system will check the passenger's balance. If the passenger has enough balance, the booking proceeds, and the payment is deducted from the passenger’s account. If not, an error message is displayed.

2. **Search for Available Flights:**
   - Passengers can search for available flights by providing different search parameters:
     - **Price**
     - **Departure Country**
     - **Destination Country**
     - **Departure Date**
     - **Departure Airport**
     - **Arrival Airport**
     - **Flight Class (Economy, Business, First Class)**

3. **Manage Bookings:**
   - Passengers can:
     - View their current bookings.
     - Modify existing bookings.
     - Cancel a booking, which will trigger a **refund** to their balance, restoring the amount spent on the booking.

### Manager Features
Managers have the ability to:
1. **Filter Bookings:**
   - Managers can filter bookings based on parameters like:
     - **Flight**
     - **Price**
     - **Departure Country**
     - **Destination Country**
     - **Departure Date**
     - **Departure Airport**
     - **Arrival Airport**
     - **Passenger**
     - **Flight Class (Economy, Business, First Class)**

2. **Batch Flight Upload:**
   - Managers can import a list of flights into the system from a CSV file, making the process of adding flights more efficient.

3. **Validate Imported Flight Data:**
   - The system validates the flight data in the CSV file against pre-defined validation rules to ensure the data is correct.
   - If errors are found in the imported data, they are displayed in detail, allowing managers to correct the issues.

4. **Dynamic Model Validation Details:**
   - The system provides dynamically generated details about validation constraints for each field of the flight data model, such as:
     - **Departure Country:**
       - Type: Free Text
       - Constraint: Required
     - **Departure Date:**
       - Type: Date Time
       - Constraint: Required, Allowed Range (today → future)

## Data Storage
The application uses a simple file-based system for data storage. It saves flight and booking information to files and provides functionality to load and manipulate this data.

## Project Structure

- **Services**: Contains the core logic for managing flights, bookings, passengers, and CSV file imports.
- **Models**: Defines data models for flights, bookings, passengers, and filters.
- **Menus**: Provides user interfaces to interact with the system, displaying options for booking flights, managing bookings, and handling manager tasks.
- **Repositories**: Handles the reading and writing of data to files for persistence.
- **Validation**: Contains logic to validate data for flights and bookings.

## How to Run the Project

1. Clone or download the repository to your local machine.
2. Open the solution in Visual Studio (or your preferred C# IDE).
3. Build and run the application in the console.
4. Follow the on-screen prompts to interact with the system.
