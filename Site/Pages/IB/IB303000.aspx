<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="IB303000.aspx.cs" Inherits="Page_IB303000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.IBRecieveStockEntry"
        PrimaryView="ReceiveShopOrderDetails">
        <CallbackCommands>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="ReceiveShopOrderDetails" Width="100%" AllowAutoHide="false">
        <Template>
            <px:PXLayoutRule LabelsWidth="S" ControlSize="M" runat="server" ID="PXLayoutRule1" StartRow="True"></px:PXLayoutRule>
            <px:PXSelector runat="server" ID="CstPXSelector" DataField="WarehouseID" CommitChanges="true" AutoRefresh="true"></px:PXSelector>
            <px:PXSelector runat="server" ID="PXSelector1" DataField="LocationID" CommitChanges="true" AutoRefresh="true"></px:PXSelector>
            <px:PXTextEdit runat="server" ID="CstPXTextEdit2" DataField="Qty"></px:PXTextEdit>
        </Template>
        <AutoSize Container="Window" Enabled="True" MinHeight="200"></AutoSize>
    </px:PXFormView>
</asp:Content>
