using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BestBuyCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerMessage("Begin Running");
            var departments = GetAllDepartments();
            foreach (var department in departments)
            {
                Console.WriteLine(department);
            }

            try
            {
                Console.WriteLine(departments[8]);
            }
            catch(Exception e)
            {
                LoggerError(e);
            }
        }

        static void LoggerMessage(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Environment.NewLine}----------{Environment.NewLine}");
            sb.Append($"{message} {DateTime.Now}");
            sb.Append($"{Environment.NewLine}----------{Environment.NewLine}");
            var filepath = "";

            File.AppendAllText(filepath + "log.txt", sb.ToString());
        }

        static void LoggerError(Exception error)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Environment.NewLine}----------{Environment.NewLine}");
            sb.Append($"{error.Message} {DateTime.Now}");
            sb.Append($"{Environment.NewLine}----------{Environment.NewLine}");
            var filePath = "";
            File.AppendAllText(filePath + "log.txt", sb.ToString());
        }
        static List<string> GetAllDepartments()
        {

            MySqlConnection conn = new MySqlConnection();

            try
            {
                LoggerMessage("Accessing Connection File");

                conn.ConnectionString = System.IO.File.ReadAllText("connectionstring.txt");
            }
            catch(Exception e)
            {
                LoggerError(e);
            }

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT Name FROM departments";

            using (conn)
            {
                conn.Open();
                List<string> allDepartments = new List<string>();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read() == true)
                {
                    string currentDepartment = reader.GetString("Name");
                    allDepartments.Add(currentDepartment);
                }

                return allDepartments;
            }
        }

        static void InsertNewDepartment(string newDepartmentName)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("connectionString.Txt");

            MySqlCommand cmd = conn.CreateCommand();

            //parameterized query to prevent sql injection
            cmd.CommandText = "INSERT INTO departments (Name) VALUES (@deptName)";
            cmd.Parameters.AddWithValue("deptName", newDepartmentName);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        static void DeleteDepartment(string departmentName)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("connectionString.txt");

            MySqlCommand cmd = conn.CreateCommand();

            //parameterized query to prevent sql injection
            cmd.CommandText = "DELETE FROM departments WHERE Name = @departmentName";
            cmd.Parameters.AddWithValue("departmentName", departmentName);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /*static void UpdateDepartmentName(string currentDepartmentName, string newDepartmentName)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("connectionString.txt");
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE departments SET Name = @newName WHERE Name = @currentName";
            cmd.Parameters.AddWithValue("newName", newDepartmentName);
            cmd.Parameters.AddWithValue("currentName", currentDepartmentName);
            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }*/

      static void UpdateDepartmentName(string currentDepartmentName, string newDepartmentName)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "connectionString.txt";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE departments SET Name = @newName WHERE Name = @currentName";
            cmd.Parameters.AddWithValue("currentName", currentDepartmentName);
            cmd.Parameters.AddWithValue("newName", newDepartmentName);
            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
