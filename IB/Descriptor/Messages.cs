using PX.Common;

namespace PX.Objects.IB.Descriptor
{
	[PXLocalizable()]
	public static class Messages
	{
		//inventory part types
		public const string Manufactured = "Manufactured";
		public const string Purchased = "Purchased";
		public const string Service = "Service";

		//inventory parts item types
		public const string Stock = "Stock";
		public const string NonStock = "Non-Stock";

		//production order status
		public const string Released = "Released";
		public const string Reserved = "Reserved";
		public const string Closed = "Closed";
		public const string Cancelled = "Cancelled";
		public const string Not_Set = "Not Set";

		//Customer Order status
		public const string CONot_Set = "Not Set";
		public const string COPlanned = "Planned";
		public const string COReleased = "Released";
		public const string COClosed = "Closed";
		public const string COCancelled = "Cancelled";

		//Customer Order - Item details Status
		public const string COItemRequired = "Required";
		public const string COItemDelivered = "Delivered";
		public const string COItemCancelled = "Cancelled";

		//Error messages
		public const string DuplicateLocationMessage = "Locations cannot be duplicated";
		public const string NullLocationMessage = "Locations cannot be empty";
		public const string NullPartTypeMessage = "Part Type should be selected!";
		public const string NoSufficientQtyMessage = "No sufficient quantity in stock!";
		public const string TotalFilterable = "TOTAL";
	}
}
