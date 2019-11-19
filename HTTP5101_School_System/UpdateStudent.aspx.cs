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
            SCHOOLDB db = new SCHOOLDB();
            ShowStudentInfo(db);
        }

        protected void ShowStudentInfo(SCHOOLDB db)
        {

            bool valid = true;
            string studentid = Request.QueryString["studentid"];
            if (String.IsNullOrEmpty(studentid)) valid = false;

            //We will attempt to get the record we need
            if (valid)
            {

                Dictionary<String, String> student_record = db.FindStudent(Int32.Parse(studentid));

                if (student_record.Count > 0)
                {
                    student_title.InnerHtml = student_record["STUDENTFNAME"] + " " + student_record["STUDENTLNAME"];
                    student_fname.Text = student_record["STUDENTFNAME"];
                    student_lname.Text = student_record["STUDENTLNAME"];
                    student_number.Text = student_record["STUDENTNUMBER"];
                    
                }
                else
                {
                    valid = false;
                }
            }

            if (!valid)
            {
                student.InnerHtml = "There was an error finding that student.";
            }
        }
    }
}