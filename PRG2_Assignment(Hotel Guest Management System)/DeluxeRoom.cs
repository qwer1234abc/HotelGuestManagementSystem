﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_Hotel_Guest_Management_System_
{
    internal class DeluxeRoom:Room
    {
        public bool AdditionBed { get; set; }
        public DeluxeRoom() { }
        public DeluxeRoom(int roomNumber, string bedConfiguration, double dailyRate, bool isAvail) : base(roomNumber, bedConfiguration, dailyRate, isAvail)
        {
        }
        public override double CalculateCharges()
        {
            double TotalCost = DailyRate;
            if (AdditionBed == true)
            {
                TotalCost += 25;
            }
            return TotalCost;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
