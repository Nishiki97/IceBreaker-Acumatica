<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="IB206000.aspx.cs" Inherits="Page_IB206000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.IBProductionOrderMaint"
        PrimaryView="OrderDetails">
        <CallbackCommands>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>

<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="OrderDetails" Width="100%" Height="" AllowAutoHide="false">
        <Template>
            <px:PXLayoutRule ControlSize="M" LabelsWidth="S" ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
            <px:PXSelector runat="server" ID="Selector1" DataField="OrderID" CommitChanges="true"></px:PXSelector>
            <px:PXDateTimeEdit runat="server" ID="PXDateTimeEdit1" DataField="ProductionOrderDate" CommitChanges="true"></px:PXDateTimeEdit>
            <px:PXDateTimeEdit runat="server" ID="PXDateTimeEdit2" DataField="RequestedDate" CommitChanges="true"></px:PXDateTimeEdit>
            <px:PXLayoutRule ControlSize="M" LabelsWidth="S" ID="PXLayoutRule2" runat="server" StartColumn="True"></px:PXLayoutRule>
            <px:PXSelector runat="server" ID="PXSelector2" DataField="ProductNumber" CommitChanges="true"></px:PXSelector>
            <px:PXTextEdit runat="server" ID="CstPXNumberEdit2" DataField="LotSize" CommitChanges="true"></px:PXTextEdit>
            <px:PXTextEdit runat="server" ID="PXTextEdit2" DataField="ProductionOrderStatus" CommitChanges="true" Enabled="false"></px:PXTextEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" AllowAutoHide="false">
        <Items>
            <px:PXTabItem Text="Product Structure Details">
                <Template>
                    <px:PXGrid SyncPosition="True" SkinID="Details" Width="100%" runat="server" ID="CstPXGrid5">
                        <Levels>
                            <px:PXGridLevel DataMember="BOMDetails">
                                <Columns>
                                    <px:PXGridColumn CommitChanges="True" DataField="PartID" Width="70"></px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" DataField="Qty" Width="280"></px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" DataField="TotalQty" Width="280"></px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" Type="CheckBox" DataField="Available" Width="20"></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True"></AutoSize>
                        <Mode InitNewRow="True"></Mode>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="150"></AutoSize>
    </px:PXTab>
</asp:Content>