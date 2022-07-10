using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.DAC;

namespace PX.Objects.IB
{
	internal class IBInventoryAllocationInquiry : PXGraph<IBInventoryAllocationInquiry, NisyInventoryAllocation>
	{
		#region View
		public SelectFrom<NisyInventoryAllocation>.View InvenotryAllocationSummaryDetails;
		#endregion
	}
}
