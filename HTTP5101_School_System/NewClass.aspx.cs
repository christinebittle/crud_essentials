using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class NewClass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SCHOOLDB db = new SCHOOLDB();
            //populate the dropdownlist with teachers in the system
            FillTeacherOptions(db);
        }

        protected void FillTeacherOptions(SCHOOLDB db)
        {
            string query = "select * from teachers";
            List<Dictionary<String, String>> rs = db.List_Query(query);
            foreach (Dictionary<String, String> row in rs)
            {
                string teachername = row["TEACHERFNAME"] + " " + row["TEACHERLNAME"];
                string teacherid = row["TEACHERID"];
                ListItem teacheroption = new ListItem(teachername, teacherid);
                class_teacherid.Items.Add(teacheroption);
            }
        }
    }
}