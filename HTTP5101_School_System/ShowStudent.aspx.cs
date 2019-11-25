﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class ShowStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SCHOOLDB db = new SCHOOLDB();
            //showing the base record student information
            ShowStudentInfo(db);
            //showing the classes the student is currently enrolled in
            ListStudentEnrollment(db);
            //populate the dropdownlist for classes for the student to pick
            if (!Page.IsPostBack) { 
                FillClassOptions(db);
            }
        }

        protected void Enrol_Student(object sender, EventArgs e)
        {
            bool valid = true;
            int classid = Int32.Parse(student_classid.SelectedValue);
            string studentid = Request.QueryString["studentid"];
            if (String.IsNullOrEmpty(studentid)) valid = false;

            SCHOOLDB db = new SCHOOLDB();
            if (valid)
            {
                db.EnrolStudent(Int32.Parse(studentid), classid);
                Response.Redirect("ShowStudent.aspx?studentid="+studentid);
            }
        }

        

        protected void Delete_Student(object sender, EventArgs e)
        {
            bool valid = true;
            string studentid = Request.QueryString["studentid"];
            if (String.IsNullOrEmpty(studentid)) valid = false;

            SCHOOLDB db = new SCHOOLDB();

            //deleting the student from the system
            if (valid) { 
                db.DeleteStudent(Int32.Parse(studentid));
                Response.Redirect("ListStudents.aspx");
            }
        }

        protected void ListStudentEnrollment(SCHOOLDB db)
        {
            //validation algorithm for student enrollment
            //want to make sure that the student id is valid
            bool valid = true;
            string studentid = Request.QueryString["studentid"];
            if (String.IsNullOrEmpty(studentid)) valid = false;
            student_classes.InnerHtml = ""; //reset this
            if (valid) {
                string query = "select CLASSES.classid, TEACHERS.teacherid, classcode, classname, concat(teacherfname, ' ', teacherlname) as 'teachername' from STUDENTSXCLASSES INNER JOIN CLASSES ON STUDENTSXCLASSES.CLASSID = CLASSES.CLASSID left join TEACHERS on TEACHERS.teacherid = classes.teacherid WHERE STUDENTSXCLASSES.STUDENTID=" + studentid;
                List<Dictionary<String, String>> Student_Classes = db.List_Query(query);
                if (Student_Classes.Count > 0) { 
                    foreach (Dictionary<String,String> Student_Class in Student_Classes)
                    {
                        string classcode = Student_Class["classcode"];
                        string classname = Student_Class["classname"];
                        string teachername = Student_Class["teachername"];
                        string teacherid = Student_Class["teacherid"];
                        string classid = Student_Class["classid"];
                        student_classes.InnerHtml += "<div class=\"listitem\">";
                        student_classes.InnerHtml += "<div class=\"col4\"><a href=\"ShowClass.aspx?classid="+classid+"\">"+classcode+"</a></div>";
                        student_classes.InnerHtml += "<div class=\"col4\">"+classname+"</div>";
                        student_classes.InnerHtml += "<div class=\"col4\"><a href=\"ShowTeacher.aspx?teacherid="+teacherid+"\">"+teachername+"</a></div>";
                        //building a direct button in here is too convoluted
                        student_classes.InnerHtml += "<div class=\"col4last\"><a href=\"UnenrolStudent.aspx?studentid="+studentid+"&classid="+classid+ "&ref=student\">Unenrol</a></div>";
                        student_classes.InnerHtml += "</div>";
                    }
                }
                else
                {
                    student_classes.InnerHtml = "This student is not part of any classes.";
                }
            }
            else
            {
                student_classes.InnerHtml = "An error occurred.";
            }
        }

        protected void ShowStudentInfo(SCHOOLDB db)
        {

            bool valid = true;
            string studentid = Request.QueryString["studentid"];
            if (String.IsNullOrEmpty(studentid)) valid = false;

            //We will attempt to get the record we need
            if (valid)
            {

                Student student_record = db.FindStudent(Int32.Parse(studentid));


                student_title_fname.InnerHtml = student_record.GetFname() + " " + student_record.GetLname();
                student_fname.InnerHtml = student_record.GetFname();
                student_lname.InnerHtml = student_record.GetLname();
                student_number.InnerHtml = student_record.GetNumber();
                enrolment_date.InnerHtml = student_record.GetEnrolDate().ToString("yyyy-M-dd");
            }
            else
            {
                valid = false;
            }
            

            if (!valid)
            {
                student.InnerHtml = "There was an error finding that student.";
            }
        }

        protected void FillClassOptions(SCHOOLDB db)
        {
            string query = "select * from classes";
            List<Dictionary<String, String>> rs = db.List_Query(query);
            foreach (Dictionary<String, String> row in rs)
            {
                string classtitle = row["CLASSCODE"] + " " + row["CLASSNAME"];
                string classid = row["CLASSID"];
                ListItem classoption = new ListItem(classtitle, classid);
                student_classid.Items.Add(classoption);
            }
        }
    }
}