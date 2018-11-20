<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuggestSimilarItem.aspx.cs" Inherits="ExcelDesign.Forms.UserControls.StatisticsControls.SuggestSimilarItems.SuggestSimilarItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../../css/mainpage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
    <script>
        function SetSuggestedItem(setItemNo, rowNo) {
            var itemNo = "<%= this.ItemNo%>";
            $.ajax({
                    type: "POST",
                    url: "SuggestSimilarItem.aspx/SetItem",
                    data: JSON.stringify({ originalItemNo: itemNo, setItemNo: setItemNo, rowNo: rowNo}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (error) {
                        if (error.d.indexOf("Error") == -1) {
                            parent.window.close();
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
            return false;
        };
    </script>
</head>
<body>
    <form id="frmSuggestSimilarItem" runat="server">
        <asp:DropDownList ID="ddlSuggestionOptions" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSuggestionOptions_SelectedIndexChanged">
            <asp:ListItem Value=""></asp:ListItem>
            <asp:ListItem Value="Product Group & Unit Cost"></asp:ListItem>
            <asp:ListItem Value="Unit Cost"></asp:ListItem>
            <asp:ListItem Value="All"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Table ID="tblSuggestedSimilarItems" runat="server" Height="100%" Width="70%">
            <asp:TableHeaderRow HorizontalAlign="Justify" ForeColor="White" BackColor="#507CD1">
                <asp:TableHeaderCell Text="Item No" />
                <asp:TableHeaderCell Text="Description" />
                <asp:TableHeaderCell Text="Unit Price" />
                <asp:TableHeaderCell Text="Select" />
            </asp:TableHeaderRow>
        </asp:Table>
    </form>
</body>
</html>
