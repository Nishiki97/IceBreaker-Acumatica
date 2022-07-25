using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.Descriptor;

namespace PX.Objects.IB.DAC
{
	[Serializable]
	[PXCacheName("Product Structure Details")]
	public class NisyProductStructure : IBqlTable
	{
		#region ProductStructureID
		[PXDBIdentity]
		public virtual int? ProductStructureID { get; set; }
		public abstract class productStructureID : PX.Data.BQL.BqlInt.Field<productStructureID> { }
		#endregion

		#region ProductID
		[PXDBInt(IsKey = true)]
		[PXDBDefault(typeof(NisyPart.partID))]
		[PXParent(typeof(SelectFrom<NisyPart>.Where<NisyPart.partID.IsEqual<productID.FromCurrent>>))]
		public virtual int? ProductID { get; set; }
		public abstract class productID : PX.Data.BQL.BqlInt.Field<productID> { }
		#endregion

		#region PartID
		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Part Name")]
		[PXDBDefault]
		[PXSelector(typeof(Search<NisyPart.partID, Where<NisyPart.partType.IsEqual<Purchased>>>),
		typeof(NisyPart.partCD),
		typeof(NisyPart.partDescription), SubstituteKey = typeof(NisyPart.partCD))]
		public virtual int? PartID { get; set; }
		public abstract class partID : PX.Data.BQL.BqlInt.Field<partID> { }
		#endregion

		#region Description
		[PXDBString(50, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Description")]
		public virtual string Description { get; set; }
		public abstract class description : PX.Data.BQL.BqlString.Field<description> { }
		#endregion

		#region Quantity
		[PXDBInt]
		[PXUIField(DisplayName = "Quantity")]
		public virtual int? Qty { get; set; }
		public abstract class qty : PX.Data.BQL.BqlInt.Field<qty> { }
		#endregion

		#region TotalQuantity
		[PXInt]
		[PXUIField(DisplayName = "Total Quantity")]
		[PXUnboundDefault(0)]
		public virtual int? TotalQty { get; set; }
		public abstract class totalQty : PX.Data.BQL.BqlInt.Field<totalQty> { }
		#endregion

		#region Available
		[PXBool]
		[PXUIField(DisplayName = "Availability")]
		public virtual bool? Available { get; set; }
		public abstract class available : PX.Data.BQL.BqlBool.Field<available> { }
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
