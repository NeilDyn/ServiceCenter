<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestControl.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TestControl" %>
<link href="../../css/mainpage.css" rel="stylesheet" type="text/css" />

<div id="TestTable" class="TestTable">
    <asp:Table ID="tblTest" runat="server">
        <asp:TableRow>
            <asp:TableCell runat="server" Font-Bold="True" HorizontalAlign="Right">Address 1:</asp:TableCell>
            <asp:TableCell runat="server">3430 Sheridan Ave</asp:TableCell>           
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell runat="server" Font-Bold="True" HorizontalAlign="Right">Address 2:</asp:TableCell>
            <asp:TableCell runat="server"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell runat="server" Font-Bold="True" HorizontalAlign="Right">Ship to Contact:</asp:TableCell>
            <asp:TableCell runat="server"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell runat="server" Font-Bold="True" HorizontalAlign="Right">City:</asp:TableCell>
            <asp:TableCell runat="server">Miami</asp:TableCell>
            <asp:TableCell runat="server" Font-Bold="True" HorizontalAlign="Right">State:</asp:TableCell>
            <asp:TableCell runat="server">FL</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell runat="server" Font-Bold="True" HorizontalAlign="Right">Zip:</asp:TableCell>
            <asp:TableCell runat="server">33140</asp:TableCell>
            <asp:TableCell runat="server" Font-Bold="True" HorizontalAlign="Right">Country:</asp:TableCell>
            <asp:TableCell runat="server">US</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:GridView ID="gdvTableTest" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="100%"
                    style="margin-right:100px">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>
