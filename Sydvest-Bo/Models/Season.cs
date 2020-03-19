using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo.Models
{
    public class Season
    {
        public int id { get; set; }
        public string name { get; set; }
        public double multiplier { get; set; }

        public string[] ToArray()
        {
            string[] arr = { id.ToString(), name, multiplier.ToString() };
            return arr;
        }
    }
}
