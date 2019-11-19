using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class ShowClass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SCHOOLDB db = new SCHOOLDB();
            //this code puts the information about the class onto the page
            ShowClassInfo(db);
            //this code prints every student inside this class
            ListClassEnrollment(db);
            //we could pick the student to go into the class..
            //makes more sense to pick the class for the student (in ShowStudent.aspx.cs)
        }

        protected void ListClassEnrollment(SCHOOLDB db)
        {
            //validation algorithm for student enrollment
            //want to make sure that the student id is valid
            bool valid = true;
            string classid = Request.QueryString["classid"];
            if (String.IsNullOrEmpty(classid)) valid = false;
            class_students.InnerHtml = ""; //reset this
            if (valid)
            {
                string query = "select STUDENTS.* from STUDENTSXCLASSES INNER JOIN STUDENTS ON STUDENTSXCLASSES.STUDENTID = STUDENTS.STUDENTID WHERE STUDENTSXCLASSES.CLASSID=" + classid;
                List<Dictionary<String, String>> Class_Students = db.List_Query(query);
                if (Class_Students.Count > 0)
                {
                    foreach (Dictionary<String, String> Class_Student in Class_Students)
                    {
                        string studentid = Class_Student["STUDENTID"];
                        string studentname = Class_Student["STUDENTFNAME"] + " " + Class_Student["STUDENTLNAME"];
                        string studentnumber = Class_Student["STUDENTNUMBER"];
                        class_students.InnerHtml += "<div class=\"listitem\">";
                        class_students.InnerHtml += "<div class=\"col3\"><a href=\"ShowStudent.aspx?studentid=" + studentid + "\">" + studentname + "</a></div>";
                        class_students.InnerHtml += "<div class=\"col3\">" + studentnumber + "</div>";
                        class_students.InnerHtml += "<div class=\"col3last\"><a href=\"Delete\">Delete</a></div>";
                        class_students.InnerHtml += "</div>";
                    }
                }
                else
                {
                    class_students.InnerHtml = "This class does not contain any students.";
                }
            }
            else
            {
                class_students.InnerHtml = "An error occurred.";
            }
        }

        protected void ShowClassInfo(SCHOOLDB db)
        {

            bool valid = true;
            string classid = Request.QueryString["classid"];
            if (String.IsNullOrEmpty(classid)) valid = false;

            //We will attempt to get the record we need
            if (valid)
            {

                Dictionary<String, String> class_record = db.FindClass(Int32.Parse(classid));

                if (class_record.Count > 0)
                {
                    string teacherid = class_record["TEACHERID"];
                    class_title_coursecode.InnerHtml = class_record["CLASSCODE"];
                    class_code.InnerHtml = class_record["CLASSCODE"];
                    class_name.InnerHtml = class_record["CLASSNAME"];
                    teacher_name.InnerHtml = "<a href=\"ShowTeacher.aspx?teacherid="+teacherid+"\">"+class_record["TEACHERNAME"]+"</a>";
                    class_start_date.InnerHtml = class_record["STARTDATE"];
                    class_finish_date.InnerHtml = class_record["FINISHDATE"];
                }
                else
                {
                    valid = false;
                }
            }

            if (!valid)
            {
                course.InnerHtml = "There was an error finding that class.";
            }
        }
    }
}