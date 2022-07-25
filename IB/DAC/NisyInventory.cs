using System;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;

namespace PX.Objects.IB.DAC
{
	[PXCacheName("Inventory Stock")]
	public class NisyInventory : IBqlTable
	{
		#region Keys
		public class PK : PrimaryKeyOf<NisyInventory>.By<partID, warehouseID, locationID>
		{
			public static NisyInventory Find(PXGraph graph, int? partId, int? warehouseid, int? locationId) => FindBy(graph, partId, warehouseid, locationId);
		}
		#endregion

		#region InventoryID
		[PXDBIdentity]
		public virtual int? InventoryID { get; set; }
		public abstract class inventoryID : PX.Data.BQL.BqlInt.Field<inventoryID> { }
		#endregion

		#region PartID
		[PXDBInt(IsKey = true)]
		[PXSelector(typeof(Search<NisyPart.partID>),
		typeof(NisyPart.partID),
		typeof(NisyPart.partCD),
		SubstituteKey = typeof(NisyPart.partCD))]
		[PXUIField(DisplayName = "Part")]
		public virtual int? PartID { get; set; }
		public abstract class partID : PX.Data.BQL.BqlInt.Field<partID> { }
		#endregion

		#region WarehouseID
		[PXDBInt(IsKey = true)]
		[PXSelector(typeof(Search<NisyWarehouse.warehouseID>),
		typeof(NisyWarehouse.warehouseCD),
		typeof(NisyWarehouse.warehouseDescription),
		SubstituteKey = typeof(NisyWarehouse.warehouseCD))]
		[PXUIField(DisplayName = "Warehouse")]
		public virtual int? WarehouseID { get; set; }
		public abstract class warehouseID : PX.Data.BQL.BqlInt.Field<warehouseID> { }
		#endregion

		#region LocationID
		[PXDBInt(IsKey = true)]
		[PXSelector(typeof(Search<NisyLocation.locationID, Where<NisyLocation.warehouseID, Equal<Current<warehouseID>>>>),
		typeof(NisyLocation.locationID),
		typeof(NisyLocation.locationCD),
		SubstituteKey = typeof(NisyLocation.locationCD))]
		[PXUIField(DisplayName = "Location")]
		public virtual int? LocationID { get; set; }
		public abstract class locationID : PX.Data.BQL.BqlInt.Field<locationID> { }
		#endregion

		#region Qty
		[PXDBDecimal]
		[PXUIField(DisplayName = "Quantity")]
		public virtual decimal? Qty { get; set; }
		public abstract class qty : PX.Data.BQL.BqlDecimal.Field<qty> { }
		#endregion

		#region Price
		[PXDBInt]
		[PXUIField(DisplayName = "Price")]
		public virtual int? Price { get; set; }
		public abstract class price : PX.Data.BQL.BqlString.Field<price> { }
		#endregion

		#region TotalPrice
		[PXDBInt]
		[PXUIField(DisplayName = "Total Price", Enabled = false)]
		[PXFormula(typeof(Mult<qty, price>))]
		public virtual int? TotalPrice { get; set; }
		public abstract class totalPrice : PX.Data.BQL.BqlString.Field<totalPrice> { }
		#endregion

		#region IsTotal
		[PXBool]
		[PXUIField(DisplayName = "Price")]
		public virtual bool? IsTotal { get; set; }
		public abstract class isTotal : PX.Data.BQL.BqlBool.Field<isTotal> { }
		#endregion

		#region CreatedDateTime
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime { get; set; }
		public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
		#endregion
		#region CreatedByID
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID { get; set; }
		public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
		#endregion
		#region CreatedByScreenID
		[PXDBCreatedByScreenID()]
		public virtual string CreatedByScreenID { get; set; }
		public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
		#endregion
		#region LastModifiedDateTime
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
		#endregion
		#region LastModifiedByID
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID { get; set; }
		public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
		#endregion
		#region LastModifiedByScreenID
		[PXDBLastModifiedByScreenID()]
		public virtual string LastModifiedByScreenID { get; set; }
		public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
		#endregion
		#region Tstamp
		[PXDBTimestamp()]
		public virtual byte[] Tstamp { get; set; }
		public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
		#endregion
		#region NoteID
		[PXNote()]
		public virtual Guid? NoteID { get; set; }
		public abstract class noteID : PX.Data.BQL.BqlGuid.Field<noteID> { }
		#endregion
	}
}
