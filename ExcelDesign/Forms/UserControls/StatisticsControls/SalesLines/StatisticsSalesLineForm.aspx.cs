using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls.SalesLines
{
    public partial class StatisticsSalesLineForm : System.Web.UI.Page
    {
        public List<StatisticsSalesLine> SalesLineList { get; set; }
        protected string pendingList;
        protected string pendingType;

        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateData();
        }

        public void PopulateData()
        {
            pendingList = Convert.ToString(Request.QueryString["PendingList"]);
            pendingType = Convert.ToString(Request.QueryString["PendingType"]);

            SalesLineList = (List<StatisticsSalesLine>)Session["StatisticsSalesLine"];
            List<StatisticsSalesLine> pendingDisplayList = new List<StatisticsSalesLine>();
            List<StatisticsSalesLine> displayList = new List<StatisticsSalesLine>();

            switch (pendingList)
            {
                case "Replacement":
                    Title = "Statistics - Pending Replacement ";

                    foreach (StatisticsSalesLine exchange in SalesLineList)
                    {
                        if (exchange.REQReturnAction.ToUpper() == "EXCHANGE")
                        {
                            pendingDisplayList.Add(exchange);
                        }
                    }
                    break;

                case "Refund":
                    Title = "Statistics - Pending Refund ";

                    foreach (StatisticsSalesLine refund in SalesLineList)
                    {
                        if (refund.REQReturnAction.ToUpper() == "REFUND")
                        {
                            pendingDisplayList.Add(refund);
                        }
                    }
                    break;

                case "Unknown":
                    Title = "Statistics - Pending Unknown ";

                    foreach (StatisticsSalesLine unknown in SalesLineList)
                    {
                        if (unknown.REQReturnAction.ToUpper() == "")
                        {
                            pendingDisplayList.Add(unknown);
                        }
                    }
                    break;

                case "PendingSQApproval":
                    Title = "Statistics - Pending Unknown ";

                    foreach (StatisticsSalesLine sqApproval in SalesLineList)
                    {
                        if (sqApproval.IsPendingSQApproval)
                        {
                            pendingDisplayList.Add(sqApproval);
                        }
                    }
                    break;
            }

            switch (pendingType)
            {
                case "AllPending":
                    Title += "All Pending";

                    displayList = pendingDisplayList;
                    break;

                case "NoInventory":
                    Title += "No Inventory";

                    foreach (StatisticsSalesLine noInv in pendingDisplayList)
                    {
                        if(noInv.IsNotInvtAvailable)
                        {
                            displayList.Add(noInv);
                        }
                    }
                    break;

                case "48Hours":
                    Title += "Older than 48 Hours";

                    foreach (StatisticsSalesLine twoDays in pendingDisplayList)
                    {
                        if (twoDays.IsOlderThan48Hours)
                        {
                            displayList.Add(twoDays);
                        }
                    }
                    break;

                case "72Hours":
                    Title += "Older than 72 Hours";

                    foreach (StatisticsSalesLine threeDays in pendingDisplayList)
                    {
                        if (threeDays.IsOlderThan72Hours)
                        {
                            displayList.Add(threeDays);
                        }
                    }
                    break;
            }

            try
            {
                int lineCount = 0;

                foreach (StatisticsSalesLine line in displayList)
                {
                    lineCount++;

                    TableRow tr = new TableRow();

                    TableCell custNo = new TableCell();
                    TableCell docNo = new TableCell();
                    TableCell externalDocNo = new TableCell();
                    TableCell createdDate = new TableCell();
                    TableCell itemNo = new TableCell();
                    TableCell desc = new TableCell();
                    TableCell qty = new TableCell();
                    TableCell status = new TableCell();

                    custNo.Text = line.CustomerNumber;
                    docNo.Text = line.DocNo;
                    externalDocNo.Text = line.ExternalDocumentNo;
                    createdDate.Text = line.CreatedDate;
                    itemNo.Text = line.ItemNo;
                    desc.Text = line.Description;
                    qty.Text = line.Qty.ToString();
                    status.Text = line.Status;

                    qty.HorizontalAlign = HorizontalAlign.Center;

                    tr.Cells.Add(custNo);
                    tr.Cells.Add(docNo);
                    tr.Cells.Add(externalDocNo);
                    tr.Cells.Add(createdDate);
                    tr.Cells.Add(itemNo);
                    tr.Cells.Add(desc);
                    tr.Cells.Add(qty);
                    tr.Cells.Add(status);

                    if (lineCount % 2 == 0)
                    {
                        tr.BackColor = Color.White;
                    }
                    else
                    {
                        tr.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                    }

                    tblPendingSQApprovalLines.Rows.Add(tr);
                }

                TableRow breakRow = new TableRow();
                TableCell breakCell = new TableCell
                {
                    Text = "<br />"
                };

                breakRow.Cells.Add(breakCell);
                tblPendingSQApprovalLines.Rows.Add(breakRow);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alertError", "alert('" + ex.Message + "');", true);
            }
        }
    }
}