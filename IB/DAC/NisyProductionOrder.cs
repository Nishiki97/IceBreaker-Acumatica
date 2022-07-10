using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IB.Descriptor;
using Messages = PX.Objects.IB.Descriptor.Messages;

namespace PX.Objects.IB.DAC
{
	[Serializable]
	[PXCacheName("Production Order")]
	public class NisyProductionOrder : IBqlTable
	{
		#region ProductionOrderID
		[PXDBIdentity]
		public virtual int? ProductionOrderID { get; set; }
		public abstract class productionOrderID : PX.Data.BQL.BqlInt.Field<productionOrderID> { }
		#endregion

		#region OrderID
		[PXDBString(200, IsUnicode = true, InputMask = "", IsKey = true)]
		[PXUIField(DisplayName = "Order No")]
		[PXDefault]
		[AutoNumber(typeof(NisySetup.ponumberingID), typeof(productionOrderDate))]
		[PXSelector(typeof(Search<orderID>),
		typeof(productionOrderDate),
		typeof(productionOrderStatus),
		SubstituteKey = typeof(orderID))]
		public virtual string OrderID { get; set; }
		public abstract class orderID : PX.Data.BQL.BqlString.Field<orderID> { }
		#endregion

		#region ProductionOrderDate
		[PXDBDate()]
		[PXUIField(DisplayName = "Order Date")]
		public virtual DateTime? ProductionOrderDate { get; set; }
		public abstract class productionOrderDate : PX.Data.BQL.BqlDateTime.Field<productionOrderDate> { }
		#endregion

		#region RequestedDate
		[PXDBDate()]
		[PXUIField(DisplayName = "Order Required Date")]
		public virtual DateTime? RequestedDate { get; set; }
		public abstract class requestedDate : PX.Data.BQL.BqlDateTime.Field<requestedDate> { }
		#endregion

		#region ProductionOrderStatus
		[PXDBString(50, IsUnicode = true)]
		[PXUIField(DisplayName = "Order Status")]
		[PXDefault(Messages.Not_Set)]
		[PXStringList(
				new string[]{
					ProductionOrderStatuses.Released,
					ProductionOrderStatuses.Reserved,
					ProductionOrderStatuses.Closed,
					ProductionOrderStatuses.Cancelled,
					ProductionOrderStatuses.Not_Set
				},
				new string[]
				{
					Messages.Released,
					Messages.Reserved,
					Messages.Closed,
					Messages.Cancelled,
					Messages.Not_Set
				})]
		public virtual string ProductionOrderStatus { get; set; }
		public abstract class productionOrderStatus : PX.Data.BQL.BqlString.Field<productionOrderStatus> { }
		#endregion

		#region ProductNumber
		[PXDBInt]
		[PXDBDefault]
		[PXSelector(typeof(Search<NisyPart.partid, Where<NisyPart.partType.IsEqual<Manufactured>>>),
		typeof(NisyPart.partid),
		typeof(NisyPart.partcd),
		SubstituteKey = typeof(NisyPart.partcd))]
		[PXUIField(DisplayName = "Product")]
		public virtual int? ProductNumber { get; set; }
		public abstract class productNumber : PX.Data.BQL.BqlInt.Field<productNumber> { }
		#endregion

		#region LotSize
		[PXDBInt]
		[PXUIField(DisplayName = "Lot Size")]
		public virtual int? LotSize { get; set; }
		public abstract class lotSize : PX.Data.BQL.BqlInt.Field<lotSize> { }
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
