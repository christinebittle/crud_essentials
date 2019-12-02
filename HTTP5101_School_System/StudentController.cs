using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace HTTP5101_School_System
{
    public class StudentController : SCHOOLDB
    {
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
                string query = "select * from STUDENTS where studentid = " + id;
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
                        Debug.WriteLine("Attempting to transfer " + key + " data of " + value);
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
            catch (Exception ex)
            {
                Debug.WriteLine("Something went wrong in the AddStudent Method!");
                Debug.WriteLine(ex.ToString());
            }

            Connect.Close();
        }


        public void UpdateStudent(int studentid, Student new_student)
        {
            //slightly better way of injecting data into strings
            //the below technique is known as string formatting. It allows us to make strings without "" + ""
            string query = "update STUDENTS set STUDENTFNAME='{0}', STUDENTLNAME='{1}', STUDENTNUMBER='{2}' where STUDENTID={3}";
            query = String.Format(query, new_student.GetFname(), new_student.GetLname(), new_student.GetNumber(), studentid);
            //The above technique is still sensitive to SQL injection
            //we will learn about parameterized queries in the 2nd semester

            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            MySqlCommand cmd = new MySqlCommand(query, Connect);
            try
            {
                //Try to update a student with the information provided to us.
                Connect.Open();
                cmd.ExecuteNonQuery();
                Debug.WriteLine("Executed query " + query);
            }
            catch (Exception ex)
            {
                //If that doesn't seem to work, check Debug>Windows>Output for the below message
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
            //This command removes all the target student's classes from the studentsxclasses table
            MySqlCommand cmd_removeclasses = new MySqlCommand(removeclasses, Connect);
            //This command removes the particular student from the table
            MySqlCommand cmd_removestudent = new MySqlCommand(removestudent, Connect);
            try
            {
                //try to execute both commands!
                Connect.Open();
                //remember to remove the relational element first
                cmd_removeclasses.ExecuteNonQuery();
                Debug.WriteLine("Executed query " + cmd_removeclasses);
                //then delete the main record
                cmd_removestudent.ExecuteNonQuery();
                Debug.WriteLine("Executed query " + cmd_removestudent);
            }
            catch (Exception ex)
            {
                //if this isn't working as intended, you can check debug>windows>output for the error message.
                Debug.WriteLine("Something went wrong in the delete Student Method!");
                Debug.WriteLine(ex.ToString());
            }

            Connect.Close();
        }

        public void EnrolStudent(int studentid, int classid)
        {
            //This function will attempt to insert into the studentsxclasses table
            //should check to see if that student is already enrolled.
            //if the student is already enrolled, we should do nothing (return)
            string query = "select count(studentxclassid) as 'student_count' from studentsxclasses where studentid = {0} and classid={1}";
            query = String.Format(query, studentid, classid);

            //The actual query which enrols the student into the class
            string enrolling = "insert into STUDENTSXCLASSES (studentid, classid) VALUES ({0},{1})";
            enrolling = String.Format(enrolling, studentid, classid);

            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            //command for checking if the student is enrolled
            MySqlCommand cmd = new MySqlCommand(query, Connect);
            //command for enrolling the student
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
                    studentcount = Int32.Parse(resultset.GetString(0)); //0th column is the count
                }
                resultset.Close();
                if (studentcount > 0) return; //exit out of the function if that student is enrolled

                //the student is not in the class, so we will enrol them
                else
                {
                    enrol_student.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Something went wrong in the enrol student method!");
                Debug.WriteLine(ex.ToString());
            }
            Connect.Close();
        }

        public void UnenrolStudent(int studentid, int classid)
        {
            //The unenrol student doesn't need to be as careful
            //we can indiscriminately remove any instance of a student belonging to a particular class
            string query = "delete from studentsxclasses where studentid = {0} and classid = {1}";
            query = String.Format(query, studentid, classid);
            MySqlConnection Connect = new MySqlConnection(ConnectionString);
            MySqlCommand cmd = new MySqlCommand(query, Connect);
            try
            {
                //try to remove the student from the class
                Connect.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //If something (anything) goes wrong with the try{} block, this block will execute
                //Check debug>windows>output 
                Debug.WriteLine("Something went wrong in the Unenrol Student method!");
                Debug.WriteLine(ex.ToString());
            }
            Connect.Close();
        }
    }
}