using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_Hotel_Guest_Management_System_
{
    internal class StandardRoom:Room
    {
        public bool RequireWifi { get; set; }
        public bool RequireBreakfast { get; set; }
        public StandardRoom() { }
        public StandardRoom(int roomNumber, string bedConfiguration, double dailyRate, bool isAvail):base(roomNumber, bedConfiguration, dailyRate, isAvail)
        {
        }
        public override double CalculateCharges()
        {
            double totalCost = DailyRate;
            if (RequireWifi)
            {
                totalCost += 10;
            }
            if (RequireBreakfast)
            {
                totalCost += 20;
            }
            return totalCost;
        }
        public override string ToString()
        {
            return $"{"Standard", -15}{RoomNumber,-15}{BedConfiguration,-15}{DailyRate,-15:C0}{(IsAvail? "Available":"Unavailable"),-15}{RequireWifi}{RequireBreakfast}";
        }

    }
}
