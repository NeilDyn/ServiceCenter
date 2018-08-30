﻿using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.CreatedPartRequest;
using ExcelDesign.Class_Objects.FunctionData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.FunctionForms
{
    public partial class CreatePartialRequest : System.Web.UI.Page
    {
        protected List<SalesHeader> Sh;

        protected string no;
        protected string externalDocumentNo;
        protected string notes;
        protected string email;

        protected bool anyLines = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserInteraction"] = true;

            if (!IsPostBack)
            {
                tcNo.Text = Convert.ToString(Request.QueryString["No"]);
                tcDocNo.Text = Convert.ToString(Request.QueryString["ExternalDocumentNo"]);
                Sh = (List<SalesHeader>)Session["SalesHeaders"];
            }

            LoadPartRequestLines();
        }

        protected void LoadPartRequestLines()
        {
            try
            {
                TableRow lineRow = new TableRow();

                Sh = (List<SalesHeader>)Session["SalesHeaders"];

                List<ReturnReason> rrList = (List<ReturnReason>)Session["ReturnReasons"];
                List<PartRequestOptions> partRequestList = (List<PartRequestOptions>)Session["PartRequestOptions"];

                int lineCount = 0;

                foreach (SalesHeader head in Sh)
                {
                    foreach (ShipmentHeader header in head.ShipmentHeaderObject)
                    {
                        if (head.SalesOrderNo == tcNo.Text)
                        {
                            foreach (ShipmentLine line in header.ShipmentLines)
                            {
                                if (line.Quantity > 0 && line.Type.ToUpper() == "ITEM")
                                {
                                    lineCount++;
                                    anyLines = true;

                                    TableRow singleRow = new TableRow();

                                    TableCell itemNo = new TableCell();
                                    TableCell desc = new TableCell();
                                    TableCell qty = new TableCell();
                                    TableCell actionQty = new TableCell();
                                    TableCell partRequest = new TableCell();
                                    TableCell reason = new TableCell();
                                    TableCell copyButtonCell = new TableCell();

                                    Button copyButton = new Button()
                                    {
                                        ID = "copyButton_" + lineCount.ToString(),
                                        Text = "Add Line",
                                    };

                                    DropDownList ddlPartRequest = new DropDownList
                                    {
                                        DataValueField = "PartRequestOption",
                                        ID = "ddlPartRequest_" + lineCount.ToString(),
                                        CssClass = "inputBox",
                                        DataSource = partRequestList
                                    };

                                    DropDownList ddlReason = new DropDownList
                                    {
                                        DataValueField = "Display",
                                        ID = "ddlReason_" + lineCount.ToString(),
                                        CssClass = "inputBox",
                                        DataSource = rrList.Where(x => x.Category == "Part Request")
                                    };

                                    TextBox actionQtyInsert = new TextBox
                                    {
                                        ID = "actionQtyInsert_" + lineCount.ToString(),
                                        Text = "1",
                                        Width = new Unit("20%"),
                                        CssClass = "inputBox"
                                    };

                                    ddlPartRequest.DataBind();
                                    ddlReason.DataBind();

                                    itemNo.ID = "itemNo_" + lineCount.ToString();
                                    qty.ID = "itemQuantity_" + lineCount.ToString();
                                    desc.ID = "desc_" + lineCount.ToString();
                                    actionQty.ID = "actionQty_" + lineCount.ToString();
                                    partRequest.ID = "partRequest_" + lineCount.ToString();
                                    reason.ID = "reason_" + lineCount.ToString();
                                    copyButtonCell.ID = "copyButtonCell_" + lineCount.ToString();

                                    itemNo.Text = line.ItemNo;
                                    desc.Text = line.Description;
                                    qty.Text = line.Quantity.ToString();
                                    actionQty.Controls.Add(actionQtyInsert);
                                    partRequest.Controls.Add(ddlPartRequest);
                                    reason.Controls.Add(ddlReason);
                                    copyButtonCell.Controls.Add(copyButton);
                                    qty.HorizontalAlign = HorizontalAlign.Center;
                                    actionQty.HorizontalAlign = HorizontalAlign.Center;

                                    singleRow.ID = "partRequestLineRow_" + lineCount.ToString();

                                    singleRow.Cells.Add(itemNo);
                                    singleRow.Cells.Add(desc);
                                    singleRow.Cells.Add(qty);
                                    singleRow.Cells.Add(actionQty);
                                    singleRow.Cells.Add(partRequest);
                                    singleRow.Cells.Add(reason);
                                    singleRow.Cells.Add(copyButtonCell);

                                    if (lineCount % 2 == 0)
                                    {
                                        singleRow.BackColor = Color.White;
                                        actionQtyInsert.BackColor = Color.White;
                                        ddlPartRequest.BackColor = Color.White;
                                        ddlReason.BackColor = Color.White;
                                    }
                                    else
                                    {
                                        singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        actionQtyInsert.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        ddlPartRequest.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        ddlReason.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                    }

                                    singleRow.Attributes.CssStyle.Add("border-collapse", "collapse");
                                    tblCreateReturnOrderTableDetails.Rows.Add(singleRow);
                                }
                            }
                        }
                    }
                }

                if (!anyLines)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "noLines", "alert('There are no items available to Part Request.');", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "noLinesClose", "parent.window.close();", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "exceptionAlert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnCreatePartRequest_Click(object sender, EventArgs e)
        {
            StringBuilder lineBuild = new StringBuilder();
            StringBuilder shippingBuild = new StringBuilder();
            string lineError = "";

            try
            {
                no = tcNo.Text;
                externalDocumentNo = tcDocNo.Text;
                notes = txtNotes.Text;
                email = txtCustEmail.Text;

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
                        string desc = string.Empty;
                        int qtyLine = 0;
                        int actionQty = 0;
                        int partRequest = 0;
                        string reason = string.Empty;

                        controlCount = 0;

                        foreach (TableCell cell in row.Cells)
                        {
                            if (cell.ID.Contains("itemNo_"))
                            {
                                itemNo = cell.Text.ToString();
                            }

                            if (cell.ID.Contains("desc_"))
                            {
                                desc = cell.Text.ToString();
                            }

                            if (cell.ID.Contains("itemQuantity_"))
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

                                    if (c.ID.Contains("ddlPartRequest_"))
                                    {
                                        partRequest = ((DropDownList)c).SelectedIndex;
                                    }

                                    if (c.ID.Contains("ddlReason_"))
                                    {
                                        reason = ((List<ReturnReason>)Session["ReturnReasons"])[index].ReasonCode;
                                    }
                                }
                            }

                            string lineValidMessage = string.Empty;

                            if ((rowCount > 1 && controlCount == 3 && actionQty != 0))
                            {
                                lineValidMessage = ValidateLine(itemNo, qtyLine, actionQty, partRequest, reason);

                                if (lineValidMessage == "Valid Line Input")
                                {
                                    lineBuild.Append(itemNo).Append(":");
                                    lineBuild.Append(desc).Append(":");
                                    lineBuild.Append(actionQty).Append(":");
                                    lineBuild.Append(partRequest).Append(":");
                                    lineBuild.Append(reason).Append(",");
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
                        CreatedPartRequestHeader cprh = new CreatedPartRequestHeader();

                        SendService ss = new SendService();

                        cprh = ss.CreatePartialRequest(no, externalDocumentNo, lineValues, notes, "", email);

                        Session["CreatedPartRequest"] = cprh;
                        Session["NoUserInteraction"] = true;

                        ClientScript.RegisterStartupScript(this.GetType(), "returnPartRequest", "alert('" + cprh.QuoteNo + "');", true);
                        ClientScript.RegisterStartupScript(this.GetType(), "openCreatedPartRequest", "OpenCreatedPartRequest();", true);
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

        protected string ValidateLine(string itemNoP, int qtyLineP, int actionQtyP, int partRequest, string reason)
        {
            string valid = "Valid Line Input";

            try
            {
                if (actionQtyP >= 0)
                {
                    if ((qtyLineP >= actionQtyP) && (qtyLineP != 0))
                    {
                        if (partRequest > 0)
                        {
                            if (!String.IsNullOrWhiteSpace(reason) || !String.IsNullOrEmpty(reason))
                            {
                                return valid;
                            }
                            else
                            {
                                return "Please select a valid Reason for Item: " + itemNoP;
                            }
                        }
                        else
                        {
                            return "Please select a valid Part Request for Item: " + itemNoP;
                        }
                    }
                    else
                    {
                        return "Cannot request more parts than quantity exists on order for Item: " + itemNoP;
                    }
                }
                else
                {
                    return "Cannot request negative quantity for Item: " + itemNoP;
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
                if (!String.IsNullOrEmpty(email) || !String.IsNullOrWhiteSpace(email))
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
                    return valid;
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