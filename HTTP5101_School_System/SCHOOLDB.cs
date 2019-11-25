using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace HTTP5101_School_System
{
    public class SCHOOLDB
    {
        //things that describe a database
        // - A connection (url, port)
        // - A username
        // - A password
        // - A database

        //do not expose these pieces of information!
        //how do I know these?
        //boot up MAMP, open start page
        //info is right below the PHPmyAdmin link
        //THIS INFO IS FOR A PUBLIC DATABASE THAT I CREATED, IT HAS READ-ONLY ACCESS
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "8889"; } }

        //ConnectionString is something that we use to connect to a database
        private static string ConnectionString {
            get {
                return "server = "+Server
                    +"; user = "+User
                    +"; database = "+Database
                    +"; port = "+Port
                    +"; password = "+Password;
            }
        }

        //returns a result set
        //is a list dictionaries
        //a dictionary is like a list but with Key:Value pairs
        //they are also known as "associative arrays"
        public List<Dictionary<String,String>> List_Query(string query)
        {
            MySqlConnection Connect = new MySqlConnection(ConnectionString);

            // INPUT -> (string) SELECT QUERY 
            // e.g. "select * from students";
            // OUTPUT -> (List<Dictionary<String,String>>) RESULT SET 
            // e.g. 
            //[
            //  {"STUDENTFNAME":"SARAH","STUDENTLNAME":"Valdez","STUDENTNUMBER":"N1678","ENROLMENTDATE":"2018-06-18"},
            //  {"STUDENTFNAME":"Jennifer","STUDENTLNAME":"FAULKNER","STUDENTNUMBER":"N1679","ENROLMENTDATE":"2018-08-02"},
            //  {"STUDENTFNAME":"Austin","STUDENTLNAME":"Simon","STUDENTNUMBER":"N1682","ENROLMENTDATE":"2018-06-14"},
            //  ...
            //] 
            List<Dictionary<String, String>> ResultSet = new List<Dictionary<String, String>>();

            // try{} catch{} will attempt to do everything inside try{}
            // if an error happens inside try{}, then catch{} will execute instead.
            // very useful for debugging without the whole program crashing!
            // this can be easily abused and should be used sparingly.
            try
            {
                Debug.WriteLine("Connection Initialized...");
                Debug.WriteLine("Attempting to execute query "+query);
                //open the db connection
                Connect.Open();
                //give the connection a query
                MySqlCommand cmd = new MySqlCommand(query, Connect);
                //grab the result set
                MySqlDataReader resultset = cmd.ExecuteReader();

                
                //for every row in the result set
                while (resultset.Read())
                {
                    Dictionary<String,String> Row = new Dictionary<String, String>();
                    //for every column in the row
                    for(int i = 0; i < resultset.FieldCount; i++)
                    {
                        Row.Add(resultset.GetName(i), resultset.GetString(i));
                        
                    }
                    
                    ResultSet.Add(Row);
                }//end row
                resultset.Close();

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Something went wrong in the List_Query method!");
                Debug.WriteLine(ex.ToString());
               
            }

            Connect.Close();
            Debug.WriteLine("Database Connection Terminated.");

            return ResultSet;
        }


        //The objective of this method in the schooldb class is to find a particular student given an integer ID
        //instead of returning a dictionary we will return type "STUDENT" in our Student.cs class
        public Student FindStudent(int id)
        {
            //Utilize the connection string
            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            //create a "blank" student so that our method can return something if we're not successful catching student data
            Student result_student = new Student();

            //we will try to grab student data from the database, if we fail, a message will appear in Debug>Windows>Output dialogue
            try
            {
                //Build a custom query with the id information provided
                string query = "select * from STUDENTS where studentid = "+id;
                Debug.WriteLine("Connection Initialized...");
                //open the db connection
                Connect.Open();
                //Run out query against the database
                MySqlCommand cmd = new MySqlCommand(query, Connect);
                //grab the result set
                MySqlDataReader resultset = cmd.ExecuteReader();

                //Create a list of students (although we're only trying to get 1)
                List<Student> students = new List<Student>();
  
                //read through the result set
                while (resultset.Read())
                {
                    //information that will store a single student
                    Student currentstudent = new Student();
                        
                    //Look at each column in the result set row, add both the column name and the column value to our Student dictionary
                    for (int i = 0; i < resultset.FieldCount; i++)
                    {
                        string key = resultset.GetName(i);
                        string value = resultset.GetString(i);
                        Debug.WriteLine("Attempting to transfer "+key+" data of "+ value);
                        //can't just generically put data into a dictionary anymore
                        //must go through each column one by one to insert data into the right property
                        switch (key)
                        {
                            case "STUDENTFNAME":
                                currentstudent.SetFname(value);
                                break;
                            case "STUDENTLNAME":
                                currentstudent.SetLname(value);
                                break;
                            case "STUDENTNUMBER":
                                currentstudent.SetNumber(value);
                                break;
                            case "ENROLMENTDATE":
                                //how to convert a string to a date?
                                //http://net-informations.com/q/faq/stringdate.html
                                //https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1
                                currentstudent.SetEnrolDate(DateTime.ParseExact(value, "M/d/yyyy hh:mm:ss tt", new CultureInfo("en-US")));
                                break;
                        }

                    }
                    //Add the student to the list of students
                    students.Add(currentstudent);
                }
                
                result_student = students[0]; //get the first student

            }
            catch (Exception ex)
            {
                //If something (anything) goes wrong with the try{} block, this block will execute
                Debug.WriteLine("Something went wrong in the find Student method!");
                Debug.WriteLine(ex.ToString());
            }

            Connect.Close();
            Debug.WriteLine("Database Connection Terminated.");

            return result_student;
        }

        //instead of returning a student, we'll provide a student
        public void AddStudent(Student new_student)
        {
            //slightly better way of injecting data into strings

            string query = "insert into students (STUDENTFNAME, STUDENTLNAME, STUDENTNUMBER, ENROLMENTDATE) values ('{0}','{1}','{2}','{3}')";
            query = String.Format(query, new_student.GetFname(), new_student.GetLname(), new_student.GetNumber(), new_student.GetEnrolDate().ToString("yyyy-MM-dd"));

            //This technique is still sensitive to SQL injection
            //

            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            MySqlCommand cmd = new MySqlCommand(query, Connect);
            try
            {
                Connect.Open();
                cmd.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                Debug.WriteLine("Something went wrong in the AddStudent Method!");
                Debug.WriteLine(ex.ToString());
            }

            Connect.Close();
        }

        //instead of returning a student, we'll provide a student
        public void UpdateStudent(int studentid, Student new_student)
        {
            //slightly better way of injecting data into strings

            string query = "update STUDENTS set STUDENTFNAME='{0}', STUDENTLNAME='{1}', STUDENTNUMBER='{2}' where STUDENTID={3}";
            query = String.Format(query, new_student.GetFname(), new_student.GetLname(), new_student.GetNumber(), studentid);

            //This technique is still sensitive to SQL injection
            //we will learn about parameterized queries in the 2nd semester

            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            MySqlCommand cmd = new MySqlCommand(query, Connect);
            try
            {
                Connect.Open();
                cmd.ExecuteNonQuery();
                Debug.WriteLine("Executed query "+query);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Something went wrong in the UpdateStudent Method!");
                Debug.WriteLine(ex.ToString());
            }

            Connect.Close();
        }

        public void DeleteStudent(int studentid)
        {
            //deleting a student will require us to modify two tables
            //one table is the studentsxclasses table (deleting where the studentid is specified)
            //one table is the students table (to delete the student)
            //Note: A MySQL trigger can be set up so that the appropriate studentsxclasses records are deleted
            //when the student is deleted. Currently this database isn't set up with a trigger

            //DELETING ON THE FOREIGN KEY OF STUDENTID IN STUDENTSXCLASSES
            string removeclasses = "delete from STUDENTSXCLASSES where STUDENTID = {0}";
            removeclasses = String.Format(removeclasses, studentid);

            //DELETING ON THE PRIMARY KEY OF STUDENTS
            string removestudent = "delete from STUDENTS where STUDENTID = {0}";
            removestudent = String.Format(removestudent, studentid);

            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            //two commands
            MySqlCommand cmd_removeclasses = new MySqlCommand(removeclasses, Connect);
            MySqlCommand cmd_removestudent = new MySqlCommand(removestudent, Connect);
            try
            {
                //try to execute both commands!
                Connect.Open();
                cmd_removeclasses.ExecuteNonQuery();
                Debug.WriteLine("Executed query " + cmd_removeclasses);
                cmd_removestudent.ExecuteNonQuery();
                Debug.WriteLine("Executed query " + cmd_removestudent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Something went wrong in the delete Student Method!");
                Debug.WriteLine(ex.ToString());
            }

            Connect.Close();
        }

        public void EnrolStudent(int studentid, int classid)
        {
            //This function will attempt to insert into the studentsxclasses table
            //should check to see if that student is already enrolled.
            string query = "select count(studentxclassid) as 'student_count' from studentsxclasses where studentid = {0} and classid={1}";
            query = String.Format(query, studentid, classid);

            string enrolling = "insert into STUDENTSXCLASSES (studentid, classid) VALUES ({0},{1})";
            enrolling = String.Format(enrolling, studentid, classid);

            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            //two commands
            MySqlCommand cmd = new MySqlCommand(query, Connect);
            MySqlCommand enrol_student = new MySqlCommand(enrolling, Connect);
            int studentcount = 0;
            try
            {
                Connect.Open();
                //Check to see if the student is already in the class
                MySqlDataReader resultset = cmd.ExecuteReader();
                //we are only selecting one column ( the count of students in a class )
                while (resultset.Read())
                {
                    studentcount = Int32.Parse(resultset.GetString(0));
                }
                resultset.Close();
                if (studentcount > 0) return; //exit out of the function if that student is enrolled

                //the student is not in the class, so we will enrol them
                else
                {
                    enrol_student.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Something went wrong in the enrol student method!");
                Debug.WriteLine(ex.ToString());
            }
            Connect.Close();
        }

        public void UnenrolStudent(int studentid, int classid)
        {
            string query = "delete from studentsxclasses where studentid = {0} and classid = {1}";
            query = String.Format(query, studentid, classid);
            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            MySqlCommand cmd = new MySqlCommand(query, Connect);
            try
            {
                Connect.Open();
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //If something (anything) goes wrong with the try{} block, this block will execute
                Debug.WriteLine("Something went wrong in the Unenrol Student method!");
                Debug.WriteLine(ex.ToString());
            }
            Connect.Close();
        }

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
                string query = "select CLASSES.*, CONCAT(TEACHERFNAME,' ',TEACHERLNAME) as 'TEACHERNAME' from CLASSES left join TEACHERS on TEACHERS.teacherid = CLASSES.teacherid where classid = " + id.ToString();
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
                        Debug.WriteLine("Attempting to transfer data of " + resultset.GetName(i));
                        Debug.WriteLine("Attempting to transfer data of " + resultset.GetString(i));
                        Course.Add(resultset.GetName(i), resultset.GetString(i));

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


        //This could be represented as
        //public Teacher FindTeacher(int id)
        public Dictionary<String, String> FindTeacher(int id)
        {
            //Utilize the connection string
            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            //create a "blank" teacher so that our method can return something if we're not successful catching student data
            Dictionary<String, String> teacher= new Dictionary<String, String>();

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