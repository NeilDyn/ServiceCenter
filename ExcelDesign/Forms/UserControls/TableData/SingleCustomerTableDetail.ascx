<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleCustomerTableDetail.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableData.SingleCustomerTableDetail" %>
<%@ Register Src="~/Forms/UserControls/MainTables/SalesOrderHeaderTable.ascx" TagName="SalesOrderHeaderTable" TagPrefix="soht"%>

<link href="../../../../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jquery.ui/1.10.2/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="//ajax.aspnetcdn.com/ajax/jquery.ui/1.10.2/themes/ui-lightness/jquery-ui.css" type="text/css" />

<asp:Table ID="tblSingleCustomerDetail" runat="server" Height="100%" Width="100%">
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify" >
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell Text="Address 2:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcAddress2" />
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify" >
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell Text="Ship to Contact:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcShiptoContact"/>
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify" >
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell Text="City:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcCity"/>
        <asp:TableCell Text="State:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcState"/>
        <asp:TableCell Text="whitespace" ForeColor="White"/>
        <asp:TableCell Text="whitespace" ForeColor="White"/>
    </asp:TableRow>
    <asp:TableRow TableSection="TableBody" HorizontalAlign="Justify" >
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell />
        <asp:TableCell Text="Zip:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcZip" style="text-align: left"/>
        <asp:TableCell Text="Country:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableCell runat="server" ID="tcCountry"/>
        <asp:TableCell Text="whitespace" ForeColor="White"/>
        <asp:TableCell Text="whitespace" ForeColor="White"/>
    </asp:TableRow>
</asp:Table>
