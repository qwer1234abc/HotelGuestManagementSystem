//==========================================================
// Student Number : S10244263
// Student Name : Beh Jueen Hao Kelvin
//==========================================================
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PRG2_Assignment_Hotel_Guest_Management_System_;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Guest> guestList = new List<Guest>();
            List<Room> roomList = new List<Room>();
            initGuests(guestList);
            initAvailableRooms(roomList, guestList);
            while (true)
            {
                DisplayMenu();
                Console.Write("Please select an option: ");
                string userSelect = Console.ReadLine();
                if (userSelect == "1")
                {
                    //Question 1
                    ListAllGuest(guestList);
                    Console.WriteLine();
                }
                else if (userSelect == "2")
                {
                    //Question 2
                    ListAllAvailableRooms(roomList);
                    Console.WriteLine();
                }
                else if (userSelect == "3")
                {
                    //Question 3
                    RegisterGuest(guestList);
                    Console.WriteLine();
                }
                else if (userSelect == "4")
                {

                    //Question 4
                    CheckInGuest(guestList, roomList);
                    Console.WriteLine();
                }
                else if (userSelect == "5")
                {
                    DisplayStayDetails(guestList);
                }
                else if (userSelect == "6")
                {
                    ExtendStay(guestList);
                }
                else if (userSelect == "7")
                {
                    MonthlyCharged(guestList);
                }
                else if (userSelect == "8")
                {
                    CheckOutGuest(guestList);
                }
                else if (userSelect == "9")
                {
                    ChangeRoom(guestList, roomList);
                }
                else if (userSelect == "0")
                {
                    Console.WriteLine("Thank you please come back again.");
                    break;
                }
                else
                {
                    Console.WriteLine("Please select a valid option.");
                }
            }
        }
        static void DisplayMenu()
        {
            Console.WriteLine("--------------Menu---------------\r\n[1] List all guests\r\n[2] List all available rooms\r\n[3] Register guest\r\n[4] Check-in guest\r\n[5] Display stay details of a guest\r\n[6] Extend stay of guest\r\n[7] Display charges for monthly and year\r\n[8] Check-out guest\r\n[9] Change Room\r\n[0] Exit\r\n---------------------------------\r\n");
        }
        static List<Guest> initGuests(List<Guest> guestList)
        {
            string[] csvLines1 = File.ReadAllLines("Guests.csv");
            string[] csvLines2 = File.ReadAllLines("Stays.csv");
            for (int i = 1; i < csvLines1.Length; i++)
            {
                string[] cells = csvLines1[i].Split(',');

                var member = new Membership
                {
                    Status = cells[2],
                    Points = int.Parse(cells[3])
                };

                var guest = new Guest
                {
                    Name = cells[0],
                    PassportNum = cells[1],
                    Member = member
                };
                guestList.Add(guest);
            }
            for (int j = 1; j < csvLines2.Length; j++)
            {
                string[] cells = csvLines2[j].Split(',');
                var guest = guestList.Where(x => x.PassportNum == cells[1]).FirstOrDefault();
                if (guest != null)
                {
                    guest.IsCheckedin = bool.Parse(cells[2]);
                    var hotelStay = new Stay
                    {
                        CheckinDate = DateTime.Parse(cells[3]),
                        CheckoutDate = DateTime.Parse(cells[4]),
                    };
                    guest.HotelStay = hotelStay;
                    guest.IsCheckedin = bool.Parse(cells[2]);
                }
            }
            return guestList;
        }
        static void ListAllGuest(List<Guest> guestList)
        {

            Console.WriteLine($"{"Name",-20}{"Passport Number",-20}{"Member Status",-20}{"Member Points",-20}{"Checked In",-20}");
            foreach (Guest guest in guestList)
            {
                Console.WriteLine(guest.ToString());
            }
        }
        static List<Room> ExtraRoomList()
        {
            List<Room> extraRoomList = new List<Room>();
            string[] csvLines1 = File.ReadAllLines("Rooms.csv");
            for (int i = 1; i < csvLines1.Length; i++)
            {
                string[] cells = csvLines1[i].Split(',');
                if (cells[0] == "Standard")
                {
                    Room room = new StandardRoom
                    {
                        RoomNumber = int.Parse(cells[1]),
                        BedConfiguration = cells[2],
                        DailyRate = double.Parse(cells[3]),
                        IsAvail = true
                    };

                    extraRoomList.Add(room);

                }
                else if (cells[0] == "Deluxe")
                {
                    Room room = new DeluxeRoom
                    {
                        RoomNumber = int.Parse(cells[1]),
                        BedConfiguration = cells[2],
                        DailyRate = double.Parse(cells[3]),
                        IsAvail = true
                    };
                    extraRoomList.Add(room);
                }

            }
            return extraRoomList;
        }
        static List<Room> initAvailableRooms(List<Room> roomList, List<Guest> guestList)
        {
            string[] csvLines1 = File.ReadAllLines("Rooms.csv");
            string[] csvLines2 = File.ReadAllLines("Stays.csv");
            for (int i = 1; i < csvLines1.Length; i++)
            {
                string[] cells = csvLines1[i].Split(',');
                if (cells[0] == "Standard")
                {
                    Room room = new StandardRoom
                    {
                        RoomNumber = int.Parse(cells[1]),
                        BedConfiguration = cells[2],
                        DailyRate = double.Parse(cells[3]),
                        IsAvail = true
                    };

                    roomList.Add(room);

                }
                else if (cells[0] == "Deluxe")
                {
                    Room room = new DeluxeRoom
                    {
                        RoomNumber = int.Parse(cells[1]),
                        BedConfiguration = cells[2],
                        DailyRate = double.Parse(cells[3]),
                        IsAvail = true
                    };
                    roomList.Add(room);
                }

            }
            for (int i = 1; i < csvLines2.Length; i++)
            {
                string[] cells = csvLines2[i].Split(',');
                bool isCheckedIn = bool.Parse(cells[2]);
                int roomNumber = int.Parse(cells[5]);
                int extraRoomNumber = int.TryParse(cells[9], out int result) ? result : -1;
                var guest = guestList.Where(x => x.PassportNum == cells[1]).FirstOrDefault();

                var hotelStay = new Stay
                {
                    CheckinDate = DateTime.Parse(cells[3]),
                    CheckoutDate = DateTime.Parse(cells[4]),
                };
                var room = roomList.Where(x => x.RoomNumber == roomNumber).FirstOrDefault();
                if (room != null && guest != null)
                {
                    if (room is StandardRoom)
                    {
                        StandardRoom sRoom = (StandardRoom)room;
                        sRoom.RequireWifi = bool.Parse(cells[6]);
                        sRoom.RequireBreakfast = bool.Parse(cells[7]);
                        guest.HotelStay.AddRoom(sRoom);
                        if (isCheckedIn)
                        {
                            sRoom.IsAvail = false;
                        }
                    }
                    else
                    {
                        DeluxeRoom dRoom = (DeluxeRoom)room;
                        dRoom.AdditionBed = bool.Parse(cells[8]);
                        guest.HotelStay.AddRoom(dRoom);
                        if (isCheckedIn)
                        {
                            dRoom.IsAvail = false;
                        }
                    }
                }
                var extraRoom = ExtraRoomList().Where(x => x.RoomNumber == extraRoomNumber).FirstOrDefault();
                if (extraRoom != null && guest != null)
                {
                    if (extraRoom is StandardRoom)
                    {
                        StandardRoom sRoom = (StandardRoom)extraRoom;
                        sRoom.RequireWifi = bool.Parse(cells[10]);
                        sRoom.RequireBreakfast = bool.Parse(cells[11]);
                        guest.HotelStay.AddRoom(sRoom);
                        if (isCheckedIn)
                        {
                            sRoom.IsAvail = false;
                        }
                    }
                    else
                    {
                        DeluxeRoom dRoom = (DeluxeRoom)extraRoom;
                        dRoom.AdditionBed = bool.Parse(cells[12]);
                        guest.HotelStay.AddRoom(dRoom);
                        if (isCheckedIn)
                        {
                            dRoom.IsAvail = false;
                        }
                    }
                }
            }
            return roomList;
        }
        static void ListAllAvailableRooms(List<Room> roomList)
        {

            Console.WriteLine($"{"Room Type",-15}{"Room Number",-15}{"Bed Type",-15}{"Daily Rate",-15}{"Availability",-15}");
            foreach (Room room in roomList)
            {
                if (room.IsAvail)
                {
                    Console.WriteLine(room.ToString());
                }
            }
        }
        static void RegisterGuest(List<Guest> guestList)
        {
            Console.Write("Enter guest name: ");
            string name = Console.ReadLine();
            Console.Write("Enter passport number: ");
            string passportNum = Console.ReadLine();

            Guest guest = new Guest
            {
                Name = name,
                PassportNum = passportNum,
                Member = new Membership("Ordinary", 0)
            };

            guestList.Add(guest);
            using (StreamWriter sw = new StreamWriter("Guests.csv", true))
            {
                sw.WriteLine(name + "," + passportNum + "," + guest.Member.Status + "," + guest.Member.Points);
            }
            Console.WriteLine("Registration successful!");
        }
        static void CheckInGuest(List<Guest> guestList, List<Room> roomList)
        {

            ListAllGuest(guestList);
            Console.Write("Select guess by entering passport number: ");
            string? guestSelection = Console.ReadLine();
            var selectedGuest = guestList.Where(x => x.PassportNum.ToLower() == guestSelection.ToLower()).FirstOrDefault();
            if (selectedGuest != null)
            {
                DateTime checkinDate;
                DateTime checkoutDate;
                while (true)
                {
                    try
                    {
                        Console.Write("Enter check-in date (dd/mm/yyyy): ");
                        checkinDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter check-out date (dd/mm/yyyy): ");
                        checkoutDate = DateTime.Parse(Console.ReadLine());

                        if (checkoutDate > checkinDate)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nCheckout date must be after check-in date. Please enter a valid date.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("\nInvalid date format. Please enter the date in the format dd/mm/yyyy.");
                    }
                }
                selectedGuest.HotelStay = new Stay(checkinDate, checkoutDate);
                Stay selectedStay = selectedGuest.HotelStay;
                while (true)
                {
                    Console.WriteLine();
                    ListAllAvailableRooms(roomList);
                    int roomSelection = 0;
                    while (true)
                    {

                        try
                        {
                            Console.Write("Select room by entering room number: ");
                            roomSelection = int.Parse(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("\nInvalid input. Please enter a valid room number.\n");
                        }
                    }
                    Room selectedRoom = roomList.Where(x => x.RoomNumber == roomSelection).FirstOrDefault();
                    if (selectedRoom != null)
                    {
                        if (selectedRoom.IsAvail)
                        {
                            if (selectedRoom is StandardRoom)
                            {
                                while (true)
                                {

                                    Console.Write("Do you require wifi [Y/N]: ");
                                    string wifi = Console.ReadLine().ToUpper();
                                    if (wifi == "Y")
                                    {
                                        (selectedRoom as StandardRoom).RequireWifi = true;
                                        break;
                                    }
                                    else if (wifi == "N")
                                    {
                                        (selectedRoom as StandardRoom).RequireWifi = false;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nPlease enter either 'Y' or 'N'.\n");
                                    }
                                }
                                while (true)
                                {

                                    Console.Write("Do you require breakfast [Y/N]: ");
                                    string breakfast = Console.ReadLine().ToUpper();
                                    if (breakfast == "Y")
                                    {
                                        (selectedRoom as StandardRoom).RequireBreakfast = true;
                                        break;
                                    }
                                    else if (breakfast == "N")
                                    {
                                        (selectedRoom as StandardRoom).RequireBreakfast = false;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nPlease enter either 'Y' or 'N'.\n");

                                    }
                                }
                                selectedRoom.IsAvail = false;
                                selectedStay.AddRoom(selectedRoom);
                                string anotherRoom;
                                while (true)
                                {

                                    Console.Write("Do you want to select another room [Y/N]: ");
                                    anotherRoom = Console.ReadLine().ToUpper();
                                    if (anotherRoom == "Y")
                                    {
                                        break;
                                        continue;
                                    }
                                    else if (anotherRoom == "N")
                                    {
                                        selectedGuest.HotelStay = selectedStay;
                                        selectedGuest.IsCheckedin = true;
                                        Console.WriteLine("Check in successful.");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nPlease enter either 'Y' or 'N'.\n");
                                    }
                                }
                                if (anotherRoom == "Y")
                                {
                                    continue;
                                }
                                if (anotherRoom == "N")
                                {
                                    break;
                                }
                            }
                            else if (selectedRoom is DeluxeRoom)
                            {
                                while (true)
                                {

                                    Console.Write("Do you require extra bed [Y/N]: ");
                                    string extrabed = Console.ReadLine().ToUpper();
                                    if (extrabed == "Y")
                                    {
                                        (selectedRoom as DeluxeRoom).AdditionBed = true;
                                        break;
                                    }
                                    else if (extrabed == "N")
                                    {
                                        (selectedRoom as DeluxeRoom).AdditionBed = false;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nPlease enter either 'Y' or 'N'.\n");
                                    }
                                }
                                selectedRoom.IsAvail = false;
                                selectedStay.AddRoom(selectedRoom);
                                string anotherRoom;
                                while (true)
                                {

                                    Console.Write("Do you want to select another room [Y/N]: ");
                                    anotherRoom = Console.ReadLine().ToUpper();
                                    if (anotherRoom == "Y")
                                    {
                                        break;
                                        continue;
                                    }
                                    else if (anotherRoom == "N")
                                    {
                                        selectedGuest.IsCheckedin = true;
                                        Console.WriteLine("Check in successful.");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nPlease enter either 'Y' or 'N'.\n");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nRoom is not available.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nRoom not found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Guest not found.");
            }
        }
        static void DisplayStayDetails(List<Guest> guestList)
        {
            ListAllGuest(guestList);
            Console.Write("Select guess by entering passport number: ");
            string? guestSelection = Console.ReadLine();
            var selectedGuest = guestList.Where(x => x.PassportNum.ToLower() == guestSelection.ToLower()).FirstOrDefault();
            if (selectedGuest != null)
            {
                Stay selectedStay = selectedGuest.HotelStay;
                Console.WriteLine($"{"Check In",-15}{"Check Out",-15}{"Room Type",-15}{"Room Number",-15}{"Bed Type",-15}{"Daily Rate",-15}{"Availability",-15}");
                foreach (Room r in selectedStay.Rooms)
                {
                    if (!r.IsAvail && selectedGuest.IsCheckedin)
                    {
                        Console.WriteLine($"{selectedStay.ToString()}{r.ToString()}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Guest not found.");
            }
        }
        static void ExtendStay(List<Guest> guestList)
        {
            ListAllGuest(guestList);
            Console.Write("Select guess by entering passport number: ");
            string? guestSelection = Console.ReadLine();
            var selectedGuest = guestList.Where(x => x.PassportNum.ToLower() == guestSelection.ToLower()).FirstOrDefault();
            if (selectedGuest != null)
            {
                if (selectedGuest.IsCheckedin)
                {
                    Console.WriteLine($"Original chechout date: {selectedGuest.HotelStay.CheckoutDate:dd/MM/yyyy}\n");
                    int extendDays;
                    while (true)
                    {
                        try
                        {

                            Console.Write("Enter number of days to extend: ");
                            extendDays = int.Parse(Console.ReadLine());
                            if (extendDays > 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nInvalid input. Please enter a positive number.\n");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("\ninvalid input. Please enter a positive number.\n");
                        }
                    }
                    Stay selectedStay = selectedGuest.HotelStay;
                    selectedStay.CheckoutDate = selectedStay.CheckoutDate.AddDays(extendDays);
                    Console.WriteLine($"Updated checkout date: {selectedStay.CheckoutDate:dd/MM/yyyy}\n");
                }
                else
                {
                    Console.WriteLine("\nGuest is not checked in.");
                }
            }
            else
            {
                Console.WriteLine("\nGuest not found.");
            }
        }
        static void MonthlyCharged(List<Guest> guestList)
        {
            int inputYear = 1;
            while (true)
            {
                try
                {

                    Console.Write("Please enter year: ");
                    inputYear = int.Parse(Console.ReadLine());
                    if (inputYear >= DateTime.MinValue.Year && inputYear <= DateTime.MaxValue.Year)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\nPlease enter date between {DateTime.MinValue.Year} & {DateTime.MaxValue.Year}.\n");
                    }
                }
                catch
                {
                    Console.WriteLine("\nPlease enter a valid year.\n");
                }
            }
            Console.WriteLine();
            Dictionary<string, double> monthlyAmounts = new Dictionary<string, double>();
            double totalAmount = 0;

            for (int i = 1; i <= 12; i++)
            {
                string monthName = new DateTime(inputYear, i, 1).ToString("MMMM");
                monthlyAmounts.Add(monthName, 0);
            }

            foreach (Guest guest in guestList)
            {
                Stay stay = guest.HotelStay;
                if (stay != null && stay.CheckoutDate.Year == inputYear)
                {
                    int month = stay.CheckoutDate.Month;
                    double monthlyAmount = 0;
                    monthlyAmount += stay.CalculateTotal();
                    totalAmount += monthlyAmount;
                    string monthName = new DateTime(inputYear, month, 1).ToString("MMMM");
                    monthlyAmounts[monthName] += monthlyAmount;
                }
            }
            Console.WriteLine($"Monthly charged amount breakdown for {inputYear}:");
            foreach (var item in monthlyAmounts)
            {
                Console.WriteLine($"{item.Key} {inputYear}: {item.Value:C2}");
            }
            Console.WriteLine($"Total: {totalAmount:C2}");
        }
        static void CheckOutGuest(List<Guest> guestList)
        {
            ListAllGuest(guestList);
            Console.Write("Select guess by entering passport number: ");
            string? guestSelection = Console.ReadLine();
            var selectedGuest = guestList.Where(x => x.PassportNum.ToLower() == guestSelection.ToLower()).FirstOrDefault();
            if (selectedGuest != null)
            {
                if (selectedGuest.IsCheckedin)
                {
                    Stay selectedStay = selectedGuest.HotelStay;
                    double finalbill = selectedStay.CalculateTotal();
                    Console.WriteLine($"Final bill: {finalbill:C2}");
                    Membership selectedMembership = selectedGuest.Member;
                    Console.WriteLine($"{"Status",-10}{"Points",-10}");
                    Console.WriteLine($"{selectedMembership.ToString()}");
                    if (selectedMembership.Status == "Silver" || selectedMembership.Status == "Gold")
                    {
                        int selectedOffset;
                        while (true)
                        {
                            try
                            {
                                Console.Write("Enter amount of points to offset: ");
                                selectedOffset = int.Parse(Console.ReadLine());
                                if (selectedOffset >= 0)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\nPlease enter a positive number.\n");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("\nPlease enter a positive number.\n");
                            }
                        }
                        if (selectedMembership.RedeemPoints(selectedOffset))
                        {
                            double deductedFinalbill = finalbill - selectedOffset;
                            Console.WriteLine("Redemption successful!");
                            Console.WriteLine($"Final bill after deduction: {deductedFinalbill:C2}");
                            Console.Write("Please press any key to make payment.");
                            Console.ReadLine();
                            selectedMembership.EarnPoints(deductedFinalbill);
                            selectedGuest.IsCheckedin = false;
                            foreach (Room r in selectedStay.Rooms)
                            {
                                r.IsAvail = true;
                            }
                            Console.WriteLine("Checkout success!");
                        }
                        else
                        {
                            Console.WriteLine("Insufficient points. \r\nPlease re-enter.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Membership status is not Gold or Silver.");
                        Console.Write("Please press any key to make payment.");
                        Console.ReadLine();
                        selectedMembership.EarnPoints(finalbill);
                        selectedGuest.IsCheckedin = false;
                        foreach (Room r in selectedStay.Rooms)
                        {
                            r.IsAvail = true;
                        }
                        Console.WriteLine("\nCheckout success!");
                    }
                }
                else
                {
                    Console.WriteLine("\nGuest is not checked in.");
                }
            }
            else
            {
                Console.WriteLine("\nGuest not found.");
            }
        }
        // extra feature
        static void ChangeRoom(List<Guest> guestList, List<Room> roomList)
        {
            ListAllGuest(guestList);
            Console.Write("Select guess by entering passport number: ");
            string guestSelection = Console.ReadLine();
            var selectedGuest = guestList.Where(x => x.PassportNum.ToLower() == guestSelection.ToLower()).FirstOrDefault();
            if (selectedGuest != null)
            {
                if (selectedGuest.IsCheckedin)
                {
                    Stay selectedStay = selectedGuest.HotelStay;
                    Console.WriteLine("Current room(s) of guest: ");
                    Console.WriteLine($"{"Room Type",-15}{"Room Number",-15}{"Bed Type",-15}{"Daily Rate",-15}{"Availability",-15}");
                    foreach (Room r in selectedStay.Rooms)
                    {
                        if (!r.IsAvail && selectedGuest.IsCheckedin)
                        {
                            Console.WriteLine($"{r.ToString()}");
                        }
                    }
                    Console.WriteLine("\r\nAvailable rooms: ");
                    ListAllAvailableRooms(roomList);
                    Console.Write("Enter current room and enter new room to change to seperated by ',': ");
                    string[] userInput = Console.ReadLine().Split(",");
                    int currentRoom = int.Parse(userInput[0]);
                    int newRoom = int.Parse(userInput[1]);
                    List<int> guestRooms = new List<int>();
                    //Room nowRoom = roomList.Where(x => x.RoomNumber == currentRoom).FirstOrDefault();
                    Room nowRoom = roomList.Where(x => x.RoomNumber == currentRoom).FirstOrDefault();
                    Room selectedRoom = roomList.Where(x => x.RoomNumber == newRoom).FirstOrDefault();

                    foreach (Room r in selectedStay.Rooms)
                    {
                        guestRooms.Add(r.RoomNumber);
                    }
                    if (guestRooms.Contains(currentRoom))
                    {
                        if (selectedRoom.IsAvail)
                        {

                            if (selectedRoom != null)
                            {
                                if (selectedRoom.IsAvail)
                                {
                                    if (selectedRoom is StandardRoom)
                                    {
                                        Console.Write("Do you require wifi [Y/N]: ");
                                        string wifi = Console.ReadLine().ToUpper();
                                        if (wifi == "Y")
                                        {
                                            (selectedRoom as StandardRoom).RequireWifi = true;
                                        }
                                        else if (wifi == "N")
                                        {
                                            (selectedRoom as StandardRoom).RequireWifi = false;

                                        }
                                        Console.Write("Do you require breakfast [Y/N]: ");
                                        string breakfast = Console.ReadLine().ToUpper();
                                        if (breakfast == "Y")
                                        {
                                            (selectedRoom as StandardRoom).RequireBreakfast = true;
                                        }
                                        else if (breakfast == "N")
                                        {
                                            (selectedRoom as StandardRoom).RequireBreakfast = false;
                                        }
                                        selectedRoom.IsAvail = false;
                                        if (nowRoom != null)
                                        {
                                            nowRoom.IsAvail = true;
                                            selectedStay.RemoveRoom(nowRoom);
                                        }
                                        selectedStay.AddRoom(selectedRoom);
                                        Console.WriteLine($"Room changed from {nowRoom.RoomNumber} to {selectedRoom.RoomNumber}");
                                    }
                                    else if (selectedRoom is DeluxeRoom)
                                    {
                                        Console.Write("Do you require extra bed [Y/N]: ");
                                        string extrabed = Console.ReadLine().ToUpper();
                                        if (extrabed == "Y")
                                        {
                                            (selectedRoom as DeluxeRoom).AdditionBed = true;
                                        }
                                        else if (extrabed == "N")
                                        {
                                            (selectedRoom as DeluxeRoom).AdditionBed = false;
                                        }
                                        selectedRoom.IsAvail = false;
                                        if (nowRoom != null)
                                        {
                                            nowRoom.IsAvail = true;
                                            selectedStay.RemoveRoom(nowRoom);
                                        }
                                        selectedStay.AddRoom(selectedRoom);
                                        Console.WriteLine($"Room changed from {nowRoom.RoomNumber} to {selectedRoom.RoomNumber}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Room is not available.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Room not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The room that the guest wants to change is currently unavailable.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Guest has not checked into this room.");
                    }
                }
                else
                {
                    Console.WriteLine("Guest is not checked in.");
                }
            }
            else
            {
                Console.WriteLine("Guest not found.");
            }
        }
    }
}
