using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.DAC;

namespace PX.Objects.IB
{
	public class IBProductStructureMaint : PXGraph<IBProductStructureMaint, NisyPart>
	{
		#region Views
		public SelectFrom<NisyPart>.View PartDetails;

		public SelectFrom<NisyProductStructure>
			.Where<NisyProductStructure.productID
				.IsEqual<NisyPart.partid.FromCurrent>>.View ProductStructureDetails;
		#endregion
	}
}
