using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class ShowTeacher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SCHOOLDB db = new SCHOOLDB();
            ShowTeacherInfo(db);
            ListTeacherClasses(db);
        }

        protected void ListTeacherClasses(SCHOOLDB db)
        {
            //validation algorithm for student enrollment
            //want to make sure that the student id is valid
            bool valid = true;
            string teacherid = Request.QueryString["teacherid"];
            if (String.IsNullOrEmpty(teacherid)) valid = false;
            teacher_classes.InnerHtml = ""; //reset this
            if (valid)
            {
                string query = "select CLASSES.classid, classcode, classname from CLASSES WHERE classes.teacherid =" + teacherid;
                List<Dictionary<String, String>> Teacher_Classes = db.List_Query(query);
                if (Teacher_Classes.Count > 0)
                {
                    foreach (Dictionary<String, String> Teacher_Class in Teacher_Classes)
                    {
                        string classcode = Teacher_Class["classcode"];
                        string classname = Teacher_Class["classname"];
                       
                        string classid = Teacher_Class["classid"];
                        teacher_classes.InnerHtml += "<div class=\"listitem\">";
                        teacher_classes.InnerHtml += "<div class=\"col2\"><a href=\"ShowClass.aspx?classid=" + classid + "\">" + classcode + "</a></div>";
                        teacher_classes.InnerHtml += "<div class=\"col2last\">" + classname + "</div>";
                        teacher_classes.InnerHtml += "</div>";
                    }
                }
                else
                {
                    teacher_classes.InnerHtml = "This teacher is not teaching any classes.";
                }
            }
            else
            {
                teacher_classes.InnerHtml = "An error occurred.";
            }
        }

        protected void ShowTeacherInfo(SCHOOLDB db)
        {

            bool valid = true;
            string teacherid = Request.QueryString["teacherid"];
            if (String.IsNullOrEmpty(teacherid)) valid = false;

            //We will attempt to get the record we need
            if (valid)
            {

                Dictionary<String, String> teacher_record = db.FindTeacher(Int32.Parse(teacherid));

                if (teacher_record.Count > 0)
                {
                    teacher_title_fname.InnerHtml = teacher_record["TEACHERFNAME"] + " " + teacher_record["TEACHERLNAME"];
                    teacher_fname.InnerHtml = teacher_record["TEACHERFNAME"];
                    teacher_lname.InnerHtml = teacher_record["TEACHERLNAME"];
                    teacher_employee_number.InnerHtml = teacher_record["EMPLOYEENUMBER"];
                    teacher_hire_date.InnerHtml = teacher_record["HIREDATE"];
                    teacher_salary.InnerHtml = teacher_record["SALARY"];
                }
                else
                {
                    valid = false;
                }
            }

            if (!valid)
            {
                teacher.InnerHtml = "There was an error finding that teacher.";
            }
        }
    }
}