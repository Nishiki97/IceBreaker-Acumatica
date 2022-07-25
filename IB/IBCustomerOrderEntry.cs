using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.DAC;
using PX.Objects.IB.Descriptor;

namespace PX.Objects.IB
{
	public class IBCustomerOrderEntry : PXGraph<IBCustomerOrderEntry, NisyCustomerOrder>
	{
		#region Views
		public SelectFrom<NisyCustomerOrder>.View CustomerOrders;

		public SelectFrom<NisyCustomerOrderPartDetails>
			.Where<NisyCustomerOrderPartDetails.customerOrderNbr.IsEqual<NisyCustomerOrder.customerOrderNbr.FromCurrent>>
			.View CustomerOrderPartDetails;

		public SelectFrom<NisyCustomerOrderNoPartDetails>
			.Where<NisyCustomerOrderNoPartDetails.customerOrderNbr.IsEqual<NisyCustomerOrder.customerOrderNbr.FromCurrent>>
			.View CustomerOrderNoPartDetails;

		public SelectFrom<NisyInventoryAllocation>.View InventoryAllocation;

		public SelectFrom<NisyInventory>.View Inventory;

		public PXSetup<NisySetup> NumberingSeqSetup;
		#endregion

		#region Graph constructor
		public IBCustomerOrderEntry()
		{
			NisySetup setup = NumberingSeqSetup.Current;
		}
		#endregion

		#region Actions
		public PXAction<NisyCustomerOrder> ReleaseOrder;
		[PXButton]
		[PXUIField(DisplayName = "Release", Enabled = true)]
		protected virtual void releaseOrder()
		{
			CustomerOrders.Current.Status = CustomerOrderStatus.COReleased;
			CustomerOrders.UpdateCurrent();
			Actions.PressSave();
		}

		public PXAction<NisyCustomerOrder> CancelOrder;
		[PXButton]
		[PXUIField(DisplayName = "Cancel", Enabled = true)]
		protected virtual void cancelOrder()
		{
			if (CheckForDeliveredStatusOfCustomerOrder() && CustomerOrders.Current.Status.Trim() == CustomerOrderStatus.COReleased || CustomerOrders.Current.Status.Trim() == CustomerOrderStatus.COPlanned)
			{
				CustomerOrders.Current.Status = CustomerOrderStatus.COCancelled;
				CustomerOrders.UpdateCurrent();
			}
			ChangeToCancelledStatusOfCustomerOrder();

			Actions.PressSave();
		}

		public PXAction<NisyCustomerOrder> DeliverOrder;
		[PXButton]
		[PXUIField(DisplayName = "Deliver", Enabled = true)]
		protected virtual void deliverOrder()
		{
			if (CustomerOrders.Current.Status.Trim() == CustomerOrderStatus.COReleased)
			{
				ChangeStatusToDelivered();
			}
		}
		#endregion

		#region Events
		protected virtual void _(Events.RowPersisting<NisyCustomerOrder> e)
		{
			NisyCustomerOrder row = e.Row;

			if (row.Status == CustomerOrderStatus.Not_Set)
			{
				row.Status = CustomerOrderStatus.COPlanned;
				CustomerOrders.Update(row);
			}
		}

		protected void _(Events.RowSelected<NisyCustomerOrder> e)
		{
			NisyCustomerOrder row = e.Row;

			if (row != null)
			{
				if (row.Status.Trim() == CustomerOrderStatus.COPlanned)
				{
					ReleaseOrder.SetEnabled(true);
					CancelOrder.SetEnabled(true);
					DeliverOrder.SetEnabled(false);
				}
				if (row.Status.Trim() == CustomerOrderStatus.Not_Set)
				{
					ReleaseOrder.SetEnabled(false);
					CancelOrder.SetEnabled(false);
					DeliverOrder.SetEnabled(false);
				}
				if (row.Status.Trim() == CustomerOrderStatus.COReleased)
				{
					ReleaseOrder.SetEnabled(false);
					DeliverOrder.SetEnabled(true);
				}
				if (row.Status.Trim() == CustomerOrderStatus.COCancelled)
				{
					ReleaseOrder.SetEnabled(false);
					CancelOrder.SetEnabled(false);
					DeliverOrder.SetEnabled(false);
				}
			}
		}

		protected void _(Events.RowSelected<NisyCustomerOrderPartDetails> e)
		{
			NisyCustomerOrderPartDetails row = e.Row;

			if (row != null)
			{
				if (row.Status.Trim() == CustomerOrderItemDetailsStatus.COItemDelivered)
				{
					DeliverOrder.SetEnabled(false);
					CancelOrder.SetEnabled(false);
				}
				if (!CheckForDeliveredStatusOfCustomerOrder() && row.Status.Trim() != CustomerOrderItemDetailsStatus.COItemRequired)
				{
					CustomerOrders.Current.Status = CustomerOrderStatus.COClosed;
					CustomerOrders.UpdateCurrent();
					// Acuminator disable once PX1043 SavingChangesInEventHandlers [Justification]
					CustomerOrders.Cache.Persist(PXDBOperation.Update);
				}
			}
		}

