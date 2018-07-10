<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="ExcelDesign.Forms.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
</head>
<body>
    <form id="frmLoginPage" runat="server">
        <asp:Table runat="server" ID="loginTable" CellPadding="0" CellSpacing="10" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableHeaderCell ColumnSpan="3" Text="User Login" ForeColor="#0099FF" Font-Bold="true" Font-Size="X-Large"/>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="User ID:" ForeColor="#0099FF" Font-Bold="true" />
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtUserID" />
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtUserID" runat="server" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Password:" ForeColor="#0099FF" Font-Bold="true"/>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtPassword" runat="server" />
                </asp:TableCell>
            </asp:TableRow>          
            <asp:TableRow>
                <asp:TableCell />
                <asp:TableCell>
                    <asp:Button Text="Login" ID="BtnLogin" Width="100%" runat="server" OnClick="UserLogin" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
