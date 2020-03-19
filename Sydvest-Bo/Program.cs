using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sydvest_Bo.Models;
using System.Configuration;
using System.Reflection;
using Sydvest_Bo.Classes;

namespace Sydvest_Bo
{
    class Program
    {
        //Shows a list of matched elements in a given table
        public static void View (object obj, string tableName)
        {
            DbAccess dbConn = new DbAccess();

            string searchRes = "";
            while (searchRes != "y" && searchRes != "n") //Will loop until an answer is given (Y/N)
            {
                Console.Write("Would you like to filter your search? (Y/N): ");
                searchRes = Console.ReadLine().ToLower();
            }
            if (searchRes == "y") //Search with filter
            {
                Console.Write("Insert the search text: ");
                string searchTxt = Console.ReadLine();
                Display.WriteTable(dbConn.GetList(obj, tableName, searchTxt));
            }
            else if (searchRes == "n") //No filter, display all elements
            {
                Display.WriteTable(dbConn.GetList(obj, tableName, ""));
            }
        }

        //Adds a new entry to a given table
        public static void Create (object obj, string tableName)
        {
            DbAccess dbConn = new DbAccess();

            PropertyInfo[] props = obj.GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                if (prop.Name != "id")
                {
                    Console.Write($"{prop.Name}: ");

                    var propType = obj.GetType().GetProperty(prop.Name).PropertyType.Name;

                    if (propType == "String")
                        obj.GetType().GetProperty(prop.Name).SetValue(obj, Console.ReadLine());
                    else if (propType == "Int32")
                        obj.GetType().GetProperty(prop.Name).SetValue(obj, Convert.ToInt32(Console.ReadLine()));
                    else if (propType == "Double")
                        obj.GetType().GetProperty(prop.Name).SetValue(obj, Convert.ToDouble(Console.ReadLine()));
                    else if (propType == "DateTime")
                    {
                        //Handle datetimes
                    }
                }
            }

            dbConn.CreateObj(obj, tableName);
        }

        //Updates an entry in a given table
        public static void Update (object obj, string tableName)
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

                        Console.WriteLine("All object props here");

                        PropertyInfo[] props = obj.GetType().GetProperties();
                        
