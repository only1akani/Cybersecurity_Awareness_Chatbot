using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace demo
{//start of namespace

    //Processes user input and matches it to the correct chatbot response
    //Extended with NLP simulation, task manager, quiz, and activity log support
    public class chat_processor
    {//start of class
        private ArrayList _reply;
        private ArrayList _ignore;
        private interest_tracker _interestTracker;
        private chat_display _chatDisplay;
        private string _username;

        //Part 3 dependencies
        private task_manager _taskManager;
        private quiz_manager _quizManager;
        private activity_log _activityLog;

        //Counter for auto-interest reminders
        private int _counting = 0;

        //Tracks whether we are mid-task-add flow (waiting for reminder input)
        private bool _awaitingReminder = false;
        private string _pendingTaskTitle = "";
        private string _pendingTaskDesc = "";

        public chat_processor(ArrayList reply, ArrayList ignore, interest_tracker interestTracker,
                              chat_display chatDisplay, string username,
                              task_manager taskManager, quiz_manager quizManager, activity_log activityLog)
        {
            _reply = reply;
            _ignore = ignore;
            _interestTracker = interestTracker;
            _chatDisplay = chatDisplay;
            _username = username;
            _taskManager = taskManager;
            _quizManager = quizManager;
            _activityLog = activityLog;
        }

        //Update username when it changes
        public void SetUsername(string username)
        {
            _username = username;
        }

        //Auto interest reminder - shows every 3 messages
        public void auto_show_interest(string username)
        {
            if (_counting == 3)
            {
                string interests = _interestTracker.GetInterests(username);
                if (!string.IsNullOrWhiteSpace(interests))
                {
                    _chatDisplay.error_method("ChatBot",
                        "Just a reminder, you mentioned you are interested in: " + interests +
                        ". Feel free to ask me anything about those topics!");
                }
                _counting = 0;
            }
            else
            {
                _counting += 1;
            }
        }

        //Maps typed words to canonical keywords used in the answer list
        private string NormaliseWord(string word)
        {
            switch (word)
            {
                case "hi":
                case "hello":
                case "hey":
                case "howzit":
                case "greetings":
                case "greeting":
                    return "greeting";

                case "hacked":
                case "hack":
                case "breached":
                case "compromised":
                    return "hacked";

                case "phish":
                case "phishing":
                    return "phishing";

                case "firewall":
                case "firewalls":
                    return "firewall";

                case "password":
                case "passwords":
                    return "password";

                case "malware":
                case "virus":
                case "viruses":
                    return "malware";

                case "vpn":
                case "vpns":
                    return "vpn";

                case "fraud":
                case "scam":
                case "scammed":
                    return "fraud";

                case "ransomware":
                    return "ransomware";

                case "encryption":
                case "encrypted":
                case "encrypt":
                    return "encryption";

                case "authentication":
                case "2fa":
                case "twofactor":
                    return "authentication";

                case "browsing":
                case "browser":
                case "browse":
                    return "browsing";

                case "cybersecurity":
                case "cyber":
                case "security":
                    return "cybersecurity";

                case "bot":
                case "chatbot":
                case "malicious":
                    return "malicious";

                default:
                    return word;
            }
        }

        //NLP helpers: detect user intent from full input using string.Contains
        //Detects if the user wants to add a task
        private bool IsAddTaskIntent(string input)
        {
            return (input.Contains("add task") ||
                    input.Contains("new task") ||
                    input.Contains("create task") ||
                    input.Contains("set up task") ||
                    input.Contains("add a task") ||
                    input.Contains("add task to") ||
                    //NLP variations
                    (input.Contains("add") && input.Contains("2fa")) ||
                    (input.Contains("add") && input.Contains("task")) ||
                    (input.Contains("set") && input.Contains("task")) ||
                    (input.Contains("enable") && input.Contains("two-factor")) ||
                    (input.Contains("enable") && input.Contains("2fa")));
        }

        //Detects if the user wants to view tasks
        private bool IsViewTasksIntent(string input)
        {
            return (input.Contains("view task") ||
                    input.Contains("show task") ||
                    input.Contains("list task") ||
                    input.Contains("my task") ||
                    input.Contains("see task") ||
                    input.Contains("show all task") ||
                    input.Contains("what are my task"));
        }

        //Detects if the user wants to set a reminder
        private bool IsReminderIntent(string input)
        {
            return (input.Contains("remind me") ||
                    input.Contains("set reminder") ||
                    input.Contains("set a reminder") ||
                    input.Contains("add reminder") ||
                    input.Contains("reminder for"));
        }

        //Detects if the user wants to start the quiz
        private bool IsStartQuizIntent(string input)
        {
            return (input.Contains("start quiz") ||
                    input.Contains("begin quiz") ||
                    input.Contains("play quiz") ||
                    input.Contains("take quiz") ||
                    input.Contains("quiz me") ||
                    input.Contains("mini game") ||
                    input.Contains("minigame") ||
                    (input.Contains("quiz") && input.Contains("start")));
        }

        //Detects if the user wants to view the activity log
        private bool IsActivityLogIntent(string input)
        {
            return (input.Contains("show activity") ||
                    input.Contains("activity log") ||
                    input.Contains("show log") ||
                    input.Contains("view log") ||
                    input.Contains("what have you done") ||
                    input.Contains("what have you done for me") ||
                    input.Contains("recent actions") ||
                    input.Contains("history"));
        }

        //Detects if the user wants to mark a task complete
        private bool IsCompleteTaskIntent(string input)
        {
            return (input.Contains("complete task") ||
                    input.Contains("mark complete") ||
                    input.Contains("finish task") ||
                    input.Contains("done with task") ||
                    (input.Contains("mark") && input.Contains("complete")));
        }

        //Detects if the user wants to delete a task
        private bool IsDeleteTaskIntent(string input)
        {
            return (input.Contains("delete task") ||
                    input.Contains("remove task") ||
                    input.Contains("cancel task"));
        }

        //Tries to extract a task ID number from the user's input
        private int ExtractTaskId(string input)
        {
            string[] words = input.Split(' ');
            foreach (string word in words)
            {
                if (int.TryParse(word, out int id))
                    return id;
            }
            return -1;
        }

        //Tries to extract a reminder timeframe from the user's input
        private string ExtractReminder(string input)
        {
            //Look for patterns like "in X days/weeks" or "tomorrow" or specific dates
            if (input.Contains("tomorrow"))
                return "tomorrow";

            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length - 1; i++)
            {
                if (int.TryParse(words[i], out _))
                {
                    string unit = words[i + 1].ToLower().TrimEnd('s');
                    if (unit == "day" || unit == "week" || unit == "month" || unit == "hour")
                        return $"{words[i]} {words[i + 1]}";
                }
            }

            //Try to find "yes remind me in X days" pattern
            if (input.Contains("yes") && input.Contains("remind"))
            {
                for (int i = 0; i < words.Length - 1; i++)
                {
                    if (int.TryParse(words[i], out _))
                    {
                        return $"{words[i]} {words[i + 1]}";
                    }
                }
            }

            return "";
        }

        //Extracts a task title/description from input (after "add task" keyword)
        private string ExtractTaskContent(string input)
        {
            string[] triggers = { "add task", "new task", "create task", "add a task" };
            foreach (string trigger in triggers)
            {
                int idx = input.IndexOf(trigger, StringComparison.OrdinalIgnoreCase);
                if (idx >= 0)
                {
                    string after = input.Substring(idx + trigger.Length).Trim();
                    //Remove leading dashes or punctuation
                    return after.TrimStart('-', '–', ':', ' ');
                }
            }
            return input;
        }
        //Main processing method
        public void ProcessQuestion(string questions)
        {
            if (string.IsNullOrWhiteSpace(questions))
            {
                _chatDisplay.error_method("ChatBot", "Please enter a valid question.");
                return;
            }

            string lower = questions.ToLower().Trim();

            //If quiz is active, route directly to quiz
            if (_quizManager.IsActive && _quizManager.AwaitingAnswer)
            {
                //Allow quitting mid-quiz
                if (lower.Contains("quit quiz") || lower.Contains("stop quiz") || lower.Contains("exit quiz"))
                {
                    _chatDisplay.error_method("ChatBot", _quizManager.QuitQuiz());
                    return;
                }

                string quizResult = _quizManager.SubmitAnswer(questions.Trim());
                _chatDisplay.error_method("ChatBot", quizResult);
                return;
            }

            //If waiting for reminder after adding a task
            if (_awaitingReminder)
            {
                //User said yes or gave a timeframe
                if (lower.Contains("yes") || lower.Contains("remind") ||
                    lower.Contains("day") || lower.Contains("week") ||
                    lower.Contains("tomorrow"))
                {
                    string reminder = ExtractReminder(lower);
                    if (string.IsNullOrWhiteSpace(reminder))
                        reminder = lower.Replace("yes", "").Replace("remind me", "").Trim();

                    string result = _taskManager.AddTask(_pendingTaskTitle, _pendingTaskDesc, reminder);
                    _activityLog.LogAction($"Reminder set: '{reminder}' for task '{_pendingTaskTitle}'.");
                    _chatDisplay.error_method("ChatBot", $"Got it! I'll remind you in {reminder}.");
                }
                else
                {
                    //User said no reminder
                    string result = _taskManager.AddTask(_pendingTaskTitle, _pendingTaskDesc, "");
                    _chatDisplay.error_method("ChatBot", "Task saved with no reminder.");
                }

                _awaitingReminder = false;
                _pendingTaskTitle = "";
                _pendingTaskDesc = "";
                return;
            }

            //NLP Intent Detection

            //Start quiz
            if (IsStartQuizIntent(lower))
            {
                _chatDisplay.error_method("ChatBot", _quizManager.StartQuiz());
                return;
            }

            //Activity log
            if (IsActivityLogIntent(lower))
            {
                _activityLog.LogAction("User requested activity log.");
                _chatDisplay.error_method("ChatBot", _activityLog.GetLogSummary());
                return;
            }

            //View tasks
            if (IsViewTasksIntent(lower))
            {
                var tasks = _taskManager.GetAllTasks();
                if (tasks.Count == 0)
                {
                    _chatDisplay.error_method("ChatBot", "You have no tasks yet. Type 'add task - [title]' to add one.");
                    return;
                }

                string taskList = "Here are your cybersecurity tasks:\n";
                foreach (var task in tasks)
                {
                    string status = task.IsCompleted ? "[Done]" : "[Pending]";
                    string reminder = string.IsNullOrWhiteSpace(task.Reminder) ? "No reminder" : $"Reminder: {task.Reminder}";
                    taskList += $"\n{task.Id}. {status} {task.Title} - {task.Description} ({reminder})";
                }

                _chatDisplay.error_method("ChatBot", taskList);
                _activityLog.LogAction("User viewed task list.");
                return;
            }

            //Mark task complete
            if (IsCompleteTaskIntent(lower))
            {
                int id = ExtractTaskId(lower);
                if (id == -1)
                {
                    _chatDisplay.error_method("ChatBot", "Please include the task ID number. E.g. 'complete task 2'");
                    return;
                }
                _chatDisplay.error_method("ChatBot", _taskManager.MarkCompleted(id));
                return;
            }

            //Delete task
            if (IsDeleteTaskIntent(lower))
            {
                int id = ExtractTaskId(lower);
                if (id == -1)
                {
                    _chatDisplay.error_method("ChatBot", "Please include the task ID number. E.g. 'delete task 2'");
                    return;
                }
                _chatDisplay.error_method("ChatBot", _taskManager.DeleteTask(id));
                return;
            }

            //Reminder intent
            if (IsReminderIntent(lower))
            {
                string reminder = ExtractReminder(lower);
                if (!string.IsNullOrWhiteSpace(reminder))
                {
                    _activityLog.LogAction($"Reminder set: '{reminder}'.");
                    _chatDisplay.error_method("ChatBot", $"Reminder set for '{reminder}' on your most recent task.");
                }
                else
                {
                    _chatDisplay.error_method("ChatBot", "Sure! How many days should I remind you? E.g. 'remind me in 3 days'");
                }
                return;
            }

            //Add task
            if (IsAddTaskIntent(lower))
            {
                string content = ExtractTaskContent(lower);

                //Build a meaningful description from the task content
                string title = string.IsNullOrWhiteSpace(content) ? "New cybersecurity task" : content;
                //Capitalise first letter for a clean title
                if (title.Length > 0)
                    title = char.ToUpper(title[0]) + title.Substring(1);

                string description = title;

                //Store pending and ask for reminder
                _pendingTaskTitle = title;
                _pendingTaskDesc = description;
                _awaitingReminder = true;

                _activityLog.LogAction($"Task initiated: '{title}'.");
                _chatDisplay.error_method("ChatBot",
                    $"Task added with the description \"{description} to ensure your data is protected.\" Would you like a reminder?");
                return;
            }

            //Fall back to keyword matching
            string[] words = questions
                .ToLower()
                .Split(new char[] { ' ', ',', '.', '?', '!', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

            bool found = false;
            string message = string.Empty;
            Random indexer = new Random();
            List<string> answers_found = new List<string>();
            HashSet<string> matchedKeywords = new HashSet<string>();

            foreach (string word in words)
            {
                if (word.Length < 3 || _ignore.Contains(word.ToLower()))
                    continue;

                //Interest detection
                if (word.Contains("interested"))
                {
                    string interestMessage = _interestTracker.SaveInterests(words, _ignore, _username);
                    message += interestMessage;
                    continue;
                }

                string keyword = NormaliseWord(word);

                if (matchedKeywords.Contains(keyword))
                    continue;

                List<string> matches = new List<string>();
                foreach (string answer in _reply)
                {
                    if (answer.ToLower().StartsWith(keyword))
                        matches.Add(answer);
                }

                if (matches.Count > 0)
                {
                    found = true;
                    matchedKeywords.Add(keyword);
                    answers_found.Add(matches[indexer.Next(0, matches.Count)]);
                }
            }

            if (found && answers_found.Count > 0)
            {
                foreach (string per_answer in answers_found)
                    message += per_answer + "\n";

                _chatDisplay.error_method("ChatBot", message.TrimEnd('\n'));
            }
            else
            {
                string[] fallbackMessages =
                {
                    "I'm sorry, I don't understand that. Could you rephrase your question?",
                    "I didn't quite understand that. Try asking me about phishing, passwords, tasks, or type 'start quiz'.",
                    "Hmm, I'm not sure how to respond to that. Can you ask something else?",
                    "I couldn't find an answer for that. Try topics like cybersecurity, malware, or 'add task'.",
                    "My apologies, I don't have information on that topic yet."
                };
                Random random = new Random();
                _chatDisplay.error_method("ChatBot", fallbackMessages[random.Next(fallbackMessages.Length)]);
            }

        }//end of ProcessQuestion method

    }//end of class

}//end of namespace