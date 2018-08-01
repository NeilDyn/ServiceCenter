<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorForm.aspx.cs" Inherits="ExcelDesign.Forms.ErrorForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error Page</title>
    <link rel="icon" type="image/ico" href="../images/icon.ico" />
</head>
<body>
    <form id="frmErrorPage" runat="server">
        <h2>An Error Has Occurred
        </h2>

        <p runat="server" id="errorMessage" />

        <ul>
            <li>
                <asp:HyperLink ID="linkHome" runat="server" NavigateUrl="~/Forms/ServiceCenter.aspx" Text="Return to homepage" />
            </li>
        </ul>
    </form>
</body>
</html>
