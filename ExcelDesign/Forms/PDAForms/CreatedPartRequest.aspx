<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatedPartRequest.aspx.cs" Inherits="ExcelDesign.Forms.PDAForms.CreatedPartRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script>
        function CloseWindow() {
            var c = window.confirm("Are you sure you wish to close this window?");
            if (c == true) {
                parent.window.close();
            };
        };
    </script>
</head>
<body>
    <form id="frmCreatedPDAPartRequestDetails" runat="server">
        <asp:Table ID="tblHeaderDetails" runat="server" Width="80%" Height="100%">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Quote No:" Style="text-align: left" Font-Bold="true" HorizontalAlign="right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcQuoteNo" HorizontalAlign="Left" Style="text-align: left" />
                <asp:TableHeaderCell Text="Ship Method:" Style="text-align: left" HorizontalAlign="right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcShipMethod" HorizontalAlign="Left" Style="text-align: left" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="External Document No:" Style="text-align: left" HorizontalAlign="right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcExternalDocNo" HorizontalAlign="Left" Style="text-align: left" />
                <asp:TableHeaderCell Text="Original Order No:" Style="text-align: left" HorizontalAlign="right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcOriginalOrderNo" HorizontalAlign="Left" Style="text-align: left" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Quote Date:" Style="text-align: left" HorizontalAlign="right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcQuoteDate" HorizontalAlign="Left" Style="text-align: left" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Channel Name:" Style="text-align: left" HorizontalAlign="right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcChannelName" HorizontalAlign="Left" Style="text-align: left" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>
                        <br />
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Shipping Information:" HorizontalAlign="Left" Font-Underline="true" Font-Bold="true" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Ship-to Name:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcShipToName" HorizontalAlign="Left" Style="text-align: left" />
                <asp:TableHeaderCell Text="Ship-to Address 1:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcShipToAddress1" HorizontalAlign="Left" Style="text-align: left" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Ship-to Contact:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcShipToContact" HorizontalAlign="Left" Style="text-align: left" />
                <asp:TableHeaderCell Text="Ship-to Address 2:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcShipToAddress2" HorizontalAlign="Left" Style="text-align: left" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Ship-to City:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcShipToCity" HorizontalAlign="Left" Style="text-align: left" />
                <asp:TableHeaderCell Text="Ship-to State:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcShipToState" HorizontalAlign="Left" Style="text-align: left" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Ship-to Zip:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcShipToZip" HorizontalAlign="Left" Style="text-align: left" />
                <asp:TableHeaderCell Text="Ship-to Country:" Style="text-align: left" HorizontalAlign="Right" ForeColor="#0099FF" />
                <asp:TableHeaderCell runat="server" ID="tcShipToCountry" HorizontalAlign="Left" Style="text-align: left" />
            </asp:TableHeaderRow>
        </asp:Table>
        <br />
        <asp:Table ID="TblReturnHeaderLines" runat="server" Height="100%" Width="100%">
            <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
                <asp:TableHeaderCell Text="Item No" HorizontalAlign="Left" />
                <asp:TableHeaderCell Text="Description" HorizontalAlign="Left" Width="50%" />
                <asp:TableHeaderCell Text="Quantity" />
                <asp:TableHeaderCell Text="Price" HorizontalAlign="Left" />
                <asp:TableHeaderCell Text="Line Amount" HorizontalAlign="Left" />
            </asp:TableHeaderRow>
        </asp:Table>
        <br />
        <br />
        <asp:Button ID="BtnClose" runat="server" Text="Close" Style="float: right" OnClientClick="CloseWindow();" />
    </form>
</body>
</html>
