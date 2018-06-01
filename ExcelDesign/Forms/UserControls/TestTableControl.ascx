<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestTableControl.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TestTableControl" %>

<asp:Table ID="tblOrderInfo" runat="server" Width="633px">
    <asp:TableRow runat="server" TableSection="TableHeader">
        <asp:TableCell runat="server" Font-Bold="True">Order Info:</asp:TableCell>
        <asp:TableCell runat="server">1</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server" TableSection="TableBody">
        <asp:TableCell runat="server" ID="gridviewCell">
            <asp:GridView ID="gdvTestTable" runat="server"></asp:GridView>
        </asp:TableCell>
    </asp:TableRow>

</asp:Table>
