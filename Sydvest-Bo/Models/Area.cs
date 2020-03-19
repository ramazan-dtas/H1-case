using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo.Models
{
    public class Area
    {
        public int id { get; set; }
        public string name { get; set; }
        public int consultant_id { get; set; } //Reference to Consultants table

        public string[] ToArray()
        {
            string[] arr = { id.ToString(), name, consultant_id.ToString() };
            return arr;
        }
    }
}
