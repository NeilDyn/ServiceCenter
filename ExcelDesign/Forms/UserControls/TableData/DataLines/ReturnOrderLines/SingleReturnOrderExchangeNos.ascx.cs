using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines
{
    public partial class SingleReturnOrderExhangeNos : System.Web.UI.UserControl
    {
        public List<string> ExchangeOrderNos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateLines()
        {
            foreach (string no in ExchangeOrderNos)
            {
                TableRow tr = new TableRow();

                TableCell orderNo = new TableCell
                {
                    Text = no,
                    ToolTip = no
                };

                tr.Cells.Add(orderNo);

                this.tblSingleReturnOrderExchangeNo.Rows.Add(tr);
            }
        }
    }
}