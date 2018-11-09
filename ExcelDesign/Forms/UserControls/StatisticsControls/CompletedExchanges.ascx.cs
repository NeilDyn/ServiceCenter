using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls
{
    public partial class CompletedExchanges : System.Web.UI.UserControl
    {
        public List<StatisticsSalesLine> ExchangeList { get; set; }

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
                tblCompletedExchanges.Rows.Add(breakRow);

                int olderThan72Hours = 0;
                int olderThan48Hours = 0;
                int olderThan24Hours = 0;
                int today = 0;

                foreach (StatisticsSalesLine line in ExchangeList)
                {
                    if (line.IsOlderThan72Hours)
                    {
                        olderThan72Hours++;
                    }
                    else if (line.IsOlderThan48Hours)
                    {
                        olderThan48Hours++;
                    }
                    else if (line.IsOlderThan24Hours)
                    {
                        olderThan24Hours++;
                    }
                    else
                    {
                        today++;
                    }
                }

                if (olderThan72Hours > 0)
                {
                    tcExchangeOlderThan72Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"CompletedExchanges\", \"72Hours\")'>" + olderThan72Hours.ToString() + "</a>";
                }
                else
                {
                    tcExchangeOlderThan72Hours.Text = olderThan72Hours.ToString();
                }

                if (olderThan48Hours > 0)
                {
                    tcExchangeOlderThan48Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"CompletedExchanges\", \"48Hours\")'>" + olderThan48Hours.ToString() + "</a>";
                }
                else
                {
                    tcExchangeOlderThan48Hours.Text = olderThan48Hours.ToString();
                }

                if (olderThan24Hours > 0)
                {
                    tcExchangeOlderThan24Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"CompletedExchanges\", \"24Hours\")'>" + olderThan24Hours.ToString() + "</a>";
                }
                else
                {
                    tcExchangeOlderThan24Hours.Text = olderThan24Hours.ToString();
                }

                if (today > 0)
                {
                    tcExchangeToday.Text = "<a href='javascript:OpenSalesLineWindow(\"CompletedExchanges\", \"Today\")'>" + today.ToString() + "</a>";
                }
                else
                {
                    tcExchangeToday.Text = today.ToString();
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