		protected void _(Events.FieldUpdated<NisyCustomerOrderPartDetails, NisyCustomerOrderPartDetails.qty> e)
		{
			NisyCustomerOrderPartDetails row = e.Row;

			if (row != null)
			{
				NisyInventoryAllocation inventoryitem = PXSelect<NisyInventoryAllocation,
				Where<NisyInventoryAllocation.partID, Equal<Required<NisyCustomerOrderPartDetails.partID
					>>>>.Select(this, row.PartID);

				if (row.Qty > inventoryitem.AvailableForSale)
				{
					e.Cache.RaiseExceptionHandling<NisyCustomerOrderPartDetails.qty>(row, row.Qty, new PXException(Messages.NoSufficientQtyMessage));
					// Acuminator disable once PX1070 UiPresentationLogicInEventHandlers [Justification]
					Save.SetEnabled(false);
				}
			}
		}

		protected void _(Events.FieldUpdated<NisyCustomerOrderPartDetails, NisyCustomerOrderPartDetails.partID> e)
		{
			NisyCustomerOrderPartDetails row = e.Row;

			if (row.PartID == null) return;

			NisyPart part = NisyPart.PK.Find(this, row.PartID);

			row.PartDescription = part.PartDescription;
			row.Price = part.Price;
		}

		protected void _(Events.FieldUpdated<NisyCustomerOrderNoPartDetails, NisyCustomerOrderNoPartDetails.noPartID> e)
		{
			NisyCustomerOrderNoPartDetails row = e.Row;

			if (row.NoPartID == null) return;

			NisyPart part = NisyPart.PK.Find(this, row.NoPartID);

			row.PartDescription = part.PartDescription;
			row.Price = part.Price;
		}

		protected void _(Events.FieldUpdated<NisyCustomerOrder, NisyCustomerOrder.customerID> e)
		{
			NisyCustomerOrder row = e.Row;

			if (row.CustomerID == null) return;

			NisyCustomer customer = NisyCustomer.PK.Find(this, row.CustomerID);

			row.CustomerAddress = customer.Address;
		}
		#endregion

		#region Methods
		public void ChangeStatusToDelivered()
		{
			foreach (NisyCustomerOrderPartDetails item in CustomerOrderPartDetails.Select())
			{
				NisyInventoryAllocation inventoryitem = PXSelect<NisyInventoryAllocation,
				Where<NisyInventoryAllocation.partID, Equal<Required<NisyCustomerOrderPartDetails.partID
					>>>>.Select(this, item.PartID);

				PXResultset<NisyInventory> inventory = PXSelect<NisyInventory,
				Where<NisyInventory.partID, Equal<Required<NisyCustomerOrderPartDetails.partID>>>, OrderBy<Desc<NisyInventory.qty>>
				>.Select(this, item.PartID);

				foreach (NisyInventory inventoryRes in inventory)
				{
					if ((int)inventoryRes.Qty >= item.Qty)
					{
						inventoryitem.AvailableForSale -= (int)item.Qty;
						inventoryitem.QtyInHand -= (int)item.Qty;
						inventoryRes.Qty -= item.Qty;

						InventoryAllocation.Update(inventoryitem);
						Inventory.Update(inventoryRes);

						Actions.PressSave();

						break;
					}
					else if ((int)inventoryRes.Qty < item.Qty) //
					{
						inventoryRes.Qty -= inventoryRes.Qty;
						inventoryitem.QtyInHand -= (int)item.Qty;
						inventoryitem.AvailableForSale -= (int)item.Qty;

						item.Qty = (int)(item.Qty - inventoryRes.Qty);

						InventoryAllocation.Update(inventoryitem);
						Inventory.Update(inventoryRes);
						Actions.PressSave();

						if (item.Qty != 0)
						{
							continue;
						}
						else
						{
							break;
						}

					}
				}

				item.Status = CustomerOrderItemDetailsStatus.COItemDelivered;
				CustomerOrderPartDetails.Update(item);
			}
			Actions.PressSave();
		}

		public bool CheckForDeliveredStatusOfCustomerOrder()
		{
			bool isNotDelivered = false;

			foreach (NisyCustomerOrderPartDetails item in CustomerOrderPartDetails.Select())
			{
				if (item.Status != CustomerOrderItemDetailsStatus.COItemDelivered)
				{
					return isNotDelivered = true;
				}
			}
			return isNotDelivered;
		}

		public void ChangeToCancelledStatusOfCustomerOrder()
		{
			foreach (NisyCustomerOrderPartDetails item in CustomerOrderPartDetails.Select())
			{
				item.Status = CustomerOrderItemDetailsStatus.COItemCancelled;
				CustomerOrderPartDetails.Update(item);
				Actions.PressSave();
			}
		}
		#endregion
	}
}
