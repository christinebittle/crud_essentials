using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class UpdateStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //We only want to show the data when
            //the user visits the page for the first time
            //make sure to 
            if (!Page.IsPostBack) {
                //this connection instance is for showing data
                SCHOOLDB db = new SCHOOLDB();
                ShowStudentInfo(db);
            }
        }

        protected void Update_Student(object sender, EventArgs e)
        {

            //this connection instance is for editing data
            SCHOOLDB db = new SCHOOLDB();

            bool valid = true;
            string studentid = Request.QueryString["studentid"];
            if (String.IsNullOrEmpty(studentid)) valid = false;
            if (valid)
            {
                Student new_student = new Student();
                //set that student data
                new_student.SetFname(student_fname.Text);
                new_student.SetLname(student_lname.Text);
                new_student.SetNumber(student_number.Text);

                //add the student to the database
                try
                { 
                    db.UpdateStudent(Int32.Parse(studentid), new_student);
                    Response.Redirect("ShowStudent.aspx?studentid="+studentid);
                }
                catch
                {
                    valid = false;
                }
                
            }

            if (!valid)
            {
                student.InnerHtml = "There was an error updating that student.";
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
                student_title.InnerHtml = student_record.GetFname() + " " + student_record.GetLname();
                student_fname.Text = student_record.GetFname();
                student_lname.Text = student_record.GetLname();
                student_number.Text = student_record.GetNumber(); 
            }

            if (!valid)
            {
                student.InnerHtml = "There was an error finding that student.";
            }
        }
    }
}