<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" 
	CodeFile="IB301000.aspx.cs" Inherits="Page_IB301000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.IBDirectInventoryReceiptEntry"
        PrimaryView="DirectInvenotryReceiptDetails"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="DirectInvenotryReceiptDetails" Width="100%" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule LabelsWidth="S" ControlSize="M" runat="server" ID="PXLayoutRule1" StartRow="True" ></px:PXLayoutRule>
			<px:PXSelector runat="server" ID="PXSelector1" DataField="PartID" CommitChanges="true" ></px:PXSelector>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit1" DataField="Qty" CommitChanges="true"></px:PXTextEdit>
			<px:PXLayoutRule LabelsWidth="S" ControlSize="M" runat="server" ID="PXLayoutRule2" StartColumn="True" ></px:PXLayoutRule>
			<px:PXSelector runat="server" ID="edWarehouseID" DataField="WarehouseID" DataSourceID="ds" CommitChanges="true"/>
			<px:PXSelector runat="server" ID="edLocationID" AutoRefresh="True" DataField="LocationID" CommitChanges="true" ></px:PXSelector>
		</Template>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" ></AutoSize>
	</px:PXFormView>
</asp:Content>