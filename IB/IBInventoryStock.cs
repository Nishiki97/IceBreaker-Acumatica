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
		[PXSelector(typeof(Search<NisyPart.partid>),
		typeof(NisyPart.partid),
		typeof(NisyPart.partcd),
		SubstituteKey = typeof(NisyPart.partcd))]
		[PXUIField(DisplayName = "Part")]
		public virtual int? PartID { get; set; }
		public abstract class partid : PX.Data.BQL.BqlInt.Field<partid> { }
		#endregion

		#region WarehouseID
		[PXInt]
		[PXSelector(typeof(Search<NisyWarehouse.warehouseid>),
		typeof(NisyWarehouse.warehousecd),
		typeof(NisyWarehouse.warehousedescription),
		SubstituteKey = typeof(NisyWarehouse.warehousecd))]
		[PXUIField(DisplayName = "Warehouse")]
		public virtual int? WarehouseID { get; set; }
		public abstract class warehouseid : PX.Data.BQL.BqlInt.Field<warehouseid> { }

		#endregion

		#region LocationID
		[PXInt]
		[PXSelector(typeof(Search<NisyLocation.locationid, Where<NisyLocation.warehouseid, Equal<Current<warehouseid>>>>),
		typeof(NisyLocation.locationid),
		typeof(NisyLocation.locationcd),
		SubstituteKey = typeof(NisyLocation.locationcd))]
		[PXUIField(DisplayName = "Location")]
		public virtual int? LocationID { get; set; }
		public abstract class locationid : PX.Data.BQL.BqlInt.Field<locationid> { }
		#endregion

		#region Qty
		[PXDecimal]
		[PXUIField(DisplayName = "Quantity", Enabled = false)]
		public virtual decimal? Qty { get; set; }
		public abstract class qty : PX.Data.BQL.BqlDecimal.Field<qty> { }
		#endregion
	}

	public class IBInventoryStock : PXGraph<IBInventoryStock>
	{
		public PXFilter<IBInventoryStockFilter> Filter;
		public PXCancel<IBInventoryStockFilter> cancel;

		[PXFilterable]
		public SelectFrom<NisyInventory>.View.ReadOnly INStatusRecords;

		#region Events
		protected virtual void _(Events.FieldSelecting<NisyInventory.locationid> e)
		{
			switch (e.ReturnValue)
			{
				case -1:
					e.ReturnState = PXFieldState.CreateInstance(PXMessages.LocalizeNoPrefix(Messages.TotalFilterable), typeof(string), false, null, null, null, null, null, nameof(NisyInventory.locationid), null, GetLocationDisplayName(), null, PXErrorLevel.Undefined, null, null, null, PXUIVisibility.Undefined, null, null, null);
					e.Cancel = true;
					{ }
					break;
			}
		}
		#endregion

		#region Methods
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
			.InnerJoin<NisyPart>.On<NisyPart.partid.IsEqual<NisyInventory.partid>>
			.InnerJoin<NisyWarehouse>.On<NisyWarehouse.warehouseid.IsEqual<NisyInventory.warehouseid>>
			.InnerJoin<NisyLocation>
			.On<NisyLocation.warehouseid.IsEqual<NisyWarehouse.warehouseid>
			.And<NisyLocation.locationid.IsEqual<NisyInventory.locationid>>>.View(this);

			if (Filter.Current.PartID != null)
			{
				cmd.WhereAnd<Where<NisyPart.partid.IsEqual<IBInventoryStockFilter.partid.FromCurrent>>>();
			}

			if (Filter.Current.WarehouseID != null)
			{
				cmd.WhereAnd<Where<NisyWarehouse.warehouseid.IsEqual<IBInventoryStockFilter.warehouseid.FromCurrent>>>();

				if (Filter.Current.LocationID != null)
				{
					cmd.WhereAnd<Where<NisyLocation.locationid.IsEqual<IBInventoryStockFilter.locationid.FromCurrent>>>();
				}
			}

			List<Type> fieldsScope = new List<Type>(new Type[]{
				typeof(NisyInventory.inventoryid),
				typeof(NisyInventory.qty),
				typeof(NisyPart.partcd),
				typeof(NisyWarehouse.warehousecd),
				typeof(NisyLocation.locationcd)
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
			.OrderBy(x => x.part.Partcd)
			.ThenBy(x => x.warehouse.Warehousecd)
			.ThenBy(x => x.location.Locationcd)
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

		private string GetLocationDisplayName()
		{
			var displayName = PXUIFieldAttribute.GetDisplayName<NisyInventory.locationid>(INStatusRecords.Cache);
			if (displayName != null) displayName = PXMessages.LocalizeNoPrefix(displayName);

			return displayName;
		}
		#endregion
	}
}
