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
    //Main window contains Task Assistant, Quiz, NLP, and Activity Log panels
    public partial class MainWindow : Window
    {//start of class

        //MySql connection details
        private const string MYSQL_PASSWORD = "Y@ndisa07_";

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

        //Part 3 fields
        activity_log activityLog;
        task_manager taskManager;
        quiz_manager quizManager;

        //Tracks whether we are in task add mode and waiting for task description input
        bool _addingTask = false;
        bool _awaitingTaskDesc = false;
        string _pendingTaskTitle = "";

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

            //Initialise Part 3 components FIRST before passing them anywhere
            activityLog = new activity_log();
            taskManager = new task_manager(activityLog, MYSQL_PASSWORD);
            quizManager = new quiz_manager(activityLog);

            //set up display and processor (chats ListView is now ready)
            chatDisplay = new chat_display(chats);
            chatProcessor = new chat_processor(reply, ignore, interestTracker, chatDisplay, username, taskManager, quizManager, activityLog);
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

        //Tab navigation buttons (switch between panels inside chat_grid)
        //Show the main chat panel
        private void show_chat_panel(object sender, RoutedEventArgs e)
        {
            chat_panel.Visibility = Visibility.Visible;
            task_panel.Visibility = Visibility.Hidden;
            quiz_panel.Visibility = Visibility.Hidden;
            log_panel.Visibility = Visibility.Hidden;
        }

        //Show the task assistant panel
        private void show_task_panel(object sender, RoutedEventArgs e)
        {
            chat_panel.Visibility = Visibility.Hidden;
            task_panel.Visibility = Visibility.Visible;
            quiz_panel.Visibility = Visibility.Hidden;
            log_panel.Visibility = Visibility.Hidden;

            RefreshTaskList();
        }

        //Show the quiz panel
        private void show_quiz_panel(object sender, RoutedEventArgs e)
        {
            chat_panel.Visibility = Visibility.Hidden;
            task_panel.Visibility = Visibility.Hidden;
            quiz_panel.Visibility = Visibility.Visible;
            log_panel.Visibility = Visibility.Hidden;
        }

        //Show the activity log panel
        private void show_log_panel(object sender, RoutedEventArgs e)
        {
            chat_panel.Visibility = Visibility.Hidden;
            task_panel.Visibility = Visibility.Hidden;
            quiz_panel.Visibility = Visibility.Hidden;
            log_panel.Visibility = Visibility.Visible;

            RefreshActivityLog();
        }

        //Task Assistant panel event handlers
        //Add task button
        private void add_task_btn(object sender, RoutedEventArgs e)
        {
            string title = task_title_input.Text.Trim();
            string description = task_desc_input.Text.Trim();
            string reminder = task_reminder_input.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                task_status_label.Content = "Please enter a task title.";
                return;
            }

            if (string.IsNullOrWhiteSpace(description))
                description = title;

            string result = taskManager.AddTask(title, description, reminder);
            task_status_label.Content = result;

            //Clear inputs
            task_title_input.Clear();
            task_desc_input.Clear();
            task_reminder_input.Clear();

            RefreshTaskList();
        }

        //Mark complete button
        private void mark_complete_btn(object sender, RoutedEventArgs e)
        {
            if (tasks_listview.SelectedItem is CyberTask selected)
            {
                string result = taskManager.MarkCompleted(selected.Id);
                task_status_label.Content = result;
                RefreshTaskList();
            }
            else
            {
                task_status_label.Content = "Please select a task first.";
            }
        }

        //Delete task button
        private void delete_task_btn(object sender, RoutedEventArgs e)
        {
            if (tasks_listview.SelectedItem is CyberTask selected)
            {
                string result = taskManager.DeleteTask(selected.Id);
                task_status_label.Content = result;
                RefreshTaskList();
            }
            else
            {
                task_status_label.Content = "Please select a task to delete.";
            }
        }

        //Refreshes the task list display
        private void RefreshTaskList()
        {
            tasks_listview.ItemsSource = null;
            tasks_listview.ItemsSource = taskManager.GetAllTasks();
        }

        //Quiz panel event handlers
        //Start quiz button
        private void start_quiz_btn(object sender, RoutedEventArgs e)
        {
            string result = quizManager.StartQuiz();
            quiz_output.Text = result;
            quiz_answer_input.IsEnabled = true;
            quiz_answer_input.Clear();
            quiz_submit_btn.IsEnabled = true;
            start_quiz_btn_ctrl.IsEnabled = false;
        }

        //Submit quiz answer button
        private void submit_quiz_answer(object sender, RoutedEventArgs e)
        {
            string answer = quiz_answer_input.Text.Trim();

            if (string.IsNullOrWhiteSpace(answer))
                return;

            string result = quizManager.SubmitAnswer(answer);
            quiz_output.Text = result;
            quiz_answer_input.Clear();

            //If quiz ended, re-enable start button
            if (!quizManager.IsActive)
            {
                quiz_answer_input.IsEnabled = false;
                quiz_submit_btn.IsEnabled = false;
                start_quiz_btn_ctrl.IsEnabled = true;
            }
        }

        //Quit quiz button
        private void quit_quiz_btn(object sender, RoutedEventArgs e)
        {
            quiz_output.Text = quizManager.QuitQuiz();
            quiz_answer_input.IsEnabled = false;
            quiz_submit_btn.IsEnabled = false;
            start_quiz_btn_ctrl.IsEnabled = true;
        }

        //Activity log panel
        private void RefreshActivityLog()
        {
            log_output.Text = activityLog.GetLogSummary();
        }

        //Refresh log button
        private void refresh_log_btn(object sender, RoutedEventArgs e)
        {
            RefreshActivityLog();
        }

    }//end of class
}//end of namespace