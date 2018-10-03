using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.CreatedPartRequest;
using ExcelDesign.Class_Objects.FunctionData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
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

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(CreatePartialRequest));

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);

            Session["UserInteraction"] = true;

            try
            {
                if (!IsPostBack)
                {
                    Session["CopyRowList"] = null;
                    Session["PartRequestTable"] = null;

                    tcNo.Text = Convert.ToString(Request.QueryString["No"]);
                    tcDocNo.Text = Convert.ToString(Request.QueryString["ExternalDocumentNo"]);
                    Sh = (List<SalesHeader>)Session["SalesHeaders"];
                }

                LoadPartRequestLines();
            }
            catch (Exception loadE)
            {
                Log.Error(loadE.Message, loadE);
            }
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
                                        OnClientClick = "return CopyLine(" + lineCount + ")"
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
                                        DataSource = rrList.Where(x => x.Category == "Part Request" || x.Category == "")
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

                                    Session["PartRequestTable"] = tblCreateReturnOrderTableDetails;
                                }
                            }
                        }
                    }
                }

                if (Session["CopyRowList"] != null)
                {
                    List<TableRow> copyList = (List<TableRow>)Session["CopyRowList"];

                    foreach (TableRow copyRow in copyList)
                    {
                        tblCreateReturnOrderTableDetails.Rows.Add(copyRow);
                        Session["PartRequestTable"] = tblCreateReturnOrderTableDetails;
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
                Log.Error(ex.Message, ex);
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
                                        /* v7.1 - 3 October 2018 - Neil Jansen
                                         * Updated logic to filter out incorrect catagories for Return Reason Code
                                         */

                                        List<ReturnReason> sr = (List<ReturnReason>)Session["ReturnReasons"];
                                        List<ReturnReason> rl = new List<ReturnReason>();
                                        foreach (ReturnReason item in sr)
                                        {
                                            if(item.Category == "Part Request" || item.Category == "")
                                            {
                                                rl.Add(item);
                                            }
                                        }                                
                                        reason = (rl)[index].ReasonCode;
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

                        Session["CopyRowTable"] = null;
                        Session["PartRequestTable"] = null;

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
                Log.Error(ex.Message, ex);
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
                Log.Error(lineEx.Message, lineEx);
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string CopyLine(int rowID)
        {
            try
            {
                Table ogTable = new Table();
                TableRow copyRow = new TableRow();

                if (HttpContext.Current.Session["PartRequestTable"] != null)
                {
                    ogTable = (Table)HttpContext.Current.Session["PartRequestTable"];
                    copyRow = ogTable.Rows[rowID];
                    int newID = ogTable.Rows.Count;

                    foreach (TableCell copyCell in copyRow.Cells)
                    {
                        copyCell.ID = copyCell.ID.Replace(rowID.ToString(), newID.ToString());

                        foreach (Control copyControl in copyCell.Controls)
                        {
                            copyControl.ID = copyControl.ID.Replace(rowID.ToString(), newID.ToString());

                            if (copyControl.GetType() == typeof(Button))
                            {
                                ((Button)copyControl).OnClientClick = ((Button)copyControl).OnClientClick.Replace(rowID.ToString(), newID.ToString());
                                copyControl.Visible = false;
                            }
                        }
                    }

                    copyRow.ID = copyRow.ID.Replace(rowID.ToString(), newID.ToString());

                    if (HttpContext.Current.Session["CopyRowList"] == null)
                    {
                        List<TableRow> copyListRow = new List<TableRow>();

                        HttpContext.Current.Session["CopyRowList"] = copyListRow;
                    }

                    List<TableRow> sessionCopyList = (List<TableRow>)HttpContext.Current.Session["CopyRowList"];

                    sessionCopyList.Add(copyRow);

                    HttpContext.Current.Session["CopyRowList"] = sessionCopyList;

                    return "Success";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return "Error - " + ex.Message;
            }

            return "Error";
        }
    }
}