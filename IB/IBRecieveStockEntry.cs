﻿using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.DAC;

namespace PX.Objects.IB
{
	public class IBRecieveStockEntry : PXGraph<IBRecieveStockEntry, NisyReceiveStock>
	{
		#region Views
		public SelectFrom<NisyReceiveStock>.View ReceiveShopOrderDetails;

		public SelectFrom<NisyInventory>.View Inventory;

		public SelectFrom<NisyProductionOrder>.View OrderDetails;

		public SelectFrom<NisyInventoryAllocation>.View InventoryAllocation;
		#endregion

		#region Event
		protected virtual void _(Events.RowPersisting<NisyReceiveStock> e)
		{
			NisyReceiveStock row = e.Row;

			NisyInventoryAllocation inventoryitem = NisyInventoryAllocation.PK.Find(this, row.PartID);
			NisyInventory inventory = NisyInventory.PK.Find(this, row.PartID, row.WarehouseID, row.LocationID);

			NisyInventory newinventory = new NisyInventory();
			newinventory.PartID = row.PartID;
			newinventory.LocationID = row.LocationID;
			newinventory.WarehouseID = row.WarehouseID;
			newinventory.Qty = row.Qty;

			switch (inventory)
			{
				case null:
					inventoryitem.QtyInHand += (int)row.Qty;
					InventoryAllocation.Update(inventoryitem);
					Inventory.Insert(newinventory);
					Inventory.Cache.Persist(PXDBOperation.Insert);
					break;
				default:
					inventoryitem.QtyInHand += (int)row.Qty;
					InventoryAllocation.Update(inventoryitem);
					inventory.Qty += row.Qty;
					Inventory.Update(inventory);
					Inventory.Cache.Persist(PXDBOperation.Update);
					break;
			}
			OrderDetails.UpdateCurrent();
		}
		#endregion
	}
}
