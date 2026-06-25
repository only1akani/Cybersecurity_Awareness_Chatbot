using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace demo
{//start of namespace

    //Represents a single cybersecurity task
    public class CyberTask
    {//start of class
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reminder { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }//end of class

    //Manages all task CRUD operations against a MySQL database
    public class task_manager
    {//start of class

        private string _connectionString;
        private string _baseConnection;
        private activity_log _activityLog;
        private bool _dbReady = false;

        public task_manager(activity_log activityLog, string password)
        {
            _activityLog = activityLog;

            //Build connection strings using the password passed in from MainWindow
            _baseConnection = $"Server=localhost;Uid=root;Pwd={password};";
            _connectionString = $"Server=localhost;Database=cyberchatbot;Uid=root;Pwd={password};";

            //Create database and table on first run
            InitialiseDatabase();
        }

        //Creates the database and tasks table on first run
        private void InitialiseDatabase()
        {
            try
            {
                //Step 1: connect without a database to create it if needed
                using (var connection = new MySqlConnection(_baseConnection))
                {
                    connection.Open();
                    string createDb = "CREATE DATABASE IF NOT EXISTS cyberchatbot;";
                    using (var cmd = new MySqlCommand(createDb, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                //Step 2: connect to cyberchatbot and create the Tasks table
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string createTable = @"
                        CREATE TABLE IF NOT EXISTS Tasks (
                            Id          INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
                            Title       VARCHAR(255) NOT NULL,
                            Description TEXT         NOT NULL,
                            Reminder    VARCHAR(255),
                            IsCompleted TINYINT(1)   NOT NULL DEFAULT 0,
                            CreatedAt   DATETIME     NOT NULL
                        );";
                    using (var cmd = new MySqlCommand(createTable, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                _dbReady = true;
            }
            catch (Exception ex)
            {
                _dbReady = false;
                MessageBox.Show(
                    "Could not connect to MySQL database.\n\n" +
                    "Please check:\n" +
                    "1. MySQL service is running\n" +
                    "2. Your password in MainWindow.xaml.cs is correct\n\n" +
                    "Error: " + ex.Message,
                    "Database Connection Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        //Adds a new task to the MySQL database
        public string AddTask(string title, string description, string reminder = "")
        {
            if (!_dbReady)
                return "Database is not connected. Tasks cannot be saved right now.";

            if (string.IsNullOrWhiteSpace(title))
                return "Please provide a task title.";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string insert = @"
                        INSERT INTO Tasks (Title, Description, Reminder, IsCompleted, CreatedAt)
                        VALUES (@title, @desc, @reminder, 0, @created);";
                    using (var cmd = new MySqlCommand(insert, connection))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@desc", description);
                        cmd.Parameters.AddWithValue("@reminder", reminder ?? "");
                        cmd.Parameters.AddWithValue("@created", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }

                string reminderNote = string.IsNullOrWhiteSpace(reminder)
                    ? "No reminder set."
                    : $"Reminder set for: {reminder}.";

                _activityLog.LogAction($"Task added: '{title}' ({reminderNote})");
                return $"Task added: \"{description}\". {reminderNote}";
            }
            catch (Exception ex)
            {
                return "Error saving task: " + ex.Message;
            }
        }

        //Returns all tasks from the MySQL database
        public List<CyberTask> GetAllTasks()
        {
            var tasks = new List<CyberTask>();

            if (!_dbReady)
                return tasks;

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string select = "SELECT Id, Title, Description, Reminder, IsCompleted, CreatedAt FROM Tasks ORDER BY Id DESC;";
                    using (var cmd = new MySqlCommand(select, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new CyberTask
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.GetString(2),
                                Reminder = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                IsCompleted = reader.GetInt32(4) == 1,
                                CreatedAt = reader.GetDateTime(5)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tasks: " + ex.Message, "Database Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return tasks;
        }

        //Marks a task as completed in the MySQL database
        public string MarkCompleted(int taskId)
        {
            if (!_dbReady)
                return "Database is not connected.";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string update = "UPDATE Tasks SET IsCompleted = 1 WHERE Id = @id;";
                    using (var cmd = new MySqlCommand(update, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", taskId);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                            return $"No task found with ID {taskId}.";
                    }
                }

                _activityLog.LogAction($"Task ID {taskId} marked as completed.");
                return $"Task {taskId} marked as completed!";
            }
            catch (Exception ex)
            {
                return "Error updating task: " + ex.Message;
            }
        }

        //Deletes a task from the MySQL database
        public string DeleteTask(int taskId)
        {
            if (!_dbReady)
                return "Database is not connected.";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string delete = "DELETE FROM Tasks WHERE Id = @id;";
                    using (var cmd = new MySqlCommand(delete, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", taskId);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                            return $"No task found with ID {taskId}.";
                    }
                }

                _activityLog.LogAction($"Task ID {taskId} deleted.");
                return $"Task {taskId} deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Error deleting task: " + ex.Message;
            }
        }

    }//end of class

}//end of namespace