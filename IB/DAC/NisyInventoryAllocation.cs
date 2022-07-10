using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

namespace PX.Objects.IB.DAC
{
	[PXCacheName("Inventory Allocation")]
	public class NisyInventoryAllocation : IBqlTable
	{
		#region Keys
		public class PK : PrimaryKeyOf<NisyInventoryAllocation>.By<partid>
		{
			public static NisyInventoryAllocation Find(PXGraph graph, int? partId) => FindBy(graph, partId);
		}
		#endregion

		#region InventoryAllocationID
		[PXDBIdentity]
		public virtual int? InventoryAllocationID { get; set; }
		public abstract class inventoryallocationid : PX.Data.BQL.BqlInt.Field<inventoryallocationid> { }
		#endregion

		#region PartID
		[PXDBInt(IsKey = true)]
		[PXSelector(typeof(Search<NisyPart.partid>),
		typeof(NisyPart.partid),
		typeof(NisyPart.partcd),
		SubstituteKey = typeof(NisyPart.partcd))]
		[PXUIField(DisplayName = "Part")]
		public virtual int? PartID { get; set; }
		public abstract class partid : PX.Data.BQL.BqlInt.Field<partid> { }
		#endregion

		#region AvailableForSale
		[PXDBInt]
		[PXDefault(0)]
		[PXUIField(DisplayName = "Available For Sale")]
		[PXFormula(typeof(Sub<qtyinhand, reservedqty>))]
		public virtual int? AvailableForSale { get; set; }
		public abstract class availableforsale : PX.Data.BQL.BqlString.Field<availableforsale> { }
		#endregion

		#region ReservedQty
		[PXDBInt]
		[PXDefault(0)]
		[PXUIField(DisplayName = "Reserved Qty")]
		public virtual int? ReservedQty { get; set; }
		public abstract class reservedqty : PX.Data.BQL.BqlString.Field<reservedqty> { }
		#endregion

		#region QtyInHand
		[PXDBInt]
		[PXDefault(0)]
		[PXUIField(DisplayName = "Qty In Hand")]
		public virtual int? QtyInHand { get; set; }
		public abstract class qtyinhand : PX.Data.BQL.BqlString.Field<qtyinhand> { }
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
