using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo.Models
{
    public class Inspector
    {
        public int id { get; set; }
        public string name { get; set; }
        public double hourly_rate { get; set; }

        public string[] ToArray()
        {
            string[] arr = { id.ToString(), name, hourly_rate.ToString() };
            return arr;
        }
    }
}
