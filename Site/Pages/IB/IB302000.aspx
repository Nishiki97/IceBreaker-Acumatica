<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" 
	ValidateRequest="false" CodeFile="IB302000.aspx.cs" Inherits="Page_IB302000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.IBCustomerOrderEntry"
        PrimaryView="CustomerOrders"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>

<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="CustomerOrders" Width="100%" Height="" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule ControlSize="M" LabelsWidth="M" ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
			<px:PXTextEdit runat="server" ID="PXTextEdit1" DataField="CustomerOrderNbr" CommitChanges="true" ></px:PXTextEdit>
			<px:PXDateTimeEdit runat="server" ID="PXDateTimeEdit1" DataField="OrderDate" ></px:PXDateTimeEdit>
			<px:PXSelector runat="server" ID="PXSelector2" DataField="CustomerID" CommitChanges="true"></px:PXSelector>
			<px:PXTextEdit runat="server" ID="PXTextEdit2" DataField="CustomerAddress" ></px:PXTextEdit>
			<px:PXLayoutRule ControlSize="M" LabelsWidth="M" ID="PXLayoutRule2" runat="server" StartColumn="True"></px:PXLayoutRule>
			<px:PXTextEdit runat="server" ID="PXTextEdit3" DataField="Status"></px:PXTextEdit>
            <px:PXTextEdit runat="server" ID="PXTextEdit4" DataField="TotalPrice" ></px:PXTextEdit>
		</Template>
	</px:PXFormView>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="PXTab1" runat="server" Width="100%" Height="150px" DataSourceID="ds" AllowAutoHide="false">
        <Items>
            <px:PXTabItem Text="Part Details">
                <Template>
                    <px:PXGrid SyncPosition="True" Width="100%" SkinID="Details" runat="server" ID="CstPXGrid6">
                        <Levels>
                            <px:PXGridLevel DataMember="CustomerOrderPartDetails">
                                <Columns>
                                   <px:PXGridColumn CommitChanges="True" DataField="PartID" Width="70" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="PartDescription" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="Qty" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="Price" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="TotalPrice" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="Status" Width="280" ></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True"></AutoSize>
                        <Mode InitNewRow="True"></Mode>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="No Part Details">
                <Template>
                    <px:PXGrid SyncPosition="True" Width="100%" SkinID="Details" runat="server" ID="CstPXGrid5">
                        <Levels>
                            <px:PXGridLevel DataMember="CustomerOrderNoPartDetails">
                                <Columns>
                                    <px:PXGridColumn CommitChanges="True" DataField="NoPartID" Width="70" ></px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" DataField="PartID" Width="70" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="PartDescription" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="Qty" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="Price" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="TotalPrice" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="Status" Width="280" ></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True"></AutoSize>
                        <Mode InitNewRow="True"></Mode>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXTab>
</asp:Content>