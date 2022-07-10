using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;
using PX.Objects.IB.Descriptor;
using Messages = PX.Objects.IB.Descriptor.Messages;

namespace PX.Objects.IB.DAC
{
	[Serializable]
	[PXCacheName("Customer Order Part Details")]
	public class NisyCustomerOrderPartDetails : IBqlTable
	{
		#region CustomerOrderNbr
		[PXDBString(IsKey = true)]
		[PXDBDefault(typeof(NisyCustomerOrder.customerOrderNbr))]
		[PXParent(typeof(SelectFrom<NisyCustomerOrder>.Where<NisyCustomerOrder.customerOrderNbr.IsEqual<customerOrderNbr.FromCurrent>>))]
		public virtual string CustomerOrderNbr { get; set; }
		public abstract class customerOrderNbr : BqlString.Field<customerOrderNbr> { }
		#endregion

		#region PartID
		[PXDBInt(IsKey = true)]
		[PXSelector(typeof(Search<NisyPart.partid, Where<NisyPart.itemtype.IsEqual<Stock>>>),
		typeof(NisyPart.partcd),
		typeof(NisyPart.partDescription),
		SubstituteKey = typeof(NisyPart.partcd))]
		public virtual int? PartID { get; set; }
		public abstract class partID : BqlInt.Field<partID> { }
		#endregion

		#region PartDescription
		[PXDBString(100)]
		[PXDefault]
		[PXUIField(DisplayName = "Description")]
		public virtual string PartDescription { get; set; }
		public abstract class partDescription : BqlString.Field<partDescription> { }
		#endregion

		#region Qty
		[PXDBDecimal()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Quantity")]
		public virtual Decimal? Qty { get; set; }
		public abstract class qty : BqlDecimal.Field<qty> { }
		#endregion

		#region Price
		[PXDBDecimal()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Price", Required = true)]
		public virtual Decimal? Price { get; set; }
		public abstract class price : BqlDecimal.Field<price> { }
		#endregion

		#region TotalPrice
		[PXDecimal()]
		[PXUIField(DisplayName = "Total Price")]
		[PXFormula(
		typeof(Mult<qty, price>),
		typeof(SumCalc<NisyCustomerOrder.orderTotalPart>))]
		public virtual Decimal? TotalPrice { get; set; }
		public abstract class totalPrice : BqlDecimal.Field<totalPrice> { }
		#endregion

		#region Status
		[PXDBString(20, IsUnicode = true, InputMask = "")]
		[PXDefault(Messages.COItemRequired)]
		[PXUIField(DisplayName = "Status", Enabled = false)]
		public virtual string Status { get; set; }
		public abstract class status : BqlString.Field<status> { }
		#endregion

		#region CreatedDateTime
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime { get; set; }
		public abstract class createdDateTime : BqlDateTime.Field<createdDateTime> { }
		#endregion
		#region CreatedByID
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID { get; set; }
		public abstract class createdByID : BqlGuid.Field<createdByID> { }
		#endregion
		#region CreatedByScreenID
		[PXDBCreatedByScreenID()]
		public virtual string CreatedByScreenID { get; set; }
		public abstract class createdByScreenID : BqlString.Field<createdByScreenID> { }
		#endregion
		#region LastModifiedDateTime
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		public abstract class lastModifiedDateTime : BqlDateTime.Field<lastModifiedDateTime> { }
		#endregion
		#region LastModifiedByID
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID { get; set; }
		public abstract class lastModifiedByID : BqlGuid.Field<lastModifiedByID> { }
		#endregion
		#region LastModifiedByScreenID
		[PXDBLastModifiedByScreenID()]
		public virtual string LastModifiedByScreenID { get; set; }
		public abstract class lastModifiedByScreenID : BqlString.Field<lastModifiedByScreenID> { }
		#endregion
		#region Tstamp
		[PXDBTimestamp()]
		public virtual byte[] Tstamp { get; set; }
		public abstract class tstamp : BqlByteArray.Field<tstamp> { }
		#endregion
		#region NoteID
		[PXNote()]
		public virtual Guid? NoteID { get; set; }
		public abstract class noteID : BqlGuid.Field<noteID> { }
		#endregion
	}
}
