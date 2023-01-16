using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_Hotel_Guest_Management_System_
{
    internal class Room
    {
        public int RoomNumber { get; set; }
        public string BedConfiguration { get; set; }
        public double DailyRate { get; set; }
        public bool IsAvail { get; set; }

        public Room() { }
        public Room(int roomNumber, string bedConfiguration, double dailyRate, bool isAvail)
        {
            RoomNumber = roomNumber;
            BedConfiguration = bedConfiguration;
            DailyRate = dailyRate;
            IsAvail = isAvail;
        }
        public virtual double CalculateCharges()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"{RoomNumber, -15}{BedConfiguration, -15}{DailyRate, -15}{IsAvail, -15}";
        }
    }
}
