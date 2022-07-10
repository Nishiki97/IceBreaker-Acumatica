<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="IB204000.aspx.cs" Inherits="Page_IB204000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.IBInventoryMaint"
        PrimaryView="InvenotrySummaryDetails">
        <CallbackCommands>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>

<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="InvenotrySummaryDetails" Width="100%" Height="" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule ControlSize="M" LabelsWidth="M" ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
			<px:PXSelector runat="server" ID="PXSelector1" DataField="PartID" CommitChanges="true" AutoRefresh="true"></px:PXSelector>
			<px:PXSelector runat="server" ID="CstPXSelector1" DataField="WarehouseID" CommitChanges="true" AutoRefresh="true"></px:PXSelector>
			<px:PXSelector runat="server" ID="CstPXSelector4" DataField="LocationID" CommitChanges="true" AutoRefresh="true"></px:PXSelector>
			<px:PXLayoutRule ControlSize="M" LabelsWidth="M" ID="PXLayoutRule2" runat="server" StartColumn="True"></px:PXLayoutRule>
			<px:PXNumberEdit runat="server" ID="PXNumberEdit3" DataField="Qty" CommitChanges="true" AutoRefresh="true"></px:PXNumberEdit>
			<px:PXNumberEdit runat="server" ID="PXNumberEdit2" DataField="Price" CommitChanges="true" AutoRefresh="true"></px:PXNumberEdit>
			<px:PXNumberEdit runat="server" ID="PXNumberEdit1" DataField="TotalPrice" CommitChanges="true" AutoRefresh="true" ></px:PXNumberEdit>
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" AllowAutoHide="false">
		<Items>
			<px:PXTabItem Text="Stock Details">
				<Template>
					<px:PXGrid SyncPosition="True" SkinID="Details" Width="100%" runat="server" ID="CstPXGrid5">
						<Levels>
							<px:PXGridLevel DataMember="DirectInvenotryReceiptDetails" >
								<Columns>
									<px:PXGridColumn CommitChanges="True" DataField="PartID" Width="70" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="WarehouseID" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="LocationID" Width="70" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="Qty" Width="280" ></px:PXGridColumn>
								</Columns>
								<%--<RowTemplate>
									<px:PXSegmentMask runat="server" ID="CstPXSegmentMask6" DataField="InventoryID" AutoRefresh="True" ></px:PXSegmentMask>
								</RowTemplate>--%>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" ></AutoSize>
						<Mode InitNewRow="True" ></Mode></px:PXGrid>
				</Template>
			</px:PXTabItem>
			</Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
	</px:PXTab>
</asp:Content>
