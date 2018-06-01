using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ExcelDesign.Forms
{
    public partial class ErrorForm : System.Web.UI.Page
    {
        protected Control test;

        protected void Page_Load(object sender, EventArgs e)
        {           
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(this.Button1.Text == "+")
            {
                this.Button1.Text = "-";
                test = LoadControl("UserControls/TestTableControl.ascx");
                this.CustomerOrderCell.Controls.Add(test);
            }
            else
            {
                this.Button1.Text = "+";
                this.CustomerOrderCell.Controls.Remove(test);
            }
        }
    }
}