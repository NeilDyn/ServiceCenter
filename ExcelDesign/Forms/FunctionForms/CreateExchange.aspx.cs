using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.CreatedExchange;
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
    public partial class CreateExchange : System.Web.UI.Page
    {
        protected List<ReturnHeader> Rh;
        protected string rmaNo;
        protected string docNo;

        protected bool anyLines = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                tcRMANo.Text = Convert.ToString(Request.QueryString["RMANo"]);
                tcDocNo.Text = Convert.ToString(Request.QueryString["ExternalDocumentNo"]);
            }

            LoadCreateExchangeLines();
        }

        protected void LoadCreateExchangeLines()
        {
            try
            {
                TableRow lineRow = new TableRow();

                Rh = (List<ReturnHeader>)Session["ReturnHeaders"];

                int lineCount = 0;
                string rmaNo = tcRMANo.Text;

                foreach (ReturnHeader head in Rh)
                {
                    foreach (ReceiptHeader header in head.ReceiptHeaderObj)
                    {
                        if(head.RMANo == rmaNo)
                        {
                            foreach (ReceiptLine line in header.ReceiptLines)
                            {
                                if(line.REQReturnAction == "Exchange")
                                {
                                    if(line.QuantityReceived - line.QuantityExchanged > 0)
                                    {
                                        lineCount++;
                                        anyLines = true;

                                        TableRow singleRow = new TableRow();

                                        TableCell itemNo = new TableCell();
                                        TableCell desc = new TableCell();
                                        TableCell qtyReceived = new TableCell();
                                        TableCell actionQty = new TableCell();

                                        TextBox actionQtyInsert = new TextBox
                                        {
                                            ID = "actionQtyInsert_" + lineCount.ToString(),
                                            Text = (line.QuantityReceived - line.QuantityExchanged).ToString(),
                                            Width = new Unit("15%")
                                        };

                                        itemNo.ID = "itemNo_" + lineCount.ToString();
                                        desc.ID = "desc_" + lineCount.ToString();
                                        qtyReceived.ID = "qtyReceived_" + lineCount.ToString();
                                        actionQty.ID = "actionQty_" + lineCount.ToString();

                                        itemNo.Text = line.ItemNo;
                                        desc.Text = line.Description;
                                        qtyReceived.Text = (line.QuantityReceived - line.QuantityExchanged).ToString();
                                        actionQty.Controls.Add(actionQtyInsert);

                                        qtyReceived.HorizontalAlign = HorizontalAlign.Center;
                                        actionQty.HorizontalAlign = HorizontalAlign.Center;

                                        singleRow.ID = "exchangeOrderLineRow_" + lineCount.ToString();

                                        singleRow.Cells.Add(itemNo);
                                        singleRow.Cells.Add(desc);
                                        singleRow.Cells.Add(qtyReceived);
                                        singleRow.Cells.Add(actionQty);

                                        if(lineCount % 2 == 0)
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
                    }
                }

                if(!anyLines)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "noLines", "alert('There are no items available to exchange.');", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "noLinesClose", "parent.window.close();", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "exceptionAlert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void BtnCreateExchange_Click(object sender, EventArgs e)
        {
            StringBuilder lineBuild = new StringBuilder();
            string lineError = "";

            try
            {
                rmaNo = tcRMANo.Text;
                docNo = tcDocNo.Text;

                bool allValidLines = true;
                int rowCount = 0;
                int controlCount = 0;

                foreach (TableRow row in tblCreateReturnOrderTableDetails.Rows)
                {
                    rowCount++;
                    string itemNo = string.Empty;
                    int qtyReceivedLine = 0;
                    int actionQty = 0;

                    controlCount = 0;

                    foreach (TableCell cell in row.Cells)
                    {
                        if(cell.ID.Contains("itemNo"))
                        {
                            itemNo = cell.Text.ToString();
                        }

                        if(cell.ID.Contains("qtyReceived_"))
                        {
                            int.TryParse(cell.Text.ToString(), out qtyReceivedLine);
                        }

                        foreach (Control c in cell.Controls)
                        {
                            controlCount++;

                            if(c.GetType() == typeof(TextBox))
                            {
                                string value = ((TextBox)c).Text;
                                int.TryParse(value, out actionQty);
                            }
                        }

                        string lineValidMessage = string.Empty;

                        if((rowCount > 1 && controlCount == 1))
                        {
                            lineValidMessage = ValidateLine(itemNo, qtyReceivedLine, actionQty);

                            if (lineValidMessage == "Valid Line Input")
                            {
                                lineBuild.Append(itemNo).Append(":");
                                lineBuild.Append(actionQty).Append(",");
                            }
                            else
                            {
                                allValidLines = false;

                                if(lineError == "")
                                {
                                    lineError = lineValidMessage;
                                }
                            }
                        }
                    }
                }

                if(allValidLines)
                {
                    string lineValues = lineBuild.ToString();

                    CreatedExchangeHeader ceh = new CreatedExchangeHeader();

                    SendService ss = new SendService();

                    ceh = ss.CreateExchangeOrder(rmaNo, docNo, lineValues);
                    HttpContext.Current.Session["CreatedExchange"] = ceh;
                    ClientScript.RegisterStartupScript(this.GetType(), "returnOrderNo", "alert('" + ceh.OrderNo + "');", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "openCreatedRMA", "OpenCreateExchange();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "lineError", "alert('" + lineError + "');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('" + ex.Message + "');", true);

                if (ex.Message.ToLower().Contains("session"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "closeErrorAlert", "parent.window.close();", true);
                }
            }
        }

        protected string ValidateLine(string itemNoP, int qtyReceivedLineP, int actionQtyP)
        {
            string valid = "Valid Line Input";

            try
            {
                if(actionQtyP >= 0)
                {
                    if((qtyReceivedLineP >= actionQtyP) && (qtyReceivedLineP != 0))
                    {
                        return valid;
                    }
                    else
                    {
                        return "Cannot exchange more quantity than received on return for Item: " + itemNoP;
                    }
                }
                else
                {
                    return "Cannot exchange negative quantity for Item: " + itemNoP;
                }
            }
            catch (Exception lineEx)
            {
                return lineEx.Message;
            }
        }
    }
}