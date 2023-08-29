# Hotel Guest Management System

This is a console application for managing hotel guests and their bookings. It was created as an assignment for the Programming 2 module at Ngee Ann Polytechnic. This assignment tests the knowledge of OOP as well as C# syntax.

## Features

- Create and store guest, room, and booking information
- View lists of all guests and available rooms
- Register new guests
- Check-in guests to rooms
- View booking details for a guest 
- Extend a guest's booking
- Check-out guests and process payment
- View monthly and yearly revenue reports
- Changing rooms of guests
## Classes

The key classes in this application are:

- `Guest`: Represents a hotel guest with attributes like name, passport number, membership status etc.
- `Room`: Represents a hotel room with attributes like room number, type, rate, availability etc. 
- `Booking`: Represents a hotel room booking with attributes like guest, room, check-in/out dates etc.
- `Membership`: Represents a guest's membership status and points.

## Usage

The application presents a text-based menu for users to choose actions like registering a new guest, checking in a guest etc. 

Sample usage flow:

1. Register a new guest
2. Check-in the guest to a room on a date range  
3. View booking details for that guest
4. Check-out the guest and process payment
5. View monthly/yearly revenue reports
6 Change rooms

The application validates most user inputs. It also updates guest membership status and points automatically based on their bookings.
