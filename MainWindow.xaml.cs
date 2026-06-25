using demo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demo
{//start of namespace
    //Main window, handles all UI events and connects the separate classes
    public partial class MainWindow : Window
    {//start of class

        //creating an instance for the class Array
        ArrayList reply = new ArrayList();
        ArrayList ignore = new ArrayList();
        user_name check_name = new user_name();

        //variables
        Input_cleaner sanitizer = new Input_cleaner();
        interest_tracker interestTracker = new interest_tracker();

        //These are set up after the ListView is ready (in the constructor)
        chat_display chatDisplay;
        chat_processor chatProcessor;

        //Stores the current username across the session
        string username = string.Empty;
        string pre_question = string.Empty;
        int counting = 0;


        public MainWindow()
        {
            InitializeComponent();

            //load reply and ignore lists
            new respond(reply, ignore) { };

            //creating an instance for the class voice_greeting 
            //with an object name greet
            voice_greeting greet = new voice_greeting();

            //call the voice method
            greet.greet();

            //set up display and processor (chats ListView is now ready)
            chatDisplay = new chat_display(chats);
            chatProcessor = new chat_processor(reply, ignore, interestTracker, chatDisplay, username);
        }


        //proceed event handler
        private void proceed(object sender, RoutedEventArgs e)
        {
            //Hide home page grid and set Username grid visible
            home_grid.Visibility = Visibility.Hidden;
            username_grid.Visibility = Visibility.Visible;
        }


        //submit name event handler
        private void submit_name(object sender, RoutedEventArgs e)
        {
            //check the user name from memory recall
            username = check_name.submit_name(usernames_input, chats);

            //Update the processor so it knows who the user is
            chatProcessor.SetUsername(username);

            //Hide username page grid and set chats grid visible
            username_grid.Visibility = Visibility.Hidden;
            chat_grid.Visibility = Visibility.Visible;
        }


        //send event handler
        private void send(object sender, RoutedEventArgs e)
        {
            //Get the question from the design and sanitize it
            string rawQuestion = question.Text.ToString().Trim();

            if (string.IsNullOrWhiteSpace(rawQuestion))
            {
                chatDisplay.error_method("ChatBot", "Please enter a question.");
                return;
            }

            //Remove special characters and clean the question
            string questions = sanitizer.RemoveSpecialCharacters(rawQuestion);

            //Show what the user typed
            chatDisplay.error_method(username, rawQuestion);

            //ai chats and auto_show_interest
            chatProcessor.auto_show_interest(username);
            chatProcessor.ProcessQuestion(questions);

            question.Clear();
        }


    }//end of class
}//end of namespace