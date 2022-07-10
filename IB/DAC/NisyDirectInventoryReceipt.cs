using PX.Data;
using System;

namespace PX.Objects.IB.DAC
{
	[PXCacheName("Direct Inventory Receipt")]
	public class NisyDirectInventoryReceipt : IBqlTable
	{
		#region InventoryID
		[PXDBIdentity(IsKey = true)]
		[PXUIField(DisplayName = "Inventory ID")]
		public virtual int? Inventoryid { get; set; }
		public abstract class inventoryid : PX.Data.BQL.BqlInt.Field<inventoryid> { }
		#endregion

		#region PartID
		[PXDBInt]
		[PXSelector(typeof(Search<NisyPart.partid>),
		typeof(NisyPart.partid),
		typeof(NisyPart.partcd),
		SubstituteKey = typeof(NisyPart.partcd))]
		[PXUIField(DisplayName = "Part")]
		public virtual int? PartID { get; set; }
		public abstract class partid : PX.Data.BQL.BqlInt.Field<partid> { }
		#endregion

		#region WarehouseID
		[PXDBInt]
		[PXSelector(typeof(Search<NisyWarehouse.warehouseid>),
		typeof(NisyWarehouse.warehousecd),
		typeof(NisyWarehouse.warehousedescription),
		SubstituteKey = typeof(NisyWarehouse.warehousecd))]
		[PXUIField(DisplayName = "Warehouse")]
		public virtual int? WarehouseID { get; set; }
		public abstract class warehouseid : PX.Data.BQL.BqlInt.Field<warehouseid> { }

		#endregion

		#region LocationID
		[PXDBInt]
		[PXSelector(typeof(Search<NisyLocation.locationid, Where<NisyLocation.warehouseid, Equal<Current<warehouseid>>>>),
		typeof(NisyLocation.locationid),
		typeof(NisyLocation.locationcd),
		SubstituteKey = typeof(NisyLocation.locationcd))]
		[PXUIField(DisplayName = "Location")]
		public virtual int? LocationID { get; set; }
		public abstract class locationid : PX.Data.BQL.BqlInt.Field<locationid> { }
		#endregion

		#region Qty
		[PXDBInt]
		[PXUIField(DisplayName = "Quantity")]
		public virtual int? Qty { get; set; }
		public abstract class qty : PX.Data.BQL.BqlString.Field<qty> { }
		#endregion

		#region IsReleased
		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? IsReleased { get; set; }
		public abstract class isReleased : PX.Data.BQL.BqlBool.Field<isReleased> { }
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
