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
        static void Main(string[] args)
        {
            bool userAllowed = false;
            while (!userAllowed)
            {
                Console.Write("Username: ");
                string un = Console.ReadLine();
                Console.Write("Password: ");
                string pwd = Functionality.ReadPassword();

                //Checks for a match
                if (un == "admin" && pwd == "admin") //All ok
                {
                    userAllowed = true;
                }
                else //Wrong un/pwd - try again
                {
                    Console.Clear();
                    Console.WriteLine("Username or password incorrect, try again...");
                }
            }

            ConsoleKey mainKey = new ConsoleKey();
            while (mainKey != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Press any key to start, Escape to exit...");

                if ((mainKey = Console.ReadKey().Key) != ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("Sydvest-Bo  - H1 Case\n");

                    //Save the input and make it lowercase
                    string answer = Functionality.Select(new string[] { "Houses", "Owners", "Reservations", "Areas", "Seasons", "Inspectors", "Consultants", "Standards", "Weeks" }).ToLower();
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
                            targetTable = "SeasonPrices";
                            break;

                        case "inspectors":
                            controlObj = new Inspector();
                            targetTable = "Inspectors";
                            break;

                        case "consultants":
                            controlObj = new Consultant();
                            targetTable = "Consultants";
                            break;

                        case "standards":
                            controlObj = new Standard();
                            targetTable = "Standards";
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
                        //Save the input and make it lowercase
                        string action = Functionality.Select(new string[] { "View", "Create", "Update", "Delete" }).ToLower();
                        Console.Clear();

                        switch (action)
                        {
                            case "view":
                                SqlHandler.View(controlObj, targetTable);
                                break;

                            case "create":
                                SqlHandler.Create(controlObj, targetTable);
                                break;

                            case "update":
                                SqlHandler.Update(controlObj, targetTable, typeof(Area));
                                break;

                            case "delete":
                                SqlHandler.Delete(controlObj, targetTable);
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
        }   
    }
}