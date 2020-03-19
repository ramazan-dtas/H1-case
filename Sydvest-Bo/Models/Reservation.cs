using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo.Models
{
    public class Reservation
    {
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int house_id { get; set; } //Reference to Houses table
        public string guest_name { get; set; }

        public string[] ToArray()
        {
            string[] arr = { start_date.ToString(), end_date.ToString(), house_id.ToString(), guest_name.ToString() };
            return arr;
        }
    }
}
