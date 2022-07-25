<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false"
	CodeFile="IB205000.aspx.cs" Inherits="Page_IB205000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.nisyProductStructureMaint"
        PrimaryView="PartDetails"
        >
	</px:PXDataSource>
</asp:Content>

<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="PartDetails" Width="100%" Height="" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule ControlSize="M" LabelsWidth="M" ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
			<px:PXSelector runat="server" ID="CstPXSelector1" DataField="PartCD" CommitChanges="True"  ></px:PXSelector>
			<px:PXTextEdit runat="server" ID="CstPXNumberEdit2" DataField="PartDescription" CommitChanges="True" ></px:PXTextEdit></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" AllowAutoHide="false">
		<Items>
			<px:PXTabItem Text="Parts">
				<Template>
					<px:PXGrid SyncPosition="True" SkinID="Details" Width="100%" runat="server" ID="CstPXGrid5">
						<Levels>
							<px:PXGridLevel DataMember="ProductStructureDetails" >
								<Columns>
									<px:PXGridColumn CommitChanges="True" DataField="PartID" Width="70" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="Qty" Width="280"></px:PXGridColumn>
								</Columns>
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