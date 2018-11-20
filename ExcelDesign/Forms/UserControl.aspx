<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserControl.aspx.cs" Inherits="ExcelDesign.Forms.UserControl" %>

<!DOCTYPE html>

<%@ Register Src="~/Headers/Navbar.ascx" TagName="UserControlNavbar" TagPrefix="ucnav" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Control</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="icon" type="image/ico" href="../images/icon.ico" />
    <ucnav:UserControlNavbar ID="UserControlNavbar" runat="server" />
    <script type="text/javascript">
        function UpdatePassword() {
            var userID = "<%= this.tcUserID.Text %>";
            var password = prompt("Please enter a new password.");

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
                        success: function (error) {
                            if (error.d.indexOf("Error") == -1) {
                                alert("Password successfully updated!");
                            } else {
                                alert(error.d);
                            }
                        },
                        error: function (xhr, status, text) {
                            console.log(xhr.status);
                            console.log(xhr.text);
                            console.log(xhr.responseText);
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
        <asp:Table ID="TblUserControl" runat="server" Height="100%" Width="30%" HorizontalAlign="Left">
            <asp:TableRow>
                <asp:TableCell Text="User ID"/>
                <asp:TableCell ID="tcUserID" />
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Password" />
                <asp:TableCell ID="tcPassword" />
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Create RMA" />
                <asp:TableCell ID="tcCreateRMA" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCreateRMA" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Create Return Label" />
                <asp:TableCell ID="tcCreateReturnLabel" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCreateReturnLabel" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Create Exchange Order" />
                <asp:TableCell ID="tcCreateExchangeOrder" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCreateExchangeOrder" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>      
            <asp:TableRow>
                <asp:TableCell Text="Create Part Request" />
                <asp:TableCell ID="tcCreatePartialRequest" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCreatePartialRequest" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow>
                <asp:TableCell Text="Create Refund" />
                <asp:TableCell ID="tcCreateRefund" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCreateRefund" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Cancel Order" />
                <asp:TableCell ID="tcCancelOrder" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCancelOrder" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Can Partial Refund" />
                <asp:TableCell ID="tcPartialRefund" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxPartialRefund" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Create PDA Replacement RMA" />
                <asp:TableCell ID="tcCreatePDARMA" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCreatePDARMA" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow>
                <asp:TableCell Text="Create PDA Replacement Exchange" />
                <asp:TableCell ID="tcCreatePDAExchange" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCreatePDAExchange" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow>
                <asp:TableCell Text="Create PDA Part Request" />
                <asp:TableCell ID="tcCreatePDAPartRequest" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCreatePDAPartRequest" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Create PDA Refund" />
                <asp:TableCell ID="tcCreatePDARefund" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCreatePDARefund" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Cancel PDA Order" />
                <asp:TableCell ID="tcCancelPDAOrder" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxCancelPDAOrder" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Can Partial Refund PDA" />
                <asp:TableCell ID="tcPartialRefundPDA" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxPartialRefundPDA" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Partial Refund Tier" />
                <asp:TableCell ID="tcPartialrefundTier" />
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Admin" />
                <asp:TableCell ID="tcAdmin" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxAdmin" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Developer" />
                <asp:TableCell ID="tcDeveloper" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxDeveloper" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Supervisor" />
                <asp:TableCell ID="tcSupervisor" HorizontalAlign="Center">
                    <asp:CheckBox ID="cbxSupervisor" runat="server" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Last Password Update" />
                <asp:TableCell ID="tcLastPasswordUpdate" />
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Password Expiry Date" />
                <asp:TableCell ID="tcPasswordExpiryDate" />
            </asp:TableRow> 
            <asp:TableFooterRow>
                <asp:TableHeaderCell>
                    <asp:Button ID="BtnUpdatePassword" runat="server" Text="Update Password" OnClientClick="UpdatePassword()" />
                </asp:TableHeaderCell>
            </asp:TableFooterRow>
        </asp:Table>
    </form>
</body>
</html>
