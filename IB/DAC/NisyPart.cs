using System;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IB.Descriptor;

namespace PX.Objects.IB.DAC
{
	[Serializable]
	[PXCacheName("Inventory Items")]
	[PXPrimaryGraph(typeof(IBInventoryPartMaint))]
	public class NisyPart : IBqlTable
	{
		#region Keys
		public class PK : PrimaryKeyOf<NisyPart>.By<partid>
		{
			public static NisyPart Find(PXGraph graph, int? partNo) => FindBy(graph, partNo);
		}
		#endregion

		#region Partid
		[PXDBIdentity]
		public virtual int? Partid { get; set; }
		public abstract class partid : PX.Data.BQL.BqlInt.Field<partid> { }
		#endregion

		#region Partcd
		[PXDBString(50, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa", IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Item Code")]
		[PXSelector(typeof(Search<partcd, Where<partType.IsEqual<Manufactured>>>),
		typeof(partcd),
		typeof(partDescription))]
		public virtual string Partcd { get; set; }
		public abstract class partcd : PX.Data.BQL.BqlString.Field<partcd> { }
		#endregion

		#region PartDescription
		[PXDBString(100, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Part Description")]
		public virtual string PartDescription { get; set; }
		public abstract class partDescription : PX.Data.BQL.BqlString.Field<partDescription> { }
		#endregion

		#region ItemType
		[PXDBString(50, IsUnicode = true, InputMask = "")]
		[PXDefault(ItemTypes.NonStock)]
		[PXUIField(DisplayName = "Item Type", Required = true)]
		[PXStringList(
				new string[]{
					ItemTypes.Stock,
					ItemTypes.NonStock
				},
				new string[]
				{
					Messages.Stock,
					Messages.NonStock
				})]
		public virtual string ItemType { get; set; }
		public abstract class itemtype : PX.Data.BQL.BqlString.Field<itemtype> { }
		#endregion

		#region PartType
		[PXDBString(50, IsUnicode = true, InputMask = "")]
		[PXDefault(PartTypes.Service)]
		[PXUIField(DisplayName = "Part Type", Required = true)]
		[PXStringList(
				new string[]{
					PartTypes.Manufactured,
					PartTypes.Purchased
				},
				new string[]
				{
					Messages.Manufactured,
					Messages.Purchased
				})]
		public virtual string PartType { get; set; }
		public abstract class partType : PX.Data.BQL.BqlString.Field<partType> { }
		#endregion

		#region Price
		[PXDBDecimal()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Price", Required = true)]
		public virtual Decimal? Price { get; set; }
		public abstract class price : PX.Data.BQL.BqlDecimal.Field<price> { }
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

	[PXCacheName("BOM details")]
	public class nisyBOMPart : NisyPart
	{
		#region Partcd
		[PXDBString(50, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa", IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Part Code")]
		[PXSelector(typeof(Search<partcd, Where<partType.IsEqual<Manufactured>>>),
		typeof(partcd),
		typeof(partDescription))]
		public new virtual string Partcd { get; set; }
		public new abstract class partcd : PX.Data.BQL.BqlString.Field<partcd> { }
		#endregion
	}
}
