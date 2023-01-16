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
        public void EarnPoints(double x)
        {

        }
        public bool RedeemPoints(int x)
        {
            return true;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
