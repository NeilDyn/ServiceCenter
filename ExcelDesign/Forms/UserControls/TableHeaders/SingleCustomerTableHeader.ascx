﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleCustomerTableHeader.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.TableHeaders.SingleCustomerTableHeader" %>
<%@ Register Src="~/Forms/UserControls/TableData/SingleCustomerTableDetail.ascx" TagName="SingleCustomerTableDetail" TagPrefix="sctd" %>

<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        if (<%= this.CustomerCount %> > 1) {
            $("[id$=customerDetails_<%= this.Count %>]").hide();

            $("[id$=trInfo1_<%= this.Count %>]").hide();
            $("[id$=trInfo2_<%= this.Count %>]").hide();
            $("[id$=trInfo3_<%= this.Count %>]").hide();
        }

        $("[id$=btnExpload_<%= this.Count %>]").click(function () {
            $("[id$=customerDetails_<%= this.Count %>]").toggle();
            $("[id$=trInfo1_<%= this.Count %>]").toggle();
            $("[id$=trInfo2_<%= this.Count %>]").toggle();
            $("[id$=trInfo3_<%= this.Count %>]").toggle();

            if ($("[id$=btnExpload_<%= this.Count %>]").val() == "+") {
                $("[id$=btnExpload_<%= this.Count %>]").val("-");
            }
            else {
                $("[id$=btnExpload_<%= this.Count %>]").val("+");
            }
        });

        $("[id$=btnSelectCustomer_<%= this.Count %>]").click(function () {
            var custData = <%= this.Count %>;

            $.ajax({
                type: "POST",
                url: "ServiceCenter.aspx/SetActiveCustomer",
                data: JSON.stringify({ custID: custData }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (returned) {
                    if (returned.d == "success") {
                        __doPostBack('[id$=btnReload_<%= this.Count %>', '');
                    } else {
                        alert(returned.d);
                    }
                },
                error: function (xhr, status, text) {
                    console.log(xhr.status);
                    console.log(xhr.text);
                    console.log(xhr.responseText);
                },
            });
        });

    });

</script>

<asp:Table ID="tblSingleCustomerTableHeader" runat="server" Width="100%" Height="100%">
    <asp:TableHeaderRow TableSection="TableHeader" runat="server" ID="customerColumns">
        <asp:TableHeaderCell runat="server" ID="ExpandCurrentCustomer" Width="5%">
            <asp:Button ID="btnExpload" runat="server" Text="+" OnClientClick="return false;" />
        </asp:TableHeaderCell>
        <asp:TableHeaderCell runat="server" ID="CustomerSequence" Font-Bold="true" Font-Underline="true" Font-Size="Large" HorizontalAlign="Left"/>
        <asp:TableHeaderCell Font-Bold="true" Text="Name:" HorizontalAlign="Left" Style="text-align: right"/>
        <asp:TableHeaderCell runat="server" ID="thcCustomerName" HorizontalAlign="Left" Style="text-align: left"/>
        <asp:TableHeaderCell Text="Address 1:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableHeaderCell runat="server" ID="tcAddress1" HorizontalAlign="Left" Style="text-align: left"/>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
        <asp:TableHeaderCell HorizontalAlign="Right" ID="lblSelectActive" Text="Select As Active:"/>
        <asp:TableHeaderCell HorizontalAlign="Right">
            <asp:Button ID="btnSelectCustomer" runat="server" Text="Set Active" OnClientClick="return false;" />
        </asp:TableHeaderCell>
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify" ID="trInfo1">
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
        <asp:TableHeaderCell Text="Address 2:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableHeaderCell runat="server" ID="tcAddress2" />
        <asp:TableHeaderCell Text="Zip:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableHeaderCell runat="server" ID="tcZip" style="text-align: left"/>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />

    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify" Id="trInfo2">
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
        <asp:TableHeaderCell Text="City:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableHeaderCell runat="server" ID="tcCity"/>
        <asp:TableHeaderCell Text="State:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableHeaderCell runat="server" ID="tcState"/>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
    </asp:TableHeaderRow>
    <asp:TableHeaderRow TableSection="TableBody" HorizontalAlign="Justify" ID="trInfo3">
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
        <asp:TableHeaderCell Text="Ship to Contact:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableHeaderCell runat="server" ID="tcShiptoContact"/>
        <asp:TableHeaderCell Text="Country:" Font-Bold="true" HorizontalAlign="Left" style="text-align: right"/>
        <asp:TableHeaderCell runat="server" ID="tcCountry"/>
        <asp:TableHeaderCell />
        <asp:TableHeaderCell />
    </asp:TableHeaderRow>
</asp:Table>



