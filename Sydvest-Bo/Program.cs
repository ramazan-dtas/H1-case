﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine("Sydvest-Bo  - H1 Case");
            Console.SetCursorPosition(30, 15);
            Console.WriteLine("Are you [Owner], or will you make a [reservation] or will you [create] sommerhus ");
            Console.SetCursorPosition(40, 20);
            Console.WriteLine("[will you search on a sommerhus]");
            string svar = Console.ReadLine().ToLower();
            Console.Clear();
            //Here it will save the input and make it automatic input lowercase 

            //here will it check if the input is "ejere"
            if (svar == "owner")
            {
                //here i convert the class Sommerhusejere to P1
                Sommerhusejere p1 = new Sommerhusejere();
                Console.WriteLine("will you create, change or delete");
                //Here i convert the ansver to lower case
                svar = Console.ReadLine().ToLower();
                //Here i says if the answer is "oprette" 
                if (svar == "create")
                {
                    p1.Opret();
                    Console.ReadKey();
                    Console.Clear();
                }
                //Here i says if the answer is "rette" 
                if (svar == "change")
                {
                    p1.Rette();
                    Console.ReadKey();
                    Console.Clear();
                }
                //Here i says if the answer is "slette" 
                if (svar == "delete")
                {
                    p1.Slette();
                    Console.ReadKey();
                    Console.Clear();
                }
                //Here i says if user wants to reserve a house
                if (svar == "reservation")
                {
                    //here i convert the class ResSommerhus to p1
                    ResSommerhus p2 = new ResSommerhus();

                    Console.WriteLine("will you create, change or delete a reservation");
                    svar = Console.ReadLine();

                    if (svar == "create")
                    {
                        p2.Opret();
                        Console.ReadKey();
                        Console.Clear();
                    }

                    if (svar == "change")
                    {
                        p2.Rette();
                        Console.ReadKey();
                        Console.Clear();
                    }

                    if (svar == "delete")
                    {
                        p2.Slette();
                        Console.ReadKey();
                        Console.Clear();
                    }

                }

                else if (svar == "create")
                {
                    //here i convert the class Sommerhus to p1
                    Sommerhus p3 = new Sommerhus();

                    Console.WriteLine("will you create, change or delete a summer house");
                    svar = Console.ReadLine();

                    if (svar == "create")
                    {
                        p3.Opret();
                        Console.ReadKey();
                        Console.Clear();
                    }

                    if (svar == "change")
                    {
                        p1.Rette();
                        Console.ReadKey();
                        Console.Clear();
                    }

                    if (svar == "delete")
                    {
                        p1.Slette();
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

            }

            else if (svar == "searching")
            {
                Søgning p4 = new Søgning();
                int svar2;
                Console.WriteLine("write you zip number to search on a summer house at zip code you want to live in ");
                //here i say integer svar2 has now the values in ReadLineInt inside him
                //now it take the input from WriteLine controls if there is letters in it
                svar2 = ReadLineInt();
                //Here i saysv if the input is over 1000 and under 9999 it should go up 
                if (svar2 >= 1000 && svar2 <= 9999)
                {
                    Console.WriteLine("Jens er sej");
                    p4.søg();
                }
                else
                {
                    Console.WriteLine("Try again");

                }
            }
        }
        class Sommerhusejere
        {
            public void Opret()
            {
                Console.WriteLine("oprette");
                Console.ReadKey();
                Console.Clear();
            }

            public void Rette()
            {
                Console.WriteLine("rette");
                Console.ReadKey();
                Console.Clear();
            }

            public void Slette()
            {
                Console.WriteLine("slette");
                Console.ReadKey();
                Console.Clear();
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
                Console.WriteLine("opretteres");
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
                Console.WriteLine("Det er frederiksberg");
            }
        }
    }
}