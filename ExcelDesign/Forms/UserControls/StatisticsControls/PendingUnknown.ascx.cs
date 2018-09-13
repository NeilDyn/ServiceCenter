using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls
{
    public partial class Pending_Unknown : System.Web.UI.UserControl
    {
        public List<StatisticsSalesLine> UnknownList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateData()
        {
            try
            {
                TableRow breakRow = new TableRow();
                TableCell breakCell = new TableCell
                {
                    Text = "<br />"
                };

                breakRow.Cells.Add(breakCell);
                tblPendingUnknown.Rows.Add(breakRow);

                int invNotAvail = 0;
                int olderThan72Hours = 0;
                int olderThan48Hours = 0;

                foreach (StatisticsSalesLine line in UnknownList)
                {
                    if (!line.IsPendingSQApproval)
                    {
                        if (line.IsOlderThan72Hours)
                        {
                            olderThan72Hours++;
                        }
                        else if (line.IsOlderThan48Hours)
                        {
                            olderThan48Hours++;
                        }

                        if (line.IsNotInvtAvailable)
                        {
                            invNotAvail++;
                        }
                    }
                }

                if (olderThan72Hours > 0)
                {
                    tcUnknownOlderThan72Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"Unknown\", \"72Hours\")'>" + olderThan72Hours.ToString() + "</a>";
                }
                else
                {
                    tcUnknownOlderThan72Hours.Text = olderThan72Hours.ToString();
                }

                if (invNotAvail > 0)
                {
                    tcUnknownNoInvAvail.Text = "<a href='javascript:OpenSalesLineWindow(\"Unknown\", \"NoInventory\")'>" + invNotAvail.ToString() + "</a>";
                }
                else
                {
                    tcUnknownNoInvAvail.Text = invNotAvail.ToString();
                }

                if (olderThan48Hours > 0)
                {
                    tcUnknownOlderThan48Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"Unknown\", \"48Hours\")'>" + olderThan48Hours.ToString() + "</a>";
                }
                else
                {
                    tcUnknownOlderThan48Hours.Text = olderThan48Hours.ToString();
                }

                if(UnknownList.Count > 0)
                {
                    tcAllUnknownPending.Text = "<a href='javascript:OpenSalesLineWindow(\"Unknown\", \"AllPending\")'>" + UnknownList.Count.ToString() + "</a>";
                }
                else
                {
                    tcAllUnknownPending.Text = UnknownList.Count.ToString();
                }

            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;

                if (Request.Url.AbsoluteUri.Contains("Forms"))
                {
                    Response.Redirect("ErrorForm.aspx");
                }
                else
                {
                    Response.Redirect("Forms/ErrorForm.aspx");
                }
            }
        }
    }
}