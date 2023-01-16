using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_Hotel_Guest_Management_System_
{
    internal class Stay
    {
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public List<Room> Rooms { get; set; }
        public Stay() { }
        public Stay(DateTime checkinDate, DateTime checkoutDate)
        {
            CheckinDate = checkinDate;
            CheckoutDate = checkoutDate;
        }
        public void AddRoom(Room r)
        {
            Rooms.Add(r);
        }
        public double CalculateTotal()
        {
            int dayStayed = CheckoutDate.Subtract(CheckinDate).Days;
            return 3;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
