<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserControl.aspx.cs" Inherits="ExcelDesign.Forms.UserControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Control</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        function UpdatePassword() {
            var userID = "<%= this.tcUserID.Text %>";
            var password = "<%= this.txtPassword.Text %>";

            if (password.trim() == null || password.trim() == "") {
                alert("Please insert a valid password");
            } else {
                var confirmPassword = prompt("Please confirm new password entry");

                if (password == confirmPassword) {
                    $.ajax({
                        type: "POST",
                        url: "ServiceCenter.aspx/UpdateUserPassword",
                        data: JSON.stringify({ currentUser: userID, newPassword: password }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (orderNo) {
                            alert("Password successfully updated!");
                        },
                        error: function (xhr, status, text) {
                            //console.log(xhr.status);
                            //console.log(xhr.text);
                            //console.log(xhr.responseText);
                            alert("test");
                        },
                    });
                } else {
                    alert("Passwords do not match!");
                }
            }
        }
    </script>
    <link href="../css/mainpage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmUserControl" runat="server">
        <asp:Table ID="TblUserControl" runat="server" Height="100%" Width="100%" HorizontalAlign="Center">
            <asp:TableHeaderRow ForeColor="White" BackColor="#507CD1">
                <asp:TableHeaderCell Text="User ID" />
                <asp:TableHeaderCell Text="Password" />
                <asp:TableHeaderCell Text="Create RMA" />
                <asp:TableHeaderCell Text="Create Return Label" />
                <asp:TableHeaderCell Text="Admin" />
                <asp:TableHeaderCell Text="Developer" />
                <asp:TableHeaderCell Text="Last Password Update" />
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell ID="tcUserID" />
                <asp:TableCell ID="tcPassword" HorizontalAlign="Center">
                    <asp:TextBox ID="txtPassword" runat="server" Width="90%" />
                </asp:TableCell><asp:TableCell ID="tcCreateRMA" Width="10%" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCreateRMA" runat="server" Enabled="false" />
                </asp:TableCell><asp:TableCell ID="tcCreateReturnLabel" Width="10%" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCreateReturnLabel" runat="server" Enabled="false"/>
                </asp:TableCell><asp:TableCell ID="tcAdmin" Width="10%" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxAdmin" runat="server" Enabled="false" />
                </asp:TableCell><asp:TableCell ID="tcDeveloper" Width="10%" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxDeveloper" runat="server" Enabled="false" />
                </asp:TableCell><asp:TableCell ID="tcLastPasswordUpdate" Width="10%" />
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <asp:Button ID="btnHomepage" runat="server" Text="Homepage" Style="float: right" OnClick="btnHomepage_Click" />
        <asp:Button ID="BtnUpdatePassword" runat="server" Text="Update Password" Style="float: right" OnClientClick="UpdatePassword()" />
    </form>
</body>
</html>
