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

namespace ExcelDesign.Forms.FunctionForms
{
    public partial class CreateReturn : System.Web.UI.Page
    {
        protected List<ShipmentHeader> Sh;
        protected string orderNo;
        protected string docNo;
        protected string notes;
        protected int defect;
        protected string email;

        protected bool resources;
        protected bool printRMA;
        protected bool createLabel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                List<Defects> doList = (List<Defects>)Session["Defects"];
                List<ReturnReason> rrList = (List<ReturnReason>)Session["ReturnReasons"];

                ddlDefectOptions.DataValueField = "Option";
                ddlDefectOptions.DataSource = doList;
                ddlDefectOptions.DataBind();

                tcOrderNo.Text = Convert.ToString(Request.QueryString["OrderNo"]);
                tcDocNo.Text = Convert.ToString(Request.QueryString["ExternalDocumentNo"]);
            }

            LoadCreateReturnLines();

        }

        protected void LoadCreateReturnLines()
        {
            TableRow lineRow = new TableRow();

            Sh = (List<ShipmentHeader>)Session["ShipmentHeader"];

            List<ReturnReason> rrList = (List<ReturnReason>)Session["ReturnReasons"];

            int lineCount = 0;

            foreach (ShipmentHeader header in Sh)
            {
                foreach (ShipmentLine line in header.ShipmentLines)
                {
                    if (line.Quantity > 0 && line.Type.ToUpper() == "ITEM")
                    {
                        lineCount++;

                        TableRow singleRow = new TableRow();

                        TableCell itemNo = new TableCell();
                        TableCell desc = new TableCell();
                        TableCell qty = new TableCell();
                        TableCell actionQty = new TableCell();
                        TableCell returnReasonCode = new TableCell();

                        DropDownList ddlReturnReasonCode = new DropDownList
                        {
                            DataValueField = "Description",
                            DataSource = rrList,
                            ID = "ddlReturnReasonCode_" + lineCount.ToString()
                        };

                        TextBox actionQtyInsert = new TextBox
                        {
                            ID = "actionQtyInsert_" + lineCount.ToString(),
                            Text = "0"
                        };

                        ddlReturnReasonCode.DataBind();

                        itemNo.ID = "itemNo_" + lineCount.ToString();
                        qty.ID = "itemQuanity_" + lineCount.ToString();
                        desc.ID = "desc_" + lineCount.ToString();
                        actionQty.ID = "actionQty_" + lineCount.ToString();
                        returnReasonCode.ID = "returnReasonCode_" + lineCount.ToString();

                        itemNo.Text = line.ItemNo;
                        desc.Text = line.Description;
                        qty.Text = line.Quantity.ToString();
                        actionQty.Controls.Add(actionQtyInsert);
                        returnReasonCode.Controls.Add(ddlReturnReasonCode);

                        qty.HorizontalAlign = HorizontalAlign.Center;

                        singleRow.ID = "returnOrderLineRow_" + lineCount.ToString();

                        singleRow.Cells.Add(itemNo);
                        singleRow.Cells.Add(desc);
                        singleRow.Cells.Add(qty);
                        singleRow.Cells.Add(actionQty);
                        singleRow.Cells.Add(returnReasonCode);

                        if (lineCount % 2 == 0)
                        {
                            singleRow.BackColor = Color.White;
                        }
                        else
                        {
                            singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                        }

                        singleRow.Attributes.CssStyle.Add("border-collapse", "collapse");
                        tblCreateReturnOrderTableDetails.Rows.Add(singleRow);
                    }
                }
            }
        }

        protected void BtnCreateRMA_Click(object sender, EventArgs e)
        {
            StringBuilder lineBuild = new StringBuilder();
            string lineError = "";

            try
            {
                orderNo = tcOrderNo.Text;
                docNo = tcDocNo.Text;
                notes = txtNotes.Text;
                defect = ddlDefectOptions.SelectedIndex;
                resources = cbxResources.Checked;
                printRMA = cbxPrintRMA.Checked;
                createLabel = cbxCreateLable.Checked;
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
                        int qtyLine = 0;
                        int actionQty = 0;
                        string reasonCode = string.Empty;

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
                                    reasonCode = ((List<ReturnReason>)Session["ReturnReasons"])[index].ReasonCode;
                                }
                            }

                            string lineValidMessage = string.Empty;

                            if ((rowCount > 1 && controlCount == 2 && actionQty > 0))
                            {
                                lineValidMessage = ValidateLine(itemNo, qtyLine, actionQty, reasonCode);

                                if (lineValidMessage == "Valid Line Input")
                                {
                                    lineBuild.AppendLine(itemNo).Append(":");
                                    lineBuild.Append(actionQty).Append(":");
                                    lineBuild.Append(reasonCode).Append(",");
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
                        crh = ss.CreateReturnOrder(orderNo, docNo, string.Empty, defect, notes, resources, printRMA, createLabel, email, lineValues);
                        Session["CreatedRMA"] = crh;
                        ClientScript.RegisterStartupScript(this.GetType(), "returnRMA", "alert('" + crh.RMANo + "');", true);
                        ClientScript.RegisterStartupScript(this.GetType(), "openCreatedRMA", "OpenCreatedRMA();", true);                      
                    }
                    else
                    {
                        Response.Write(lineError);
                    }
                }
                else
                {
                    Response.Write(validateMsg);
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
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
                            return "Please select a valid return reason for Item: '" + itemNoP + "'";
                        }
                    }
                    else
                    {
                        return "Cannot return more quantity than exists on order for Item: '" + itemNoP + "'";
                    }
                }
                else
                {
                    return "Cannot return negative quantity for Item: '" + itemNoP + "'";
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
                if (defect > 0)
                {
                    if (createLabel)
                    {
                        if (!String.IsNullOrWhiteSpace(email))
                        {
                            return valid;
                        }
                        else
                        {
                            return "Updated email is required for creating a return label.";
                        }
                    }
                    else
                    {
                        return valid;
                    }
                }
                else
                {
                    return "Please select a valid defect option.";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}