using ExcelDesign.Class_Objects.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.FunctionForms
{
    public partial class RMAPDFForm : System.Web.UI.Page
    {
        protected MemoryStream MemStream { get; set; }

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(RMAPDFForm));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string rmaNo = Convert.ToString(Request.QueryString["RMANo"]);

                Title = rmaNo + ".pdf";
                RMAInstructionsPDF createPDF = new RMAInstructionsPDF();
                MemStream = createPDF.CreatePDF(rmaNo);

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.OutputStream.Write(MemStream.GetBuffer(), 0, MemStream.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

                Session["Error"] = ex.Message;
                Response.Redirect("../ErrorForm.aspx");
            }
            finally
            {
                Response.End();
            }
        }
    }
}