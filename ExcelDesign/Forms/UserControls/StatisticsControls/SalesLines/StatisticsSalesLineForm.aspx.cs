using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls.SalesLines
{
    /* v7.1 3 October 2018 - Neil Jansen
     * Added Send Service Object as static for AJAX WebMethod for Processing Replacements
     * Added Log reference to log any error occurrences
     * Added logic for Is Older than 24 hour bucket
     * Added new WebMethod for AJAX call to Process Replacements
     * Added new button columns that only display if the Pending List is of type Replacement
     */

    public partial class StatisticsSalesLineForm : System.Web.UI.Page
    {
        public List<StatisticsSalesLine> SalesLineList { get; set; }
        protected static SendService StaticService = new SendService();

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(StatisticsSalesLineForm));

        public string pendingList { get; set; }
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
                    ProcessColumn.Visible = true;
                    RefundProcessing.Visible = false;

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
                    ProcessColumn.Visible = true;
                    RefundProcessing.Visible = true;

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
                    ProcessColumn.Visible = false;
                    RefundProcessing.Visible = false;

                    foreach (StatisticsSalesLine unknown in SalesLineList)
                    {
                        if ((unknown.REQReturnAction.ToUpper() == "UNKNOWN") && (!unknown.IsPendingSQApproval))
                        {
                            pendingDisplayList.Add(unknown);
                        }
                    }
                    break;

                case "PendingSQApproval":
                    Title = "Statistics - Pending Unknown ";
                    ProcessColumn.Visible = false;
                    RefundProcessing.Visible = false;

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
                        if (twoDays.IsOlderThan48Hours && !twoDays.IsOlderThan72Hours)
                        {
                            displayList.Add(twoDays);
                        }
                    }
                    break;

                case "24Hours":
                    Title += "Older than 24 Hours";

                    foreach (StatisticsSalesLine oneDay in pendingDisplayList)
                    {
                        if (oneDay.IsOlderThan24Hours && !oneDay.IsOlderThan48Hours)
                        {
                            displayList.Add(oneDay);
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
                int processCount = 0;

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
                    TableCell reqReturnAction = new TableCell();
                    TableCell status = new TableCell();

                    custNo.Text = line.CustomerNumber;
                    docNo.Text = line.DocNo;
                    externalDocNo.Text = line.ExternalDocumentNo;
                    createdDate.Text = line.CreatedDate;
                    itemNo.Text = line.ItemNo;
                    desc.Text = line.Description;
                    qty.Text = line.Qty.ToString();
                    reqReturnAction.Text = line.REQReturnAction;
                    status.Text = line.Status;

                    docNo.ID = "docNo_" + lineCount.ToString();

                    qty.HorizontalAlign = HorizontalAlign.Center;

                    tr.Cells.Add(custNo);
                    tr.Cells.Add(docNo);
                    tr.Cells.Add(externalDocNo);
                    tr.Cells.Add(createdDate);
                    tr.Cells.Add(itemNo);
                    tr.Cells.Add(desc);
                    tr.Cells.Add(qty);
                    tr.Cells.Add(reqReturnAction);
                    tr.Cells.Add(status);

                    if(pendingList == "Refund")
                    {
                        TableCell refundProcess = new TableCell
                        {
                            Text = line.CustAllowRefund ? "Auto" : "Maunual"
                        };

                        tr.Cells.Add(refundProcess);
                    }

                    if (lineCount % 2 == 0)
                    {
                        tr.BackColor = Color.White;
                    }
                    else
                    {
                        tr.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                    }

                    if (pendingList == "Replacement" && !line.Status.ToLower().Contains("no inventory"))
                    {
                        processCount++;
                        docNo.ID = "docNoInv_" + lineCount.ToString();

                        TableCell processCell = new TableCell();
                        CheckBox cb = new CheckBox
                        {
                            ID = "cbxProcess" + lineCount.ToString(),
                            Checked = false
                        };

                        processCell.Controls.Add(cb);
                        tr.Cells.Add(processCell);
                    }

                    if(pendingList == "Refund" && !line.Status.ToLower().Contains("no inventory") && line.CustAllowRefund)
                    {
                        processCount++;
                        docNo.ID = "docNoInv_" + lineCount.ToString();

                        TableCell processCell = new TableCell();
                        CheckBox cb = new CheckBox
                        {
                            ID = "cbxProcess" + lineCount.ToString(),
                            Checked = false
                        };

                        processCell.Controls.Add(cb);
                        tr.Cells.Add(processCell);
                    }

                    tblStatisticsSalesLines.Rows.Add(tr);
                }

                TableRow breakRow = new TableRow();
                TableCell breakCell = new TableCell
                {
                    Text = "<br />"
                };

                breakRow.Cells.Add(breakCell);
                tblStatisticsSalesLines.Rows.Add(breakRow);

                if((pendingList == "Replacement" || pendingList == "Refund") && processCount > 0)
                {
                    TableRow checkboxRow = new TableRow
                    {
                        ID = "ProcessCheckBox"
                    };

                    TableRow buttonRow = new TableRow
                    {
                        ID = "ProcessButton"
                    };

                    TableCell checkBoxCell = new TableCell();
                    TableCell buttonCell = new TableCell();

                    CheckBox checkBox = new CheckBox
                    {
                        ID = "cbxSelectAll",
                        Text = "Select All"
                    };

                    Button button = new Button
                    {
                        ID = "BtnProcessAll",
                        Text = "Process",
                        OnClientClick = "return ProcessItems()"
                    };

                    checkBoxCell.Controls.Add(checkBox);
                    buttonCell.Controls.Add(button);

                    checkboxRow.Cells.Add(new TableCell());
                    checkboxRow.Cells.Add(new TableCell());
                    checkboxRow.Cells.Add(new TableCell());
                    checkboxRow.Cells.Add(new TableCell());
                    checkboxRow.Cells.Add(new TableCell());
                    checkboxRow.Cells.Add(new TableCell());
                    checkboxRow.Cells.Add(new TableCell());
                    checkboxRow.Cells.Add(new TableCell());
                    checkboxRow.Cells.Add(new TableCell());
                    checkboxRow.Cells.Add(new TableCell());
                    checkboxRow.Cells.Add(checkBoxCell);

                    buttonRow.Cells.Add(new TableCell());
                    buttonRow.Cells.Add(new TableCell());
                    buttonRow.Cells.Add(new TableCell());
                    buttonRow.Cells.Add(new TableCell());
                    buttonRow.Cells.Add(new TableCell());
                    buttonRow.Cells.Add(new TableCell());
                    buttonRow.Cells.Add(new TableCell());
                    buttonRow.Cells.Add(new TableCell());
                    buttonRow.Cells.Add(new TableCell());
                    buttonRow.Cells.Add(new TableCell());
                    buttonRow.Cells.Add(buttonCell);

                    tblStatisticsSalesLines.Rows.Add(checkboxRow);
                    tblStatisticsSalesLines.Rows.Add(buttonRow);
                }
                else
                {
                    ProcessColumn.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alertError", "alert('" + ex.Message + "');", true);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ProcessItems(string rmaList, string type)
        {
            try
            {
                string sessionID = string.Empty;
                if (HttpContext.Current.Session["ActiveUser"] != null)
                {
                    User u = (User)HttpContext.Current.Session["ActiveUser"];
                    sessionID = u.SessionID;
                }
                else
                {
                    sessionID = "{A0A0A0A0-A0A0-A0A0-A0A0-A0A0A0A0A0A0}";
                }


                StaticService.ProcessItems(rmaList, sessionID, type);

                HttpContext.Current.Session["NoUserInteraction"] = true;
                HttpContext.Current.Session["UserInteraction"] = true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e);
                return "Error - " + e.Message;
            }

            return "Success";
        }
    }
}