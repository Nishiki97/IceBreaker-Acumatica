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
			.Where<partID.IsEqual<NisyInventory.partID.FromCurrent>
			.And<warehouseID.IsEqual<NisyInventory.warehouseID.FromCurrent>>
			.And<locationID.IsEqual<NisyInventory.locationID.FromCurrent>>>
			.View DirectInvenotryReceiptDetails;
		#endregion
	}
}
