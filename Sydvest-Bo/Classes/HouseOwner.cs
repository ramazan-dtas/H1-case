using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo.Classes
{
    public class HouseOwner
    {
        public void Opret()
        {
            Console.WriteLine("Indtast fornavn: ");
            string fornavn = Console.ReadLine();

            Console.WriteLine("Indtast efternavn: ");
            string efternavn = Console.ReadLine();

            Console.WriteLine("Indtast email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Indtast adresse: ");
            string adresse = Console.ReadLine();

            Console.WriteLine("indtast postnummer");
            string postnummer = Console.ReadLine();


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
}
