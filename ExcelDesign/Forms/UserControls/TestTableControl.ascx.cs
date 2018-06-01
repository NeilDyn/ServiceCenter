using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;

namespace ExcelDesign.Forms.UserControls
{
    public partial class TestTableControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Person> person = new List<Person>();
            person.Add(new Person("Neil", "Jansen"));
            person.Add(new Person("Jaco", "Botha"));
            person.Add(new Person("Nicholas", "Christodoulou"));

            this.gdvTestTable.DataSource = person;
            this.gdvTestTable.DataBind();
        }
    }
}