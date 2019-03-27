using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ExcelDesign.Class_Objects.Documents
{
    /* v10 - 26 March - Neil Jansen
     * Changed Verbiage of document
     */

    public class RMAInstructionsPDF
    {
        protected Chunk chunk;
        protected Paragraph mainHeading;
        protected Paragraph mainHeading2;
        protected Paragraph subHeading;
        protected Paragraph heading2;
        protected Paragraph heading3;
        protected Paragraph detail;
        protected Paragraph detail2;
        protected Paragraph detail3;
        protected Paragraph detail4;
        protected Paragraph detail5;
        protected Paragraph thankYou;
        protected Paragraph customerService;
        protected Paragraph displayRMA;
        protected Phrase barcodeRMA;
        protected Font font = new Font(Font.HELVETICA, 12f, Font.NORMAL);
        protected List checkList = new List(List.UNORDERED, 20f);
        protected List checkList2 = new List(List.UNORDERED, 20f);
        protected List returnAddressList = new List(List.UNORDERED, 20f);
        protected string text = string.Empty;
        protected string checkString = "\u2610";
        protected Barcode39 barcode = new Barcode39();
        //protected BaseFont barcodeFont = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("../fonts/") + "CBARN3.ttf", BaseFont.CP1250, BaseFont.EMBEDDED);
        protected Image barcodeImage;
        protected PdfContentByte barcodeByte;
        protected PdfWriter writer;

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(RMAInstructionsPDF));

        public MemoryStream CreatePDF(string rmaNo)
        {
            var doc = new Document();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                try
                {
                    //string path = HttpContext.Current.Server.MapPath("../PDFs");
                    //writer = PdfWriter.GetInstance(doc, new FileStream(path + "/" + rmaNo + ".pdf", FileMode.Create));
                    writer = PdfWriter.GetInstance(doc, memoryStream);
                    doc.Open();
                    barcodeByte = writer.DirectContent;

                    mainHeading = new Paragraph("Return Merchandise Authorization Instructions", new Font(Font.HELVETICA, 20f, Font.BOLD))
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 40f
                    };

                    text = @"Thank you for the opportunity to help you.  We understand the importance of processing your
return in a timely manner.  Please print this check list, complete the items below and place inside
of the box to ensure prompt resolution for your return.";

                    detail = new Paragraph(text, new Font(Font.HELVETICA, 11f))
                    {
                        SpacingAfter = 40f
                    };

                    heading2 = new Paragraph("All Returns:", new Font(Font.HELVETICA, 20f, Font.BOLD))
                    {
                        SpacingAfter = 20f
                    };

                    returnAddressList.SetListSymbol("");
                    returnAddressList.IndentationLeft = 30f;
                    returnAddressList.Add(new ListItem(" ", font));
                    returnAddressList.Add(new ListItem("RETURNS DEPT", font));
                    returnAddressList.Add(new ListItem(rmaNo, font));
                    returnAddressList.Add(new ListItem("20000 NE, 15TH CT", font));
                    returnAddressList.Add(new ListItem("Miami, FL 33179", font));
                    returnAddressList.Add(new ListItem(" ", font));

                    checkList.IndentationLeft = 30f;
                    checkList.SetListSymbol("O");
                    checkList.Add(new ListItem("RMA is clearly written on box along with the return address", font));
                    checkList.Add(returnAddressList);
                    checkList.Add(new ListItem("Print Return Authorization sheet with the barcode and place inside box", font));

                    heading3 = new Paragraph("For Cell phones, Tablets or Smart watches", new Font(Font.HELVETICA, 20f, Font.BOLD))
                    {
                        SpacingBefore = 20f,
                        SpacingAfter = 20f
                    };

                    checkList2.IndentationLeft = 30f;
                    checkList2.SetListSymbol("O");
                    checkList2.Add(new ListItem("Remove all passwords from the device", font));
                    checkList2.Add(new ListItem("Go to the cloud or account and remove the device from your account", font));
                    checkList2.Add(new ListItem(@"Only return the device. Do not include any accessories such as cases,
headphones etc.", font));
                    checkList2.Add(new ListItem("Remove your memory card and/or SIM card", font));
                    checkList2.Add(new ListItem("Remove only screen locks or passwords enabled on the device", font));

                    mainHeading2 = new Paragraph("Return Merchandise Authorization Sheet", new Font(Font.HELVETICA, 20f, Font.BOLD))
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 20f
                    };

                    subHeading = new Paragraph("Must Be Printed and Placed Inside Return Box", new Font(Font.HELVETICA, 14f, Font.BOLD | Font.UNDERLINE))
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 40f
                    };

                    displayRMA = new Paragraph(rmaNo, new Font(Font.HELVETICA, 20f, Font.BOLD))
                    {
                        SpacingAfter = 50f
                    };

                    barcode.Code = rmaNo;
                    barcode.Size = 48f;
                    barcode.Font = null;
                    barcodeImage = barcode.CreateImageWithBarcode(barcodeByte, null, null);

                    barcodeRMA = new Phrase(new Chunk(barcodeImage, 0, 0));

                    text = "Your RMA number is valid for 15 days. Once the return arrives it takes 3-5 business days to process.";

                    detail2 = new Paragraph(text, new Font(Font.HELVETICA, 11f))
                    {
                        SpacingBefore = 50f,
                        SpacingAfter = 10f
                    };

                    text = @"Please retain your RMA number if you need to contact us by email or phone to check the status of your
return.";

                    detail3 = new Paragraph(text, new Font(Font.HELVETICA, 11f))
                    {
                        SpacingAfter = 10f
                    };

                    text = @"Warranty is only for manufacturer defects, warranty doesn't cover any physical damage (cracked screen,
dents) or water damage.";

                    detail4 = new Paragraph(text, new Font(Font.HELVETICA, 11f))
                    {
                        SpacingAfter = 10f
                    };

                    text = @"If your device has any physical or water damage your exchange will be denied and you will be responsible
to provide us with a pre-paid return label so we can ship the device back to you.";

                    detail5 = new Paragraph(text, new Font(Font.HELVETICA, 11f))
                    {
                        SpacingAfter = 20f
                    };

                    thankYou = new Paragraph("Thank you,", new Font(Font.HELVETICA, 11f))
                    {
                        SpacingAfter = 10f
                    };

                    customerService = new Paragraph("Customer Service", new Font(Font.HELVETICA, 11f));

                    doc.Add(mainHeading);
                    doc.Add(detail);
                    doc.Add(heading2);
                    doc.Add(checkList);
                    doc.Add(heading3);
                    doc.Add(checkList2);

                    doc.NewPage();
                    doc.Add(mainHeading2);
                    doc.Add(subHeading);
                    doc.Add(displayRMA);
                    doc.Add(barcodeRMA);
                    doc.Add(detail2);
                    doc.Add(detail3);
                    doc.Add(detail4);
                    doc.Add(detail5);
                    doc.Add(thankYou);
                    doc.Add(customerService);
                }
                catch (DocumentException dex)
                {
                    Log.Error(dex.Message, dex);
                    throw (dex);
                }
                catch (IOException ioex)
                {
                    Log.Error(ioex.Message, ioex);
                    throw (ioex);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex);
                    throw (ex);
                }
                finally
                {
                    doc.Close();
                }

                return memoryStream;
            }
        }

        public RMAInstructionsPDF()
        {

        }
    }
}