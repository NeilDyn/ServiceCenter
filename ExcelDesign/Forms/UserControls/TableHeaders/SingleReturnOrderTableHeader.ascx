<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleReturnOrderTableHeader.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableHeaders.SingleReturnOrderTableHeader" %>
<%@ Register Src="~/Forms/UserControls/TableData/SingleReturnOrderDetail.ascx" TagName="SingleReturnOrderDetail" TagPrefix="srod" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblSingleReturnOrderTableHeader" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableHeader" runat="server">
        <asp:TableHeaderCell runat="server" ID="ExpandCurrentCustomer" Width="2%">
            <asp:Button ID="btnExpandCurrentCustomer" runat="server" Text="+" />     
        </asp:TableHeaderCell>
        <asp:TableHeaderCell runat="server" ID="ReturnOrderSequence" Font-Bold="true" Font-Underline="true" Font-Size="Large" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Font-Bold="true" Text="RMA No:" HorizontalAlign="Right" style="text-align:right"/>
        <asp:TableHeaderCell runat="server" ID="thcRMANo" HorizontalAlign="Right" style="text-align:left"/>
        <asp:TableHeaderCell Font-Bold="true" Text="External Document No:" HorizontalAlign="Right" style="text-align:right"/>
        <asp:TableHeaderCell runat="server" ID="thcExternalDocumentNo" HorizontalAlign="Right" style="text-align:left"/>
    </asp:TableHeaderRow>
</asp:Table>