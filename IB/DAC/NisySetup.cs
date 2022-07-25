using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IB.Descriptor;

namespace PX.Objects.IB.DAC
{
	[Serializable]
	[PXCacheName("Numbering Sequence")]
	public class NisySetup : IBqlTable
	{
		#region PONumberingID
		[PXDefault("PRODUCTIONORDER")]
		[PXSelector(typeof(Numbering.numberingID),
		DescriptionField = typeof(Numbering.descr))]
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Product Numbering Sequence")]
		public virtual string PONumberingID { get; set; }
		public abstract class pONumberingID : PX.Data.BQL.BqlString.Field<pONumberingID> { }
		#endregion

		#region SONumberingID
		[PXDefault("NISYSO")]
		[PXSelector(typeof(Numbering.numberingID),
		DescriptionField = typeof(Numbering.descr))]
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Sales Numbering Sequence")]
		public virtual string SONumberingID { get; set; }
		public abstract class sONumberingID : PX.Data.BQL.BqlString.Field<sONumberingID> { }
		#endregion


		#region CreatedDateTime
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime { get; set; }
		public abstract class createdDateTime :
			PX.Data.BQL.BqlDateTime.Field<createdDateTime>
		{ }
		#endregion

		#region CreatedByID
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID { get; set; }
		public abstract class createdByID :
			PX.Data.BQL.BqlGuid.Field<createdByID>
		{ }
		#endregion

		#region CreatedByScreenID
		[PXDBCreatedByScreenID()]
		public virtual string CreatedByScreenID { get; set; }
		public abstract class createdByScreenID :
			PX.Data.BQL.BqlString.Field<createdByScreenID>
		{ }
		#endregion

		#region LastModifiedDateTime
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		public abstract class lastModifiedDateTime :
			PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime>
		{ }
		#endregion

		#region LastModifiedByID
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID { get; set; }
		public abstract class lastModifiedByID :
			PX.Data.BQL.BqlGuid.Field<lastModifiedByID>
		{ }
		#endregion

		#region LastModifiedByScreenID
		[PXDBLastModifiedByScreenID()]
		public virtual string LastModifiedByScreenID { get; set; }
		public abstract class lastModifiedByScreenID :
			PX.Data.BQL.BqlString.Field<lastModifiedByScreenID>
		{ }
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
