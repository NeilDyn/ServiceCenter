<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleCustomerTableDetail.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.SingleCustomerTableDetail" %>
<%@ Register Src="~/Forms/UserControls/MainTables/SalesOrderHeaderTable.ascx" TagName="SalesOrderHeaderTable" TagPrefix="soht"%>

<link href="../../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<asp:Table ID="tblSingleCustomerDetail" runat="server" Height="100%" Width="100%">
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell Text="Address 2:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcAddress2" />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell Text="Ship to Contact:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcShiptoContact"/>
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell Text="City:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcCity"/>
        <asp:TableCell Text="State:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcState"/>
        <asp:TableCell />
        <asp:TableCell />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify">
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell Text="Zip:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcZip" style="text-align: left"/>
        <asp:TableCell Text="Country:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcCountry"/>
        <asp:TableCell />
        <asp:TableCell />
    </asp:TableRow>
</asp:Table>
