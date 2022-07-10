using PX.Data;
using System;

namespace PX.Objects.IB.DAC
{
	[Serializable]
	[PXCacheName("Warehouse Details")]
	public class NisyWarehouse : IBqlTable
	{
		#region WarehouseID
		[PXDBIdentity]
		public virtual int? Warehouseid { get; set; }
		public abstract class warehouseid : PX.Data.BQL.BqlInt.Field<warehouseid> { }
		#endregion

		#region WarehouseCD
		[PXDBString(50, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa", IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Warehouse Name")]
		[PXSelector(typeof(Search<warehousecd>),
		typeof(warehousecd),
		typeof(warehousedescription),
		SubstituteKey = typeof(warehousecd))]
		public virtual string Warehousecd { get; set; }
		public abstract class warehousecd : PX.Data.BQL.BqlString.Field<warehousecd> { }
		#endregion

		#region WarehouseDescription
		[PXDBString(200, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Description")]
		public virtual string Warehousedescription { get; set; }
		public abstract class warehousedescription : PX.Data.BQL.BqlString.Field<warehousedescription> { }
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
