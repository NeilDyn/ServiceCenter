using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls.SuggestSimilarItems
{
    public partial class SuggestSimilarItem : System.Web.UI.Page
    {
        protected CallService cs = new CallService();

        public string ItemNo { get; set; }
        public string RowNo { get; set; }
        public static string ItemNoStatic { get; set; }
        public static string RowNoStatic { get; set; }
        public int SuggestionOption { get; set; }
        public string SuggestedItem { get; set; }
        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(SuggestSimilarItem));

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);
            try
            {
                if (!this.Page.User.Identity.IsAuthenticated || Session["ActiveUser"] == null)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }

                if (!IsPostBack)
                {
                    LoadSuggestionLines();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "exceptionAlert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void LoadSuggestionLines()
        {
            int lineCount = 0;
            List<Item> items = new List<Item>();
            List<string> selectedItem = new List<string>();

            ItemNo = Convert.ToString(Request.QueryString["ItemNo"]);
            RowNo = Convert.ToString(Request.QueryString["RowNo"]);
            SuggestionOption = ddlSuggestionOptions.SelectedIndex;

            items = cs.GetSuggestedSimilarItems(ItemNo, SuggestionOption);

            if (Session[ItemNo] != null)
            {
                selectedItem = (List<string>)Session[ItemNo];

                TableRow originalRow = new TableRow();
                TableCell originalNo = new TableCell();
                TableCell originalDesc = new TableCell();
                TableCell originalCost = new TableCell();

                originalNo.Text = selectedItem[0];
                originalDesc.Text = selectedItem[1];
                originalCost.Text = "$     " + selectedItem[2];
                originalCost.HorizontalAlign = HorizontalAlign.Right;

                originalRow.Cells.Add(originalNo);
                originalRow.Cells.Add(originalDesc);
                originalRow.Cells.Add(originalCost);

                tblOriginalItem.Rows.Add(originalRow);
            }

            foreach (Item item in items)
            {
                lineCount++;

                TableRow singleRow = new TableRow();
                TableCell no = new TableCell();
                TableCell desc = new TableCell();
                TableCell unitPrice = new TableCell();
                TableCell select = new TableCell();

                Button btnSelect = new Button
                {
                    ID = "btnSelect_" + lineCount.ToString(),
                    Text = "Select",
                    OnClientClick = "return SetSuggestedItem('" + item.ItemNo + "', '" + RowNo + "')"
                };

                no.ID = "itemNo_" + lineCount.ToString();
                desc.ID = "desc_" + lineCount.ToString();
                unitPrice.ID = "unitPrice_" + lineCount.ToString();
                select.ID = "select_" + lineCount.ToString();

                no.Text = item.ItemNo;
                desc.Text = item.Description;
                unitPrice.Text = "$     " + item.UnitPrice.ToString();

                unitPrice.HorizontalAlign = HorizontalAlign.Right;

                select.Controls.Add(btnSelect);

                singleRow.Cells.Add(no);
                singleRow.Cells.Add(desc);
                singleRow.Cells.Add(unitPrice);
                singleRow.Cells.Add(select);

                if (lineCount % 2 == 0)
                {
                    singleRow.BackColor = Color.White;
                }
                else
                {
                    singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                }

                tblSuggestedSimilarItems.Rows.Add(singleRow);
            }
        }

        protected void ddlSuggestionOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSuggestionLines();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SetItem(string originalItemNo, string setItemNo, string rowNo)
        {
            HttpContext.Current.Session[originalItemNo + "_" + rowNo] = setItemNo;

            return "Success";
        }
    }
}