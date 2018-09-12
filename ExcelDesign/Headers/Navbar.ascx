<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="ExcelDesign.Headers.Navbar" %>

<link href="../css/mainpage.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">

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

    function Homepage() {
        if (location.href.includes("Forms")) {
            location.replace("ServiceCenter.aspx");
        } else {
            location.replace("Forms/ServiceCenter.aspx");
        }
    }

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
    };

    function OpenStatisticsPanel() {
        if (location.href.includes("Forms")) {
            location.replace("Statistics.aspx");
        } else {
            location.replace("Forms/Statistics.aspx");
        }
    };

    function OpenAbout() {
        if (location.href.includes("Forms")) {
            location.replace("AboutPage.aspx");
        } else {
            location.replace("Forms/AboutPage.aspx");
        }
    };

    function SubmitFeedback() {
        window.open("https://docs.google.com/forms/d/e/1FAIpQLSfr07VbYNgq2yuyrRVCJUFtbssytAX563c7ZQBht_xZNx4EKg/viewform", "_blank");
    };
</script>

<div runat="server" class="HeaderNavbar" style="margin-left: 0">
    <ul>
        <li><a href="#" class="disableLink"></a></li>
        <li><a href="javascript:UserLogout()">Logout</a></li>
        <li runat="server" id="aboutPage"><a href="javascript:OpenAbout()">About</a></li>
        <li><a href="javascript:Homepage()">Homepage</a></li>
        <li><a href="javascript:OpenUserControlPanel()">Control Panel</a></li>
        <li runat="server" id="adminPanel"><a href="javascript:OpenAdminControlPanel()">Admin Panel</a></li>
        <li runat="server" id="statisticsPanel"><a href="javascript:OpenStatisticsPanel()">Statistics</a></li>
        <li><a href="javascript:SubmitFeedback()">Submit Feedback</a></li>
        <li runat="server" id="currentUser" style="padding: 0 20px"></li>
        <li style="padding: 0 20px" runat="server" id="versionList" />
        <li runat="server" id="applicationType" style="padding: 0 20px"></li>
    </ul>
</div>
<br />
<br />
