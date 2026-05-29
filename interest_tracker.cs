using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace demo
{//Start of namespace
    public class interest_tracker
    {//Start of class

        private string _filename = "interested_topic.txt";

        // Saves interests mentioned in the user's words to file
        // Returns a message fragment to include in the chatbot's reply
        public string SaveInterests(string[] words, ArrayList ignore, string username)
        {
            string store_interests = string.Empty;
            bool found_interest = false;
            HashSet<string> currentInterests = new HashSet<string>();

            foreach (string interest in words)
            {
                string clean = interest.ToLower().Trim();
                clean = Regex.Replace(clean, @"[^a-zA-Z0-9\s]", "");

                // Filter out noise words
                if (!ignore.Contains(clean) && clean != "interested" && clean != "and" && clean != "in" && clean.Length >= 3)
                {
                    found_interest = true;
                    currentInterests.Add(clean);
                }
            }

            store_interests = string.Join(", ", currentInterests);

            if (!found_interest || string.IsNullOrWhiteSpace(store_interests))
                return "Please specify what you're interested in (e.g., 'I am interested in cybersecurity')";

            bool userFound = false;

            if (File.Exists(_filename))
            {
                string[] lines = File.ReadAllLines(_filename);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(username))
                    {
                        userFound = true;

                        //get all the interests
                        string existing = lines[i]
                            .Replace(username + " interested in:", "")
                            .ToLower();

                        HashSet<string> existingSet = new HashSet<string>(
                            existing.Split(',')
                                    .Select(x => x.Trim())
                                    .Where(x => x != "")
                        );

                        //remove duplicates
                        foreach (string item in currentInterests)
                            existingSet.Add(item);

                        string finalList = string.Join(", ", existingSet);
                        lines[i] = username + " interested in: " + finalList;
                        File.WriteAllLines(_filename, lines);

                        return "great, i added " + store_interests + " to your interests and ";
                    }
                }
            }

            if (!userFound)
            {
                File.AppendAllText(_filename, username + " interested in: " + store_interests + "\n");
                return "great, i will remember that you are interested in " + store_interests + " and ";
            }

            return string.Empty;
        }

        // Reads the user's interests from file and returns them as a string
        // Called by chat_processor's auto_show_interest
        public string GetInterests(string username)
        {
            if (!File.Exists(_filename))
                return string.Empty;

            string[] lines = File.ReadAllLines(_filename);

            foreach (string line in lines)
            {
                if (line.StartsWith(username))
                {
                    int colonIndex = line.IndexOf("interested in:");
                    if (colonIndex >= 0)
                        return line.Substring(colonIndex + 14).Trim();
                }
            }

            return string.Empty;
        }

    }//end of class
}//end of namespace