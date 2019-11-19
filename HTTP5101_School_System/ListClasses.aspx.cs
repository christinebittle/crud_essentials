using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class ListClasses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SCHOOLDB db = new SCHOOLDB();
            ListClassesInfo(db);
        }

        protected void ListClassesInfo(SCHOOLDB db)
        {
            classes_result.InnerHtml = "";
            string query = "select CLASSES.*, concat (TEACHERFNAME, ' ', TEACHERLNAME) as 'TEACHERNAME' from CLASSES inner join TEACHERS on TEACHERS.teacherid = CLASSES.teacherid";
            string searchkey = class_search.Text;
            if (searchkey != "")
            {
                query += " WHERE CLASSCODE like '%" + searchkey + "%' ";
                query += " or CLASSNAME like '%" + searchkey + "%' ";
                query += " or concat (TEACHERFNAME, ' ', TEACHERLNAME) like '%" + searchkey + "%' ";
            }
            sql_debugger.InnerHtml = query;

            List<Dictionary<String, String>> rs = db.List_Query(query);
            foreach (Dictionary<String, String> row in rs)
            {
                classes_result.InnerHtml += "<div class=\"listitem\">";

                string classid = row["CLASSID"];
                string teacherid = row["TEACHERID"];

                string classcode = row["CLASSCODE"];
                classes_result.InnerHtml += "<div class=\"col5\"><a href=\"ShowClass.aspx?classid=" + classid + "\">" + classcode + "</a></div>";

                string classname = row["CLASSNAME"];
                classes_result.InnerHtml += "<div class=\"col5\">" + classname + "</div>";

                string teachername = row["TEACHERNAME"];
                classes_result.InnerHtml += "<div class=\"col5\"><a href=\"ShowTeacher.aspx?teacherid="+teacherid+"\">" + teachername + "</a></div>";

                string startdate = row["STARTDATE"];
                classes_result.InnerHtml += "<div class=\"col5\">" + startdate + "</div>";

                string finishdate = row["FINISHDATE"];
                classes_result.InnerHtml += "<div class=\"col5last\">" + finishdate + "</div>";


                classes_result.InnerHtml += "</div>";
            }
        }
    }
}