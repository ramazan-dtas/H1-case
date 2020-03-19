using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo.Models
{
    public class Consultant
    {
        public int id { get; set; }
        public string name { get; set; }

        public string[] ToArray ()
        {
            string[] arr = { id.ToString(), name };
            return arr;
        }
    }

}