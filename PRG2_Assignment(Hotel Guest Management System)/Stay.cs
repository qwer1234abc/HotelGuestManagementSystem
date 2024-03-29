﻿using System;
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
        public List<Room> RoomList { get; set; } = new List<Room>();
        public Stay() { }
        public Stay(DateTime checkinDate, DateTime checkoutDate)
        {
            CheckinDate = checkinDate;
            CheckoutDate = checkoutDate;
        }
        public void AddRoom(Room r)
        {
            RoomList.Add(r);
        }
        // extra feature method
        public void RemoveRoom(Room r)
        {
            RoomList.Remove(r);
        }
        public double CalculateTotal()
        {
            double total = 0;
            int dayStayed = CheckoutDate.Subtract(CheckinDate).Days;
            foreach(Room room in RoomList)
            {
                total += room.CalculateCharges() * dayStayed;
            }
            return total;
        }
        public override string ToString()
        {
            return $"{CheckinDate,-15:dd/MM/yyyy}{CheckoutDate,-15:dd/MM/yyyy}";
        }
    }
}
