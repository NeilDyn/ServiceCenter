using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
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

        public string PendingList { get; set; }
        protected string pendingType;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);
            try
            {
                if (!this.Page.User.Identity.IsAuthenticated || Session["ActiveUser"] == null)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }

                PopulateData();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "exceptionAlert", "alert('" + ex.Message + "');", true);
            }
        }

        public void PopulateData()
        {
            User u = (User)Session["ActiveUser"];
            PendingList = Convert.ToString(Request.QueryString["PendingList"]);
            pendingType = Convert.ToString(Request.QueryString["PendingType"]);

            SalesLineList = (List<StatisticsSalesLine>)Session["StatisticsSalesLine"];
            List<StatisticsSalesLine> pendingDisplayList = new List<StatisticsSalesLine>();
            List<StatisticsSalesLine> displayList = new List<StatisticsSalesLine>();

            bool processAndSuggest = false;

            switch (PendingList)
            {
                case "Replacement":
                    Title = "Statistics - Pending Replacement ";
                    ProcessColumn.Visible = true;
                    RefundProcessing.Visible = false;
                    ExchangeOrderNo.Visible = false;
                    
                    if(u.Supervisor)
                    {
                        SimilarItemNo.Visible = true;
                        LookupSimalarItem.Visible = true;
                        RemoveSelectedSimilarItem.Visible = true;
                    }

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
                    SimilarItemNo.Visible = false;
                    LookupSimalarItem.Visible = false;
                    RemoveSelectedSimilarItem.Visible = false;

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
                    SimilarItemNo.Visible = false;
                    LookupSimalarItem.Visible = false;
                    RemoveSelectedSimilarItem.Visible = false;

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
                    SimilarItemNo.Visible = false;
                    LookupSimalarItem.Visible = false;
                    RemoveSelectedSimilarItem.Visible = false;

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
                    SimilarItemNo.Visible = false;
                    LookupSimalarItem.Visible = false;
                    RemoveSelectedSimilarItem.Visible = false;

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

                    if ((PendingList == "Replacement" || PendingList == "Refund") && pendingDisplayList.Any(x => !x.Status.ToLower().Contains("no inventory")))
                    {
                        ProcessColumn.Visible = true;
                    }
                    else
                    {
                        ProcessColumn.Visible = false;
                    }

                    if (SimilarItemNo.Visible && pendingDisplayList.Any(x => x.Status.ToLower().Contains("no inventory")) && u.Supervisor)
                    {
                        SimilarItemNo.Visible = true;
                        LookupSimalarItem.Visible = true;
                        RemoveSelectedSimilarItem.Visible = true;
                    }

                    if(ProcessColumn.Visible && SimilarItemNo.Visible)
                    {
                        processAndSuggest = true;
                    }

                    displayList = pendingDisplayList;
                    break;

                case "NoInventory":
                    Title += "No Inventory";

                    ProcessColumn.Visible = false;
                    RefundProcessing.Visible = false;                 

                    if (SimilarItemNo.Visible && u.Supervisor)
                    {
                        SimilarItemNo.Visible = true;
                        LookupSimalarItem.Visible = true;
                        RemoveSelectedSimilarItem.Visible = true;
                    }

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
                int suggestionCount = 0;

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

                    if (PendingList == "Unknown" && u.Supervisor)
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

                    if (PendingList == "Refund")
                    {
                        TableCell refundProcess = new TableCell
                        {
                            Text = line.CustAllowRefund ? "Auto" : "Maunual"
                        };

                        tr.Cells.Add(refundProcess);
                    }

                    if (PendingList == "Replacement" && !line.Status.ToLower().Contains("no inventory"))
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

                    if (PendingList == "Refund" && line.CustAllowRefund)
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

                    if (PendingList == "CompletedExchanges")
                    {
                        tr.Cells.Add(exchangeOrderNo);
                    }

                    if (PendingList == "Replacement" && line.Status.ToLower().Contains("no inventory") && u.Supervisor)
                    {
                        suggestionCount++;
                        docNo.ID = "docNoRepInv_" + lineCount.ToString();
                        TableCell suggestItemCell = new TableCell();
                        TableCell clearSuggestionCell = new TableCell();

                        TableCell suggestItemNoCell = new TableCell
                        {
                            ID = "suggestItemNo_" + lineCount.ToString()
                        };

                        Button btnLookupSimilarItem = new Button
                        {
                            ID = "btnLookupSimilarItem" + lineCount.ToString(),
                            Text = "...",
                            OnClientClick = "return LookupSimilarItem('" + line.ItemNo + "', '" + lineCount + "')"
                        };

                        ImageButton imgBtnClearSuggestion = new ImageButton
                        {
                            ID = "imgBtnClearSuggestion_" + lineCount.ToString(),
                            ImageUrl = "~/images/cancel.png",
                            Height = new Unit("50%"),
                            Width = new Unit("40%"),
                            OnClientClick = "return RemoveSimilarItem('" + line.ItemNo + "', '" + lineCount + "')"
                        };

                        List<string> currItem = new List<string>
                        {
                            line.ItemNo,
                            line.Description,
                            line.UnitCost
                        };

                        Session[line.ItemNo] = currItem; 

                        suggestItemCell.HorizontalAlign = HorizontalAlign.Center;
                        clearSuggestionCell.HorizontalAlign = HorizontalAlign.Center;

                        if (Session[line.ItemNo + "_" + lineCount.ToString()] != null)
                        {
                            suggestItemNoCell.Text = Convert.ToString(Session[line.ItemNo + "_" + lineCount.ToString()]);
                        }

                        suggestItemCell.Controls.Add(btnLookupSimilarItem);
                        clearSuggestionCell.Controls.Add(imgBtnClearSuggestion);

                        if(processAndSuggest)
                        {
                            tr.Cells.Add(new TableCell());
                        }

                        tr.Cells.Add(suggestItemNoCell);
                        tr.Cells.Add(suggestItemCell);
                        tr.Cells.Add(clearSuggestionCell);
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

                if ((PendingList == "Replacement" || PendingList == "Refund") && processCount > 0)
                {
                    TableRow checkboxRow = new TableRow();
                    TableRow buttonRow = new TableRow();
                    TableRow buttonREQRow = new TableRow();

                    TableCell checkBoxCell = new TableCell();
                    TableCell buttonCell = new TableCell();

                    CheckBox checkBox = new CheckBox
                    {
                        ID = "cbxSelectAllProcessItems",
                        Text = "Select All"
                    };

                    Button button = new Button
                    {
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
                    processAndSuggest = false;
                }

                if (PendingList == "Replacement" && suggestionCount > 0)
                {
                    TableRow buttonRow = new TableRow();

                    TableCell buttonCell = new TableCell();
                    Button button = new Button
                    {
                        Text = "Update Suggest Similar Items",
                        OnClientClick = "return ProcessSuggestSimilarItems()"
                    };

                    buttonCell.Controls.Add(button);

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

                    tblStatisticsSalesLines.Rows.Add(buttonRow);
                }

                if (PendingList == "Unknown" && u.Supervisor)
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string RemoveSimilarItem(string itemNo, string lineNo)
        {
            try
            {
                HttpContext.Current.Session[itemNo + "_" + lineNo] = null;
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
        public static string ProcessSuggestSimilarItems(string suggestionList)
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


                StaticService.ProcessSuggestSimilarItems(suggestionList, sessionID);

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