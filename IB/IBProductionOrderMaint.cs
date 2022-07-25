using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.DAC;
using PX.Objects.IB.Descriptor;
using System.Collections;
using System.Linq;

namespace PX.Objects.IB
{
	public class IBProductionOrderMaint : PXGraph<IBProductionOrderMaint, NisyProductionOrder>
	{
		#region Views
		public SelectFrom<NisyProductionOrder>.View OrderDetails;

		public SelectFrom<NisyInventoryAllocation>.View InventoryAllocation;

		public SelectFrom<NisyInventory>.View Inventory;

		public SelectFrom<NisyProductStructure>
			.Where<NisyProductStructure.productID
			.IsEqual<NisyProductionOrder.productNumber.FromCurrent>>.View BOMDetails;

		public PXSetup<NisySetup> NumberingSeqSetup;
		#endregion

		#region Graph constructor
		public IBProductionOrderMaint()
		{
			NisySetup setup = NumberingSeqSetup.Current;
		}
		#endregion

		#region Actions
		public PXAction<NisyProductionOrder> Release;
		[PXButton]
		[PXUIField(DisplayName = "Release")]
		protected virtual IEnumerable release(PXAdapter adapter) => adapter.Get();

		public PXAction<NisyProductionOrder> IssueMaterial;
		[PXButton]
		[PXUIField(DisplayName = "Issue Material")]
		protected virtual IEnumerable issueMaterial(PXAdapter adapter)
		{
			var bomItems = BOMDetails.Select();

			PXLongOperation.StartOperation(this, delegate ()
			{
				var productionordermaint = PXGraph.CreateInstance<IBProductionOrderMaint>();
				productionordermaint.ReduceStock(bomItems);
			});

			return adapter.Get();
		}

		public PXAction<NisyProductionOrder> ReceiveShopOrder;
		[PXButton(OnClosingPopup = PXSpecialButtonType.Refresh, ClosePopup = true)]
		[PXUIField(DisplayName = "Receive Shop Order")]
		protected virtual void receiveShopOrder()
		{
			if (OrderDetails.Current != null)
			{
				var dialogBoxGraph = CreateInstance<IBRecieveStockEntry>();

				dialogBoxGraph.ReceiveShopOrderDetails.Cache.Clear();
				dialogBoxGraph.ReceiveShopOrderDetails.Current = SelectFrom<NisyReceiveStock>.View.Select(dialogBoxGraph);
				dialogBoxGraph.ReceiveShopOrderDetails.Current.Qty = OrderDetails.Current.LotSize;
				dialogBoxGraph.ReceiveShopOrderDetails.Current.PartID = OrderDetails.Current.ProductNumber;
				dialogBoxGraph.ReceiveShopOrderDetails.Current.WarehouseID = null;
				dialogBoxGraph.ReceiveShopOrderDetails.Current.LocationID = null;

				dialogBoxGraph.ReceiveShopOrderDetails.UpdateCurrent();

				dialogBoxGraph.Inventory.Current = Inventory.Current;

				dialogBoxGraph.OrderDetails.Current = OrderDetails.Current;
				Save.Press();

				throw new PXPopupRedirectException(dialogBoxGraph, "Receive Shop Order");
			}

		}
		#endregion

		#region WorlflowEventHandler
		public PXWorkflowEventHandler<NisyProductionOrder, NisyReceiveStock> OnSaveReceiveStock;
		#endregion

		#region Events
		protected virtual void _(Events.FieldUpdated<NisyProductionOrder, NisyProductionOrder.lotSize> e)
		{
			foreach (NisyProductStructure item in BOMDetails.Select())
			{
				NisyProductStructure copy = (NisyProductStructure)BOMDetails.Cache.CreateCopy(item);
				object totalQuantity = e.Row.LotSize * copy.Qty;

				BOMDetails.Cache.RaiseFieldUpdating<NisyProductStructure.totalQty>(copy, ref totalQuantity);

				copy.TotalQty = (int)totalQuantity;
				BOMDetails.Update(copy);
			}

			SetQuantityAvailability();
		}

