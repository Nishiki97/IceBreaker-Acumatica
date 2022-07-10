using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IB.DAC;
using PX.Objects.IB.Descriptor;

namespace PX.Objects.IB
{
	public class IBInventoryPartMaint : PXGraph<IBInventoryPartMaint, NisyPart>
	{
		#region Views
		public SelectFrom<NisyPart>.View InventoryParts;
		#endregion

		#region Events
		protected virtual void _(Events.RowSelected<NisyPart> e)
		{
			NisyPart row = e.Row;

			if (row != null) { PXUIFieldAttribute.SetVisible<NisyPart.partType>(e.Cache, e.Row, row.ItemType.Equals(ItemTypes.Stock)); }
		}

		protected virtual void _(Events.RowPersisting<NisyPart> e)
		{
			NisyPart row = e.Row;

			if (row.ItemType == ItemTypes.Stock && row.PartType == null) { e.Cache.RaiseExceptionHandling<NisyPart.partType>(row, row.PartType, new PXException(Messages.NullPartTypeMessage)); }
		}

		protected virtual void _(Events.FieldUpdated<NisyPart, NisyPart.itemtype> e)
		{
			NisyPart row = e.Row;

			if (row.ItemType == ItemTypes.NonStock) { e.Cache.SetValueExt<NisyPart.partType>(row, PartTypes.Service); }
			else { e.Cache.SetValueExt<NisyPart.partType>(row, null); }
		}
		#endregion
	}
}
