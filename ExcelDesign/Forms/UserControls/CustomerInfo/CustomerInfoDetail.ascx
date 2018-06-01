<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerInfoDetail.ascx.cs" Inherits="ExcelDesign.Forms.UserControls.CustomerInfo" %>
<link href="../../../css/mainpage.css" rel="stylesheet" type="text/css" />

<div id="Details" runat="server">
    <div id="DetailedCustomerCaptions" class="detailedCustomerCaption">
        <asp:Label ID="lblNameCaption" runat="server" Text="Name:" />
        <br />
        <asp:Label ID="lblAddress1Caption" runat="server" Text="Address 1:" />
        <br />
        <asp:Label ID="lblAddress2Caption" runat="server" Text="Address 2:" />
        <br />
        <asp:Label ID="lblShipToContactCaption" runat="server" Text="Ship to Contact:" />
        <br />
        <asp:Label ID="lblCityCaption" runat="server" Text="City:" />
        <br />
        <asp:Label ID="lblZipCaption" runat="server" Text="Zip:" />
    </div>
    <div id="DetailedCustomerInfo" class="detailedCustomerInfo">
        <asp:Label ID="lblName" runat="server" />
        <br />
        <asp:Label ID="lblAddress1" runat="server" />
        <br />
        <asp:Label ID="lblAddress2" runat="server" />
        <br />
        <asp:Label ID="lblShipToContact" runat="server"/>
        <br />
        <asp:Label ID="lblCity" runat="server" />
        <br />
        <asp:Label ID="lblZip" runat="server" />
    </div>
    <div id="DetailedCustomerCaptionsCon" class="detailedCustomerCaptionCon">
        <asp:Label ID="lblStateCaption" runat="server" Text="State:" />
        <br />
        <asp:Label ID="lblCountryCaption" runat="server" Text="Country:" />
    </div>
    <div id="DetailedCustomerInfoCon" class="detailedCustomerInfoCon">
        <asp:Label ID="lblState" runat="server" />
        <br />
        <asp:Label ID="lblCountry" runat="server" />
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />       
</div>    
