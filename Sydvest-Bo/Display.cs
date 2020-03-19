using ConsoleTables;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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
        public static void WriteTable (IEnumerable<object> list)
        {
            if (list.Count() > 0)
            {
                List<string> tableHeaders = new List<string>();
                Keys keys = new Keys();
                

                var d = (IDictionary<string, object>)list.FirstOrDefault();

                foreach (var key in d.Keys)
                {
                    tableHeaders.Add(key);
                }
                var table = new ConsoleTable(tableHeaders.ToArray());
                
                foreach (var obj in list)
                {
                    List<string> objValues = new List<string>();
                    var objPair = (IDictionary<string, object>)obj;
                    foreach (var key in d.Keys)
                    {
                        objValues.Add(Convert.ToString(objPair[key]));
                    }
                    table.AddRow(objValues.ToArray());
                }

                table.Write();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No Values found");
            }
        }
    }

    public class Keys
    {
        public List<string> GetPropertyKeysForDynamic(object dynamicToGetPropertiesFor)
        {
            var jObject = (JObject) JToken.FromObject(dynamicToGetPropertiesFor);
            Dictionary<string, object> values = jObject.ToObject<Dictionary<string, object>>();
            List<string> toReturn = new List<string>();
            foreach (string key in values.Keys)
            {
                toReturn.Add(key);
            }
            return toReturn;
        }
    }
}
