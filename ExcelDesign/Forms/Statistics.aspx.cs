using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.StatisticsControls;
using ExcelDesign.Forms.UserControls.StatisticsControls.SalesLInes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms
{
    public partial class Statistics : System.Web.UI.Page
    {
        /* NJ 3 September 2018
         * Created statistics control panel
        */

        protected CallService cs = new CallService();
        protected List<StatisticsSalesLine> statisticsInformation;

        protected Control replacementLinesControl;
        protected Control refundLinesControl;
        protected Control quoteLinesControl;

        protected const string replacementLinesPath = "UserControls/StatisticsControls/PendingReplacements.ascx";
        protected const string refundLinesPath = "UserControls/StatisticsControls/PendingRefund.ascx";
        protected const string quoteLinesPath = "UserControls/StatisticsControls/SalesLines/PendingSQApprovalLines.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated || Session["ActiveUser"] == null)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            if (!IsPostBack)
            {
                try
                {
                    User activeUser = null;

                    if (Session["ActiveUser"] != null)
                    {
                        activeUser = (User)Session["ActiveUser"];
                        statisticsInformation = cs.GetStatisticsInformation();
                        PopulateLines();
                    }
                }
                catch (Exception ex)
                {
                    Session["Error"] = ex.Message;

                    if (Request.Url.AbsoluteUri.Contains("Forms"))
                    {
                        Response.Redirect("ErrorForm.aspx");
                    }
                    else
                    {
                        Response.Redirect("Forms/ErrorForm.aspx");
                    }
                }
            }
        }

        protected void PopulateLines()
        {
            int pendingReplacement = 0;
            int pendingRefund = 0;
            int pendingSQApproval = 0;

            List<StatisticsSalesLine> replacementList = new List<StatisticsSalesLine>();
            List<StatisticsSalesLine> refundList = new List<StatisticsSalesLine>();
            List<StatisticsSalesLine> quoteList = new List<StatisticsSalesLine>();

            try
            {
                foreach (StatisticsSalesLine line in statisticsInformation)
                {
                    switch(line.DocType.ToUpper())
                    {
                        case "RETURN ORDER":
                            if(line.REQReturnAction.ToUpper() == "EXCHANGE")
                            {
                                pendingReplacement++;
                                replacementList.Add(line);
                            }

                            if(line.REQReturnAction.ToUpper() == "REFUND")
                            {
                                pendingRefund++;
                                refundList.Add(line);
                            }
                        break;

                        case "QUOTE":
                            pendingSQApproval++;
                            quoteList.Add(line);
                        break;
                    }
                }

                if(pendingReplacement > 0)
                {
                    tcPendingReplacements.Text = "<a href='javascript:expandReplacements()'>" + pendingReplacement.ToString() + "</a>";
                }
                else
                {
                    tcPendingReplacements.Text = pendingReplacement.ToString();
                }

                if(pendingRefund > 0)
                {
                    tcPendingRefunds.Text = "<a href='javascript:expandRefunds()'>" + pendingRefund.ToString() + "</a>";
                }
                else
                {
                    tcPendingRefunds.Text = pendingRefund.ToString();
                }

                if (pendingSQApproval > 0)
                {
                    tcPendingSQApproval.Text = "<a href='javascript:expandPendingSQQuotes()'>" + pendingSQApproval.ToString() + "</a>";
                }
                else
                {
                    tcPendingSQApproval.Text = pendingSQApproval.ToString();
                }

                PopulateReplacementLines(replacementList);
                PopulateRefundLines(refundList);
                PopulatePendingSQ(quoteList);
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;

                if (Request.Url.AbsoluteUri.Contains("Forms"))
                {
                    Response.Redirect("ErrorForm.aspx");
                }
                else
                {
                    Response.Redirect("Forms/ErrorForm.aspx");
                }
            }
        }

        protected void PopulateReplacementLines(List<StatisticsSalesLine> replacementList)
        {
            replacementLinesControl = LoadControl(replacementLinesPath);
            ((PendingReplacements)replacementLinesControl).ReplacementList = replacementList;
            ((PendingReplacements)replacementLinesControl).PopulateData();

            expandReplacementDetails.Controls.Add(replacementLinesControl);
        }

        protected void PopulateRefundLines(List<StatisticsSalesLine> refundList)
        {
            refundLinesControl = LoadControl(refundLinesPath);
            ((PendingRefund)refundLinesControl).RefundList = refundList;
            ((PendingRefund)refundLinesControl).PopulateData();

            expandRefundDetails.Controls.Add(refundLinesControl);
        }

        protected void PopulatePendingSQ(List<StatisticsSalesLine> quoteList)
        {
            quoteLinesControl = LoadControl(quoteLinesPath);
            ((PendingSQApprovalLines)quoteLinesControl).PendingSQLApprovalList = quoteList;
            ((PendingSQApprovalLines)quoteLinesControl).PopulateData();

            expandPendingSQApprovalDetails.Controls.Add(quoteLinesControl);
        }
    }
}