using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.StatisticsControls.SalesLInes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls
{
    public partial class PendingReplacements : System.Web.UI.UserControl
    {
        public List<StatisticsSalesLine> ReplacementList { get; set; }

        protected TableCell invLinesCell;
        protected TableCell older72HoursLinesCell;

        protected Control invLinesControl;
        protected Control older72HoursLinesControl;

        protected const string invLinesPath = "SalesLines/ReplacementNoInvLines.ascx";
        protected const string older72HoursLinesPath = "SalesLines/ReplacementOlder72HoursLines.ascx";

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
                tblPendingReplacements.Rows.Add(breakRow);

                int invNotAvail = 0;
                int olderThan72Hours = 0;

                List<StatisticsSalesLine> olderThan72HoursList = new List<StatisticsSalesLine>();
                List<StatisticsSalesLine> invNotAvailList = new List<StatisticsSalesLine>();

                foreach (StatisticsSalesLine line in ReplacementList)
                {                  
                    if(line.IsOlderThan72Hours)
                    {
                        olderThan72Hours++;
                        olderThan72HoursList.Add(line);
                    }

                    if (line.IsNotInvtAvailable)
                    {
                        invNotAvail++;
                        invNotAvailList.Add(line);
                    }

                }

                if(olderThan72Hours > 0)
                {
                    tcReplacementOlderThan72Hours.Text = "<a href='javascript:expandReplacementsOlderThan72Hours()'>" + olderThan72Hours.ToString() + "</a>";
                }
                else
                {
                    tcReplacementOlderThan72Hours.Text = olderThan72Hours.ToString();
                }

                if (invNotAvail > 0)
                {
                    tcReplacementNoInvAvail.Text = "<a href='javascript:expandInvNotAvail()'>" + invNotAvail.ToString() + "</a>";
                }
                else
                {
                    tcReplacementNoInvAvail.Text = invNotAvail.ToString();
                }

                PopulateNoInventoryLines(invNotAvailList);
                PopulateOlderThan72HoursLines(olderThan72HoursList);
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

        protected void PopulateNoInventoryLines(List<StatisticsSalesLine> noInvList)
        {
            invLinesCell = new TableCell();
            invLinesControl = LoadControl(invLinesPath);
            ((ReplacementNoInvLines)invLinesControl).NoInvReplacementList = noInvList;
            ((ReplacementNoInvLines)invLinesControl).PopulateData();

            invLinesCell.Controls.Add(invLinesControl);
            invLinesCell.ColumnSpan = 4;
            invLinesCell.Height = new Unit("100%");
            invLinesCell.Width = new Unit("100%");
            expandReplacementNoInventory.Cells.Add(invLinesCell);        
        }

        protected void PopulateOlderThan72HoursLines(List<StatisticsSalesLine> olderThan72HoursList)
        {
            older72HoursLinesCell = new TableCell();
            older72HoursLinesControl = LoadControl(older72HoursLinesPath);
            ((ReplacementOlder72HoursLines)older72HoursLinesControl).Older72HoursReplacementList = olderThan72HoursList;
            ((ReplacementOlder72HoursLines)older72HoursLinesControl).PopulateData();

            older72HoursLinesCell.Controls.Add(older72HoursLinesControl);
            older72HoursLinesCell.ColumnSpan = 4;
            older72HoursLinesCell.Height = new Unit("100%");
            older72HoursLinesCell.Width = new Unit("100%");
            expand72HoursReplacement.Cells.Add(older72HoursLinesCell);
        }
    }
}