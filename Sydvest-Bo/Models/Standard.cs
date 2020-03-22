using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo.Models
{
    public class Standard
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }

        public string[] ToArray()
        {
            return new string[] { id.ToString(), name, price.ToString() };
        }
    }
}
