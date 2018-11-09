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
            User u = (User)Session["ActiveUser"];
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
                    ExchangeOrderNo.Visible = false;

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
                    ExchangeOrderNo.Visible = false;

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
                    ExchangeOrderNo.Visible = false;

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
                    ExchangeOrderNo.Visible = false;

                    foreach (StatisticsSalesLine sqApproval in SalesLineList)
                    {
                        if (sqApproval.IsPendingSQApproval)
                        {
                            pendingDisplayList.Add(sqApproval);
                        }
                    }
                    break;

                case "CompletedExchanges":
                    Title = "Statistics - Processed Exchanges ";
                    ProcessColumn.Visible = false;
                    RefundProcessing.Visible = false;
                    ExchangeOrderNo.Visible = true;

                    foreach (StatisticsSalesLine completeExchange in SalesLineList)
                    {
                        if (completeExchange.Status.ToUpper().Contains("EXCHANGE PROCESSED"))
                        {
                            pendingDisplayList.Add(completeExchange);
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
                        if (noInv.IsNotInvtAvailable)
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

                case "Today":
                    Title += "Today";

                    foreach (StatisticsSalesLine today in pendingDisplayList)
                    {
                        if (!today.IsOlderThan72Hours && !today.IsOlderThan48Hours && !today.IsOlderThan24Hours)
                        {
                            displayList.Add(today);
                        }
                    }
                    break;
            }

            try
            {
                int lineCount = 0;
                int processCount = 0;

                List<string> reqActions = new List<string>
                {
                    "Unknown",
                    "Refund",
                    "Exchange"
                };

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
                    TableCell exchangeOrderNo = new TableCell();

                    DropDownList reqReturnSelect = new DropDownList
                    {
                        ID = "ddlREQReturnSelect_" + lineCount.ToString(),
                        DataSource = reqActions,
                        CssClass = "inputBox"
                    };

                    reqReturnSelect.DataBind();

                    custNo.Text = line.CustomerNumber;
                    docNo.Text = line.DocNo;
                    externalDocNo.Text = line.ExternalDocumentNo;
                    createdDate.Text = line.CreatedDate;
                    itemNo.Text = line.ItemNo;
                    desc.Text = line.Description;
                    qty.Text = line.Qty.ToString();
                    status.Text = line.Status;
                    exchangeOrderNo.Text = line.ExchangeOrderNo;

                    itemNo.ID = "itemNo_" + lineCount.ToString();
                    docNo.ID = "docNo_" + lineCount.ToString();

                    if (pendingList == "Unknown" && u.Supervisor)
                    {
                        reqReturnAction.Controls.Add(reqReturnSelect);
                    }
                    else
                    {
                        reqReturnAction.Text = line.REQReturnAction;
                    }

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

                    if (pendingList == "Refund")
                    {
                        TableCell refundProcess = new TableCell
                        {
                            Text = line.CustAllowRefund ? "Auto" : "Maunual"
                        };

                        tr.Cells.Add(refundProcess);
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

                    if (pendingList == "Refund" && line.CustAllowRefund)
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

                    if (pendingList == "CompletedExchanges")
                    {
                        tr.Cells.Add(exchangeOrderNo);
                    }

                    if (lineCount % 2 == 0)
                    {
                        tr.BackColor = Color.White;
                        exchangeOrderNo.BackColor = Color.White;
                    }
                    else
                    {
                        tr.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                        exchangeOrderNo.BackColor = ColorTranslator.FromHtml("#EFF3FB");
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

                if ((pendingList == "Replacement" || pendingList == "Refund") && processCount > 0)
                {
                    TableRow checkboxRow = new TableRow
                    {
                        ID = "ProcessCheckBox"
                    };

                    TableRow buttonRow = new TableRow
                    {
                        ID = "ProcessButton"
                    };

                    TableRow buttonREQRow = new TableRow
                    {
                        ID = "UpdateREQButton"
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

                    if (pendingList == "Unknown" && u.Supervisor)
                    {
                        TableRow buttonREQRow = new TableRow
                        {
                            ID = "UpdateREQButton"
                        };

                        TableCell updateButtonCell = new TableCell();

                        Button updateREQReturnAction = new Button
                        {
                            ID = "BtnUpdateREQReturnActions",
                            Text = "Update REQ Return Actions",
                            OnClientClick = "return UpdateREQReturnActions()"
                        };

                        updateButtonCell.Controls.Add(updateREQReturnAction);

                        buttonREQRow.Cells.Add(new TableCell());
                        buttonREQRow.Cells.Add(new TableCell());
                        buttonREQRow.Cells.Add(new TableCell());
                        buttonREQRow.Cells.Add(new TableCell());
                        buttonREQRow.Cells.Add(new TableCell());
                        buttonREQRow.Cells.Add(new TableCell());
                        buttonREQRow.Cells.Add(new TableCell());
                        buttonREQRow.Cells.Add(new TableCell());
                        buttonREQRow.Cells.Add(new TableCell());
                        buttonREQRow.Cells.Add(updateButtonCell);

                        tblStatisticsSalesLines.Rows.Add(buttonREQRow);
                    }
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string UpdateREQReturnActions(string rmaList)
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

                StaticService.UpdateREQReturnAction(rmaList, sessionID);

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