using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.CreatedReturn;
using ExcelDesign.Class_Objects.FunctionData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.PDAForms
{
    public partial class CreateRMA : System.Web.UI.Page
    {
        protected List<SalesHeader> Sh;
        protected List<ReturnHeader> Rh;
        public Customer cust;
        protected string no;
        protected string docNo;
        protected string notes;
        protected string email;
        protected string returnTrackingNo;
        protected string imeiNo;

        protected string shipToName;
        protected string shipToAddress1;
        protected string shipToAddress2;
        protected string shipToCity;
        protected string shipToCode;
        protected string shipToState;

        protected bool resources;
        protected bool printRMA;
        protected bool createLabel;
        protected bool update;

        protected string updateRma = string.Empty;
        protected string existingTrackingNo = string.Empty;
        protected bool anyLines = false;

        protected string createdOrderNo;

        protected Thread worker;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserInteraction"] = true;

            if (!IsPostBack)
            {
                List<ReturnReason> rrList = (List<ReturnReason>)Session["ReturnReasons"];

                tcNo.Text = Convert.ToString(Request.QueryString["No"]);
                tcDocNo.Text = Convert.ToString(Request.QueryString["ExternalDocumentNo"]);
                updateRma = Convert.ToString(Request.QueryString["CreateOrUpdate"]);
                existingTrackingNo = Convert.ToString(Request.QueryString["ReturnTrackingNo"]);
                cust = (Customer)Session["SelectedCustomer"];
                Sh = (List<SalesHeader>)Session["SalesHeaders"];

                createdOrderNo = Convert.ToString(Request.QueryString["CreatedOrderNo"]);

                Rh = (List<ReturnHeader>)Session["ReturnHeaders"];

                if (cust != null)
                {
                    tcShipToName.Text = cust.Name;
                    tcShipToAddress1.Text = cust.Address1;
                    tcShipToAddress2.Text = cust.Address2;
                    tcShipToState.Text = cust.State;
                    tcShipToCity.Text = cust.City;
                    tcShipToCode.Text = cust.Zip;
                }

                if (updateRma.ToUpper() == "TRUE")
                {
                    noTitle.Text = "RMA No:";
                    btnCreateRMA.Text = "Update RMA";
                    btnCancelRMA.Visible = true;

                    lblInsertTrackingNo.Visible = true;
                    txtInsertTrackingNo.Visible = true;

                    lblDefaultShipping.Visible = false;
                    cbxDefaultShipping.Visible = false;

                    if (Rh != null)
                    {
                        foreach (ReturnHeader rhItem in Rh)
                        {
                            foreach (SalesHeader head in Sh)
                            {
                                foreach (ShipmentHeader header in head.ShipmentHeaderObject)
                                {
                                    foreach (ShipmentLine line in header.ShipmentLines)
                                    {
                                        foreach (PostedPackage package in head.PostedPackageObject)
                                        {
                                            foreach (PostedPackageLine packageLine in package.PostedPackageLines)
                                            {
                                                if (packageLine.ItemNo == line.ItemNo && package.PostedSourceID == header.No)
                                                {
                                                    if (rhItem.IMEINo == packageLine.SerialNo)
                                                    {
                                                        tcIMEINo.Text = rhItem.IMEINo;
                                                        txtShipToName.Text = rhItem.ShipToName;
                                                        txtShipToAddress1.Text = rhItem.ShipToAddress1;
                                                        txtShipToAddress2.Text = rhItem.ShipToAddress2;
                                                        txtShipToCity.Text = rhItem.ShipToCity;
                                                        txtShipToCode.Text = rhItem.ShipToCode;
                                                        txtShipToState.Text = rhItem.ShipToState;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    txtShipToName.Enabled = false;
                    txtShipToAddress1.Enabled = false;
                    txtShipToAddress2.Enabled = false;
                    txtShipToCity.Enabled = false;
                    txtShipToCode.Enabled = false;
                    txtShipToState.Enabled = false;

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
                    btnCreateRMA.Text = "Create RMA";
                    btnCancelRMA.Visible = false;

                    lblInsertTrackingNo.Visible = false;
                    txtInsertTrackingNo.Visible = false;

                    lblDefaultShipping.Visible = true;
                    cbxDefaultShipping.Visible = true;

                    if (Rh != null)
                    {
                        foreach (ReturnHeader itemRh in Rh)
                        {
                            foreach (SalesHeader head in Sh)
                            {
                                foreach (ShipmentHeader header in head.ShipmentHeaderObject)
                                {
                                    foreach (ShipmentLine line in header.ShipmentLines)
                                    {
                                        foreach (PostedPackage package in head.PostedPackageObject)
                                        {
                                            foreach (PostedPackageLine packageLine in package.PostedPackageLines)
                                            {
                                                if (packageLine.ItemNo == line.ItemNo && package.PostedSourceID == header.No)
                                                {
                                                    if (itemRh.IMEINo == packageLine.SerialNo)
                                                    {
                                                        tcIMEINo.Text = itemRh.IMEINo;
                                                        ClientScript.RegisterStartupScript(this.GetType(), "imeiExists", "alert('There is already an open RMA for " + Rh[0].IMEINo + "');", true);
                                                        ClientScript.RegisterStartupScript(this.GetType(), "imeiExistsClose", "parent.window.close();", true);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (Sh != null && tcIMEINo.Text == "")
                    {
                        foreach (SalesHeader head in Sh)
                        {
                            foreach (ShipmentHeader header in head.ShipmentHeaderObject)
                            {
                                foreach (ShipmentLine line in header.ShipmentLines)
                                {
                                    foreach (PostedPackage package in head.PostedPackageObject)
                                    {
                                        foreach (PostedPackageLine packageLine in package.PostedPackageLines)
                                        {
                                            if (packageLine.ItemNo == line.ItemNo && package.PostedSourceID == header.No)
                                            {
                                                tcIMEINo.Text = packageLine.SerialNo;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
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

            cust = (Customer)Session["SelectedCustomer"];
            createdOrderNo = Convert.ToString(Request.QueryString["CreatedOrderNo"]);
            LoadCreateReturnLines();
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

                                    DropDownList ddlReturnReasonCode = new DropDownList
                                    {
                                        DataValueField = "Display",
                                        DataSource = rrList.Where(x => x.Category != "Part Request"),
                                        ID = "ddlReturnReasonCode_" + lineCount.ToString(),
                                        CssClass = "inputBox"
                                    };

                                    DropDownList ddlREQReturnAction = new DropDownList
                                    {
                                        DataSource = reqList,
                                        ID = "ddlREQReturnAction_" + lineCount.ToString(),
                                        CssClass = "inputBox"
                                    };

                                    //TextBox actionQtyInsert = new TextBox
                                    //{
                                    //    ID = "actionQtyInsert_" + lineCount.ToString(),
                                    //    Text = "1"/**(line.Quantity - removeqty).ToString()*/,
                                    //    Width = new Unit("20%"),
                                    //    CssClass = "inputBox"
                                    //};

                                    ddlReturnReasonCode.DataBind();
                                    ddlREQReturnAction.DataBind();

                                    itemNo.ID = "itemNo_" + lineCount.ToString();
                                    qty.ID = "itemQuantity_" + lineCount.ToString();
                                    desc.ID = "desc_" + lineCount.ToString();
                                    actionQty.ID = "actionQty_" + lineCount.ToString();
                                    returnReasonCode.ID = "returnReasonCode_" + lineCount.ToString();
                                    reqReturnAction.ID = "reqReturnReason_" + lineCount.ToString();

                                    itemNo.Text = line.ItemNo;
                                    desc.Text = line.Description;
                                    qty.Text = "1";
                                    actionQty.Text = "1"; // Can only return one IMEI on PDA Replacement
                                    returnReasonCode.Controls.Add(ddlReturnReasonCode);
                                    ddlREQReturnAction.SelectedIndex = 1;
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
                                        actionQty.BackColor = Color.White;
                                        ddlREQReturnAction.BackColor = Color.White;
                                        ddlReturnReasonCode.BackColor = Color.White;
                                    }
                                    else
                                    {
                                        singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        actionQty.BackColor = ColorTranslator.FromHtml("#EFF3FB");
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

                                            DropDownList ddlReturnReasonCode = new DropDownList
                                            {
                                                DataValueField = "Display",
                                                DataSource = rrList.Where(x => x.Category != "Part Request"),
                                                ID = "ddlReturnReasonCode_" + lineCount.ToString(),
                                                CssClass = "inputBox"
                                            };

                                            DropDownList ddlREQReturnAction = new DropDownList
                                            {
                                                DataSource = reqList,
                                                ID = "ddlREQReturnAction_" + lineCount.ToString(),
                                                CssClass = "inputBox"
                                            };

                                            //TextBox actionQtyInsert = new TextBox
                                            //{
                                            //    ID = "actionQtyInsert_" + lineCount.ToString(),
                                            //    Text = "1",
                                            //    Width = new Unit("20%"),
                                            //    CssClass = "inputBox"
                                            //};

                                            ddlReturnReasonCode.DataBind();
                                            ddlREQReturnAction.DataBind();

                                            itemNo.ID = "itemNo_" + lineCount.ToString();
                                            qty.ID = "itemQuantity_" + lineCount.ToString();
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
                                            qty.Text = "1";
                                            actionQty.Text = "1"; // Can only return one IMEI on PDA Replacement
                                            returnReasonCode.Controls.Add(ddlReturnReasonCode);
                                            reqReturnAction.Controls.Add(ddlREQReturnAction);
                                            ddlREQReturnAction.SelectedIndex = 1;

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
                                                actionQty.BackColor = Color.White;
                                                ddlREQReturnAction.BackColor = Color.White;
                                                ddlReturnReasonCode.BackColor = Color.White;
                                            }
                                            else
                                            {
                                                singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                                actionQty.BackColor = ColorTranslator.FromHtml("#EFF3FB");
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
                ClientScript.RegisterStartupScript(this.GetType(), "exceptionAlert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void BtnCreateRMA_Click(object sender, EventArgs e)
        {
            StringBuilder lineBuild = new StringBuilder();
            StringBuilder shippingBuild = new StringBuilder();
            string lineError = "";

            try
            {
                shipToName = txtShipToName.Text;
                shipToAddress1 = txtShipToAddress1.Text;
                shipToAddress2 = txtShipToAddress2.Text;
                shipToCity = txtShipToCity.Text;
                shipToCode = txtShipToCode.Text;
                shipToState = txtShipToState.Text;

                no = tcNo.Text;
                docNo = tcDocNo.Text;
                notes = txtNotes.Text;
                resources = cbxResources.Checked;
                printRMA = cbxPrintRMA.Checked;
                createLabel = cbxCreateLabel.Checked;
                email = txtCustEmail.Text;
                returnTrackingNo = txtInsertTrackingNo.Text;
                imeiNo = tcIMEINo.Text;

                string validateMsg = ValidateInput();
                bool allValidLines = true;
                int rowCount = 0;
                int controlCount = 0;

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

                            if (cell.ID.Contains("itemQuantity_"))
                            {
                                int.TryParse(cell.Text.ToString(), out qtyLine);
                            }

                            if (cell.ID.Contains("actionQty_"))
                            {
                                int.TryParse(cell.Text.ToString(), out actionQty);
                            }

                            foreach (Control c in cell.Controls)
                            {
                                controlCount++;

                                //if (c.GetType() == typeof(TextBox))
                                //{
                                //    string value = ((TextBox)c).Text;
                                //    int.TryParse(value, out actionQty);
                                //}

                                if (c.GetType() == typeof(DropDownList))
                                {
                                    int index = ((DropDownList)c).SelectedIndex;

                                    if (c.ID.Contains("ddlReturnReasonCode"))
                                    {
                                        reasonCode = ((List<ReturnReason>)Session["ReturnReasons"])[index].ReasonCode;
                                    }
                                    else if (c.ID.Contains("ddlREQReturnAction"))
                                    {
                                        reqReturnAction = index;
                                    }
                                }
                            }

                            string lineValidMessage = string.Empty;

                            if ((rowCount > 1 && controlCount == 2 && actionQty != 0))
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

                        shippingBuild.Append(shipToName).Append(":");
                        shippingBuild.Append(shipToAddress1).Append(":");
                        shippingBuild.Append(shipToAddress2).Append(":");
                        shippingBuild.Append(shipToCity).Append(":");
                        shippingBuild.Append(shipToCode).Append(":");
                        shippingBuild.Append(shipToState);

                        string shippingDetails = shippingBuild.ToString();

                        crh = ss.CreateReturnOrder(no, docNo, string.Empty, notes, resources, printRMA, createLabel, email, lineValues, update, returnTrackingNo,
                            shippingDetails, imeiNo);

                        if (createLabel)
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
                                    ss.IssueReturnLabel(crh.RMANo, email, sessionID);
                                }
                                catch (Exception workerE)
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "labelError", "alert('" + workerE.Message.Replace("'", "\"") + "');", true);
                                }
                            });

                            worker.Start();
                        }

                        Session["CreatedRMA"] = crh;
                        Session["NoUserInteraction"] = true;

                        if (!createLabel)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "returnRMA", "alert('" + crh.RMANo + "');", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "returnRMA", "alert('" + crh.RMANo + ", Return label is being processed and will be emailed within 1-2 hours');", true);
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

                ClientScript.RegisterStartupScript(this.GetType(), "deletedRMA", "alert('" + delete + "');", true);
                ClientScript.RegisterStartupScript(this.GetType(), "closeRMA", "parent.window.close();", true);
            }
            catch (Exception ex)
            {
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
                                return valid;
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
                    else
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
                return lineEx.Message;
            }
        }

        protected string ValidateInput()
        {
            string valid = "All Input Valid";

            try
            {
                if (!String.IsNullOrEmpty(shipToName) || !String.IsNullOrWhiteSpace(shipToName))
                {
                    if (!String.IsNullOrEmpty(shipToAddress1) || !String.IsNullOrWhiteSpace(shipToAddress1))
                    {
                        if (!String.IsNullOrEmpty(shipToCity) || !String.IsNullOrWhiteSpace(shipToCity))
                        {
                            if (!String.IsNullOrEmpty(shipToCode) || !String.IsNullOrWhiteSpace(shipToCode))
                            {
                                if (!String.IsNullOrEmpty(shipToState) || !String.IsNullOrWhiteSpace(shipToState))
                                {
                                    if (shipToState.Length <= 2)
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
                                    else
                                    {
                                        return "Ship To State should be a maximum of 2 characters.";
                                    }
                                    
                                }
                                else
                                {
                                    return "Please insert a valid Ship to State.";
                                }
                            }
                            else
                            {
                                return "Please insert a valid Ship to Code.";
                            }
                        }
                        else
                        {
                            return "Please insert a valid Ship to City.";
                        }
                    }
                    else
                    {
                        return "Please insert a valid Ship to Address 1.";
                    }
                }
                else
                {
                    return "Please insert a valid Ship to Name.";
                }
            }
            catch (Exception e)
            {
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
            catch (Exception)
            {
                return false;
            }
        }
    }
}
