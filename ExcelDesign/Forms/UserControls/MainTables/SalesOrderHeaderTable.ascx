<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderHeaderTable.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.MainTables.SalesOrderHeaderTable" %>
<%@ Register Src="~/Forms/UserControls/TableHeaders/SingleSalesOrderTableHeader.ascx" TagName="SingleSalesOrderTableHeader" TagPrefix="ssoth" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        if (<%= this.SalesOrderCount %> > 1) {
            $("[id$=salesOrderDetailHeader_<%= this.CustID %>_<%= this.salesCount %>]").hide();
        }

        $("[id$=btnExpandOrder_<%= this.CustID %>]").click(function () {
            $("[id$=salesOrderDetailHeader_<%= this.CustID %>_<%= this.salesCount %>]").toggle();
            if ($("[id$=btnExpandOrder_<%= this.CustID %>]").val() == "+") {
                $("[id$=btnExpandOrder_<%= this.CustID %>]").val("-");
            } else {
                $("[id$=btnExpandOrder_<%= this.CustID %>]").val("+");
            }
        });
    });
</script>

<asp:Table ID="tblSalesOrderHeader" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableHeader">
        <asp:TableHeaderCell runat="server" ID="ExpandCurrentCustomer" Width="2%">
            <asp:Button ID="btnExpandOrder" runat="server" Text="+" OnClientClick="return false;"/>     
        </asp:TableHeaderCell>
        <asp:TableHeaderCell Text="Order Info:" HorizontalAlign="Left" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
        <asp:TableHeaderCell Text="Total Orders:" HorizontalAlign="Right" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
        <asp:TableHeaderCell runat="server" ID="thcTotalOrders" HorizontalAlign="Right" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
    </asp:TableHeaderRow>
    <asp:TableRow>
        <asp:TableCell />
        <asp:TableCell ColumnSpan="3"> <hr class="TableHeaderLine"/></asp:TableCell>
    </asp:TableRow>
</asp:Table>