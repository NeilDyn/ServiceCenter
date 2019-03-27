using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.CreatedReturn;
using ExcelDesign.Class_Objects.FunctionData;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Threading;

namespace ExcelDesign.Forms.FunctionForms
{
    /* v7.1 - 3 October 2018 - Neil Jansen
     * Updated logic to filter out incorrect catagories for Return Reason Code
     */
     
     /* v9.2 - 12 December 2018- Neil Jansen
     * Added Zendesk Ticket # field to design and added logic to send through webservice to NAV
     */

    public partial class CreateReturn : System.Web.UI.Page
    {
        protected List<SalesHeader> Sh;
        public Customer cust;
        protected string no;
        protected string docNo;
        protected string notes;
        protected string email;
        protected string returnTrackingNo;
        protected int zendeskTicketNo;

        protected bool resources;
        protected bool printRMA;
        protected bool createLabel;
        protected bool update;

        protected string updateRma = string.Empty;
        protected string existingTrackingNo = string.Empty;
        protected bool anyLines = false;

        protected string createdOrderNo;

        protected Thread worker;

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(CreateReturn));

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserInteraction"] = true;

            try
            {
                if (!IsPostBack)
                {
                    List<ReturnReason> rrList = (List<ReturnReason>)Session["ReturnReasons"];

                    tcNo.Text = Convert.ToString(Request.QueryString["No"]);
                    tcDocNo.Text = Convert.ToString(Request.QueryString["ExternalDocumentNo"]);
                    updateRma = Convert.ToString(Request.QueryString["CreateOrUpdate"]);
                    existingTrackingNo = Convert.ToString(Request.QueryString["ReturnTrackingNo"]);
                    cust = (Customer)Session["SelectedCustomer"];

                    if (cust != null)
                    {
                        tcShipToName.Text = cust.Name;
                        tcShipToAddress1.Text = cust.Address1;
                        tcShipToAddress2.Text = cust.Address2;
                        tcShipToState.Text = cust.State;
                        tcShipToCity.Text = cust.City;
                        tcShipToCode.Text = cust.Zip;
                    }

                    createdOrderNo = Convert.ToString(Request.QueryString["CreatedOrderNo"]);

                    if (updateRma.ToUpper() == "TRUE")
                    {
                        noTitle.Text = "RMA No:";
                        BtnCreateRMA.Text = "Update RMA";
                        BtnCancelRMA.Visible = true;
                    
                        lblInsertTrackingNo.Visible = true;
                        txtInsertTrackingNo.Visible = true;

                        if (existingTrackingNo != string.Empty)
                        {
                            txtInsertTrackingNo.Text = existingTrackingNo;
                            txtInsertTrackingNo.Enabled = false;
                        }
                        else
                        {
                            txtInsertTrackingNo.Enabled = true;
                        }
                    }
                    else
                    {
                        noTitle.Text = "Order No:";
                        BtnCreateRMA.Text = "Create RMA";
                        BtnCancelRMA.Visible = false;

                        lblInsertTrackingNo.Visible = false;
                        txtInsertTrackingNo.Visible = false;
                    }

                    if (Session["ActiveUser"] != null)
                    {
                        User activeUser = (User)Session["ActiveUser"];

                        if (!activeUser.Admin && !activeUser.Developer)
                        {
                            if (!activeUser.CreateReturnLabel)
                            {
                                cbxCreateLabel.Visible = false;
                                lblCreateLabel.Visible = false;
                            }
                        }
                    }
                }

                createdOrderNo = Convert.ToString(Request.QueryString["CreatedOrderNo"]);
                LoadCreateReturnLines();
            }
            catch (Exception loadE)
            {
                Log.Error(loadE.Message, loadE);
            }
        }

        protected void LoadCreateReturnLines()
        {
            try
            {
                TableRow lineRow = new TableRow();

                Sh = (List<SalesHeader>)Session["SalesHeaders"];

                List<ReturnReason> rrList = (List<ReturnReason>)Session["ReturnReasons"];

                List<string> reqList = new List<string>
                {
                    "",
                    "Exchange",
                    "Refund"
                };

                int lineCount = 0;
                string filterNo = string.Empty;

                if (createdOrderNo != null)
                {
                    filterNo = createdOrderNo;
                }
                else
                {
                    filterNo = tcNo.Text;
                }

                foreach (SalesHeader head in Sh)
                {
                    foreach (ShipmentHeader header in head.ShipmentHeaderObject)
                    {
                        if (head.SalesOrderNo == filterNo)
                        {
                            Session["AllowRefund"] = head.WarrantyProp.AllowRefund;

                            foreach (ShipmentLine line in header.ShipmentLines)
                            {
                                int removeqty = 0;
                                foreach (ReceiptLine returnLine in header.ReturnLines)
                                {
                                    if (line.ItemNo == returnLine.ItemNo)
                                    {
                                        removeqty += returnLine.Quantity;
                                    }
                                }

                                if (line.Quantity - removeqty > 0 && line.Type.ToUpper() == "ITEM")
                                {
                                    lineCount++;
                                    anyLines = true;

                                    TableRow singleRow = new TableRow();

                                    TableCell itemNo = new TableCell();
                                    TableCell desc = new TableCell();
                                    TableCell qty = new TableCell();
                                    TableCell actionQty = new TableCell();
                                    TableCell returnReasonCode = new TableCell();
                                    TableCell reqReturnAction = new TableCell();

                                    /* v7.1 - 3 October 2018 - Neil Jansen
                                     * Updated logic to filter out incorrect catagories for Return Reason Code
                                     */
                                    DropDownList ddlReturnReasonCode = new DropDownList
                                    {
                                        DataValueField = "Display",
                                        DataSource = rrList.Where(x => x.Category != "Part Request" && x.Category != "Cancel Order" && x.Category != "Partial Refund" && x.Category != "Unknown"
                                                                   && x.Category != "Nonconformed" && x.Category != "Vendor Rejected"),
                                        ID = "ddlReturnReasonCode_" + lineCount.ToString(),
                                        CssClass = "inputBox"
                                    };

                                    DropDownList ddlREQReturnAction = new DropDownList
                                    {
                                        DataSource = reqList,
                                        ID = "ddlREQReturnAction_" + lineCount.ToString(),
                                        CssClass = "inputBox"
                                    };

                                    TextBox actionQtyInsert = new TextBox
                                    {
                                        ID = "actionQtyInsert_" + lineCount.ToString(),
                                        Text = (line.Quantity - removeqty).ToString(),
                                        Width = new Unit("15%"),
                                        CssClass = "inputBox"
                                    };

                                    ddlReturnReasonCode.DataBind();
                                    ddlREQReturnAction.DataBind();

                                    itemNo.ID = "itemNo_" + lineCount.ToString();
                                    qty.ID = "itemQuanity_" + lineCount.ToString();
                                    desc.ID = "desc_" + lineCount.ToString();
                                    actionQty.ID = "actionQty_" + lineCount.ToString();
                                    returnReasonCode.ID = "returnReasonCode_" + lineCount.ToString();
                                    reqReturnAction.ID = "reqReturnReason_" + lineCount.ToString();

                                    itemNo.Text = line.ItemNo;
                                    desc.Text = line.Description;
                                    qty.Text = (line.Quantity - removeqty).ToString();
                                    actionQty.Controls.Add(actionQtyInsert);
                                    returnReasonCode.Controls.Add(ddlReturnReasonCode);
                                    reqReturnAction.Controls.Add(ddlREQReturnAction);

                                    qty.HorizontalAlign = HorizontalAlign.Center;
                                    actionQty.HorizontalAlign = HorizontalAlign.Center;

                                    singleRow.ID = "returnOrderLineRow_" + lineCount.ToString();

                                    singleRow.Cells.Add(itemNo);
                                    singleRow.Cells.Add(desc);
                                    singleRow.Cells.Add(qty);
                                    singleRow.Cells.Add(actionQty);
                                    singleRow.Cells.Add(returnReasonCode);
                                    singleRow.Cells.Add(reqReturnAction);

                                    if (lineCount % 2 == 0)
                                    {
                                        singleRow.BackColor = Color.White;
                                        actionQtyInsert.BackColor = Color.White;
                                        ddlREQReturnAction.BackColor = Color.White;
                                        ddlReturnReasonCode.BackColor = Color.White;
                                    }
                                    else
                                    {
                                        singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        actionQtyInsert.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        ddlREQReturnAction.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        ddlReturnReasonCode.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                    }

                                    singleRow.Attributes.CssStyle.Add("border-collapse", "collapse");
                                    tblCreateReturnOrderTableDetails.Rows.Add(singleRow);
                                }
                            }
                        }
                        else
                        {
                            foreach (string rmaLine in header.RMANo)
                            {
                                Session["AllowRefund"] = head.WarrantyProp.AllowRefund;

                                if (rmaLine == filterNo)
                                {
                                    foreach (ShipmentLine line in header.ShipmentLines)
                                    {
                                        string req = string.Empty;
                                        string rr = string.Empty;

                                        foreach (ReceiptLine returnLine in header.ReturnLines)
                                        {
                                            if (line.ItemNo == returnLine.ItemNo)
                                            {
                                                req = returnLine.REQReturnAction;
                                                rr = returnLine.ReturnReasonCode;
                                            }
                                        }

                                        if (line.Type.ToUpper() == "ITEM")
                                        {
                                            lineCount++;
                                            anyLines = true;

                                            TableRow singleRow = new TableRow();

                                            TableCell itemNo = new TableCell();
                                            TableCell desc = new TableCell();
                                            TableCell qty = new TableCell();
                                            TableCell actionQty = new TableCell();
                                            TableCell returnReasonCode = new TableCell();
                                            TableCell reqReturnAction = new TableCell();

                                            /* v7.1 - 3 October 2018 - Neil Jansen
                                             * Updated logic to filter out incorrect catagories for Return Reason Code
                                             */

                                            DropDownList ddlReturnReasonCode = new DropDownList
                                            {
                                                DataValueField = "Display",
                                                DataSource = rrList.Where(x => x.Category != "Part Request" && x.Category != "Cancel Order" && x.Category != "Partial Refund" && x.Category != "Unknown"
                                                                   && x.Category != "Nonconformed" && x.Category != "Vendor Rejected"),
                                                ID = "ddlReturnReasonCode_" + lineCount.ToString(),
                                                CssClass = "inputBox"
                                            };

                                            DropDownList ddlREQReturnAction = new DropDownList
                                            {
                                                DataSource = reqList,
                                                ID = "ddlREQReturnAction_" + lineCount.ToString(),
                                                CssClass = "inputBox"
                                            };

                                            TextBox actionQtyInsert = new TextBox
                                            {
                                                ID = "actionQtyInsert_" + lineCount.ToString(),
                                                Text = (line.Quantity).ToString(),
                                                Width = new Unit("15%"),
                                                CssClass = "inputBox"
                                            };

                                            ddlReturnReasonCode.DataBind();
                                            ddlREQReturnAction.DataBind();

                                            itemNo.ID = "itemNo_" + lineCount.ToString();
                                            qty.ID = "itemQuanity_" + lineCount.ToString();
                                            desc.ID = "desc2_" + lineCount.ToString();
                                            actionQty.ID = "actionQty_" + lineCount.ToString();
                                            returnReasonCode.ID = "returnReasonCode_" + lineCount.ToString();
                                            reqReturnAction.ID = "reqReturnAction_" + lineCount.ToString();

                                            if (rr != string.Empty)
                                            {
                                                foreach (ReturnReason rrr in rrList)
                                                {
                                                    if (rrr.ReasonCode == rr)
                                                    {
                                                        ddlReturnReasonCode.SelectedValue = rrr.Display;
                                                    }
                                                }
                                            }

                                            if (req != string.Empty)
                                            {
                                                ddlREQReturnAction.SelectedValue = req;
                                            }

                                            itemNo.Text = line.ItemNo;
                                            desc.Text = line.Description;
                                            qty.Text = (line.Quantity).ToString();
                                            actionQty.Controls.Add(actionQtyInsert);
                                            returnReasonCode.Controls.Add(ddlReturnReasonCode);
                                            reqReturnAction.Controls.Add(ddlREQReturnAction);

                                            qty.HorizontalAlign = HorizontalAlign.Center;
                                            actionQty.HorizontalAlign = HorizontalAlign.Center;

                                            singleRow.ID = "returnOrderLineRow_" + lineCount.ToString();

                                            singleRow.Cells.Add(itemNo);
                                            singleRow.Cells.Add(desc);
                                            singleRow.Cells.Add(qty);
                                            singleRow.Cells.Add(actionQty);
                                            singleRow.Cells.Add(returnReasonCode);
                                            singleRow.Cells.Add(reqReturnAction);

                                            if (lineCount % 2 == 0)
                                            {
                                                singleRow.BackColor = Color.White;
                                                actionQtyInsert.BackColor = Color.White;
                                                ddlREQReturnAction.BackColor = Color.White;
                                                ddlReturnReasonCode.BackColor = Color.White;
                                            }
                                            else
                                            {
                                                singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                                actionQtyInsert.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                                ddlREQReturnAction.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                                ddlReturnReasonCode.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                            }

                                            singleRow.Attributes.CssStyle.Add("border-collapse", "collapse");
                                            tblCreateReturnOrderTableDetails.Rows.Add(singleRow);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (!anyLines)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "noLines", "alert('There are no items available to return.');", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "noLinesClose", "parent.window.close();", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "exceptionAlert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void BtnCreateRMA_Click(object sender, EventArgs e)
        {
            StringBuilder lineBuild = new StringBuilder();
            string lineError = "";

            try
            {
                no = tcNo.Text;
                docNo = tcDocNo.Text;
                notes = txtNotes.Text;
                resources = cbxResources.Checked;
                printRMA = cbxPrintRMA.Checked;
                createLabel = cbxCreateLabel.Checked;
                email = txtCustEmail.Text;
                returnTrackingNo = txtInsertTrackingNo.Text;

                string validateMsg = ValidateInput();
                bool allValidLines = true;
                int rowCount = 0;
                int controlCount = 0;

                if (!String.IsNullOrWhiteSpace(txtZendeskTicketNo.Text) || !String.IsNullOrEmpty(txtZendeskTicketNo.Text))
                {
                    if (txtZendeskTicketNo.Text.Length == 7)
                    {
                        int.TryParse(txtZendeskTicketNo.Text, out zendeskTicketNo);
                    }
                    else
                    {
                        validateMsg = "Zendesk Ticket # should be 7 numeric characters.";
                    }
                }

                if (validateMsg == "All Input Valid")
                {
                    foreach (TableRow row in tblCreateReturnOrderTableDetails.Rows)
                    {
                        rowCount++;
                        string itemNo = string.Empty;
                        int qtyLine = 0;
                        int actionQty = 0;
                        string reasonCode = string.Empty;
                        int reqReturnAction = -1;

                        controlCount = 0;

                        foreach (TableCell cell in row.Cells)
                        {
                            if (cell.ID.Contains("itemNo_"))
                            {
                                itemNo = cell.Text.ToString();
                            }

                            if (cell.ID.Contains("itemQuanity_"))
                            {
                                int.TryParse(cell.Text.ToString(), out qtyLine);
                            }

                            foreach (Control c in cell.Controls)
                            {
                                controlCount++;

                                if (c.GetType() == typeof(TextBox))
                                {
                                    string value = ((TextBox)c).Text;
                                    int.TryParse(value, out actionQty);
                                }

                                if (c.GetType() == typeof(DropDownList))
                                {
                                    int index = ((DropDownList)c).SelectedIndex;

                                    if (c.ID.Contains("ddlReturnReasonCode"))
                                    {
                                        /* 3 October 2018 - Neil Jansen
                                         * Updated logic to filter out incorrect catagories for Return Reason Code
                                         */

                                        List<ReturnReason> sr = (List<ReturnReason>)Session["ReturnReasons"];
                                        List<ReturnReason> rl = new List<ReturnReason>();
                                        foreach (ReturnReason item in sr)
                                        {
                                            if (item.Category != "Part Request" && item.Category != "Cancel Order" && item.Category != "Partial Refund" && item.Category != "Unknown"
                                                                   && item.Category != "Nonconformed" && item.Category != "Vendor Rejected")
                                            {
                                                rl.Add(item);
                                            }
                                        }
                                        reasonCode = (rl)[index].ReasonCode;
                                    }
                                    else if (c.ID.Contains("ddlREQReturnAction"))
                                    {
                                        reqReturnAction = index;
                                    }
                                }
                            }

                            string lineValidMessage = string.Empty;

                            if ((rowCount > 1 && controlCount == 3 && actionQty != 0))
                            {
                                lineValidMessage = ValidateLine(itemNo, qtyLine, actionQty, reasonCode, reqReturnAction);

                                if (lineValidMessage == "Valid Line Input")
                                {
                                    lineBuild.Append(itemNo).Append(":");
                                    lineBuild.Append(actionQty).Append(":");
                                    lineBuild.Append(reasonCode).Append(":");
                                    lineBuild.Append(reqReturnAction).Append(",");
                                }
                                else
                                {
                                    allValidLines = false;

                                    if (lineError == "")
                                    {
                                        lineError = lineValidMessage;
                                    }
                                }
                            }
                        }
                    }

                    if (allValidLines)
                    {
                        string lineValues = lineBuild.ToString();
                        CreatedReturnHeader crh = new CreatedReturnHeader();

                        SendService ss = new SendService();

                        if (no.ToUpper().Contains("RMA"))
                        {
                            update = true;
                        }
                        else
                        {
                            update = false;
                        }

                        /* 12 December 2018 - Neil Jansen 
                         * Pass Zendesk Ticket #
                         */
                        crh = ss.CreateReturnOrder(no, docNo, string.Empty, notes, resources, printRMA, createLabel, email, lineValues, update, returnTrackingNo, 
                            string.Empty, string.Empty, zendeskTicketNo);
                        
                        if(createLabel)
                        {
                            string sessionID = string.Empty;
                            if (Session["ActiveUser"] != null)
                            {
                                User u = (User)Session["ActiveUser"];
                                sessionID = u.SessionID;
                            }
                            else
                            {
                                sessionID = "{A0A0A0A0-A0A0-A0A0-A0A0-A0A0A0A0A0A0}";
                            }

                            worker = new Thread(() =>
                            {
                                try
                                {
                                    ss.LegacyReturnLabel(crh.RMANo, email, sessionID);
                                }
                                catch (Exception workerE)
                                {
                                    Log.Error(workerE.Message, workerE);
                                    ClientScript.RegisterStartupScript(this.GetType(), "labelError", "alert('" + workerE.Message.Replace("'", "\"") + "');", true);
                                }
                            });

                            worker.Start();
                        }

                        Session["CreatedRMA"] = crh;
                        Session["NoUserInteraction"] = true;
                        
                        if(!createLabel)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "returnRMA", "alert('" + crh.RMANo + "');", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "returnLabelRMA", "alert('" + crh.RMANo + ", Return label is being processed and will be emailed within 1-2 hours');", true);
                        }

                        ClientScript.RegisterStartupScript(this.GetType(), "openCreatedRMA", "OpenCreatedRMA();", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "lineError", "alert('" + lineError + "');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "validateMsg", "alert('" + validateMsg + "');", true);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

                ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('" + ex.Message.Replace("'", "\"") + "');", true);

                if (ex.Message.ToLower().Contains("session"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "closeErrorAlert", "parent.window.close();", true);
                }
            }
        }

        protected void BtnCancelRMA_Click(object sender, EventArgs e)
        {
            try
            {
                SendService ss = new SendService();

                string delete = ss.DeleteRMA(tcNo.Text);
                Session["NoUserInteraction"] = true;

                ClientScript.RegisterStartupScript(this.GetType(), "deletedRMA", "alert('" + delete + "');", true);
                ClientScript.RegisterStartupScript(this.GetType(), "closeRMA", "parent.window.close();", true);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "alertError", "alert('" + ex.Message + "');", true);

                if (ex.Message.ToLower().Contains("session"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "closeRMA", "parent.window.close();", true);
                }
            }
        }

        protected string ValidateLine(string itemNoP, int qtyLineP, int actionQtyP, string reasonCodeP, int reqReturnActionP)
        {
            string valid = "Valid Line Input";
            bool refundAllow = Convert.ToBoolean(Session["AllowRefund"]);

            try
            {
                if (actionQtyP >= 0)
                {
                    if ((qtyLineP >= actionQtyP) && (qtyLineP != 0))
                    {
                        if (!String.IsNullOrWhiteSpace(reasonCodeP) || !String.IsNullOrEmpty(reasonCodeP))
                        {
                            if (reqReturnActionP > 0)
                            {
                                if(reqReturnActionP == 2 && !refundAllow)
                                {
                                    return "Refund warranty has expired for the policy of 30 Day Refund and 90 Exchange.";             
                                }
                                else
                                {
                                    return valid;
                                }
                            }
                            else
                            {
                                return "Please select a valid Return Action for Item: " + itemNoP;
                            }
                        }
                        else
                        {
                            return "Please select a valid return reason for Item: " + itemNoP;
                        }
                    }
                    {
                        return "Cannot return more quantity than exists on order for Item: " + itemNoP;
                    }
                }
                else
                {
                    return "Cannot return negative quantity for Item: " + itemNoP;
                }
            }
            catch (Exception lineEx)
            {
                Log.Error(lineEx.Message, lineEx);
                return lineEx.Message;
            }
        }

        protected string ValidateInput()
        {
            string valid = "All Input Valid";

            try
            {
                if (createLabel)
                {
                    if (returnTrackingNo == string.Empty)
                    {
                        User activeUser = (User)Session["ActiveUser"];

                        if (activeUser.CreateReturnLabel || activeUser.Developer || activeUser.Admin)
                        {
                            if (!String.IsNullOrWhiteSpace(email))
                            {
                                if (IsValidEmail(email))
                                {
                                    return valid;
                                }
                                else
                                {
                                    return "Invalid email address entered.";
                                }
                            }
                            else
                            {
                                return "Updated email is required for creating a return label.";
                            }
                        }
                        else
                        {
                            return "You do not have the required permission to issue a return label.";
                        }
                    }
                    else
                    {
                        return "You cannot create a UPS Return Label and insert a Return Tracking Number.";
                    }
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(returnTrackingNo) || !String.IsNullOrEmpty(returnTrackingNo))
                    {
                        if (returnTrackingNo.Length < 41)
                        {
                            return valid;
                        }
                        else
                        {
                            return "Maximum length for a Return Tracking No is 40.";
                        }
                    }     
                    else
                    {
                        return valid;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e);
                return e.Message;
            }
        }

        protected bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch (Exception mailE)
            {
                Log.Error(mailE.Message, mailE);

                return false;
            }
        }
    }
}