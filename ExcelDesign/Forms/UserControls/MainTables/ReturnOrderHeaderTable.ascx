<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReturnOrderHeaderTable.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.MainTables.ReturnOrderHeaderTable" %>
<%@ Register Src="~/Forms/UserControls/TableHeaders/SingleReturnOrderTableHeader.ascx" TagName="SingleReturnOrderTableHeader" TagPrefix="sroth" %>
<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=salesReturnDetailHeader_<%= this.CustID %>]").hide();

        $("[id$=btnExpandReturn_<%= this.CustID %>]").click(function () {
            $("[id$=salesReturnDetailHeader_<%= this.CustID %>]").toggle();
            if ($("[id$=btnExpandReturn_<%= this.CustID %>]").val() == "+") {
                $("[id$=btnExpandReturn_<%= this.CustID %>]").val("-");
            } else {
                $("[id$=btnExpandReturn_<%= this.CustID %>]").val("+");
            }
        });
    });
</script>

<asp:Table ID="tblReturnOrderHeader" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableHeader">
        <asp:TableHeaderCell runat="server" ID="ExpandCurrentCustomer" Width="2%">
            <asp:Button ID="btnExpandReturn" runat="server" Text="+" OnClientClick="return false;"/>     
        </asp:TableHeaderCell>
        <asp:TableHeaderCell Text="Return Info:" HorizontalAlign="Left" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
        <asp:TableHeaderCell Text="Total Returns:" HorizontalAlign="Right" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
        <asp:TableHeaderCell runat="server" ID="thcTotalReturns" HorizontalAlign="Right" ForeColor="#0099FF" Font-Bold="True" Font-Size="Large"/>
    </asp:TableHeaderRow>
    <asp:TableRow>
        <asp:TableCell />
        <asp:TableCell ColumnSpan="3"> <hr class="TableHeaderLine"/></asp:TableCell>
    </asp:TableRow>
</asp:Table>