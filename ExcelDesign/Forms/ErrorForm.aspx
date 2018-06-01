<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorForm.aspx.cs" Inherits="ExcelDesign.Forms.ErrorForm" %>
<%@ Register Src="~/Forms/UserControls/TestTableControl.ascx" TagName="OrderInfoTable" TagPrefix="tblOrderInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error Page</title>
</head>
<body>
    <form id="frmErrorPage" runat="server">
        <asp:Table ID="tblTest" runat="server" Width="80%">
            <asp:TableRow runat="server" TableSection="TableHeader">
                <asp:TableCell runat="server">Customer Info:</asp:TableCell>
                <asp:TableCell runat="server">Total Customers</asp:TableCell>
                <asp:TableCell runat="server" Font-Bold="True">1</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Table ID="tblCustomerInfo" runat="server">
                        <asp:TableRow runat="server" TableSection="TableHeader">
                            <asp:TableCell runat="server">Customer 1</asp:TableCell>                        
                        
</asp:TableRow>
                        
<asp:TableRow runat="server">
                            <asp:TableCell runat="server">
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="+" />
                            
</asp:TableCell>
                        
</asp:TableRow>
                        
<asp:TableRow runat="server" ID="CustomerOrderRow">
                            <asp:TableCell runat="server"></asp:TableCell>
                            
<asp:TableCell runat="server" ID="CustomerOrderCell"></asp:TableCell>                          
                        
</asp:TableRow>
                        
<asp:TableRow>
    <asp:TableCell></asp:TableCell>
                            <asp:TableCell runat="server" ID="TESTA">EK IS A ORDER NOMMER</asp:TableCell>
                        
</asp:TableRow>
                        
<asp:TableRow runat="server" ID="CustomerReturnRow">
                        </asp:TableRow>
                    
</asp:Table>
                
</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
    </form>
</body>
</html>
