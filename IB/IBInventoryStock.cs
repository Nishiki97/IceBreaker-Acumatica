using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Extensions;
using PX.Objects.IB.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Messages = PX.Objects.IB.Descriptor.Messages;

namespace PX.Objects.IB
{
	[PXHidden]
	public class IBInventoryStockFilter : IBqlTable
	{
		#region PartID
		[PXInt]
		[PXSelector(typeof(Search<NisyPart.partID>),
		typeof(NisyPart.partID),
		typeof(NisyPart.partCD),
		SubstituteKey = typeof(NisyPart.partCD))]
		[PXUIField(DisplayName = "Part")]
		public virtual int? PartID { get; set; }
		public abstract class partid : Data.BQL.BqlInt.Field<partid> { }
		#endregion

		#region WarehouseID
		[PXInt]
		[PXSelector(typeof(Search<NisyWarehouse.warehouseID>),
		typeof(NisyWarehouse.warehouseCD),
		typeof(NisyWarehouse.warehouseDescription),
		SubstituteKey = typeof(NisyWarehouse.warehouseCD))]
		[PXUIField(DisplayName = "Warehouse")]
		public virtual int? WarehouseID { get; set; }
		public abstract class warehouseid : Data.BQL.BqlInt.Field<warehouseid> { }
		#endregion

		#region LocationID
		[PXInt]
		[PXSelector(typeof(Search<NisyLocation.locationID, Where<NisyLocation.warehouseID, Equal<Current<warehouseid>>>>),
		typeof(NisyLocation.locationID),
		typeof(NisyLocation.locationCD),
		SubstituteKey = typeof(NisyLocation.locationCD))]
		[PXUIField(DisplayName = "Location")]
		public virtual int? LocationID { get; set; }
		public abstract class locationid : Data.BQL.BqlInt.Field<locationid> { }
		#endregion

		#region Qty
		[PXDecimal]
		[PXUIField(DisplayName = "Quantity", Enabled = false)]
		public virtual decimal? Qty { get; set; }
		public abstract class qty : Data.BQL.BqlDecimal.Field<qty> { }
		#endregion
	}

	public class IBInventoryStock : PXGraph<IBInventoryStock>
	{
		public PXFilter<IBInventoryStockFilter> Filter;
		public PXCancel<IBInventoryStockFilter> cancel;

		[PXFilterable]
		public SelectFrom<NisyInventory>.View.ReadOnly INStatusRecords;

		protected virtual IEnumerable iNStatusRecords()
		{
			INStatusRecords.Cache.Clear();

			foreach (var item in FetchINStatusRecord().OfType<NisyInventory>())
			{
				INStatusRecords.Cache.Hold(item);
			}

			var resultsset = INStatusRecords.Cache.Cached.RowCast<NisyInventory>();
			var totalraw = CalculateSummaryTotal(resultsset);
			var delegateresult = new PXDelegateResult() { IsResultSorted = true };
			var sortedresult = PXView.Sort(resultsset).RowCast<NisyInventory>();

			if (!PXView.ReverseOrder)
			{
				delegateresult.AddRange(sortedresult);
				delegateresult.Add(totalraw);
			}
			else
			{
				delegateresult.Add(totalraw);
				delegateresult.AddRange(sortedresult);
			}
			return delegateresult;
		}

		protected virtual IEnumerable FetchINStatusRecord()
		{
			var cmd = new SelectFrom<NisyInventory>
				.InnerJoin<NisyPart>.On<NisyPart.partID.IsEqual<NisyInventory.partID>>
				.InnerJoin<NisyWarehouse>.On<NisyWarehouse.warehouseID.IsEqual<NisyInventory.warehouseID>>
				.InnerJoin<NisyLocation>
				.On<NisyLocation.warehouseID.IsEqual<NisyWarehouse.warehouseID>
				.And<NisyLocation.locationID.IsEqual<NisyInventory.locationID>>>.View(this);

			if (Filter.Current.PartID != null)
			{
				cmd.WhereAnd<Where<NisyPart.partID.IsEqual<IBInventoryStockFilter.partid.FromCurrent>>>();
			}

			if (Filter.Current.WarehouseID != null)
			{
				cmd.WhereAnd<Where<NisyWarehouse.warehouseID.IsEqual<IBInventoryStockFilter.warehouseid.FromCurrent>>>();

				if (Filter.Current.LocationID != null)
				{
					cmd.WhereAnd<Where<NisyLocation.locationID.IsEqual<IBInventoryStockFilter.locationid.FromCurrent>>>();
				}
			}

			List<Type> fieldsScope = new List<Type>(new Type[]{
				typeof(NisyInventory.inventoryID),
				typeof(NisyInventory.qty),
				typeof(NisyPart.partCD),
				typeof(NisyWarehouse.warehouseCD),
				typeof(NisyLocation.locationCD)
				});

			var resultSet = new List<(NisyInventory instatus, NisyPart part, NisyWarehouse warehouse, NisyLocation location)>();

			using (new PXFieldScope(cmd.View, fieldsScope.ToArray()))
			{
				foreach (PXResult<NisyInventory, NisyPart, NisyWarehouse, NisyLocation> result in cmd.Select())
				{
					NisyInventory inSatus = result;
					NisyPart part = result;
					NisyWarehouse warehouse = result;
					NisyLocation location = result;

					Filter.Current.Qty += inSatus.Qty;
					resultSet.Add((inSatus, part, warehouse, location));
				}
			}

			return resultSet
			.OrderBy(x => x.part.PartCD)
			.ThenBy(x => x.warehouse.WarehouseCD)
			.ThenBy(x => x.location.LocationCD)
			.Select(x => x.instatus);
		}

		private NisyInventory CalculateSummaryTotal(IEnumerable<NisyInventory> resultsset)
		{
			NisyInventory total = resultsset.CalculateSumTotal(INStatusRecords.Cache);

			total.PartID = null;
			total.WarehouseID = null;
			total.LocationID = -1;
			total.Price = null;
			total.IsTotal = true;

			return total;
		}

		protected virtual void _(Events.FieldSelecting<NisyInventory.locationID> e)
		{
			switch (e.ReturnValue)
			{
				case -1:
					// Acuminator disable once PX1050 HardcodedStringInLocalizationMethod [Justification]
					e.ReturnState = PXFieldState.CreateInstance(PXMessages.LocalizeNoPrefix("TOTAL"), typeof(string), false, null, null, null, null, null, nameof(NisyInventory.locationID), null, GetLocationDisplayName(), null, PXErrorLevel.Undefined, null, null, null, PXUIVisibility.Undefined, null, null, null);
					e.Cancel = true;
					{ }
					break;
			}
		}

		private string GetLocationDisplayName()
		{
			var displayName = PXUIFieldAttribute.GetDisplayName<NisyInventory.locationID>(INStatusRecords.Cache);
			if (displayName != null) displayName = PXMessages.LocalizeNoPrefix(displayName);

			return displayName;
		}
	}
}