                        //Gets the desired property to be changed - Will loop until the user exists
                        string propToChange = "";
                        while (true)
                        {
                            Console.Write("Which property would you like to change? (type 'exit' to exit the loop): ");
                            propToChange = Console.ReadLine();

                            if (propToChange == "exit") { break; } //Breaks out of the loop if the user types 'exit'

                            if (propToChange != "id") //Checks if the user is trying to update the 'id'
                            {
                                if (Array.Find(props, x => x.Name == propToChange) != null) //Property exists and is not 'id' - all good
                                {
                                    //Gets the new value for the object property
                                    Console.Write($"Insert the new value for {propToChange}: ");
                                    string newVal = Console.ReadLine();

                                    var d = (IDictionary<string, object>)entries.FirstOrDefault();
                                    obj.GetType().GetProperty(propToChange).SetValue(obj, newVal);

                                    dynamic newObj = obj;


                                    newObj.id = Convert.ToInt32(d["id"]);

                                    //Attempts to update the object in the Db
                                    if(!dbConn.ChangeObj(newObj, tableName)) //Update failed
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Something went wrong. Make sure the foreign keys being updated do exist in the other tables...");
                                    }
                                    else //Update succeded
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"'{propToChange}' successfully updated to '{newVal}'!");
                                        obj = newObj;
                                    }
                                }
                                else //Property not found
                                {
                                    Console.Clear();
                                    Console.WriteLine("The property entered doesn't exist or was misspelled");
                                }
                            }
                            else //User tried to update the id
                            {
                                Console.Clear();
                                Console.WriteLine("Cannot change primary key 'id'...");
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
                        while (deleteAns != "y" && deleteAns != "n") //Gets a confirmation from the user
                        {
                            Console.Write("Are you sure you want to delete this entry? (Y/N): ");
                            deleteAns = Console.ReadLine().ToLower();
                        }

                        if (deleteAns == "y") //Proceed with deletion
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
        
        static void Main(string[] args)
        {
            /*
            bool userAllowed = false;

            while (userAllowed == false)
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                //Check if exists in database
                if (username == "admin" && password == "admin")
                {
                    userAllowed = true;
                }
                else
                {
                    Console.WriteLine("Username or password incorrect, try again.");
                    Console.ReadKey();
                }
            }
            */

            ConsoleKey mainKey = new ConsoleKey();

            while (mainKey != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Press any key to start, Escape to exit...");
                mainKey = Console.ReadKey().Key;

                Console.Clear();

                if (mainKey != ConsoleKey.Escape)
                {
                    Console.WriteLine("Sydvest-Bo  - H1 Case\n");
                    Console.WriteLine("[Houses] [Owners] [Reservations] [Areas] [Seasons] [Inspectors] [Consultants]");

                    //Here it will save the input and make it automatic input lowercase 
                    string answer = Console.ReadLine().ToLower();
                    Console.Clear();

                    object controlObj;
                    string targetTable;

                    bool validAnswer = true;
                    switch (answer)
                    {
                        case "houses":
                            controlObj = new House();
                            targetTable = "Houses";
                            break;

                        case "owners":
                            controlObj = new HouseOwner();
                            targetTable = "HouseOwners";
                            break;

                        case "reservations":
                            controlObj = new Reservation();
                            targetTable = "Reservations";
                            break;

                        case "areas":
                            controlObj = new Area();
                            targetTable = "Areas";
                            break;

                        case "seasons":
                            controlObj = new Season();
                            targetTable = "Seasons";
                            break;

                        case "inspectors":
                            controlObj = new Inspector();
                            targetTable = "Inspectors";
                            break;

                        case "consultants":
                            controlObj = new Consultant();
                            targetTable = "Consultants";
                            break;

                        case "weeks":
                            controlObj = new Week();
                            targetTable = "Weeks";
                            break;

                        default:
                            controlObj = new object();
                            targetTable = "";
                            validAnswer = false;
                            break;
                    }

                    if (validAnswer)
                    {
                        Console.WriteLine("[View] [Create] [Update] [Delete]");
                        string action = Console.ReadLine().ToLower();

                        switch (action)
                        {
                            case "view":
                                View(controlObj, targetTable);
                                break;

                            case "create":
                                Create(controlObj, targetTable);
                                break;

                            case "update":
                                Update(controlObj, targetTable);
                                break;

                            case "delete":
                                Delete(controlObj, targetTable);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No match, try again...");
                    }

                    Console.ReadKey();
                }
            }

            //if (answer == "houses")
            //{
            //    Console.WriteLine("[View] [Change] [Create] [Delete]");
            //    string action = Console.ReadLine().ToLower();

            //    DataAccess.Houses dbHouses = new DataAccess.Houses();

            //    if (action == "view")
            //    {
            //        View(new House(), "Houses");
            //    }

            //    else if (action == "change")
            //    {
            //        string ChangeAns = "";
            //        while (ChangeAns != "y" && ChangeAns != "n")
            //        {
            //            Console.Write("What will you change? ");
            //            ChangeAns = Console.ReadLine().ToLower();
            //        }

            //        if (ChangeAns == "y")
            //        {
            //            if (ChangeAns == "address")
            //            {
            //                //Changing address
            //            }
            //            if (ChangeAns == "name")
            //            {
            //                //Changing house name
            //            }
            //            if (ChangeAns == "area")
            //            {
            //                //Changing area
            //            }
            //            if (ChangeAns == "owner")
            //            {
            //                //Changing house owner
            //            }
            //            if (ChangeAns == "inspector")
            //            {
            //                //Changing inspector
            //            }
            //            if (ChangeAns == "standard")
            //            {
            //                //Changing standard
            //            }
            //        }
            //        if (ChangeAns == "n")
            //        {
            //            //if user press n
            //        }

            //    }

            //    else if (action == "create")
            //    {
            //    }
                 
            //    else if (action == "delete")
            //    {
            //        string ChangeAns = "";

            //        if (ChangeAns == "address")
            //        {
            //            //Changing address
            //        }
            //        else if (ChangeAns == "name")
            //        {
            //            //Changing house name
            //        }
            //    }
            //}

            //else if (answer == "owners")
            //{
            //    Console.WriteLine("[View] [Change] [Create] [Delete]");
            //    string action = Console.ReadLine().ToLower();

            //    if (action == "view")
            //    {
            //        View(new HouseOwner(), "HouseOwners");
            //    }

            //    else if (action == "change")
            //    {
            //        Update(new HouseOwner(), "houseOwners");
            //    }

            //    else if (action == "create")
            //    {
            //        Create(new HouseOwner(), "HouseOwners");
            //    }

            //    else if (action == "delete")
            //    {
            //        Delete(new HouseOwner(), "HouseOwners");
            //    }

            //}
            
            //else if (answer == "areas")
            //{
            //    Console.WriteLine("[View] [Update] [Create] [Delete]");
            //    string action = Console.ReadLine().ToLower();

            //    if (action == "view")
            //    {
            //        View(new Area(), "Areas");
            //    }

            //    else if (action == "update")
            //    {
            //        Update(new Area(), "Areas");
            //    }

            //    else if (action == "create")
            //    {
            //        Create(new Area(), "Areas");
            //    }

            //    else if (action == "delete")
            //    {
            //        Delete(new Area(), "Areas");
            //    }

            //}
            
            //else if (answer == "reservations")
            //{
            //    Console.WriteLine("[View] [Change] [Update] [Delete]");
            //    string action = Console.ReadLine().ToLower();

            //    if (action == "view")
            //    {
            //        View(new Reservation(), "Reservations");
            //    }

            //    else if (action == "update")
            //    {
            //        Update(new Reservation(), "Reservations");
            //    }

            //    else if (action == "create")
            //    {
            //        Create(new Reservation(), "Reservations");
            //    }

            //    else if (action == "delete")
            //    {
            //        Delete(new Reservation(), "Reservations");
            //    }

            //}
            
            //else if (answer == "seasons")
            //{
            //    Console.WriteLine("[View] [Change] [Create] [Delete]");
            //    string action = Console.ReadLine().ToLower();

            //    if (action == "view")
            //    {
            //        View(new Season(), "Seasons");
            //    }

            //    else if (action == "update")
            //    {
            //        Update(new Season(), "Seasons");
            //    }

            //    else if (action == "create")
            //    {
            //        Create(new Season(), "Seasons");
            //    }

            //    else if (action == "delete")
            //    {
            //        Delete(new Season(), "Seasons");
            //    }
            //}
            
            //else if (answer == "inspectors")
            //{
            //    Console.WriteLine("[View] [Change] [Create] [Delete]");
            //    string action = Console.ReadLine().ToLower();

            //    if (action == "view")
            //    {
            //        View(new Inspector(), "Inspectors");
            //    }

            //    else if (action == "update")
            //    {
            //        Update(new Inspector(), "Inspectors");
            //    }

            //    else if (action == "create")
            //    {
            //        Create(new Inspector(), "Inspectors");
            //    }

            //    else if (action == "delete")
            //    {
            //        Delete(new Inspector(), "Inspectors");
            //    }

            //}

            //else if (answer == "consultants")
            //{
            //    Console.WriteLine("[View] [Change] [Create] [Delete]");
            //    string action = Console.ReadLine().ToLower();

            //    if (action == "view")
            //    {
            //        View(new Consultant(), "Consultants");
            //    }

            //    else if (action == "update")
            //    {
            //        Update(new Consultant(), "Consultants");
            //    }

            //    else if (action == "create")
            //    {
            //        Create(new Consultant(), "Consultants");
            //    }

            //    else if (action == "delete")
            //    {
            //        Delete(new Consultant(), "Consultants");
            //    }

            //}

            Console.ReadKey();
        }   
    }
}