﻿using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.FunctionData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.FunctionForms
{
    /* v9.2 - 13 December 2018 - Neil Jansen
     * Added Zendesk Ticket # field to design and added logic to send through webservice to NAV
     */

    public partial class PartialRefund : System.Web.UI.Page
    {
        protected List<SalesHeader> Sh;
        protected string no;
        protected string docNo;
        protected int zendeskTicketNo;

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(PartialRefund));

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
                }

                LoadPartialRefundLines();
                LoadAlreadyPartialRefundLines();
            }
            catch (Exception loadE)
            {
                Log.Error(loadE.Message, loadE);
            }
        }

        protected void LoadPartialRefundLines()
        {
            try
            {
                TableRow lineRow = new TableRow();

                Sh = (List<SalesHeader>)Session["SalesHeaders"];

                List<ReturnReason> rrList = (List<ReturnReason>)Session["ReturnReasons"];
                List<RefundOptions> ro = new RefundOptions().Populate();

                User u = (User)Session["ActiveUser"];


                int lineCount = 0;
                double orderTotal = 0;
                string filterNo = string.Empty;

                filterNo = tcNo.Text;

                foreach (SalesHeader head in Sh)
                {
                    foreach (ShipmentHeader header in head.ShipmentHeaderObject)
                    {
                        if (head.SalesOrderNo == filterNo)
                        {
                            foreach (ShipmentLine line in header.ShipmentLines)
                            {
                                if (line.Quantity > 0)
                                {
                                    lineCount++;

                                    TableRow singleRow = new TableRow();

                                    TableCell itemNo = new TableCell();
                                    TableCell desc = new TableCell();
                                    TableCell qty = new TableCell();
                                    TableCell actionQty = new TableCell();
                                    TableCell returnReasonCode = new TableCell();
                                    TableCell refundOption = new TableCell();
                                    TableCell lineAmount = new TableCell();
                                    TableCell refundAmount = new TableCell();

                                    DropDownList ddlReturnReasonCode = new DropDownList
                                    {
                                        DataValueField = "Display",
                                        DataSource = rrList.Where(x => x.Category == "Partial Refund" || x.Category == ""),
                                        ID = "ddlReturnReasonCode_" + lineCount.ToString(),
                                        CssClass = "inputBox"
                                    };

                                    DropDownList ddlRefundOption = new DropDownList
                                    {
                                        DataValueField = "Option",
                                        DataSource = ro.Where(o => o.Tier == u.RefundTier),
                                        ID = "ddlRefundOption_" + lineCount.ToString(),
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
                                    ddlRefundOption.DataBind();

                                    itemNo.ID = "itemNo_" + lineCount.ToString();
                                    qty.ID = "itemQuanity_" + lineCount.ToString();
                                    desc.ID = "desc_" + lineCount.ToString();
                                    actionQty.ID = "actionQty_" + lineCount.ToString();
                                    returnReasonCode.ID = "returnReasonCode_" + lineCount.ToString();
                                    refundOption.ID = "refundOption_" + lineCount.ToString();
                                    lineAmount.ID = "lineAmount_" + lineCount.ToString();
                                    refundAmount.ID = "refundAmount_" + lineCount.ToString();

                                    itemNo.Text = line.ItemNo;
                                    desc.Text = line.Description;
                                    qty.Text = (line.Quantity).ToString();
                                    actionQty.Controls.Add(actionQtyInsert);
                                    returnReasonCode.Controls.Add(ddlReturnReasonCode);
                                    refundOption.Controls.Add(ddlRefundOption);
                                    lineAmount.Text = "$      " + line.LineAmount.ToGBString();

                                    if(ro.Count > 0)
                                    {
                                        refundAmount.Text = "$      " + Math.Round((line.LineAmount * 0.1), 2).ToGBString(); //10% is default
                                    }

                                    orderTotal += line.LineAmount;

                                    qty.HorizontalAlign = HorizontalAlign.Center;
                                    actionQty.HorizontalAlign = HorizontalAlign.Center;

                                    singleRow.ID = "partialRefundLineRow_" + lineCount.ToString();

                                    singleRow.Cells.Add(itemNo);
                                    singleRow.Cells.Add(desc);
                                    singleRow.Cells.Add(qty);
                                    singleRow.Cells.Add(actionQty);
                                    singleRow.Cells.Add(returnReasonCode);
                                    singleRow.Cells.Add(refundOption);
                                    singleRow.Cells.Add(lineAmount);
                                    singleRow.Cells.Add(refundAmount);

                                    if (lineCount % 2 == 0)
                                    {
                                        singleRow.BackColor = Color.White;
                                        actionQtyInsert.BackColor = Color.White;
                                        ddlReturnReasonCode.BackColor = Color.White;
                                        ddlRefundOption.BackColor = Color.White;
                                    }
                                    else
                                    {
                                        singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        actionQtyInsert.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        ddlReturnReasonCode.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        ddlRefundOption.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                    }

                                    singleRow.Attributes.CssStyle.Add("border-collapse", "collapse");
                                    tblPartialRefundDetails.Rows.Add(singleRow);
                                }
                            }
                        }
                    }
                }

                Session["OrderTotal"] = orderTotal;

                // Implements 20 dollar refund tier
                if (orderTotal <= 20)
                {
                    for (int i = 1; i < lineCount + 1; i++)
                    {
                        foreach (TableRow row in tblPartialRefundDetails.Rows)
                        {
                            foreach (TableCell cell in row.Cells)
                            {
                                foreach (Control c in cell.Controls)
                                {
                                    if (c.ID.Contains("ddlRefundOption_" + i.ToString()))
                                    {
                                        DropDownList ddlRefundOption = (DropDownList)c;
                                        ddlRefundOption.DataSource = ro.Where(o => o.Tier == "20Dollar");
                                        ddlRefundOption.DataBind();
                                    }                                  
                                }
                            }
                        }                           
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "exceptionAlert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void LoadAlreadyPartialRefundLines()
        {
            try
            {
                bool partialRefundExists = false;
                int lineCount = 0;
                TableRow lineRow = new TableRow();

                Sh = (List<SalesHeader>)Session["SalesHeaders"];

                foreach (SalesHeader header in Sh)
                {
                    foreach (PartialRefunded partref in header.PartialRefunds)
                    {
                        if (partref.RefundAmount > 0)
                        {
                            lineCount++;

                            partialRefundExists = true;

                            TableRow singleRow = new TableRow();

                            TableCell itemNo = new TableCell();
                            TableCell desc = new TableCell();
                            TableCell returnReason = new TableCell();
                            TableCell refundAmount = new TableCell();
                            TableCell refundSalesTax = new TableCell();
                            TableCell refundShippingTax = new TableCell();

                            itemNo.ID = "infoItemNo_" + lineCount.ToString();
                            desc.ID = "infoDesc_" + lineCount.ToString();
                            returnReason.ID = "infoReturnReasonCode_" + lineCount.ToString();
                            refundAmount.ID = "infoRefundAmount_" + lineCount.ToString();
                            refundSalesTax.ID = "infoRefundSalesTax_" + lineCount.ToString();
                            refundShippingTax.ID = "infoRefundShippingTax_" + lineCount.ToString();

                            itemNo.Text = partref.ItemNo;
                            desc.Text = partref.Description;
                            returnReason.Text = partref.ReturnReason;
                            refundAmount.Text = "$      " + partref.RefundAmount.ToGBString();
                            refundSalesTax.Text = "$    " + partref.RefundSalesTax.ToGBString();
                            refundShippingTax.Text = "$    " + partref.RefundShippingTax.ToGBString();

                            refundAmount.HorizontalAlign = HorizontalAlign.Right;
                            refundSalesTax.HorizontalAlign = HorizontalAlign.Right;
                            refundShippingTax.HorizontalAlign = HorizontalAlign.Right;

                            singleRow.Cells.Add(itemNo);
                            singleRow.Cells.Add(desc);
                            singleRow.Cells.Add(returnReason);
                            singleRow.Cells.Add(refundAmount);
                            singleRow.Cells.Add(refundSalesTax);
                            singleRow.Cells.Add(refundShippingTax);

                            if (lineCount % 2 == 0)
                            {
                                singleRow.BackColor = Color.White;
                            }
                            else
                            {
                                singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                            }

                            tblAlreadyPartialRefunded.Rows.Add(singleRow);
                        }
                    }
                }

                if(!partialRefundExists)
                {
                    tblAlreadyPartialRefunded.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "exceptionAlert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void BtnCreatePartialRefund_Click(object sender, EventArgs e)
        {
            StringBuilder lineBuild = new StringBuilder();
            string lineError = "";

            try
            {
                no = tcNo.Text;
                docNo = tcDocNo.Text;

                User u = (User)Session["ActiveUser"];

                string validateMsg = string.Empty;
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

                if (validateMsg == string.Empty)
                {
                    foreach (TableRow row in tblPartialRefundDetails.Rows)
                    {
                        rowCount++;
                        string itemNo = string.Empty;
                        int qtyLine = 0;
                        int actionQty = 0;
                        string reasonCode = string.Empty;
                        decimal refundOption = 0;

                        controlCount = 0;
                        int cellCount = 0;

                        foreach (TableCell cell in row.Cells)
                        {
                            cellCount++;

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

                                    if (c.ID.Contains("ddlReturnReasonCode_"))
                                    {
                                        List<ReturnReason> sr = (List<ReturnReason>)Session["ReturnReasons"];
                                        List<ReturnReason> rl = new List<ReturnReason>();
                                        foreach (ReturnReason item in sr)
                                        {
                                            if (item.Category == "Partial Refund" || item.Category == "")
                                            {
                                                rl.Add(item);
                                            }
                                        }
                                        reasonCode = (rl)[index].ReasonCode;
                                    }

                                    if (c.ID.Contains("ddlRefundOption"))
                                    {
                                        List<RefundOptions> ro = new RefundOptions().Populate();
                                        List<RefundOptions> newRo = new List<RefundOptions>();
                                        double total = 0;

                                        foreach (RefundOptions option in ro)
                                        {
                                            if (Session["OrderTotal"] != null)
                                            {
                                                total = Convert.ToDouble(Session["OrderTotal"]);
                                            }

                                            if (total <= 20)
                                            {
                                                if (option.Tier == "20Dollar")
                                                {
                                                    newRo.Add(option);
                                                }
                                            }
                                            else if (option.Tier == u.RefundTier)
                                            {
                                                newRo.Add(option);
                                            }
                                        }

                                        string value = (newRo)[index].Option.Replace("%", "");
                                        Decimal.TryParse(value, out refundOption);
                                    }
                                }
                            }

                            string lineValidMessage = string.Empty;

                            if ((rowCount > 1 && controlCount == 3 && actionQty != 0 && cellCount == 6))
                            {
                                lineValidMessage = ValidateLine(itemNo, qtyLine, actionQty, reasonCode);

                                if (lineValidMessage == "Valid Line Input")
                                {
                                    lineBuild.Append(itemNo).Append(":");
                                    lineBuild.Append(actionQty).Append(":");
                                    lineBuild.Append(reasonCode).Append(":");
                                    lineBuild.Append(refundOption).Append(',');
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
                        SendService ss = new SendService();

                        ss.PartialRefund(no, docNo, lineValues, zendeskTicketNo);
                        Session["NoUserInteraction"] = true;

                        ClientScript.RegisterStartupScript(this.GetType(), "refundedOrder", "alert('Order " + no + " has been partially refunded.');", true);
                        ClientScript.RegisterStartupScript(this.GetType(), "closeRefundOrder", "CloseAfterCancel();", true);
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

                ClientScript.RegisterStartupScript(this.GetType(), "exceptionAlert", "alert('" + ex.Message.Replace("'", "\"") + "');", true);

                if (ex.Message.ToLower().Contains("session"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "closeErrorAlert", "parent.window.close();", true);
                }
            }
        }

        protected string ValidateLine(string itemNoP, int qtyLineP, int actionQtyP, string reasonCodeP)
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
                            return valid;
                        }
                        else
                        {
                            return "Please select a valid return reason for Item: " + itemNoP;
                        }
                    }
                    else
                    {
                        return "Cannot refund more quantity than exists on order for Item: " + itemNoP;
                    }
                }
                else
                {
                    return "Cannot refund negative quantity for Item: " + itemNoP;
                }
            }
            catch (Exception lineEx)
            {
                Log.Error(lineEx.Message, lineEx);
                return lineEx.Message;
            }
        }
    }
}