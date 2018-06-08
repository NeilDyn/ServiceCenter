<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleReturnOrderTableHeader.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableHeaders.SingleReturnOrderTableHeader" %>
<%@ Register Src="~/Forms/UserControls/TableData/SingleReturnOrderDetail.ascx" TagName="SingleReturnOrderDetail" TagPrefix="srod" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=singleReturnOrderDetail_<%= this.CustID %>_<%= this.HeadCount %>]").hide();

        $("[id$=btnExpandCurrentReturn_<%= this.CustID %>_<%= this.HeadCount %>]").click(function () {
            $("[id$=singleReturnOrderDetail_<%= this.CustID %>_<%= this.HeadCount %>]").toggle();
            if ($("[id$=btnExpandCurrentReturn_<%= this.CustID %>_<%= this.HeadCount %>]").val() == "+") {
                $("[id$=btnExpandCurrentReturn_<%= this.CustID %>_<%= this.HeadCount %>]").val("-");
            } else {
                $("[id$=btnExpandCurrentReturn_<%= this.CustID %>_<%= this.HeadCount %>]").val("+");
            }
        });
    });
</script>

<asp:Table ID="tblSingleReturnOrderTableHeader" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableHeader" runat="server">
        <asp:TableHeaderCell runat="server" ID="ExpandCurrentCustomer" Width="2%">
            <asp:Button ID="btnExpandCurrentReturn" runat="server" Text="+" OnClientClick="return false;" />
        </asp:TableHeaderCell>
        <asp:TableHeaderCell runat="server" ID="ReturnOrderSequence" Font-Bold="true" Font-Underline="true" Font-Size="Large" HorizontalAlign="Left" />
        <asp:TableHeaderCell Font-Bold="true" Text="RMA No:" HorizontalAlign="Right" Style="text-align: right" />
        <asp:TableHeaderCell runat="server" ID="thcRMANo" HorizontalAlign="Right" Style="text-align: left" />
        <asp:TableHeaderCell Font-Bold="true" Text="External Document No:" HorizontalAlign="Right" Style="text-align: right" />
        <asp:TableHeaderCell runat="server" ID="thcExternalDocumentNo" HorizontalAlign="Right" Style="text-align: left" />
    </asp:TableHeaderRow>
    <%--<asp:TableFooterRow>
        <asp:TableCell ColumnSpan ="6">
            <hr class="Seperator"/>
        </asp:TableCell>
    </asp:TableFooterRow>--%>
</asp:Table>
