using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;

namespace ExcelDesign.Forms.UserControls
{
    public partial class CustomerInfoDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Populate(Customer c)
        {
            if (c != null)
            {
                //this.lblName.Text = c.Name;
                //this.lblAddress1.Text = c.Address1;
                //this.lblAddress2.Text = c.Address2;
                //this.lblShipToContact.Text = c.ShipToContact;
                //this.lblCity.Text = c.City;
                //this.lblZip.Text = c.Zip;
                //this.lblState.Text = c.State;
                //this.lblCountry.Text = c.Country;
            }
        }
    }
}