using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

namespace PX.Objects.IB.DAC
{
	[Serializable]
	[PXCacheName("Customer Details")]
	public class NisyCustomer : IBqlTable
	{
		#region Keys
		public class PK : PrimaryKeyOf<NisyCustomer>.By<customerID>
		{
			public static NisyCustomer Find(PXGraph graph, int? customerid) => FindBy(graph, customerid);
		}
		#endregion

		#region CustomerID
		[PXDBIdentity]
		public virtual int? CustomerID { get; set; }
		public abstract class customerID : PX.Data.BQL.BqlInt.Field<customerID> { }
		#endregion

		#region CustomerName
		[PXDBString(50, IsUnicode = true, InputMask = "", IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Customer Name")]
		public virtual string CustomerName { get; set; }
		public abstract class customerName : PX.Data.BQL.BqlString.Field<customerName> { }
		#endregion

		#region Address
		[PXDBString(200, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Address")]
		public virtual string Address { get; set; }
		public abstract class address : PX.Data.BQL.BqlString.Field<address> { }
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
