using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo.Models
{
    public class Week
    {
        public int id { get; set; }
        public int weekNum { get; set; }
        public int seasonId { get; set; } //Reference to SeasonPrices table

        public string[] ToArray()
        {
            string[] arr = { id.ToString(), weekNum.ToString(), seasonId.ToString() };
            return arr;
        }
    }
}
