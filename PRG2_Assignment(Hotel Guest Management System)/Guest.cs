﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_Hotel_Guest_Management_System_
{
    internal class Guest
    {
        public string Name { get; set; }
        public string PassportNum { get; set; }
        public Stay HotelStay { get; set; }
        public Membership Member { get; set; }
        public bool IsCheckedin { get; set; }

        public Guest() { }

        public Guest(string name, string passportNum, Stay hotelStay, Membership member)
        {
            Name = name;
            PassportNum = passportNum;
            HotelStay = hotelStay;
            Member = member;
        }
        public override string ToString()
        {
            return $"{Name, -20}{PassportNum, -20}{HotelStay.CheckinDate, -20:dd/MM/yyyy}{HotelStay.CheckoutDate, -20:dd/MM/yyyy}{Member.Status, -20}{Member.Points, -20}";
        }
    }
}
