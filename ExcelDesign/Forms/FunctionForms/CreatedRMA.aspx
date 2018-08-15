<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatedRMA.aspx.cs" Inherits="ExcelDesign.Forms.FunctionForms.CreatedRMA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

        });

        function CloseWindow() {
            var c = window.confirm("Are you sure you wish to close this window?");
            if (c == true) { 
                parent.window.close();               
            };
        };

        function OpenPDF() {
            window.open("RMAPDFForm.aspx?RMANo=<%= this.CRH.RMANo %>", "_blank");
        };

        function UpdateRMA() {
            var width = 1500;
            var height = 500;
            var left = (screen.width - width) + 500;
            var top = (screen.height - height) * 0.5;
            window.open("CreateReturn.aspx?No=<%= this.RmaNo %>&ExternalDocumentNo=<%= this.ExtDocNo %>&CreateOrUpdate=<%= this.Update %>&CreatedOrderNo=<%= this.OrderNo %>",
                null,
                "left=" + left + ",width=" + width + ",height=" + height + ",top=" + top + ",status=no,resizable=no,toolbar=no,location=no,menubar=no,directories=no");
        };

        function DeleteConfirmation() {
            return window.confirm("Are you sure you wish to delete this Return?");
        }
    </script>
</head>
<body>
    <form id="frmCreatedReturnDetails" runat="server">
        <div>
            <asp:Table ID="tblHeaderDetails" runat="server" Width="80%" Height="100%">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="RMA No:" Style="text-align: left" Font-Bold="true" HorizontalAlign="right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcRmaNo" HorizontalAlign="Left" Style="text-align: left" />
                    <asp:TableHeaderCell Text="Return Tracking No:" Style="text-align: left" HorizontalAlign="right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcReturnTrackingNo" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="External Document No:" Style="text-align: left" HorizontalAlign="right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcExternalDocNo" HorizontalAlign="Left" Style="text-align: left" />
                    <asp:TableHeaderCell Text="Order Date:" Style="text-align: left" HorizontalAlign="right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcOrderDate" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Date Created:" Style="text-align: left" HorizontalAlign="right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcDateCreated" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Channel Name:" Style="text-align: left" HorizontalAlign="right" ForeColor="#0099FF"/>
                    <asp:TableHeaderCell runat="server" ID="tcChannelName" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <br />
        <asp:Table ID="TblReturnHeaderLines" runat="server" Height="100%" Width="100%">
            <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
                <asp:TableHeaderCell Text="Item No" HorizontalAlign="Left" />
                <asp:TableHeaderCell Text="Description" HorizontalAlign="Left" Width="50%" />
                <asp:TableHeaderCell Text="Quantity" />
                <asp:TableHeaderCell Text="Price" HorizontalAlign="Left" />
                <asp:TableHeaderCell Text="Line Amount" HorizontalAlign="Left" />
                <asp:TableHeaderCell Text="Return Reason" HorizontalAlign="Left" />
                <asp:TableHeaderCell Text="REQ Return Action" HorizontalAlign="Left" />
            </asp:TableHeaderRow>
        </asp:Table>
        <br />
        <br />
        <asp:Button ID="BtnClose" runat="server" Text="Close" Style="float: right" OnClientClick="CloseWindow();" />
        <asp:Button ID="BtnCancelRMA" runat="server" Text="Cancel Return" Style="float: right" OnClick="BtnCancelRMA_Click" OnClientClick="return DeleteConfirmation();"/>
        <asp:Button ID="BtnUpdateRMA" runat="server" Text="Update Return" Style="float: right" OnClick="BtnUpdateRMA_Click"  />
        <asp:Button ID="BtnPrintRMAInstructions" runat="server" Text="Print RMA Instructions" Style="float: right" OnClientClick="OpenPDF();" />
    </form>
</body>
</html>
