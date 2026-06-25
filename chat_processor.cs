using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace demo
{//start of namespace
    //Processes user input and matches it to the correct chatbot response
    public class chat_processor
    {//start of class

        private ArrayList _reply;
        private ArrayList _ignore;
        private interest_tracker _interestTracker;
        private chat_display _chatDisplay;
        private string _username;

        //counter lives here, not in MainWindow
        private int _counting = 0;

        public chat_processor(ArrayList reply, ArrayList ignore, interest_tracker interestTracker, chat_display chatDisplay, string username)
        {
            _reply = reply;
            _ignore = ignore;
            _interestTracker = interestTracker;
            _chatDisplay = chatDisplay;
            _username = username;
        }

        //Update username when it changes (set after login)
        public void SetUsername(string username)
        {
            _username = username;
        }


        //method count to show interests - only reminds, never processes
        public void auto_show_interest(string username)
        {
            if (_counting == 3)
            {
                string interests = _interestTracker.GetInterests(username);

                if (!string.IsNullOrWhiteSpace(interests))
                {
                    // Just show the reminder message — do NOT call ProcessQuestion
                    _chatDisplay.error_method("ChatBot",
                        "Just a reminder, you mentioned you are interested in: " + interests +
                        ". Feel free to ask me anything about those topics!");
                }

                //reset counting
                _counting = 0;
            }
            else
            {
                _counting += 1;
            }
        }
        //end of auto_show_interest method


        //Maps what the user types to the keyword used in the answer list
        private string NormaliseWord(string word)
        {
            switch (word)
            {
                //User said some form of hello, map to greeting answers
                case "hi":
                case "hello":
                case "hey":
                case "howzit":
                case "greetings":
                case "greeting":
                    return "greeting";

                //User is saying their account was compromised, map to hacked answers
                case "hacked":
                case "hack":
                case "breached":
                case "compromised":
                    return "hacked";

                //Both spellings of phishing map to the same answers
                case "phish":
                case "phishing":
                    return "phishing";

                //Singular and plural both map to firewall answers
                case "firewall":
                case "firewalls":
                    return "firewall";

                //Singular and plural both map to password answers
                case "password":
                case "passwords":
                    return "password";

                //Virus is treated the same as malware
                case "malware":
                case "virus":
                case "viruses":
                    return "malware";

                //Singular and plural both map to vpn answers
                case "vpn":
                case "vpns":
                    return "vpn";

                //Scam and scammed are treated the same as fraud
                case "fraud":
                case "scam":
                case "scammed":
                    return "fraud";


                case "ransomware":
                    return "ransomware";

                //Different forms of the word encryption map to the same answers
                case "encryption":
                case "encrypted":
                case "encrypt":
                    return "encryption";

                //2fa and twofactor map to authentication answers
                case "authentication":
                case "2fa":
                case "twofactor":
                    return "authentication";

                //Browser and browse map to browsing answers
                case "browsing":
                case "browser":
                case "browse":
                    return "browsing";

                //Cyber and security alone still map to cybersecurity answers
                case "cybersecurity":
                case "cyber":
                case "security":
                    return "cybersecurity";

                //Bot and chatbot map to malicious answers
                case "bot":
                case "chatbot":
                case "malicious":
                    return "malicious";

                //Word didn't match any topic, return it unchanged and let normal matching handle it
                default:
                    return word;
            }
        }


        //Main AI logic - matches user input words against the reply list
        public void ProcessQuestion(string questions)
        {
            if (string.IsNullOrWhiteSpace(questions))
            {
                _chatDisplay.error_method("ChatBot", "Please enter a valid question.");
                return;
            }

            string[] words = questions
                .ToLower()
                .Split(new char[] { ' ', ',', '.', '?', '!', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

            bool found = false;
            string message = string.Empty;
            Random indexer = new Random();
            List<string> answers_found = new List<string>();

            // Track which keywords have already been matched so we never add two answers for the same topic
            HashSet<string> matchedKeywords = new HashSet<string>();

            foreach (string word in words)
            {
                if (word.Length < 3 || _ignore.Contains(word.ToLower()))
                    continue;

                // --- Interest detection ---
                if (word.Contains("interested"))
                {
                    string interestMessage = _interestTracker.SaveInterests(words, _ignore, _username);
                    message += interestMessage;
                    continue;
                }

                // Normalise the word to its answer-list keyword
                string keyword = NormaliseWord(word);

                // Skip if we already found an answer for this keyword
                if (matchedKeywords.Contains(keyword))
                    continue;

                // Find all answers that match this keyword
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
                    // Pick one random answer for this keyword
                    answers_found.Add(matches[indexer.Next(0, matches.Count)]);
                }
            }

            // Build and display the response
            if (found && answers_found.Count > 0)
            {
                foreach (string per_answer in answers_found)
                    message += per_answer + "\n";

                _chatDisplay.error_method("ChatBot", message.TrimEnd('\n'));
            }
            else
            {
                string[] fallbackMessages = {
                    "I'm sorry, I don't understand that. Could you rephrase your question?",
                    "I didn't quite get that. Try asking me about topics like phishing, passwords, or VPNs.",
                    "Hmm, I'm not sure how to respond to that. Can you ask something else?",
                    "I couldn't find an answer for that. Try asking about cybersecurity, malware, or online safety.",
                    "My apologies, I don't have information on that topic yet."
                };

                Random random = new Random();
                _chatDisplay.error_method("ChatBot", fallbackMessages[random.Next(fallbackMessages.Length)]);
            }

        }//end of ProcessQuestion method

    }//end of class
}//end of namespace