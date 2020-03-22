using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydvest_Bo.Classes
{
    public class Functionality
    {
        //Gets a password without showing it to the end user
        public static string ReadPassword()
        {
            ConsoleKeyInfo info;
            ConsoleKey key;
            List<char> pwdStrList = new List<char>();
            int cursorPos = 0;
            while ((key = (info = Console.ReadKey(true)).Key) != ConsoleKey.Enter) //Will listen for any keys until the 'Enter' key is pressed
            {
                if (key == ConsoleKey.LeftArrow && cursorPos > 0) //Go one character to the left
                {
                    cursorPos--;
                    continue;
                }
                if (key == ConsoleKey.RightArrow && cursorPos < pwdStrList.Count) //Go one character to the right
                {
                    cursorPos++;
                    continue;
                }
                if (key == ConsoleKey.Backspace) //Remove the character behind the current one
                {
                    if (cursorPos > 0)
                    {
                        cursorPos--;
                        pwdStrList.RemoveAt(cursorPos);
                    }
                    continue;
                }
                if (key == ConsoleKey.Delete) //Remove the current character
                {
                    if (cursorPos < pwdStrList.Count)
                    {
                        pwdStrList.RemoveAt(cursorPos);
                    }
                    continue;
                }
                if (" qwertyuiopasdfghjklzxcvbnm1234567890.,-".Contains(info.KeyChar.ToString().ToLower()))
                {
                    pwdStrList.Add(info.KeyChar);
                    cursorPos++;
                }
            }

            return new string(pwdStrList.ToArray());
        }

        public static string Select (string[] choices)
        {
            Console.CursorVisible = false;
            List<int> choicesPos = new List<int>();
            int pointer = 0;

            int count = 0;
            foreach (string choice in choices)
            {
                Console.Write("[");
                if (count == 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                choicesPos.Add(Console.CursorLeft);
                Console.Write(choice);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("]");
                Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);

                count++;
            }

            ConsoleKey key;
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter) //Will listen for any keys until the 'Enter' key is pressed
            {
                if (key == ConsoleKey.LeftArrow && pointer > 0) //Go one to the left
                {
                    ChangeChoice(pointer - 1);
                    continue;
                }
                if (key == ConsoleKey.RightArrow && pointer < choicesPos.Count - 1) //Go one to the right
                {
                    ChangeChoice(pointer + 1);
                    continue;
                }
            }

            Console.CursorVisible = true;
            return choices[pointer];

            void ChangeChoice (int newPos)
            {

                Console.SetCursorPosition(choicesPos[newPos], Console.CursorTop);

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                Console.Write(choices[newPos]);


                Console.SetCursorPosition(choicesPos[pointer], Console.CursorTop);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write(choices[pointer]);



                pointer = newPos;
            }
        }
    }
}
