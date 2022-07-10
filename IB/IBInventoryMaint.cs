using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.DAC;
using static PX.Objects.IB.DAC.NisyDirectInventoryReceipt;

namespace PX.Objects.IB
{
	public class IBInventoryMaint : PXGraph<IBInventoryMaint, NisyInventory>
	{
		#region View
		public SelectFrom<NisyInventory>.View InvenotrySummaryDetails;

		public SelectFrom<NisyDirectInventoryReceipt>
			.Where<partid.IsEqual<NisyInventory.partid.FromCurrent>
			.And<warehouseid.IsEqual<NisyInventory.warehouseid.FromCurrent>>
			.And<locationid.IsEqual<NisyInventory.locationid.FromCurrent>>>
			.View DirectInvenotryReceiptDetails;
		#endregion
	}
}
