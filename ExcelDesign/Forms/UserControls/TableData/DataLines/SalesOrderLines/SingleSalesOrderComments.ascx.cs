using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.TableData.DataLines.SalesOrderLines
{
    public partial class SingleSalesOrderComments : System.Web.UI.UserControl
    {
        public List<Comment> CommentLines { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateData()
        {
            int lineCount = 0;

            foreach (Comment comment in CommentLines)
            {
                lineCount++;

                TableRow tr = new TableRow();

                TableCell dateCell = new TableCell();
                TableCell commentCell = new TableCell();

                dateCell.Text = comment.Date;
                dateCell.ToolTip = comment.Date;

                commentCell.Text = comment.CommentText;
                commentCell.ToolTip = comment.CommentText;

                tr.Cells.Add(dateCell);
                tr.Cells.Add(commentCell);

                if (lineCount % 2 == 0)
                {
                    tr.BackColor = Color.White;
                }
                else
                {
                    tr.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                }

                this.SinglSalesOrderCommentsTable.Rows.Add(tr);
            }
        }
    }
}