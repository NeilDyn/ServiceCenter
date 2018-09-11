<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsSalesLineForm.aspx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.SalesLines.StatisticsSalesLineForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
</head>
<body>
    <form id="frmStatisticsSalesLineDetail" runat="server">
        <asp:Table ID="tblPendingSQApprovalLines" runat="server" Height="100%" Width="100%" ScrollBars="Auto">
            <asp:TableHeaderRow HorizontalAlign="Justify" ForeColor="White" BackColor="#507CD1">
                <asp:TableHeaderCell Text ="Customer No" />
                <asp:TableHeaderCell Text="Document No" />
                <asp:TableHeaderCell Text="External Document No" />
                <asp:TableHeaderCell Text="Created Date" />
                <asp:TableHeaderCell Text="Item No" />
                <asp:TableHeaderCell Text="Description" />
                <asp:TableHeaderCell Text="Qty" />
                <asp:TableHeaderCell Text="Status" />
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell ColumnSpan="8">
                <hr class="HeaderLine" />
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        </asp:Table>
    </form>
</body>
</html>
