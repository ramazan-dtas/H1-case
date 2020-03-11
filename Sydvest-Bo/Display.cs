using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo
{
    public static class Display
    {
        //Draws a list of objects to the console as a table
        public static void WriteTable (IEnumerable<dynamic> list)
        {
            List<string> tableHeaders = new List<string>();

            PropertyInfo[] props = list.FirstOrDefault().GetType().GetProperties();

            foreach (PropertyInfo prop in props) {
                tableHeaders.Add(prop.Name);
            }
            var table = new ConsoleTable(tableHeaders.ToArray());

            foreach (var obj in list)
            {
                List<string> objValues = new List<string>();
                
                table.AddRow(obj.ToArray());
            }

            table.Write();
            Console.WriteLine();
        }
    }
}
