using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class ListTeachers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SCHOOLDB db = new SCHOOLDB();
            ListTeachersInfo(db);

        }

        protected void ListTeachersInfo(SCHOOLDB db)
        {
            teachers_result.InnerHtml = "";
            string query = "select * from teachers";
            string searchkey = teacher_search.Text;

            if (searchkey != "")
            {
                query+= " WHERE TEACHERFNAME LIKE '%"+searchkey+"%'";
                query += " OR TEACHERLNAME LIKE '%" + searchkey + "%'";
                query += " OR EMPLOYEENUMBER LIKE '%" + searchkey + "%'";
            }

            List<Dictionary<String, String>> rs = db.List_Query(query);
            foreach (Dictionary<String, String> row in rs)
            {
                teachers_result.InnerHtml += "<div class=\"listitem\">";
                string teacherid = row["TEACHERID"];

                string teachername = row["TEACHERFNAME"] + " " + row["TEACHERLNAME"];
                teachers_result.InnerHtml += "<div class=\"col4\"><a href=\"ShowTeacher.aspx?teacherid="+teacherid+"\">" + teachername + "</a></div>";
                
                string employeenumber = row["EMPLOYEENUMBER"];
                teachers_result.InnerHtml += "<div class=\"col4\">" + employeenumber + "</div>";

                string salary = row["SALARY"];
                teachers_result.InnerHtml += "<div class=\"col4\">$" + salary + "</div>";

                string hiredate = row["HIREDATE"];
                teachers_result.InnerHtml += "<div class=\"col4last\">" + hiredate + "</div>";

                teachers_result.InnerHtml += "</div>";
            }
        }
    }
}