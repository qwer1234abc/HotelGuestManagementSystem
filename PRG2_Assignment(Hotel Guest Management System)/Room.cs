using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_Hotel_Guest_Management_System_
{
    internal abstract class Room
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
        public abstract double CalculateCharges();

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
