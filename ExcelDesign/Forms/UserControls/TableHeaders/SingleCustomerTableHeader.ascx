<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleCustomerTableHeader.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableHeaders.SingleCustomerTableHeader" %>
<%@ Register Src="~/Forms/UserControls/TableData/SingleCustomerTableDetail.ascx" TagName="SingleCustomerTableDetail" TagPrefix="sctd"%>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblSingleCustomerTableHeader" runat="server" Width="85%" Height="100%">
    <asp:TableHeaderRow TableSection="TableHeader" runat="server">
        <asp:TableHeaderCell runat="server" ID="ExpandCurrentCustomer" Width="2%">
            <asp:Button ID="btnExpandCurrentCustomer" runat="server" Text="+" />     
        </asp:TableHeaderCell>
        <asp:TableHeaderCell runat="server" ID="CustomerSequence" Font-Bold="true" Font-Underline="true" Font-Size="Large" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Font-Bold="true" Text="Name:" HorizontalAlign="Left" style="text-align:right"/>
        <asp:TableHeaderCell runat="server" ID="thcCustomerName" HorizontalAlign="Left" style="text-align:left"/>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
    </asp:TableHeaderRow>
</asp:Table>
