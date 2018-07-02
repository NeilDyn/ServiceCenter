<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatedRMA.aspx.cs" Inherits="ExcelDesign.Forms.FunctionForms.CreatedRMA" %>

<!DOCTYPE html>

<link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {

        });

        function CloseWindow() {
            var c = window.confirm("Are you sure you wish to close this window?");
            if (c == true) {
                parent.window.close();
            };
        };
    </script>
</head>
<body>
    <form id="frmCreatedReturnDetails" runat="server">
        <div>
            <asp:Table ID="tblHeaderDetails" runat="server" Width="80%" Height="100%">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="RMA No:" Style="text-align: left" Font-Bold="true" HorizontalAlign="right" />
                    <asp:TableHeaderCell runat="server" ID="tcRmaNo" HorizontalAlign="Left" Style="text-align: left" />
                    <asp:TableHeaderCell Text="Return Tracking No:" Style="text-align: left" HorizontalAlign="right" />
                    <asp:TableHeaderCell runat="server" ID="tcReturnTrackingNo" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="External Document No:" Style="text-align: left" HorizontalAlign="right" />
                    <asp:TableHeaderCell runat="server" ID="tcExternalDocNo" HorizontalAlign="Left" Style="text-align: left" />
                    <asp:TableHeaderCell Text="Order Date:" Style="text-align: left" HorizontalAlign="right" />
                    <asp:TableHeaderCell runat="server" ID="tcOrderDate" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Date Created:" Style="text-align: left" HorizontalAlign="right" />
                    <asp:TableHeaderCell runat="server" ID="tcDateCreated" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Channel Name:" Style="text-align: left" HorizontalAlign="right" />
                    <asp:TableHeaderCell runat="server" ID="tcChannelName" HorizontalAlign="Left" Style="text-align: left" />
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <br />
        <asp:GridView ID="gdvReturnHeaderLines" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="100%" Width="100%" OnRowDataBound="gdvReturnHeaderLines_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <br />
        <asp:Button ID="BtnClose" runat="server" Text="Close" Style="float: right" OnClientClick="CloseWindow();"/>
        <asp:Button ID="BtnViewRMAInstructions" runat="server" Text="View RMA Instructions" Style="float: right"  />
    </form>
</body>
</html>
