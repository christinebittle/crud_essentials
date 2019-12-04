using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTP5101_School_System
{
    public partial class FeaturedClasses : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SCHOOLDB db = new SCHOOLDB();
            ListFeaturedClasses(db);
        }
        
        protected void ListFeaturedClasses(SCHOOLDB db)
        {
            //query the most popular classes (most students)
            string query = "select count(studentsxclasses.studentid) as 'popularity', CLASSES.* from CLASSES LEFT JOIN STUDENTSXCLASSES ON CLASSES.classid = STUDENTSXCLASSES.classid group by CLASSES.classid limit 4";
            List<Dictionary<String, String>> rs = db.List_Query(query);

            foreach(Dictionary<String,String> row in rs)
            {
                featured_classes.InnerHtml += "<div class=\"featuretile\"><div class=\"tile\"><div class=\"tileheader\">"+row["CLASSCODE"]+"</div>"+"popularity:"+row["popularity"]+"</div></div>";


            }
        }
    }
}