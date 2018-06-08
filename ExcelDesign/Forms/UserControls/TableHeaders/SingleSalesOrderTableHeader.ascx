<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSalesOrderTableHeader.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableHeaders.SingleSalesOrderTableHeader" %>
<%@ Register Src="~/Forms/UserControls/TableData/SingleSalesOrderDetail.ascx" TagName="SingleSalesOrderDetail" TagPrefix="ssod" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=singleSalesOrderDetail_<%= this.CustID %>_<%= this.HeadCount %>]").hide();

        $("[id$=btnExpandCurrentOrder_<%= this.CustID %>_<%= this.HeadCount %>]").click(function () {
            $("[id$=singleSalesOrderDetail_<%= this.CustID %>_<%= this.HeadCount %>]").toggle();

            if ($("[id$=btnExpandCurrentOrder_<%= this.CustID %>_<%= this.HeadCount %>]").val() == "+") {
                $("[id$=btnExpandCurrentOrder_<%= this.CustID %>_<%= this.HeadCount %>]").val("-");
            }
            else {
                $("[id$=btnExpandCurrentOrder_<%= this.CustID %>_<%= this.HeadCount %>]").val("+");
            }
        });
    });
</script>

<asp:Table ID="tblSingleSalesOrderTableHeader" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableHeader" runat="server">
        <asp:TableHeaderCell runat="server" ID="ExpandCurrentCustomer" Width="2%">
            <asp:Button ID="btnExpandCurrentOrder" runat="server" Text="+" OnClientClick="return false;"/>     
        </asp:TableHeaderCell>
        <asp:TableHeaderCell runat="server" ID="SalesOrderSequence" Font-Bold="true" Font-Underline="true" Font-Size="Large" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Font-Bold="true" Text="External Document No:" HorizontalAlign="Right" style="text-align:right"/>
        <asp:TableHeaderCell runat="server" ID="thcExternalDocumentNo" HorizontalAlign="Right" style="text-align:left"/>
    </asp:TableHeaderRow>
    <%--<asp:TableFooterRow>
        <asp:TableCell ColumnSpan ="4">
            <hr class="Seperator"/>
        </asp:TableCell>
    </asp:TableFooterRow>--%>
</asp:Table>