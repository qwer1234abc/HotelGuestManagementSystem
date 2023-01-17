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
            initAvailableRooms(roomList);
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
            Console.WriteLine("--------------Menu---------------\r\n[1] List all guests\r\n[2] List all available rooms\r\n[3] Register guest\r\n[4] Check-in guest\r\n[5] Display stay details of a guest\r\n[6] Extend stay of guest\r\n[0] Exit\r\n---------------------------------\r\n");
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

            Console.WriteLine($"{"Name",-20}{"Passport Number",-20}{"Check In Date",-20}{"Check Out Date",-20}{"Member Status",-20}{"Member Points",-20}");
            foreach (Guest guest in guestList)
            {
                Console.WriteLine(guest.ToString());
            }
        }
        static List<Room> initAvailableRooms(List<Room> roomList)
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
                if (isCheckedIn)
                {
                    var room = roomList.Where(x => x.RoomNumber == roomNumber).FirstOrDefault();
                    if (room != null)
                    {
                        room.IsAvail = false;
                    }
                    var extraRoom = roomList.Where(x => x.RoomNumber == extraRoomNumber).FirstOrDefault();
                    if (extraRoom != null)
                    {
                        extraRoom.IsAvail = false;
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
                // Prompt user to enter check-in and check-out date
                Console.Write("Enter check-in date (dd/mm/yyyy):");
                DateTime checkinDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Console.Write("Enter check-out date (dd/mm/yyyy):");
                DateTime checkoutDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Stay selectedStay = new Stay(checkinDate, checkoutDate);
                while (true)
                {
                    ListAllAvailableRooms(roomList);
                    Console.Write("Select room by entering room number: ");
                    int roomSelection = Convert.ToInt32(Console.ReadLine());
                    Room selectedRoom = roomList.Where(x => x.RoomNumber == roomSelection).FirstOrDefault();
                    if (selectedRoom != null)
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
                            selectedStay.AddRoom(selectedRoom);
                            Console.Write("Do you want to select another room [Y/N]: ");
                            string anotherRoom = Console.ReadLine().ToUpper();
                            if (anotherRoom == "Y")
                            {
                                continue;
                            }
                            else if (anotherRoom == "N")
                            {
                                selectedGuest.HotelStay = selectedStay;
                                selectedGuest.IsCheckedin = true;
                                Console.WriteLine("Check in successful.");
                                break;
                            }
                        }
                        else if (selectedRoom is DeluxeRoom)
                        {
                            DeluxeRoom selectedDeluxeRoom = (DeluxeRoom)selectedRoom;
                            Console.Write("Do you require extra bed [Y/N]: ");
                            string extrabed = Console.ReadLine().ToUpper();
                            if (extrabed == "Y")
                            {
                                selectedDeluxeRoom.AdditionBed = true;
                            }
                            else if (extrabed == "N")
                            {
                                selectedDeluxeRoom.AdditionBed = false;
                            }
                            selectedDeluxeRoom.IsAvail = false;
                            selectedStay.AddRoom(selectedDeluxeRoom);
                            Console.Write("Do you want to select another room [Y/N]: ");
                            string anotherRoom = Console.ReadLine().ToUpper();
                            if (anotherRoom == "Y")
                            {
                                continue;
                            }
                            else if (anotherRoom == "N")
                            {
                                selectedGuest.HotelStay = selectedStay;
                                selectedGuest.IsCheckedin = true;
                                Console.WriteLine("Check in successful.");
                                break;
                            }
                        }
                    }
                    else if (selectedRoom == null)
                    {
                        Console.WriteLine("Please enter the correct room.");
                    }
                }
            }
            else if (selectedGuest == null)
            {
                Console.WriteLine("Please enter the correct guest.");
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
                foreach (Room r in selectedStay.Rooms)
                {
                    Console.WriteLine($"{selectedStay.ToString()}{r.ToString()}");
                }
            }
            else if (selectedGuest == null)
            {
                Console.WriteLine("Please enter correct guest.");
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
                if (selectedGuest.IsCheckedin == true)
                {
                    Console.Write("Enter number of days to extend: ");
                    int extendDays = int.Parse(Console.ReadLine());
                    Stay selectedStay = selectedGuest.HotelStay;
                    selectedStay.CheckoutDate = selectedStay.CheckoutDate.AddDays(extendDays);
                    Console.WriteLine($"Updated checkout date: {selectedStay.CheckoutDate:dd/MM/yyyy}");
                }
                else
                {
                    Console.WriteLine("Guest is not checked in.");
                }
            }
            else if (selectedGuest == null)
            {
                Console.WriteLine("Please enter correct guest.");
            }
        }
    }
}