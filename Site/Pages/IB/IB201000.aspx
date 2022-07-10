<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="IB201000.aspx.cs" Inherits="Page_IB201000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.IBInventoryPartMaint"
        PrimaryView="InventoryParts">
        <CallbackCommands>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="InventoryParts" Width="100%" AllowAutoHide="false">
        <Template>
            <px:PXLayoutRule LabelsWidth="S" ControlSize="M" runat="server" ID="PXLayoutRule1" StartRow="True"></px:PXLayoutRule>
            <px:PXSelector runat="server" ID="CstPXSelector" DataField="Partcd"></px:PXSelector>
            <px:PXTextEdit runat="server" ID="CstPXTextEdit2" DataField="PartDescription"></px:PXTextEdit>
            <px:PXLayoutRule runat="server" ID="PXLayoutRule2" StartColumn="True" LabelsWidth="S" ControlSize="M" />
            <px:PXDropDown runat="server" ID="CstPXDropDown3" DataField="ItemType" CommitChanges="true"></px:PXDropDown>
            <px:PXDropDown runat="server" ID="PXDropDown1" DataField="PartType" CommitChanges="true"></px:PXDropDown>
            <px:PXNumberEdit runat="server" ID="CstPXNumberEdit1" DataField="Price" CommitChanges="true"></px:PXNumberEdit>
        </Template>
        <AutoSize Container="Window" Enabled="True" MinHeight="200"></AutoSize>
    </px:PXFormView>
</asp:Content>
