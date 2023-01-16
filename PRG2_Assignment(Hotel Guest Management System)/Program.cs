using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRG2_Assignment_Hotel_Guest_Management_System_;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            //Question 1
            List<Guest> guestList = new List<Guest>();
            Console.WriteLine($"{"Name",-20}{"Passport Number",-20}{"Check In Date",-20}{"Check Out Date",-20}{"Member Status",-20}{"Member Points",-20}");
            ListAllGuest(guestList);
            Console.WriteLine();
            //Question 2
            List<Room> roomList = new List<Room>();
            Console.WriteLine($"{"Room Number", -15}{"Bed Type", -15}{"Daily Rate", -15}{"Availability", -15}");
            ListAllAvailableRooms(roomList);
            Console.WriteLine();
            //Question 3
            RegisterGuest(guestList);

        }
        static void ListAllGuest(List<Guest> guestList)
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
                }
            }
            foreach (Guest guest in guestList)
            {
                Console.WriteLine(guest.ToString());
            }
        }
        static void ListAllAvailableRooms(List<Room> roomList)
        {
            string[] csvLines1 = File.ReadAllLines("Rooms.csv");
            string[] csvLines2 = File.ReadAllLines("Stays.csv");
            for (int i = 1; i < csvLines1.Length; i++)
            {
                string[] cells = csvLines1[i].Split(',');
                Room room = new Room
                {
                    RoomNumber = int.Parse(cells[1]),
                    BedConfiguration = cells[2],
                    DailyRate = double.Parse(cells[3]),
                    IsAvail = true
                };
                roomList.Add(room);
            }
            for (int i = 1; i < csvLines2.Length; i++)
            {
                string[] cells = csvLines2[i].Split(',');
                DateTime checkin = DateTime.Parse(cells[3]);
                DateTime checkout = DateTime.Parse(cells[4]);
                int roomNumber = int.Parse(cells[5]);
                int extraRoomNumber = -1;
                if (int.TryParse(cells[9], out extraRoomNumber))
                {
                    var room = roomList.Where(x => x.RoomNumber == roomNumber).FirstOrDefault();
                    if (room != null && DateTime.Now >= checkin && DateTime.Now <= checkout)
                    {
                        room.IsAvail = false;
                    }
                    var extraRoom = roomList.Where(x => x.RoomNumber == extraRoomNumber).FirstOrDefault();
                    if (extraRoom != null && DateTime.Now >= checkin && DateTime.Now <= checkout)
                    {
                        extraRoom.IsAvail = false;
                    }
                }
            }
            foreach (Room room in roomList)
            {
                Console.WriteLine(room.ToString());
            }
        }
        static void RegisterGuest(List<Guest> guestList)
        {
            Console.Write("Enter guest name:");
            string name = Console.ReadLine();
            Console.Write("Enter passport number:");
            string passportNum = Console.ReadLine();
            Console.WriteLine("Enter membership status:");
            string status = Console.ReadLine();
            Console.WriteLine("Enter membership points:");
            int points = Convert.ToInt32(Console.ReadLine());

            Guest guest = new Guest
            {
                Name = name,
                PassportNum = passportNum,
                Member = new Membership(status, points)
            };

            guestList.Add(guest);
            using (StreamWriter sw = new StreamWriter ("Guests.csv", true))
            {
                sw.WriteLine(name + "," + passportNum + "," + status + "," + points);
            }

        }
    }
}