using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class Tenancy
    {
        public string RentalUnit { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double RentPCM { get; set; }

        public double Deposit { get; set; }
    }
}
