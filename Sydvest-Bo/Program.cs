using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Sydvest-Bo  - H1 Case");
            Console.SetCursorPosition(30, 15);
            Console.WriteLine("Er du [ejere], eller vil du [reservere] eller skal du [oprette] sommerhus ");
            Console.SetCursorPosition(40, 20); 
            Console.WriteLine("[Vil du søge på sommerhuse]");
            //Here it will save the input 
            string svar = Console.ReadLine().ToLower();

            if(svar == "ejere")
            {
                Sommerhusejere p1 = new Sommerhusejere();
                Console.WriteLine("Vil du oprette, rette eller slette");
                svar = Console.ReadLine();
                if(svar == "oprette")
                {
                    p1.Opret();
                }

                if(svar == "rette")
                {
                    p1.Rette();
                }

                if(svar == "slette")
                {
                    p1.Slette();
                }
            }
            
            if(svar == "reserverer")
            {
                ResSommerhus p1 = new ResSommerhus();

                Console.WriteLine("Vil du oprette, rette eller slette");
                svar = Console.ReadLine();

                if(svar == "oprette")
                {
                    p1.Opret();
                }

                if(svar == "rette")
                {
                    p1.Rette();
                }

                if(svar == "slette")
                {
                    p1.Slette();
                }
            }
            if(svar == "søg")
            {
                Søgning p1 = new Søgning();
                Console.WriteLine("Indtast det postnummer du vil søge bolig ");
                int post = Convert.ToInt32(Console.ReadLine());

                if(post == 2200)
                {
                    p1.søg();
                }
            }
        }
        class Sommerhusejere
        {
            public void Opret()
            {
                Console.WriteLine("oprette");
            }

            public void Rette()
            {
                Console.WriteLine("rette");
            }

            public void Slette()
            {
                Console.WriteLine("slette");
            }
        }

        class Sommerhus
        {
            public void Opret()
            {
                Console.WriteLine("oprettehus");
            }

            public void Rette()
            {
                Console.WriteLine("rettehus");
            }

            public void Slette()
            {
                Console.WriteLine("slettehus");
            }
        }

        class ResSommerhus
        {
            public void Opret()
            {
                Console.WriteLine("opretteres");
            }

            public void Rette()
            {
                Console.WriteLine("retteres");
            }

            public void Slette()
            {
                Console.WriteLine("sletter");
            }
        }

        class Søgning
        {
            public void søg()
            {

            }
        }
    }
}