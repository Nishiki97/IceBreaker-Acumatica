<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" 
    CodeFile="IB101000.aspx.cs" Inherits="Page_IB101000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.IBSetupMaint"
        PrimaryView="Setup">
        <CallbackCommands>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Setup" Width="100%" AllowAutoHide="false">
        <Template>
            <px:PXLayoutRule ControlSize="SM" LabelsWidth="M" ID="PXLayoutRule1" runat="server" StartRow="True">
            </px:PXLayoutRule>
            <px:PXSelector AllowEdit="True" runat="server" ID="PXSelector2" DataField="PONumberingID" Width="300"></px:PXSelector>
            <px:PXSelector AllowEdit="True" runat="server" ID="PXSelector1" DataField="SONumberingID" Width="300"></px:PXSelector>
        </Template>
        <AutoSize Container="Window" Enabled="True" MinHeight="200" />
    </px:PXFormView>
</asp:Content>