using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo.Models
{
    public class House
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int area_id { get; set; } //Reference to Areas table
        public int owner_id { get; set; } //Reference to Owners table
        public int inspector_id { get; set; } //Reference to Inspectors table
        public int standard_id { get; set; } //Reference to Standards table

        public string[] ToArray()
        {
            string[] arr = { id.ToString(), name, address, area_id.ToString(), owner_id.ToString(), inspector_id.ToString(), standard_id.ToString() };
            return arr;
        }
    }
}
