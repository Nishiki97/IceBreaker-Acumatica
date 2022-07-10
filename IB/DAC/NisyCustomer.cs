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
		public class PK : PrimaryKeyOf<NisyCustomer>.By<customerid>
		{
			public static NisyCustomer Find(PXGraph graph, int? customerid) => FindBy(graph, customerid);
		}
		#endregion

		#region CustomerID
		[PXDBIdentity]
		public virtual int? Customerid { get; set; }
		public abstract class customerid : PX.Data.BQL.BqlInt.Field<customerid> { }
		#endregion

		#region CustomerName
		[PXDBString(50, IsUnicode = true, InputMask = "", IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Customer Name")]
		public virtual string Customername { get; set; }
		public abstract class customername : PX.Data.BQL.BqlString.Field<customername> { }
		#endregion

		#region Address
		[PXDBString(200, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Address")]
		public virtual string Address { get; set; }
		public abstract class address : PX.Data.BQL.BqlString.Field<address> { }
		#endregion
	}
}
