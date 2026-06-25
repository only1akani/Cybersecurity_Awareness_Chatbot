using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo
{//start of namespace

    //Records all significant chatbot actions so users can review what the bot has done
    public class activity_log
    {//start of class

        //Stores log entries as a list of strings with timestamps
        private List<string> _log = new List<string>();

        //Adds a new entry to the activity log with a timestamp
        public void LogAction(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return;

            string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {description}";
            _log.Add(entry);
        }

        //Returns the last 10 log entries, most recent first
        public List<string> GetRecentLog(int count = 10)
        {
            return _log
                .AsEnumerable()
                .Reverse()
                .Take(count)
                .ToList();
        }

        //Returns a formatted string of recent actions for display
        //Shows total action count alongside recent entries
        public string GetLogSummary()
        {
            List<string> recent = GetRecentLog(10);

            if (recent.Count == 0)
                return "No actions have been recorded yet.";

            string summary = $"Showing last {recent.Count} action(s) out of {_log.Count} total:\n";
            int count = 1;
            foreach (string entry in recent)
            {
                summary += $"{count}. {entry}\n";
                count++;
            }

            return summary.TrimEnd('\n');
        }

        //Returns total number of log entries recorded
        public int TotalEntries()
        {
            return _log.Count;
        }

    }//end of class

}//end of namespace
