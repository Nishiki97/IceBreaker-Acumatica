using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.DAC;

namespace PX.Objects.IB
{
	public class IBCustomerMaint : PXGraph<IBCustomerMaint>
	{
		#region Views
		public SelectFrom<NisyCustomer>.View CustomerDetails;
		#endregion

		public PXSave<NisyCustomer> Save;
		public PXCancel<NisyCustomer> Cancel;
	}
}
