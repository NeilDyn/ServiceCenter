<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleCustomerTableHeader.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableHeaders.SingleCustomerTableHeader" %>
<%@ Register Src="~/Forms/UserControls/TableData/SingleCustomerTableDetail.ascx" TagName="SingleCustomerTableDetail" TagPrefix="sctd" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("[id$=customerDetails_<%= this.Count %>]").hide();

        $("[id$=btnExpload_<%= this.Count %>]").click(function () {
            $("[id$=customerDetails_<%= this.Count %>]").toggle();

            if ($("[id$=btnExpload_<%= this.Count %>]").val() == "+") {
                $("[id$=btnExpload_<%= this.Count %>]").val("-");
            }
            else {
                $("[id$=btnExpload_<%= this.Count %>]").val("+");
            }
        });

        <%--$("[id$=btnSelectCustomer_<%= this.Count %>]").click(function () {

            $.ajax({
                    type: "POST",
                    url: "ServiceCenter.aspx/SetActiveCustomer",
                   // data: "{selectedCustomer: " + JSON.stringify(<%= this.SingleCustomer %>) + "}");,
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    Onsuccess: function () {
                        alert("Success")                     
                    },
                    Onerror: function() {
                        alert("Failure");
                    },
                });
        });--%>

        <%--$("[id$=chkBox_<%= this.Count %>]").click(function () {
            if ($(this).is(":checked")) {
                $("[id*=customerHeader_]").toggle();
                $("[id*=customerHeader_<%= this.Count %>]").toggle();

                $.ajax({
                    type: "POST",
                    url: "ServiceCenter.aspx/SetActiveCustomer",
                    data: "{setAs: active, countID:" + <%= this.Count %> + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        alert('SetSession executed.');
                    }
                });
            } else {
                $("[id*=customerHeader_]").toggle();
                $("[id*=customerHeader_<%= this.Count %>]").toggle();

                $.ajax({
                    type: "POST",
                    url: "ServiceCenter.aspx/SetActiveCustomer",
                    data: "{setAs: active, countID:" + <%= this.Count %> + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        alert('SetSession executed.');
                    }
                });
            }
        });--%>
    });

</script>

<asp:Table ID="tblSingleCustomerTableHeader" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableHeader" runat="server">
        <asp:TableHeaderCell runat="server" ID="ExpandCurrentCustomer" Width="2%">
            <asp:Button ID="btnExpload" runat="server" Text="+" OnClientClick="return false;" />
        </asp:TableHeaderCell>
        <asp:TableHeaderCell runat="server" ID="CustomerSequence" Font-Bold="true" Font-Underline="true" Font-Size="Large" HorizontalAlign="Left" />
        <asp:TableHeaderCell Font-Bold="true" Text="Name:" HorizontalAlign="Left" Style="text-align: right" />
        <asp:TableHeaderCell runat="server" ID="thcCustomerName" HorizontalAlign="Left" Style="text-align: left" />
        <asp:TableHeaderCell HorizontalAlign="Right" Text="Select As Active:" />
        <asp:TableHeaderCell HorizontalAlign="Right">
            <asp:Button ID="btnSelectCustomer" runat="server" Text="Button" OnClientClick="return false;"/>
        </asp:TableHeaderCell>
        <asp:TableHeaderCell HorizontalAlign="Right" />
    </asp:TableHeaderRow>
</asp:Table>



