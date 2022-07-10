using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.DAC;

namespace PX.Objects.IB
{
	public class IBSetupMaint : PXGraph<IBSetupMaint>
	{
		public PXSave<NisySetup> Save;
		public PXCancel<NisySetup> Cancle;

		#region Views
		public SelectFrom<NisySetup>.View Setup;
		#endregion
	}
}
