using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace HTTP5101_School_System
{
    public class CourseController : SCHOOLDB
    {
        //This COULD be represented as
        //public Course Find Class()
        //class is a reserved keyword so use that instead
        public Dictionary<String, String> FindClass(int id)
        {
            //Utilize the connection string
            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            //create a "blank" course (class is a keyword) so that 
            //our method can return something if we're not successful
            //catching course data
            Dictionary<String, String> course = new Dictionary<String, String>();

            //we will try to grab student data from the database, if we fail, a message will appear in Debug>Windows>Output dialogue
            try
            {
                //Build a custom query with the id information provided
                string query = "select CLASSES.*, CONCAT(IFNULL(TEACHERFNAME,'no teacher'),' ',IFNULL(TEACHERLNAME,'')) as 'TEACHERNAME' from CLASSES left join TEACHERS on TEACHERS.teacherid = CLASSES.teacherid where classid = " + id.ToString();
                Debug.WriteLine("Connection Initialized...");
                //open the db connection
                Connect.Open();
                //Run out query against the database
                MySqlCommand cmd = new MySqlCommand(query, Connect);
                //grab the result set
                MySqlDataReader resultset = cmd.ExecuteReader();

                //Create a list of courses (although we're only trying to get 1)
                List<Dictionary<String, String>> Courses = new List<Dictionary<String, String>>();

                //read through the result set
                while (resultset.Read())
                {
                    //information that will store a single student
                    Dictionary<String, String> Course = new Dictionary<String, String>();

                    //Look at each column in the result set row, add both the column name and the column value to our Student dictionary
                    for (int i = 0; i < resultset.FieldCount; i++)
                    {
                        string value = "";
                        Debug.WriteLine("Attempting to transfer data of " + resultset.GetName(i));
                        //patch for nullable columns (such as teacherid)
                        if (!resultset.IsDBNull(i))
                        {

                            Debug.WriteLine("Attempting to transfer data of " + resultset.GetString(i));
                            value = resultset.GetString(i);
                            
                        }
                        Course.Add(resultset.GetName(i), value);
                    }
                    //Add the student to the list of courses
                    Courses.Add(Course);
                }

                course = Courses[0]; //get the first course

            }
            catch (Exception ex)
            {
                //If something (anything) goes wrong with the try{} block, this block will execute
                Debug.WriteLine("Something went wrong in the find Class method!");
                Debug.WriteLine(ex.ToString());
            }

            Connect.Close();
            Debug.WriteLine("Database Connection Terminated.");

            return course;
        }

    }
}