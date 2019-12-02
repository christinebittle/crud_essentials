using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class NewStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Add_Student(object sender, EventArgs e)
        {
            //create connection
            StudentController db = new StudentController();

            //create a new particular student
            Student new_student = new Student();
            //set that student data
            new_student.SetFname(student_fname.Text);
            new_student.SetLname(student_lname.Text);
            new_student.SetNumber(student_number.Text);
            new_student.SetEnrolDate(DateTime.Now);

            //add the student to the database
            db.AddStudent(new_student);


            Response.Redirect("ListStudents.aspx");
        }

        
    }
}