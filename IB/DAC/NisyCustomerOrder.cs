using System;
using PX.Data;
using PX.Objects.CS;
using Messages = PX.Objects.IB.Descriptor.Messages;

namespace PX.Objects.IB.DAC
{
	[Serializable]
	[PXCacheName("Customer Order")]
	public class NisyCustomerOrder : IBqlTable
	{
		#region CustomerOrderNbr
		[PXDBString(200, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Order Number")]
		[AutoNumber(typeof(NisySetup.sONumberingID), typeof(orderDate))]
		public virtual string CustomerOrderNbr { get; set; }
		public abstract class customerOrderNbr : PX.Data.BQL.BqlString.Field<customerOrderNbr> { }
		#endregion

		#region OrderDate
		[PXDBDate]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Order Date")]
		public virtual DateTime? OrderDate { get; set; }
		public abstract class orderDate : PX.Data.BQL.BqlDateTime.Field<orderDate> { }
		#endregion

		#region CustomerID
		[PXDBInt(IsKey = true)]
		[PXDefault(typeof(NisyCustomer.customername))]
		[PXUIField(DisplayName = "Customer Name")]
		[PXSelector(typeof(Search<NisyCustomer.customerid>),
		typeof(NisyCustomer.customerid),
		typeof(NisyCustomer.customername),
		SubstituteKey = typeof(NisyCustomer.customername))]
		public virtual int? CustomerID { get; set; }
		public abstract class customerID : PX.Data.BQL.BqlInt.Field<customerID> { }
		#endregion

		#region CustomerAddress
		[PXDBString(50)]
		[PXDefault]
		[PXUIField(DisplayName = "Customer Address")]
		public virtual string CustomerAddress { get; set; }
		public abstract class customerAddress : PX.Data.BQL.BqlString.Field<customerAddress> { }
		#endregion

		#region Status
		[PXDBString(50, IsUnicode = true, InputMask = "")]
		[PXDefault(Messages.CONot_Set)]
		[PXUIField(DisplayName = "Status", Enabled = false)]
		public virtual string Status { get; set; }
		public abstract class status : PX.Data.BQL.BqlString.Field<status> { }
		#endregion

		#region OrderTotalPart
		[PXDecimal()]
		[PXUnboundDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? OrderTotalPart { get; set; }
		public abstract class orderTotalPart : PX.Data.BQL.BqlDecimal.Field<orderTotalPart> { }
		#endregion

		#region OrderTotalNoPart
		[PXDecimal()]
		[PXUnboundDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? OrderTotalNoPart { get; set; }
		public abstract class orderTotalNoPart : PX.Data.BQL.BqlDecimal.Field<orderTotalNoPart> { }
		#endregion

		#region TotalPrice
		[PXDBDecimal()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Total Price", Enabled = false)]
		[PXFormula(typeof(Add<orderTotalPart, orderTotalNoPart>))]
		public virtual Decimal? TotalPrice { get; set; }
		public abstract class totalPrice : PX.Data.BQL.BqlDecimal.Field<totalPrice> { }
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
