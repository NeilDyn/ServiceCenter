<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceCenter.aspx.cs" Inherits="ExcelDesign.Forms.ServiceCenter" %>
<%@ Register Src="~/Forms/UserControls/SingleControls/MultipleCustomers.ascx" TagName="MultipleCustomers" TagPrefix="mc" %>
<%@ Register Src="~/Forms/UserControls/MainTables/CustomerInfoTable.ascx" TagName="CustomerInfoTable" TagPrefix="tib" %>

<head runat="server">
    <title>Customer Service Center</title>
    <link href="../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script>
        var sess_interval = 60000;
        var sess_minutes = 20000;
        var sess_warningMinutes = 10000;
        var sess_intervalID;
        var sess_lastActivity;
        var interval;

        var interval = setInterval('checkSession()', sess_interval);

        function checkSession() {
            sess_minutes--;

            if (sess_minutes == sess_warningMinutes) {
                var confirmActive = confirm("Warning, your session will expire in 1 minute. Do you want to continue working?", "Session is about to expire");

                if (confirmActive) {
                    sess_minutes = 20000;
                    alert("Session extended");
                } else {
                    alert("Session timeout");
                }
            }
        }
    </script>
</head>
<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <form id="frmOrderDetails" runat="server">        
        <div id="ServiceCenterHeader">
            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/Logo.png" />
            <asp:Label ID="lblCustomerServicePortal" runat="server" style="margin-left: 200px;" Text="Customer Service Portal" ForeColor="#0099FF" Font-Bold="True" Font-Size="XX-Large" CssClass="HeaderLabel"/>        
        </div>
        <hr class="HeaderLine"/>
        
        <div class="SearchArea" id="SearchArea" style="margin-top: 20px;">        
            <asp:Label ID="Label1" runat="server" Text="Search" ForeColor="#0099FF" Font-Bold="True"/>
            <asp:TextBox ID="txtSearchBox" runat="server" Width="700px" BorderColor="Black" BorderWidth="2px" style="margin-left: 35px" />
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" style="margin-left: 30px" Text="Search" />
            <asp:Label ID="Label2" runat="server" Text="Search Options:" ForeColor="#0099FF" Font-Bold="True" style="margin-left: 30px" />
            <asp:DropDownList ID="DdlSearchOptions" runat="server" style="margin-left: 35px" Width="260px">
                <asp:ListItem Value="Default (Exludes Ship-to Filters)"></asp:ListItem>
                <asp:ListItem Value="Search All"></asp:ListItem>
                <asp:ListItem Value="PO Number"></asp:ListItem>
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