using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.DAC;

namespace PX.Objects.IB
{
	public class IBLocationMaint : PXGraph<IBLocationMaint, NisyWarehouse>
	{
		#region Views
		public SelectFrom<NisyWarehouse>.View WarehouseDetails;

		public SelectFrom<NisyLocation>
			.Where<NisyLocation.warehouseID
			.IsEqual<NisyWarehouse.warehouseID.FromCurrent>>.View LocationDetails;
		#endregion
	}
}
