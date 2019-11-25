using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class UpdateClass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SCHOOLDB db = new SCHOOLDB();
            
            //Populate the data for the interface of the class
            ShowClassInfo(db);

            //Fillteacher options presents a dropdownlist with the current teacher preselected
            

        }

        protected void ShowClassInfo(SCHOOLDB db)
        {

            bool valid = true;
            string classid = Request.QueryString["classid"];
            if (String.IsNullOrEmpty(classid)) valid = false;

            //We will attempt to get the record we need
            if (valid)
            {

                Dictionary<String, String> class_record = db.FindClass(Int32.Parse(classid));

                if (class_record.Count > 0)
                {
                    string teacherid = class_record["TEACHERID"];
                    class_title.InnerHtml = class_record["CLASSCODE"]+" "+class_record["CLASSNAME"];
                    class_code.Text = class_record["CLASSCODE"];
                    class_name.Text= class_record["CLASSNAME"];
                    class_start_date.Text = class_record["STARTDATE"];
                    class_finish_date.Text = class_record["FINISHDATE"];
                    int class_teacherid = Int32.Parse(class_record["TEACHERID"]);
                    FillTeacherOptions(db,class_teacherid);
                }
                else
                {
                    valid = false;
                }
            }

            if (!valid)
            {
                course.InnerHtml = "There was an error finding that class.";
            }
        }

        protected void FillTeacherOptions(SCHOOLDB db, int class_current_teacherid)
        {
            string query = "select * from teachers";
            List<Dictionary<String, String>> rs = db.List_Query(query);
            foreach (Dictionary<String, String> row in rs)
            {
                string teachername = row["TEACHERFNAME"] + " " + row["TEACHERLNAME"];
                string teacherid = row["TEACHERID"];
                ListItem teacheroption = new ListItem(teachername, teacherid);
                class_teacherid.Items.Add(teacheroption);
                //preselect the teacherid with data we know
                class_teacherid.SelectedValue = class_current_teacherid.ToString();
            }
        }
    }
}