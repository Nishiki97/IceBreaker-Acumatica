using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.DAC;

namespace PX.Objects.IB
{
	public class IBDirectInventoryReceiptEntry : PXGraph<IBDirectInventoryReceiptEntry, NisyDirectInventoryReceipt>
	{
		#region Views
		public SelectFrom<NisyDirectInventoryReceipt>.View DirectInvenotryReceiptDetails;

		public SelectFrom<NisyInventory>.View InventoryDetails;

		public SelectFrom<NisyInventoryAllocation>.View InventoryAllocationDetails;
		#endregion

		#region Actions
		public PXAction<NisyDirectInventoryReceipt> ReleaseDirectInventoryReceipt;
		[PXButton]
		[PXUIField(DisplayName = "Release Inventory Receipt", Enabled = true)]
		protected virtual void releaseDirectInventoryReceipt()
		{
			NisyInventory newinventorystatus = new NisyInventory();

			newinventorystatus.PartID = DirectInvenotryReceiptDetails.Current.PartID;
			newinventorystatus.WarehouseID = DirectInvenotryReceiptDetails.Current.WarehouseID;
			newinventorystatus.LocationID = DirectInvenotryReceiptDetails.Current.LocationID;

			NisyInventory check = CheckExistingInventory();
			NisyInventoryAllocation newallocation = new NisyInventoryAllocation();
			NisyInventoryAllocation checkallocation = CheckExistingallocation();

			newallocation.PartID = DirectInvenotryReceiptDetails.Current.PartID;

			NisyInventory NewqtyInHand = PXSelectGroupBy<NisyInventory,
			Where<NisyInventory.partid, Equal<Required<NisyDirectInventoryReceipt.partid>>>,
			Aggregate<GroupBy<NisyInventory.partid, Sum<NisyInventory.qty>>>>.Select(this, DirectInvenotryReceiptDetails.Current.PartID);

			switch (check)
			{
				case null:
					newinventorystatus.Qty = DirectInvenotryReceiptDetails.Current.Qty;
					InventoryDetails.Insert(newinventorystatus);
					break;
				default:
					newinventorystatus.Qty = check.Qty + DirectInvenotryReceiptDetails.Current.Qty;
					InventoryDetails.Update(newinventorystatus);
					break;
			}

			switch (checkallocation)
			{
				case null:
					newallocation.QtyInHand = DirectInvenotryReceiptDetails.Current.Qty;
					InventoryAllocationDetails.Insert(newallocation);
					break;
				default:
					newallocation.QtyInHand = (int?)(DirectInvenotryReceiptDetails.Current.Qty + NewqtyInHand.Qty);
					newallocation.AvailableForSale = 0;
					newallocation.ReservedQty = 0;
					InventoryAllocationDetails.Update(newallocation);
					break;
			}

			NisyDirectInventoryReceipt current = new NisyDirectInventoryReceipt();
			current = DirectInvenotryReceiptDetails.Current;

			current.IsReleased = true;
			DirectInvenotryReceiptDetails.Update(current);
			Actions.PressSave();
		}
		#endregion

		#region Events
		protected virtual void _(Events.RowSelected<NisyDirectInventoryReceipt> e)
		{
			NisyDirectInventoryReceipt row = e.Row;

			if (DirectInvenotryReceiptDetails.Current.IsReleased == true) { ReleaseDirectInventoryReceipt.SetEnabled(false); }
		}
		protected virtual void _(Events.FieldUpdated<NisyDirectInventoryReceipt, NisyDirectInventoryReceipt.warehouseid> e)
		{
			e.Cache.SetValue<NisyDirectInventoryReceipt.locationid>(e.Row, null);
		}
		#endregion

		#region Methods
		private NisyInventory CheckExistingInventory()
		{
			NisyInventory newstatus = NisyInventory.PK.Find(this, DirectInvenotryReceiptDetails.Current.PartID, DirectInvenotryReceiptDetails.Current.WarehouseID, DirectInvenotryReceiptDetails.Current.LocationID);
			return newstatus;
		}
		private NisyInventoryAllocation CheckExistingallocation()
		{
			NisyInventoryAllocation newallocation = NisyInventoryAllocation.PK.Find(this, DirectInvenotryReceiptDetails.Current.PartID);
			return newallocation;
		}
		#endregion
	}
}
