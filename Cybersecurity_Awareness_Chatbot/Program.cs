using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Awareness_Chatbot
{
    internal class Program
    {
        static void Main(string[] args)
        {

            new WelcomeScreen() { };

            UserPrompt collect_name = new UserPrompt();

            //Call the welcome method
            collect_name.ShowWelcomeMessage();
            collect_name.AskForName();

            //Create an instance of the Chatbot class
            Chatbot chatting = new Chatbot();

            //Get the returned name of the user
            string name = collect_name.GetUserName();
            //Now start the chatting
            chatting.StartChat(name);


        }
    }
}

