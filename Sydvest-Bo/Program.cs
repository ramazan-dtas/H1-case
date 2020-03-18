using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sydvest_Bo.Models;
using Sydvest_Bo.Classes;

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
            string connetionString = @"Data Source=localhost;Initial Catalog=Sydvest Bo;User ID=DESKTOP-G8VN2QF\drama;Password=;Integrated Security=true";
            SqlConnection conn = new SqlConnection(connetionString);
            try
            {
                //DataAccess db = new DataAccess();

                //db.InsertConsultant(new Consultant { name = "Josh Dun" });
                //Display.WriteTable(DataAccess.Consultants.Get(""));
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
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
            Console.WriteLine("Sydvest-Bo  - H1 Case\n");
            Console.WriteLine("[Summer Houses] [House Owners] [Reservations] [Areas] [Seasons] [Inspectors]");

            //Here it will save the input and make it automatic input lowercase 
            string answer = Console.ReadLine().ToLower();
            Console.Clear();
            
            if (answer == "summer houses")
            {
                Console.WriteLine("[View] [Change] [Create] [Delete]");

                string action = Console.ReadLine().ToLower();
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
                        //Make a search
                    }
                    else if (searchRes == "n")
                    {
                        //Show everything
                    }
                }
                
                if (action == "change")
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
                    if(ChangeAns == "n")
                    {
                        //if user press n
                    }

                }
                
                if (action == "create")
                {
                    string ChangeAns = "";
                    while (ChangeAns != "y" && ChangeAns != "n")
                    {
                        Console.Write("Will you create a house? ");
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
                 
                if (action == "delete")
                {
                    string ChangeAns = "";
                    while (ChangeAns != "y" && ChangeAns != "n")
                    {
                        Console.Write("Will you delete a house? ");
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
                    }
                    if (ChangeAns == "n")
                    {
                        //if user press n
                    }
                }
            }
            //User wants to manipulate the reservations
            if (answer == "house owners")
            {
                Console.WriteLine("[View] [Change] [Create] [Delete]");
                string action = Console.ReadLine().ToLower();

                if (action == "view")
                {
                    string searchRes = "";
                    while (searchRes != "y" && searchRes != "n")
                    {
                        Console.Write("Would you like to search for a specific house owner? (Y/N): ");
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

                if (action == "change")
                {

                }

                if (action == "create")
                {

                }

                if (action == "delete")
                {

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

                if (action == "change")
                {

                }

                if (action == "create")
                {

                }

                if (action == "delete")
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

                if (action == "change")
                {

                }

                if (action == "create")
                {

                }

                if (action == "delete")
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

                if (action == "change")
                {

                }

                if (action == "create")
                {

                }

                if (action == "delete")
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

                if (action == "change")
                {

                }

                if (action == "create")
                {

                }

                if (action == "delete")
                {

                }

            }
        }

        class Sommerhus
        {
            public void Opret()
            {
                Console.WriteLine("oprettehus");
                Console.ReadKey();
                Console.Clear();
            }

            public void Rette()
            {
                Console.WriteLine("rettehus");
                Console.ReadKey();
                Console.Clear();
            }

            public void Slette()
            {
                Console.WriteLine("slettehus");
                Console.ReadKey();
                Console.Clear();
            }
        }

        class ResSommerhus
        {
            public void Opret()
            {
                //When user write zep code it will check if there is only numbers if there is not numbers it will come with error message
                int Safety;
                Console.WriteLine("Write you zep code ");
                Safety = ReadLineInt();
                //here will it check if the number is higher then 1000 or lower then 9999
                if (Safety >= 1000 && Safety <= 9999)
                {
                    Console.WriteLine("Zep code is correct");
                }
                else
                {
                    Console.WriteLine("you zep code need to be between 1000 and 9999");
                    Opret();
                }

                Console.WriteLine("Write the adress ");
                string adressAns = Console.ReadLine();


                Console.ReadKey();
                Console.Clear();
            }

            public void Rette()
            {
                Console.WriteLine("retteres");
                Console.ReadKey();
                Console.Clear();
            }

            public void Slette()
            {
                Console.WriteLine("sletter");
                Console.ReadKey();
                Console.Clear();
            }
        }

        class Søgning
        {
            public void søg()
            {
                int svar2;
                Console.WriteLine("write you zip number to search on a summer house at zip code you want to live in ");
                //here i say integer svar2 has now the values in ReadLineInt inside him
                //now it take the input from WriteLine controls if there is letters in it
                svar2 = ReadLineInt();
                //Here i saysv if the input is over 1000 and under 9999 it should go up 
                if (svar2 >= 1000 && svar2 <= 9999)
                {
                    Console.WriteLine("Jens er sej");
                    
                }
                else
                {
                    Console.WriteLine("Try again");

                }
            }
        }
    }
}