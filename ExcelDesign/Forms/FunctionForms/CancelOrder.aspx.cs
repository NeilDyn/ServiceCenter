using ExcelDesign.Class_Objects;
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
    public partial class CancelOrder : System.Web.UI.Page
    {
        protected List<SalesHeader> Sh;
        protected string no;
        protected string docNo;

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(CancelOrder));

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

                LoadCancelOrderLines();
            }
            catch (Exception loadE)
            {
                Log.Error(loadE.Message, loadE);
            }
        }

        protected void LoadCancelOrderLines()
        {
            try
            {
                TableRow lineRow = new TableRow();

                Sh = (List<SalesHeader>)Session["SalesHeaders"];

                List<ReturnReason> rrList = (List<ReturnReason>)Session["ReturnReasons"];


                int lineCount = 0;
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
                                if (line.Quantity > 0 && line.Type.ToUpper() == "ITEM")
                                {
                                    lineCount++; ;

                                    TableRow singleRow = new TableRow();

                                    TableCell itemNo = new TableCell();
                                    TableCell desc = new TableCell();
                                    TableCell qty = new TableCell();
                                    TableCell actionQty = new TableCell();
                                    TableCell returnReasonCode = new TableCell();

                                    DropDownList ddlReturnReasonCode = new DropDownList
                                    {
                                        DataValueField = "Display",
                                        DataSource = rrList.Where(x => x.Category == "Cancel Order" || x.Category == ""),
                                        ID = "ddlReturnReasonCode_" + lineCount.ToString(),
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

                                    itemNo.ID = "itemNo_" + lineCount.ToString();
                                    qty.ID = "itemQuanity_" + lineCount.ToString();
                                    desc.ID = "desc_" + lineCount.ToString();
                                    actionQty.ID = "actionQty_" + lineCount.ToString();
                                    returnReasonCode.ID = "returnReasonCode_" + lineCount.ToString();

                                    itemNo.Text = line.ItemNo;
                                    desc.Text = line.Description;
                                    qty.Text = (line.Quantity).ToString();
                                    actionQty.Controls.Add(actionQtyInsert);
                                    returnReasonCode.Controls.Add(ddlReturnReasonCode);

                                    qty.HorizontalAlign = HorizontalAlign.Center;
                                    actionQty.HorizontalAlign = HorizontalAlign.Center;

                                    singleRow.ID = "cancelOrderLineRow_" + lineCount.ToString();

                                    singleRow.Cells.Add(itemNo);
                                    singleRow.Cells.Add(desc);
                                    singleRow.Cells.Add(qty);
                                    singleRow.Cells.Add(actionQty);
                                    singleRow.Cells.Add(returnReasonCode);

                                    if (lineCount % 2 == 0)
                                    {
                                        singleRow.BackColor = Color.White;
                                        actionQtyInsert.BackColor = Color.White;
                                        ddlReturnReasonCode.BackColor = Color.White;
                                    }
                                    else
                                    {
                                        singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        actionQtyInsert.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                        ddlReturnReasonCode.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                                    }

                                    singleRow.Attributes.CssStyle.Add("border-collapse", "collapse");
                                    tblCancelOrderTableDetails.Rows.Add(singleRow);
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

        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            StringBuilder lineBuild = new StringBuilder();
            string lineError = "";

            try
            {
                no = tcNo.Text;
                docNo = tcDocNo.Text;

                bool allValidLines = true;
                int rowCount = 0;
                int controlCount = 0;

                foreach (TableRow row in tblCancelOrderTableDetails.Rows)
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

                                List<ReturnReason> sr = (List<ReturnReason>)Session["ReturnReasons"];
                                List<ReturnReason> rl = new List<ReturnReason>();
                                foreach (ReturnReason item in sr)
                                {
                                    if (item.Category == "Cancel Order" || item.Category == "")
                                    {
                                        rl.Add(item);
                                    }
                                }
                                reasonCode = (rl)[index].ReasonCode;
                            }
                        }

                        string lineValidMessage = string.Empty;

                        if ((rowCount > 1 && controlCount == 2 && actionQty != 0))
                        {
                            lineValidMessage = ValidateLine(itemNo, qtyLine, actionQty, reasonCode);

                            if (lineValidMessage == "Valid Line Input")
                            {
                                lineBuild.Append(itemNo).Append(":");
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
                    SendService ss = new SendService();

                    ss.CancelOrder(no, docNo, lineValues);
                    Session["NoUserInteraction"] = true;
                    Session["SearchValue"] = "ORDER CANCELLED";

                    ClientScript.RegisterStartupScript(this.GetType(), "cancelledOrder", "alert('Order " + no + " has been successfully cancelled.');", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "closeCancelOrder", "CloseAfterCancel();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "lineError", "alert('" + lineError + "');", true);
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
                        return "Cannot cancel more quantity than exists on order for Item: " + itemNoP;
                    }
                }
                else
                {
                    return "Cannot cancel negative quantity for Item: " + itemNoP;
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