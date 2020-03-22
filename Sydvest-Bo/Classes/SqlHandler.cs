using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo.Classes
{
    public static class SqlHandler
    {
        //Shows a list of matched elements in a given table
        public static void View(object obj, string tableName)
        {
            DbAccess dbConn = new DbAccess();

            string searchRes = "";
            while (searchRes != "yes" && searchRes != "no") //Will loop until an answer is given (Y/N)
            {
                Console.WriteLine("Would you like to filter your search? ");
                searchRes = Functionality.Select(new string[] { "YES", "NO" }).ToLower();
            }

            Console.Clear();

            if (searchRes == "yes") //Search with filter
            {
                Console.Write("Insert the search text: ");
                string searchTxt = Console.ReadLine();
                Display.WriteTable(dbConn.GetList(obj, tableName, searchTxt));
            }
            else if (searchRes == "no") //No filter, display all elements
            {
                Display.WriteTable(dbConn.GetList(obj, tableName, ""));
            }
        }

        //Adds a new entry to a given table
        public static void Create(object obj, string tableName)
        {
            DbAccess dbConn = new DbAccess();

            PropertyInfo[] props = obj.GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "id")
                {
                    var propType = obj.GetType().GetProperty(prop.Name).PropertyType.Name;

                    if (propType == "DateTime")
                    {
                        string dateAsStr = "";
                        CultureInfo provider = CultureInfo.InvariantCulture;
                        DateTime date = new DateTime();

                        //Will loop until a valid date is passed
                        bool parsed = false;
                        while (!parsed)
                        {
                            Console.Write($"{prop.Name} (DD/MM/YYYY): ");
                            dateAsStr = Console.ReadLine();

                            parsed = DateTime.TryParseExact(dateAsStr, "dd/MM/yyyy", provider, DateTimeStyles.None, out date);

                            var a = "";
                        }
                        
                        obj.GetType().GetProperty(prop.Name).SetValue(obj, date);
                    }
                    else
                    {
                        Console.Write($"{prop.Name}: ");
                        if (propType == "String")
                            obj.GetType().GetProperty(prop.Name).SetValue(obj, Console.ReadLine());
                        else if (propType == "Int32")
                            obj.GetType().GetProperty(prop.Name).SetValue(obj, Convert.ToInt32(Console.ReadLine()));
                        else if (propType == "Double")
                            obj.GetType().GetProperty(prop.Name).SetValue(obj, Convert.ToDouble(Console.ReadLine()));
                    }

                }
            }

            dbConn.CreateObj(obj, tableName);
        }

        //Updates an entry in a given table
        public static void Update(object obj, string tableName, Type objType)
        {
            DbAccess dbConn = new DbAccess();

            bool confirmed = false;
            while (!confirmed)
            {
                Console.Write("Please insert a unique identifier for the change: ");
                string searchTxt = Console.ReadLine();

                try
                {
                    //Gets a list of any entries where a field matches the searchTxt
                    List<dynamic> entries = dbConn.GetList(obj, tableName, searchTxt);

                    if (entries.Count() < 1) //No entries found
                    {
                        Console.WriteLine("No entries matched the filter...");
                    }
                    else if (entries.Count() > 1) //no single match
                    {
                        Console.Clear();
                        Display.WriteTable(entries);
                        Console.WriteLine($"Your query returned {entries.Count()} entries...");
                    }
                    else //All good - a single match
                    {
                        confirmed = true;

                        Console.Clear();
                        Display.WriteTable(entries);

                        PropertyInfo[] props = obj.GetType().GetProperties();
                        List<string> propNames = new List<string>();

                        foreach (PropertyInfo prop in props)
                        {
                            if (prop.Name != "id")
                            {
                                propNames.Add(prop.Name);
                            }
                        }
                        propNames.Add("Exit");

                        //Gets the desired property to be changed - Will loop until the user exists
                        string propToChange = "";
                        while (true)
                        {
                            Console.WriteLine("Which property would you like to change? (press 'exit' to exit the loop): ");
                            propToChange = Functionality.Select(propNames.ToArray());

                            //Breaks out of the loop if the user types 'exit'
                            if (propToChange == "Exit")
                                break;
                            
                            if (Array.Find(props, x => x.Name == propToChange) != null) //Property exists and is not 'id' - all good
                            {
                                //Gets the new value for the object property
                                Console.Write($"Insert the new value for {propToChange}: ");
                                string newVal = Console.ReadLine();

                                var d = (IDictionary<string, object>)entries.FirstOrDefault();

                                foreach (var key in d.Keys)
                                {
                                    obj.GetType().GetProperty(key).SetValue(obj, d[key]);
                                }

                                obj.GetType().GetProperty(propToChange).SetValue(obj, newVal);

                                //Attempts to update the object in the Db
                                if (!dbConn.ChangeObj(obj, tableName)) //Update failed
                                {
                                    Console.Clear();
                                    Console.WriteLine("Something went wrong. Make sure the foreign keys being updated do exist in the other tables...");
                                }
                                else //Update succeded
                                {
                                    Console.Clear();
                                    Console.WriteLine($"'{propToChange}' successfully updated to '{newVal}'!");
                                    Console.ReadKey();
                                }
                            }
                            else //Property not found
                            {
                                Console.Clear();
                                Console.WriteLine("The property entered doesn't exist or was misspelled");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    confirmed = true;
                }
            }
        }

        //Deletes an entry in a given table
        public static void Delete(object obj, string tableName)
        {
            DbAccess dbConn = new DbAccess();

            bool confirmed = false;
            while (!confirmed)
            {
                Console.Write("Please insert a unique identifier for the deletion: ");
                string searchTxt = Console.ReadLine();

                try
                {
                    //Gets a list of any entries where a field matches the searchTxt
                    List<dynamic> entries = dbConn.GetList(obj, tableName, searchTxt);

                    if (entries.Count() < 1) //No entries found
                    {
                        Console.Clear();
                        Console.WriteLine("No entries matched the filter...");
                    }
                    else if (entries.Count() > 1) //More than 1 match - Not unique
                    {
                        Console.Clear();
                        Display.WriteTable(entries);
                        Console.WriteLine($"Your query returned {entries.Count()} entries...");
                    }
                    else //All good - a single match
                    {
                        confirmed = true;

                        Console.Clear();
                        Display.WriteTable(entries); //Shows the item about to be deleted

                        string deleteAns = "";
                        while (deleteAns != "yes" && deleteAns != "no") //Gets a confirmation from the user
                        {
                            Console.WriteLine("Are you sure you want to delete this entry?: ");
                            deleteAns = Functionality.Select(new string[] { "NO", "YES" }).ToLower();
                        }

                        if (deleteAns == "yes") //Proceed with deletion
                        {
                            //Sets the new object's id to be the one from the selected entry for deletion
                            var d = (IDictionary<string, object>)entries.FirstOrDefault();
                            obj.GetType().GetProperty("id").SetValue(obj, d["id"]);

                            //Tries to delete the item from the given table
                            if (dbConn.DeleteObj(obj, tableName)) //Success
                            {
                                Console.WriteLine("Deletion successfull! Press any key to return to start menu...");
                            }
                            else //Failure
                            {
                                Console.WriteLine("Something went wrong when trying to delete this item.");
                            }

                            Console.Clear();
                        }
                        else //Deletion cancelled
                        {
                            Console.Clear();
                            Console.WriteLine("Deletion cancelled, press any key to go back to the start...");
                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }
    }
}
