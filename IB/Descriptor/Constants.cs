using PX.Data.BQL;

namespace PX.Objects.IB.Descriptor
{
	public static class PartTypes
	{
		public const string Manufactured = "M";
		public const string Purchased = "P";
		public const string Service = "S";
	}

	public class Manufactured : BqlString.Constant<Manufactured> { public Manufactured() : base(PartTypes.Manufactured) { } }
	public class Purchased : BqlString.Constant<Purchased> { public Purchased() : base(PartTypes.Purchased) { } }
	public class Service : BqlString.Constant<Service> { public Service() : base(PartTypes.Service) { } }

	public static class ItemTypes
	{
		public const string Stock = "S";
		public const string NonStock = "NS";
	}
	public class Stock : BqlString.Constant<Stock> { public Stock() : base(ItemTypes.Stock) { } }
	public class NonStock : BqlString.Constant<NonStock> { public NonStock() : base(ItemTypes.NonStock) { } }

	public static class ProductionOrderStatuses
	{
		public const string Released = "Released";
		public const string Reserved = "Reserved";
		public const string Closed = "Closed";
		public const string Cancelled = "Cancelled";
		public const string NotSet = "Not Set";
	}

	public class released : BqlString.Constant<released> { public released() : base(ProductionOrderStatuses.Released) { } }
	public class reserved : BqlString.Constant<reserved> { public reserved() : base(ProductionOrderStatuses.Reserved) { } }
	public class closed : BqlString.Constant<closed> { public closed() : base(ProductionOrderStatuses.Closed) { } }
	public class cancelled : BqlString.Constant<cancelled> { public cancelled() : base(ProductionOrderStatuses.Cancelled) { } }
	public class notSet : BqlString.Constant<notSet> { public notSet() : base(ProductionOrderStatuses.Not_Set) { } }

	public static class CustomerOrderStatus
	{
		public const string Not_Set = "Not Set";
		public const string COPlanned = "Planned";
		public const string COReleased = "Released";
		public const string COClosed = "Closed";
		public const string COCancelled = "Cancelled";
	}

	public static class CustomerOrderItemDetailsStatus
	{
		public const string COItemRequired = "Required";
		public const string COItemDelivered = "Delivered";
		public const string COItemCancelled = "Cancelled";
	}
}
