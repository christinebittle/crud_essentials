using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class UnenrolStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StudentController controller = new StudentController();
            string studentid = Request.QueryString["studentid"];
            string classid = Request.QueryString["classid"];
            if (!Page.IsPostBack) { 
                ShowConfirmation(studentid, classid, controller);
            }
        }

        public void ShowConfirmation(string studentid, string classid, StudentController controller)
        {
            string query = "select STUDENTS.*, CLASSES.* from STUDENTS inner join STUDENTSXCLASSES on STUDENTS.STUDENTID = STUDENTSXCLASSES.STUDENTID inner join CLASSES on CLASSES.CLASSID = STUDENTSXCLASSES.CLASSID where STUDENTS.STUDENTID = {0} and CLASSES.CLASSID = {1}";
            query = String.Format(query, studentid, classid);
            List<Dictionary<String,String>> rs = controller.List_Query(query);
            //first item (should only be one) is the record of enrolment between a student and a class
            Dictionary<String, String> enrolmentrecord = rs.First();
            studentname.InnerHtml = enrolmentrecord["STUDENTFNAME"] + " " + enrolmentrecord["STUDENTLNAME"];
            classname.InnerHtml = enrolmentrecord["CLASSCODE"] + " " + enrolmentrecord["CLASSNAME"];

        }

        protected void Unenrol_Student(object sender, EventArgs e)
        {
            //todo: validation on these ids
            string studentid = Request.QueryString["studentid"];
            string classid = Request.QueryString["classid"];
            string reference = Request.QueryString["ref"];

            StudentController controller = new StudentController();
            
            controller.UnenrolStudent(Int32.Parse(studentid), Int32.Parse(classid));
            if (reference == "student") Response.Redirect("ShowStudent.aspx?studentid=" + studentid);
            else Response.Redirect("ShowClass.aspx?classid="+classid);
           
        }
    }
}