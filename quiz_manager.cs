using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo
{//start of namespace

    //Represents a single quiz question
    public class QuizQuestion
    {//start of class
        public string Question { get; set; }
        public List<string> Options { get; set; }   
        public string CorrectAnswer { get; set; }   
        public string Explanation { get; set; }
        public bool IsTrueFalse { get; set; }
    }//end of class

    //Manages the cybersecurity quiz mini-game
    public class quiz_manager
    {// start of class

        private List<QuizQuestion> _questions;
        private int _currentIndex = 0;
        private int _score = 0;
        private bool _quizActive = false;
        private bool _awaitingAnswer = false;

        private activity_log _activityLog;

        public quiz_manager(activity_log activityLog)
        {
            _activityLog = activityLog;
            LoadQuestions();
        }

        //Returns true if a quiz is currently in progress
        public bool IsActive => _quizActive;

        //Returns true if the bot is waiting for the user's answer
        public bool AwaitingAnswer => _awaitingAnswer;

        //Loads all 12 cybersecurity quiz questions
        private void LoadQuestions()
        {
            _questions = new List<QuizQuestion>
            {
                //Multiple choice
                new QuizQuestion
                {
                    Question      = "What should you do if you receive an email asking for your password?",
                    Options       = new List<string> { "A) Reply with your password", "B) Delete the email", "C) Report the email as phishing", "D) Ignore it" },
                    CorrectAnswer = "C",
                    Explanation   = "Reporting phishing emails helps prevent scams and protects others.",
                    IsTrueFalse   = false
                },
                new QuizQuestion
                {
                    Question      = "Which of the following is the strongest password?",
                    Options       = new List<string> { "A) password123", "B) MyDog2010", "C) P@ssw0rd!", "D) Tr0ub4dor&3#xK9!" },
                    CorrectAnswer = "D",
                    Explanation   = "Long passphrases with mixed characters are the hardest to crack.",
                    IsTrueFalse   = false
                },
                new QuizQuestion
                {
                    Question      = "What does 2FA stand for?",
                    Options       = new List<string> { "A) Two-Factor Authentication", "B) Two-File Access", "C) Twice-Failed Attempt", "D) Two-Form Authorization" },
                    CorrectAnswer = "A",
                    Explanation   = "Two-Factor Authentication adds an extra layer of security beyond just a password.",
                    IsTrueFalse   = false
                },
                new QuizQuestion
                {
                    Question      = "What is ransomware?",
                    Options       = new List<string> { "A) A type of antivirus software", "B) Malware that locks your files and demands payment", "C) A secure backup tool", "D) A type of VPN" },
                    CorrectAnswer = "B",
                    Explanation   = "Ransomware encrypts your files and demands a ransom to restore access.",
                    IsTrueFalse   = false
                },
                new QuizQuestion
                {
                    Question      = "Which of these is a sign of a phishing website?",
                    Options       = new List<string> { "A) HTTPS in the URL", "B) A padlock icon in the browser", "C) A URL like 'www.paypa1.com'", "D) A professional-looking logo" },
                    CorrectAnswer = "C",
                    Explanation   = "Phishing sites use lookalike URLs (like '1' instead of 'l') to trick users.",
                    IsTrueFalse   = false
                },
                new QuizQuestion
                {
                    Question      = "What is social engineering?",
                    Options       = new List<string> { "A) Building secure software", "B) Manipulating people to reveal confidential information", "C) Designing secure networks", "D) Creating strong passwords" },
                    CorrectAnswer = "B",
                    Explanation   = "Social engineering exploits human psychology rather than technical vulnerabilities.",
                    IsTrueFalse   = false
                },
                new QuizQuestion
                {
                    Question      = "What does a VPN primarily do?",
                    Options       = new List<string> { "A) Speeds up your internet", "B) Encrypts your internet traffic and hides your IP", "C) Blocks all ads", "D) Removes viruses" },
                    CorrectAnswer = "B",
                    Explanation   = "A VPN encrypts your connection, making it harder for others to spy on your activity.",
                    IsTrueFalse   = false
                },
                new QuizQuestion
                {
                    Question      = "How often should you update your passwords?",
                    Options       = new List<string> { "A) Never, if they are strong", "B) Every 5 years", "C) Regularly, especially after a data breach", "D) Only when you forget them" },
                    CorrectAnswer = "C",
                    Explanation   = "Regular updates, especially after a breach, reduce the risk of account compromise.",
                    IsTrueFalse   = false
                },
 
                //True or False
                new QuizQuestion
                {
                    Question      = "True or False: Public Wi-Fi networks are always safe to use for online banking.",
                    Options       = null,
                    CorrectAnswer = "False",
                    Explanation   = "Public Wi-Fi is often unsecured. Attackers can intercept your data using man-in-the-middle attacks.",
                    IsTrueFalse   = true
                },
                new QuizQuestion
                {
                    Question      = "True or False: Clicking a link in an email is always safe if the email looks professional.",
                    Options       = null,
                    CorrectAnswer = "False",
                    Explanation   = "Phishing emails are often designed to look professional. Always verify the sender before clicking links.",
                    IsTrueFalse   = true
                },
                new QuizQuestion
                {
                    Question      = "True or False: A firewall can help block unauthorised access to your network.",
                    Options       = null,
                    CorrectAnswer = "True",
                    Explanation   = "Firewalls monitor and control incoming and outgoing traffic based on security rules.",
                    IsTrueFalse   = true
                },
                new QuizQuestion
                {
                    Question      = "True or False: Using the same password for all accounts is a good security practice.",
                    Options       = null,
                    CorrectAnswer = "False",
                    Explanation   = "If one account is compromised, using the same password puts all your other accounts at risk.",
                    IsTrueFalse   = true
                }
            };
        }

        //Starts the quiz and returns the first question
        public string StartQuiz()
        {
            _currentIndex = 0;
            _score = 0;
            _quizActive = true;
            _awaitingAnswer = true;

            _activityLog.LogAction("Quiz started.");

            return "Quiz started! Let's test your cybersecurity knowledge.\n\n" + GetCurrentQuestion();
        }

        //Returns the current question as a formatted string
        public string GetCurrentQuestion()
        {
            if (_currentIndex >= _questions.Count)
                return EndQuiz();

            QuizQuestion q = _questions[_currentIndex];
            string output = $"Question {_currentIndex + 1} of {_questions.Count}:\n{q.Question}";

            if (!q.IsTrueFalse && q.Options != null)
            {
                output += "\n";
                foreach (string option in q.Options)
                    output += "\n" + option;
            }
            else
            {
                output += "\n\nType 'True' or 'False'";
            }

            return output;
        }

        //Processes the user's answer and returns feedback + next question
        public string SubmitAnswer(string userAnswer)
        {
            if (!_quizActive || !_awaitingAnswer)
                return "No quiz is active. Type 'start quiz' to begin.";

            if (_currentIndex >= _questions.Count)
                return EndQuiz();

            QuizQuestion q = _questions[_currentIndex];
            string cleaned = userAnswer.Trim().ToUpper();
            string correct = q.CorrectAnswer.ToUpper();
            string feedback;

            //Normalise true or false input
            if (q.IsTrueFalse)
            {
                if (cleaned == "TRUE" || cleaned == "T") cleaned = "TRUE";
                if (cleaned == "FALSE" || cleaned == "F") cleaned = "FALSE";
                correct = q.CorrectAnswer.ToUpper();
            }

            if (cleaned == correct)
            {
                _score++;
                feedback = $"✔ Correct! {q.Explanation}";
            }
            else
            {
                feedback = $"✘ Incorrect. The correct answer was: {q.CorrectAnswer}.\n{q.Explanation}";
            }

            _currentIndex++;

            //Check if quiz is finished
            if (_currentIndex >= _questions.Count)
            {
                return feedback + "\n\n" + EndQuiz();
            }

            _awaitingAnswer = true;
            return feedback + "\n\n" + GetCurrentQuestion();
        }

        //Ends the quiz and returns the final score with personalised feedback
        private string EndQuiz()
        {
            _quizActive = false;
            _awaitingAnswer = false;

            string scoreLine = $"Quiz complete! Your final score: {_score} / {_questions.Count}";
            string message;

            double percentage = (double)_score / _questions.Count * 100;

            if (percentage >= 80)
                message = "Great job! You're a cybersecurity pro!";
            else if (percentage >= 50)
                message = "Good effort! Keep learning to stay safe online.";
            else
                message = "Keep learning to stay safe online! Review the topics and try again.";

            _activityLog.LogAction($"Quiz completed - Score: {_score}/{_questions.Count}.");

            return $"{scoreLine}\n{message}";
        }

        //Stops the quiz mid-way if the user wants to quit
        public string QuitQuiz()
        {
            _quizActive = false;
            _awaitingAnswer = false;
            _activityLog.LogAction($"Quiz quit early at question {_currentIndex + 1}.");
            return "Quiz ended. Type 'start quiz' to try again.";
        }

    }//end of class

}//end of namespace