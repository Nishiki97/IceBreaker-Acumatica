<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="IB401000.aspx.cs" Inherits="Page_IB401000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.IBInventoryStock"
        PrimaryView="Filter">
        <CallbackCommands>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Filter" Width="100%">
        <Template>
            <px:PXLayoutRule ControlSize="M" LabelsWidth="M" ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
            <px:PXSelector runat="server" ID="PXSelector1" DataField="PartID" CommitChanges="true" AutoRefresh="true"></px:PXSelector>
            <px:PXSelector runat="server" ID="CstPXSelector1" DataField="WarehouseID" CommitChanges="true" AutoRefresh="true"></px:PXSelector>
            <px:PXSelector runat="server" ID="CstPXSelector4" DataField="LocationID" CommitChanges="true" AutoRefresh="true"></px:PXSelector>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid SyncPosition="True" ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Details" AllowAutoHide="false" OnRowDataBound ="INStatusRecords_RowDataBound">
        <Levels>
            <px:PXGridLevel DataMember="INStatusRecords">
                <Columns>
                    <px:PXGridColumn DataField="Inventoryid" Width="70"></px:PXGridColumn>
                    <px:PXGridColumn DataField="PartID" Width="70"></px:PXGridColumn>
                    <px:PXGridColumn DataField="WarehouseID" Width="140"></px:PXGridColumn>
                    <px:PXGridColumn DataField="LocationID" Width="72" CommitChanges="True"></px:PXGridColumn>
                    <px:PXGridColumn DataField="Qty" DataType="Decimal"></px:PXGridColumn>
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150"></AutoSize>
        <ActionBar>
        </ActionBar>
    </px:PXGrid>
</asp:Content>
