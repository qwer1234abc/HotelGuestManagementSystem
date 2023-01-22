using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_Hotel_Guest_Management_System_
{
    internal class Membership
    {
        public string Status { get; set; }
        public int Points { get; set; }
        public Membership() { }
        public Membership(string status, int points)
        {
            Status = status;
            Points = points;
        }
        public void EarnPoints(double finalBill)
        {
            Points += (int)Math.Round(finalBill/10);
            if (Points >= 200)
            {
                Status = "Gold";
            }
            else if (Points >= 100)
            {
                Status = "Silver";
            }
            else
            {
                Status = "Ordinary";
            }
        }
        public bool RedeemPoints(int offset)
        {
            if (Points >= offset)
            {
                Points -= offset;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{Status,-10}{Points,-10}";
        }
    }
}
