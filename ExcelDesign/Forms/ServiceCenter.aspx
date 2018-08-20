<!DOCTYPE html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceCenter.aspx.cs" Inherits="ExcelDesign.Forms.ServiceCenter" %>

<%@ Register Src="~/Forms/UserControls/SingleControls/MultipleCustomers.ascx" TagName="MultipleCustomers" TagPrefix="mc" %>
<%@ Register Src="~/Forms/UserControls/MainTables/CustomerInfoTable.ascx" TagName="CustomerInfoTable" TagPrefix="tib" %>

<head runat="server">
    <title>Customer Service Center</title>
    <link href="../css/mainpage.css" rel="stylesheet" type="text/css" />
    <link rel="icon" type="image/ico" href="../images/icon.ico" />
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jquery.ui/1.10.2/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="//ajax.aspnetcdn.com/ajax/jquery.ui/1.10.2/themes/ui-lightness/jquery-ui.css" type="text/css" />
    <script>
        $("id$=btnExtendSessionTime").click(function () {
            ResetTimers();
        });

        var warningTimer;
        var timeoutTimer;

        //Start the timers
        function StartTimers() {
            warningTimer = setTimeout("IdleWarning()", (<%= this.SessionTime %> * 60000) - 60000);
            timeoutTimer = setTimeout("IdleTimeout()", <%= this.SessionTime %> * 60000);
        };

        //Reset the timers
        function ResetTimers() {
            clearTimeout(warningTimer);
            clearTimeout(timeoutTimer);
            StartTimers();
            $("#timeout").dialog('close');
            return false;
        };

        //Show idle timeout warning dialog
        function IdleWarning() {
            $("#timeout").dialog({
                title: "Session about to expire",
                modal: true
            });
        };

        function IdleTimeout() {
            $.ajax({
                type: "POST",
                url: "ServiceCenter.aspx/SessionCompleted",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert(response.d);
                    window.location = "LoginPage.aspx";
                },
                error: function (xhr, status, text) {
                    console.log(xhr.status);
                    console.log(xhr.text);
                    console.log(xhr.responseText);
                },
            });
        };

        function UserLogout() {
            $.ajax({
                type: "POST",
                url: "ServiceCenter.aspx/UserLogout",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (error) {
                    if (error.d.indexOf("Error") == -1) {
                        if (location.href.includes("Forms")) {
                            location.replace("LoginPage.aspx");
                        } else {
                            location.replace("Forms/LoginPage.aspx");
                        }
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
        };

        function OpenUserControlPanel() {
            if (location.href.includes("Forms")) {
                location.replace("UserControl.aspx");
            } else {
                location.replace("Forms/UserControl.aspx");
            }

        };

        function OpenAdminControlPanel() {
            if (location.href.includes("Forms")) {
                location.replace("AdminControl.aspx");
            } else {
                location.replace("Forms/AdminControl.aspx");
            }
        }

        function SubmitFeedback() {
            window.open("https://docs.google.com/forms/d/e/1FAIpQLSfr07VbYNgq2yuyrRVCJUFtbssytAX563c7ZQBht_xZNx4EKg/viewform", "_blank");
        };

    </script>
</head>
<html xmlns="http://www.w3.org/1999/xhtml">
<body onload="StartTimers();">
    <form id="frmOrderDetails" runat="server">
        <div id="timeout" style="display: none">
            <h1>Session is about to expire</h1>
            <p>Warning, your session will expire in 1 minute. Do you want to continue working?</p>
            <asp:Button ID="btnExtendSessionTime" Text="Yes" runat="server" OnClientClick="ResetTimers()" />
        </div>
        <div runat="server" class="HeaderNavbar" style="margin-left:0">
            <ul>
                <li><a href="#" class="disableLink"></a></li>
                <li><a href="javascript:UserLogout()">Logout</a></li>
                <li><a href="javascript:OpenUserControlPanel()">Control Panel</a></li>
                <li runat="server" id="adminPanel"><a href="javascript:OpenAdminControlPanel()">Admin Panel</a></li>
                <li><a href="javascript:SubmitFeedback()">Submit Feedback</a></li>
                <li runat="server" id="currentUser" style="padding: 0 20px"></li>
                <li style="padding: 0 20px">v4.2</li>
                <li runat="server" id="applicationType" style="padding: 0 20px"></li>
            </ul>
        </div>
        <br />
        <div id="ServiceCenterHeader">
            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/Logo.png" />
            <asp:Label ID="lblCustomerServicePortal" runat="server" Style="margin-left: 200px;" Text="Customer Service Portal" ForeColor="#0099FF" Font-Bold="True" Font-Size="XX-Large" CssClass="HeaderLabel" />
        </div>
        <hr class="HeaderLine" />

        <div class="SearchArea" id="SearchArea" style="margin-top: 20px;">
            <asp:Label ID="lblSearch" runat="server" Text="Search" ForeColor="#0099FF" Font-Bold="True" />
            <asp:TextBox ID="txtSearchBox" runat="server" Width="700px" BorderColor="Black" BorderWidth="2px" Style="margin-left: 35px" />
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Style="margin-left: 30px" Text="Search" />
            <asp:Label ID="lblSearchOptions" runat="server" Text="Search Options:" ForeColor="#0099FF" Font-Bold="True" Style="margin-left: 30px" />
            <asp:DropDownList ID="DdlSearchOptions" runat="server" Style="margin-left: 35px" Width="260px">
                <asp:ListItem Value="Default (Exludes Ship-to Filters)"></asp:ListItem>
                <asp:ListItem Value="Search All"></asp:ListItem>
                <asp:ListItem Value="External Document No."></asp:ListItem>
                <asp:ListItem Value="Tracking No"></asp:ListItem>
                <asp:ListItem Value="IMEI"></asp:ListItem>
                <asp:ListItem Value="Ship-to Name"></asp:ListItem>
                <asp:ListItem Value="Ship-to Address"></asp:ListItem>
                <asp:ListItem Value="RMA-No"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <br />
    </form>
</body>
</html>
