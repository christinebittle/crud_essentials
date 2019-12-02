using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class UpdateTeacher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TeacherController controller = new TeacherController();
            ShowTeacherInfo(controller);
        }

        protected void ShowTeacherInfo(TeacherController controller)
        {

            bool valid = true;
            string teacherid = Request.QueryString["teacherid"];
            if (String.IsNullOrEmpty(teacherid)) valid = false;

            //We will attempt to get the record we need
            if (valid)
            {

                Dictionary<String, String> teacher_record = controller.FindTeacher(Int32.Parse(teacherid));

                if (teacher_record.Count > 0)
                {
                    teacher_title_name.InnerHtml = teacher_record["TEACHERFNAME"] + " " + teacher_record["TEACHERLNAME"];
                    teacher_fname.Text = teacher_record["TEACHERFNAME"];
                    teacher_lname.Text = teacher_record["TEACHERLNAME"];
                    teacher_employee_number.Text = teacher_record["EMPLOYEENUMBER"];
                    
                    teacher_salary.Text = teacher_record["SALARY"];
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