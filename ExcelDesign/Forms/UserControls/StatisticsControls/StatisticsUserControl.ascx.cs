using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls
{
    public partial class StatisticsUserControl : System.Web.UI.UserControl
    {
        /* NJ 3 September 2018
        * Created statistics control panel
       */

        protected CallService cs = new CallService();
        protected List<StatisticsSalesLine> statisticsInformation;

        protected Control replacementLinesControl;
        protected Control refundLinesControl;
        protected Control unknownLinesControl;
        protected Control quoteLinesControl;
        protected Control completedExchangesControl;

        protected const string replacementLinesPath = "PendingReplacements.ascx";
        protected const string refundLinesPath = "PendingRefund.ascx";
        protected const string unknownLinesPath = "PendingUnknown.ascx";
        protected const string quoteLinesPath = "PendingSQApproval.ascx";
        protected const string completedExchangesPath = "CompletedExchanges.ascx";

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(StatisticsUserControl));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated || Session["ActiveUser"] == null)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            try
            {
                if (!IsPostBack)
                {
                    User activeUser = null;

                    if (Session["ActiveUser"] != null)
                    {
                        activeUser = (User)Session["ActiveUser"];
                        statisticsInformation = cs.GetStatisticsInformation();
                        Session["StatisticsInformation"] = statisticsInformation;
                        PopulateLines();
                    }
                }
                else
                {
                    statisticsInformation = new List<StatisticsSalesLine>();
                    statisticsInformation = (List<StatisticsSalesLine>)Session["StatisticsInformation"];
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

        protected void PopulateLines()
        {
            int pendingReplacement = 0;
            int pendingRefund = 0;
            int pendingSQApproval = 0;
            int unknown = 0;
            int completedExchanges = 0;

            List<StatisticsSalesLine> replacementList = new List<StatisticsSalesLine>();
            List<StatisticsSalesLine> refundList = new List<StatisticsSalesLine>();
            List<StatisticsSalesLine> quoteList = new List<StatisticsSalesLine>();
            List<StatisticsSalesLine> unknownList = new List<StatisticsSalesLine>();
            List<StatisticsSalesLine> completedExchangesList = new List<StatisticsSalesLine>();            

            try
            {
                foreach (StatisticsSalesLine line in statisticsInformation)
                {
                    switch (line.DocType.ToUpper())
                    {
                        case "RETURN ORDER":
                            if (line.REQReturnAction.ToUpper() == "EXCHANGE")
                            {
                                pendingReplacement++;
                                replacementList.Add(line);
                            }

                            if (line.REQReturnAction.ToUpper() == "REFUND")
                            {
                                pendingRefund++;
                                refundList.Add(line);
                            }

                            if (line.REQReturnAction.ToUpper() == "UNKNOWN" && line.IsPendingSQApproval == false)
                            {
                                unknown++;
                                unknownList.Add(line);
                            }
                            break;

                        case "QUOTE":
                            pendingSQApproval++;
                            quoteList.Add(line);
                            break;

                        case "RETURNS BUFFER":
                            if(line.Status.ToUpper().Contains("EXCHANGE PROCESSED"))
                            {
                                completedExchanges++;
                                completedExchangesList.Add(line);
                            }
                            break;
                    }
                }

                if (pendingReplacement > 0)
                {
                    tcPendingReplacements.Text = "<a href='javascript:expandReplacements()'>" + pendingReplacement.ToString() + "</a>";
                }
                else
                {
                    tcPendingReplacements.Text = pendingReplacement.ToString();
                }

                if (pendingRefund > 0)
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

                if (unknown > 0)
                {
                    tcPendingUnknown.Text = "<a href='javascript:expandPendingUnknown()'>" + unknown.ToString() + "</a>";
                }
                else
                {
                    tcPendingUnknown.Text = unknown.ToString();
                }

                if(completedExchanges > 0)
                {
                    tcCompletedExchanges.Text = "<a href='javascript:expandCompletedExchanges()'>" + completedExchanges.ToString() + "</a>";
                }
                else
                {
                    tcCompletedExchanges.Text = completedExchanges.ToString();
                }

                PopulateReplacementLines(replacementList);
                PopulateRefundLines(refundList);
                PopulatePendingSQ(quoteList);
                PopulateUnknownLines(unknownList);
                PopulateCompletedExchangeLines(completedExchangesList);

                Session["StatisticsSalesLine"] = statisticsInformation;
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;

                Log.Error(ex.Message, ex);

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

        protected void PopulateUnknownLines(List<StatisticsSalesLine> unknownLines)
        {
            unknownLinesControl = LoadControl(unknownLinesPath);
            ((Pending_Unknown)unknownLinesControl).UnknownList = unknownLines;
            ((Pending_Unknown)unknownLinesControl).PopulateData();

            expandPendingUnknown.Controls.Add(unknownLinesControl);
        }

        protected void PopulatePendingSQ(List<StatisticsSalesLine> quoteList)
        {
            quoteLinesControl = LoadControl(quoteLinesPath);
            ((PendingSQApproval)quoteLinesControl).SQList = quoteList;
            ((PendingSQApproval)quoteLinesControl).PopulateData();

            expandPendingSQApproval.Controls.Add(quoteLinesControl);
        }

        protected void PopulateCompletedExchangeLines(List<StatisticsSalesLine> exchangeList)
        {
            completedExchangesControl = LoadControl(completedExchangesPath);
            ((CompletedExchanges)completedExchangesControl).ExchangeList = exchangeList;
            ((CompletedExchanges)completedExchangesControl).PopulateData();

            expandCompletedExchanges.Controls.Add(completedExchangesControl);
        }

        protected void BtnRefreshStatistics_Click(object sender, ImageClickEventArgs e)
        {
            User activeUser = null;

            if (Session["ActiveUser"] != null)
            {
                activeUser = (User)Session["ActiveUser"];
                statisticsInformation = new List<StatisticsSalesLine>();
                statisticsInformation = cs.GetStatisticsInformation();

                Session["StatisticsInformation"] = statisticsInformation;

                PopulateLines();
            }
        }
    }
}