		protected virtual void _(Events.FieldUpdated<NisyProductStructure, NisyProductStructure.totalQty> e)
		{
			NisyInventoryAllocation inventoryitem = PXSelect<NisyInventoryAllocation,
				Where<NisyInventoryAllocation.partID, Equal<Required<NisyProductStructure.partID
					>>>>.Select(this, e.Row.PartID);

			if (e.Row.TotalQty > inventoryitem.QtyInHand)
			{
				// Acuminator disable once PX1050 HardcodedStringInLocalizationMethod [Justification]
				BOMDetails.Cache.RaiseExceptionHandling("TotalQuantity", e.Row, e.Row.TotalQty, new PXException("no quantity", typeof(NisyProductStructure.qty)));
				// Acuminator disable once PX1070 UiPresentationLogicInEventHandlers [Justification]
				Save.SetEnabled(false);
			}
			else if (inventoryitem.QtyInHand == null)
			{
				// Acuminator disable once PX1050 HardcodedStringInLocalizationMethod [Justification]
				BOMDetails.Cache.RaiseExceptionHandling("TotalQuantity", e.Row, e.Row.TotalQty, new PXException("no quantity in hand"));
			}
		}

		protected virtual void _(Events.RowSelected<NisyProductionOrder> e)
		{
			NisyProductionOrder row = e.Row;

			if (row.ProductionOrderStatus.Trim() == ProductionOrderStatuses.Released)
			{
				Save.SetEnabled(false);
			}
			if (row.ProductionOrderStatus.Trim() == ProductionOrderStatuses.Reserved)
			{
				Delete.SetEnabled(false);
				Save.SetEnabled(false);
			}
			if (row.ProductionOrderStatus.Trim() == ProductionOrderStatuses.Closed || OrderDetails.Current.ProductionOrderStatus == ProductionOrderStatuses.Closed)
			{
				Delete.SetEnabled(false);
				Save.SetEnabled(false);
			}
		}

		protected virtual void _(Events.RowSelected<NisyProductStructure> e)
		{
			NisyProductionOrder copy = OrderDetails.Current;

			if (copy.LotSize != null)
			{
				foreach (NisyProductStructure item in BOMDetails.Select())
				{
					object TotalQuantity = item.Qty * copy.LotSize;
					item.TotalQty = (int)TotalQuantity;
				}
			}
			else
			{
				foreach (NisyProductStructure item in BOMDetails.Select())
				{
					item.TotalQty = 0;
				}
			}
		}
		#endregion

		public void ReduceStock(PXResultset<NisyProductStructure> productStructure)
		{
			foreach (NisyProductStructure bomitem in productStructure)
			{
				NisyInventoryAllocation inventoryitem = PXSelect<NisyInventoryAllocation,
					Where<NisyInventoryAllocation.partID, Equal<Required<NisyProductStructure.partID
						>>>>.Select(this, bomitem.PartID);

				PXResultset<NisyInventory> locations = PXSelect<NisyInventory,
					Where<NisyInventory.partID, Equal<Required<NisyProductStructure.partID>>>, OrderBy<Desc<NisyInventory.qty>>
					>.Select(this, bomitem.PartID);

				foreach (NisyInventory location in locations)
				{
					if ((int)location.Qty >= bomitem.TotalQty)
					{
						inventoryitem.QtyInHand -= bomitem.TotalQty;
						inventoryitem.ReservedQty += bomitem.TotalQty;
						location.Qty -= bomitem.TotalQty;

						InventoryAllocation.Update(inventoryitem);
						Inventory.Update(location);

						Actions.PressSave();

						break;
					}
					else if ((int)location.Qty < bomitem.TotalQty)
					{
						location.Qty -= location.Qty;
						inventoryitem.QtyInHand -= (int)location.Qty;
						inventoryitem.ReservedQty += (int)location.Qty;

						bomitem.TotalQty = (int)(bomitem.TotalQty - location.Qty);

						InventoryAllocation.Update(inventoryitem);
						Inventory.Update(location);
						Actions.PressSave();

						if (bomitem.TotalQty != 0)
						{
							continue;
						}
						else
						{
							break;
						}

					}
				}
			}
		}

		public void SetQuantityAvailability()
		{
			var result = new SelectFrom<NisyProductStructure>
							.InnerJoin<NisyInventory>.On<NisyInventory.partID.IsEqual<NisyProductStructure.partID>>
							.Where<NisyProductStructure.productID.IsEqual<NisyProductionOrder.productNumber.FromCurrent>>
							.AggregateTo<GroupBy<NisyInventory.partID>, Sum<NisyInventory.qty>>
							.View.ReadOnly(this).Select()
							.ToDictionary(e => e.GetItem<NisyInventory>().PartID, e => e.GetItem<NisyInventory>().Qty);

			foreach (NisyProductStructure item in BOMDetails.Select())
			{
				item.Available = item.TotalQty <= result[item.PartID];
			}
		}
	}
}
