using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sydvest_Bo.Models;
using System.Configuration;

namespace Sydvest_Bo
{
    class Program
    {
        public static int ReadLineInt()
        {
            //Here it is a security for not write letters 
            int tal;
            while (!int.TryParse(Console.ReadLine(), out tal))
            {
                Console.WriteLine("Du skal svarer med tal og ikke bogstaver");
            }
            return tal;
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


            DbAccess dbConn = new DbAccess();
            //Display.WriteTable(test.GetList(new HouseOwner(), "HouseOwners", ""));

            Console.WriteLine("Sydvest-Bo  - H1 Case\n");
            Console.WriteLine("[Houses] [Owners] [Reservations] [Areas] [Seasons] [Inspectors] [Consultants]");

            //Here it will save the input and make it automatic input lowercase 
            string answer = Console.ReadLine().ToLower();
            Console.Clear();
            
            if (answer == "houses")
            {
                Console.WriteLine("[View] [Change] [Create] [Delete]");
                string action = Console.ReadLine().ToLower();

                DataAccess.Houses dbHouses = new DataAccess.Houses();

                if (action == "view")
                {
                    string searchRes = "";
                    while (searchRes != "y" && searchRes != "n")
                    {
                        Console.Write("Would you like to search for a specific house? (Y/N): ");
                        searchRes = Console.ReadLine().ToLower();
                    }
                    if (searchRes == "y")
                    {
                        Console.Write("Insert the search text:");
                        string searchTxt = Console.ReadLine();
                        Display.WriteTable(dbConn.GetList(new House(), "Houses", searchTxt));
                    }
                    else if (searchRes == "n")
                    {
                        Display.WriteTable(dbConn.GetList(new House(),"Houses", ""));
                    }
                }

                else if (action == "change")
                {
                    string ChangeAns = "";
                    while (ChangeAns != "y" && ChangeAns != "n")
                    {
                        Console.Write("What will you change? ");
                        ChangeAns = Console.ReadLine().ToLower();
                    }

                    if (ChangeAns == "y")
                    {
                        if (ChangeAns == "address")
                        {
                            //Changing address
                        }
                        if (ChangeAns == "name")
                        {
                            //Changing house name
                        }
                        if (ChangeAns == "area")
                        {
                            //Changing area
                        }
                        if (ChangeAns == "owner")
                        {
                            //Changing house owner
                        }
                        if (ChangeAns == "inspector")
                        {
                            //Changing inspector
                        }
                        if (ChangeAns == "standard")
                        {
                            //Changing standard
                        }
                    }
                    if (ChangeAns == "n")
                    {
                        //if user press n
                    }

                }

                else if (action == "create")
                {
                }
                 
                else if (action == "delete")
                {
                    string ChangeAns = "";

                    if (ChangeAns == "address")
                    {
                        //Changing address
                    }
                    else if (ChangeAns == "name")
                    {
                        //Changing house name
                    }
                }
            }
            //User wants to manipulate the house owners
            else if (answer == "owners")
            {
                Console.WriteLine("[View] [Change] [Create] [Delete]");
                string action = Console.ReadLine().ToLower();

                DataAccess.HouseOwners dbOwners = new DataAccess.HouseOwners();

                if (action == "view") //Get a list over the matched house owners
                {
                    string searchRes = "";
                    while (searchRes != "y" && searchRes != "n")
                    {
                        Console.Write("Would you like to search for a specific owner? (Y/N): ");
                        searchRes = Console.ReadLine().ToLower();
                    }
                    if (searchRes == "y")
                    {
                        Console.Write("Insert the search text:");
                        string searchTxt = Console.ReadLine();
                        Display.WriteTable(dbConn.GetList(new HouseOwner(), "HouseOwners", searchTxt));
                    }
                    else if (searchRes == "n")
                    {
                        Display.WriteTable(dbConn.GetList(new HouseOwner(), "HouseOwners", ""));
                    }
                }

                else if (action == "change") //Update a specific house owner
                {
                    bool confirmed = false;
                    while (!confirmed)
                    {
                        Console.Write("Please insert a unique identifier for the owner: ");
                        string searchTxt = Console.ReadLine();

                        try
                        {
                            //Gets a list of any entries where a field matches the searchTxt
                            List<dynamic> entries = dbConn.GetList(new HouseOwner(), "HouseOwners", searchTxt);

                            if (entries.Count() < 1) //No entries found
                            {
                                Console.WriteLine("No entries matched the search text...");
                            }
                            else if (entries.Count() > 1) //no single match
                            {
                                Console.Clear();
                                Display.WriteTable(entries);
                                Console.WriteLine("Your query returned 2 or more entries...");
                            }
                            else //All good - a single match
                            {
                                Console.Write("New Name: ");
                                HouseOwner newObj = new HouseOwner { id = entries.FirstOrDefault().id, name = Console.ReadLine() };
                                dbConn.ChangeObj(newObj, "HouseOwners");
                                confirmed = true;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey();
                            confirmed = true;
                        }
                    }
                }

                else if (action == "create")
                {
                    Console.Write("Name: ");
                    string ownerName = Console.ReadLine();

                    HouseOwner newOwner = new HouseOwner { name = ownerName };
                    dbConn.CreateObj(newOwner, "HouseOwners");
                }

                else if (action == "delete")
                {
                    bool confirmed = false;
                    while (!confirmed)
                    {
                        Console.Write("Please insert a unique identifier for the owner: ");
                        string searchTxt = Console.ReadLine();

                        try
                        {
                            List<dynamic> entries = dbConn.GetList(new HouseOwner(), "HouseOwners", searchTxt);

                            if (entries.Count() < 1) //No entries found
                            {
                                Console.WriteLine("No entries matched the search text...");
                            }
                            else if (entries.Count() > 1) //no single match
                            {
                                Console.Clear();
                                Display.WriteTable(entries);
                                Console.WriteLine("Your query returned 2 or more entries...");
                            }
                            else //All good - a single match
                            {
                                HouseOwner toBeDeleted = new HouseOwner { id = entries.FirstOrDefault().id };
                                dbConn.DeleteObj(toBeDeleted, "HouseOwners");
                                confirmed = true;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey();
                            confirmed = true;
                        }
                    }
                }

            }

            //User wants to manipulate the areas
            if (answer == "areas")
            {
                Console.WriteLine("[View] [Change] [Create] [Delete]");
                string action = Console.ReadLine().ToLower();

                if (action == "view")
                {
                    string searchRes = "";
                    while (searchRes != "y" && searchRes != "n")
                    {
                        Console.Write("Would you like to search for a specific area? (Y/N): ");
                        searchRes = Console.ReadLine().ToLower();
                    }
                    if (searchRes == "y")
                    {
                        //Make a search
                    }
                    else if (searchRes == "n")
                    {
                        //Show everything
                    }
                }

                else if (action == "change")
                {

                }

                else if (action == "create")
                {

                }

                else if (action == "delete")
                {

                }

            }

            //User wants to manipulate the reservations
            if (answer == "reservations")
            {
                Console.WriteLine("[View] [Change] [Create] [Delete]");
                string action = Console.ReadLine().ToLower();

                if (action == "view")
                {
                    string searchRes = "";
                    while (searchRes != "y" && searchRes != "n")
                    {
                        Console.Write("Would you like to search for a specific reservation? (Y/N): ");
                        searchRes = Console.ReadLine().ToLower();
                    }
                    if (searchRes == "y")
                    {
                        //Make a search
                    }
                    else if (searchRes == "n")
                    {
                        //Show everything
                    }
                }

                else if (action == "change")
                {

                }

                else if (action == "create")
                {

                }

                else if (action == "delete")
                {
                    
                }

            }

            //User wants to manipulate the seasons & prices
            if (answer == "seasons")
            {
                Console.WriteLine("[View] [Change] [Create] [Delete]");
                string action = Console.ReadLine().ToLower();

                if (action == "view")
                {
                    string searchRes = "";
                    while (searchRes != "y" && searchRes != "n")
                    {
                        Console.Write("Would you like to search for a specific season? (Y/N): ");
                        searchRes = Console.ReadLine().ToLower();
                    }
                    if (searchRes == "y")
                    {
                        //Make a search
                    }
                    else if (searchRes == "n")
                    {
                        //Show everything
                    }
                }

                else if (action == "change")
                {

                }

                else if (action == "create")
                {

                }

                else if (action == "delete")
                {

                }

            }

            //User wants to manipulate the seasons & prices
            if (answer == "inspectors")
            {
                Console.WriteLine("[View] [Change] [Create] [Delete]");
                string action = Console.ReadLine().ToLower();

                if (action == "view")
                {
                    string searchRes = "";
                    while (searchRes != "y" && searchRes != "n")
                    {
                        Console.Write("Would you like to search for a specific inspector? (Y/N): ");
                        searchRes = Console.ReadLine().ToLower();
                    }
                    if (searchRes == "y")
                    {
                        //Make a search
                    }
                    else if (searchRes == "n")
                    {
                        //Show everything
                    }
                }

                else if (action == "change")
                {

                }

                else if (action == "create")
                {

                }

                else if (action == "delete")
                {

                }

            }
            
            //User wants to manipulate the seasons & prices
            if (answer == "consultants")
            {
                Console.WriteLine("[View] [Change] [Create] [Delete]");
                string action = Console.ReadLine().ToLower();

                if (action == "view")
                {
                    string searchRes = "";
                    while (searchRes != "y" && searchRes != "n")
                    {
                        Console.Write("Would you like to search for a specific consultant? (Y/N): ");
                        searchRes = Console.ReadLine().ToLower();
                    }
                    if (searchRes == "y")
                    {
                        //Make a search
                    }
                    else if (searchRes == "n")
                    {
                        //Show everything
                    }
                }

                else if (action == "change")
                {

                }

                else if (action == "create")
                {

                }

                else if (action == "delete")
                {

                }

            }

            Console.ReadKey();
        }   
    }
}