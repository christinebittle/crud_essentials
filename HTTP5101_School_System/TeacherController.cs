using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace HTTP5101_School_System
{
    public class TeacherController : SCHOOLDB
    {
        //This could be represented as
        //public Teacher FindTeacher(int id)
        public Dictionary<String, String> FindTeacher(int id)
        {
            //Utilize the connection string
            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            //create a "blank" teacher so that our method can return something if we're not successful catching student data
            Dictionary<String, String> teacher = new Dictionary<String, String>();

            //we will try to grab teacher data from the database, if we fail, a message will appear in Debug>Windows>Output dialogue
            try
            {
                //Build a custom query with the id information provided
                string query = "select * from TEACHERS where teacherid = " + id;
                Debug.WriteLine("Connection Initialized...");
                //open the db connection
                Connect.Open();
                //Run out query against the database
                MySqlCommand cmd = new MySqlCommand(query, Connect);
                //grab the result set
                MySqlDataReader resultset = cmd.ExecuteReader();

                //Create a list of teachers (although we're only trying to get 1)
                List<Dictionary<String, String>> Teachers = new List<Dictionary<String, String>>();

                //read through the result set
                while (resultset.Read())
                {
                    //information that will store a single teacher
                    Dictionary<String, String> Teacher = new Dictionary<String, String>();

                    //Look at each column in the result set row, add both the column name and the column value to our Student dictionary
                    for (int i = 0; i < resultset.FieldCount; i++)
                    {
                        Debug.WriteLine("Attempting to transfer data of " + resultset.GetName(i));
                        Debug.WriteLine("Attempting to transfer data of " + resultset.GetString(i));
                        Teacher.Add(resultset.GetName(i), resultset.GetString(i));

                    }
                    //Add the teacher to the list of teachers
                    Teachers.Add(Teacher);
                }

                teacher = Teachers[0]; //get the first teacher

            }
            catch (Exception ex)
            {
                //If something (anything) goes wrong with the try{} block, this block will execute
                Debug.WriteLine("Something went wrong in the find Teacher method!");
                Debug.WriteLine(ex.ToString());
            }

            Connect.Close();
            Debug.WriteLine("Database Connection Terminated.");

            return teacher;
        }
    }
}