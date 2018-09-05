﻿using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls.SalesLInes
{
    public partial class RefundLines : System.Web.UI.UserControl
    {
        public List<StatisticsSalesLine> OlderThan72HoursList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateData()
        {
            try
            {
                int lineCount = 0;

                foreach (StatisticsSalesLine line in OlderThan72HoursList)
                {
                    lineCount++;

                    TableRow tr = new TableRow();

                    TableCell docNo = new TableCell();
                    TableCell createdDate = new TableCell();
                    TableCell itemNo = new TableCell();
                    TableCell desc = new TableCell();
                    TableCell qty = new TableCell();

                    docNo.Text = line.DocNo;
                    createdDate.Text = line.CreatedDate;
                    itemNo.Text = line.ItemNo;
                    desc.Text = line.Description;
                    qty.Text = line.Qty.ToString();

                    qty.HorizontalAlign = HorizontalAlign.Center;

                    tr.Cells.Add(docNo);
                    tr.Cells.Add(createdDate);
                    tr.Cells.Add(itemNo);
                    tr.Cells.Add(desc);
                    tr.Cells.Add(qty);

                    if (lineCount % 2 == 0)
                    {
                        tr.BackColor = Color.White;
                    }
                    else
                    {
                        tr.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                    }

                    tblRefundLines.Rows.Add(tr);
                }

                TableRow breakRow = new TableRow();
                TableCell breakCell = new TableCell
                {
                    Text = "<br />"
                };

                breakRow.Cells.Add(breakCell);
                tblRefundLines.Rows.Add(breakRow);
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
}