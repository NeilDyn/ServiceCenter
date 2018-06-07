<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleCustomerTableHeader.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableHeaders.SingleCustomerTableHeader" %>
<%@ Register Src="~/Forms/UserControls/TableData/SingleCustomerTableDetail.ascx" TagName="SingleCustomerTableDetail" TagPrefix="sctd"%>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=customerDetails_<%= this.Count %>]").hide();

        $("[id$=btnExpload_<%= this.Count %>]").click(function () {
            $("[id$=customerDetails_<%= this.Count %>]").toggle();
            if ($("[id$=btnExpload_<%= this.Count %>]").val() == "+") {
                $("[id$=btnExpload_<%= this.Count %>]").val("-");
            } else {
                $("[id$=btnExpload_<%= this.Count %>]").val("+");
            }
        });
    });
</script>

<asp:Table ID="tblSingleCustomerTableHeader" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableHeader" runat="server">
        <asp:TableHeaderCell runat="server" ID="ExpandCurrentCustomer" Width="2%">
            <asp:Button ID="btnExpload" runat="server" Text="+" OnClientClick="return false;"/>
</asp:TableHeaderCell>
        <asp:TableHeaderCell runat="server" ID="CustomerSequence" Font-Bold="true" Font-Underline="true" Font-Size="Large" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Font-Bold="true" Text="Name:" HorizontalAlign="Left" style="text-align:right"/>
        <asp:TableHeaderCell runat="server" ID="thcCustomerName" HorizontalAlign="Left" style="text-align:left"/>
        <asp:TableHeaderCell HorizontalAlign="Right" />
    </asp:TableHeaderRow>
    <asp:TableRow runat="server" ID="customerDetails" TableSection="TableBody">

    </asp:TableRow>
</asp:Table>



