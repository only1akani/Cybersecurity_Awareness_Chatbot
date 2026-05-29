using System;
using System.Threading;

namespace Cybersecurity_Awareness_Chatbot
{
    public class UserPrompt
    {
        private string name = string.Empty;

        //Welcome the user
        public void ShowWelcomeMessage()
        { 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("==___==___==___==___==___==___==___==___==___==___==___==___==___==___==___==");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("               -> Welcome to the Cybersecurity Awareness Chatbot <-          ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("==___==___==___==___==___==___==___==___==___==___==___==___==___==___==___==");
            Console.ResetColor();

            Thread.Sleep(800);
        }
        public void AskForName()
        { 

            //Colours for chatbot
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("chatBot : ");

            //Prompt user for name
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Please enter your name...");

            //Reset the colour
            Console.ResetColor();

            //Check using do while and prompt user again
            do
            { //Do while starts

                //User entering name
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("user : ");

                //Input colour
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                name = Console.ReadLine();

                //Reset colour
                Console.ResetColor();


            } while (!ValidateName());//Do while ends


        }

        //Boolean method to check if the user entered name
        private Boolean ValidateName()
        { 


            //Check if the name is entered using if statement
            if (name == "")
            {
                //Show error message
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("chatBot : ");

                //Prompt user to enter name again
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please try to enter your name...");

                //Reset colour
                Console.ResetColor();

                Thread.Sleep(600);
                return false;
            }
            else
            {
                //Return the success message
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("chatBot : ");

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                TypeWriter("Hey ", 30);

                //Highlight the name in user colour
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                TypeWriter(name, 40);

                //Back to bot message colour
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                TypeWriter(", how can I help you today?", 30);

                //Reset colour
                Console.WriteLine();
                Console.ResetColor();

                Thread.Sleep(700);
                return true;
            }

        }
        //Typing effect to simulate natural conversation
        private void TypeWriter(string text, int delay = 40)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }
        //Method to return the user name to be used across classes
        public string GetUserName()
        {
            //Returning the name of the user
            return name;
        }
    }
}