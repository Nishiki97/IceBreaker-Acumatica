using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.IB.DAC;
using PX.Objects.IB.Descriptor;
using static PX.Data.WorkflowAPI.BoundedTo<PX.Objects.IB.IBProductionOrderMaint, PX.Objects.IB.DAC.nisyProductionOrder>;
using static PX.Objects.IB.DAC.nisyProductionOrder;

namespace PX.Objects.IB.Workflows
{
	public class IBProductionOrderWorkflow : PX.Data.PXGraphExtension<IBProductionOrderMaint>
	{
		public static bool IsActive() => false;

		#region Constants
		public static class States
		{
			public const string NotSet = ProductionOrderStatuses.Not_Set;
			public const string Released = ProductionOrderStatuses.Released;
			public const string Reserved = ProductionOrderStatuses.Reserved;
			public const string Closed = ProductionOrderStatuses.Closed;

			public class notSet : PX.Data.BQL.BqlString.Constant<notSet>
			{
				public notSet() : base(NotSet) { }
			}
			public class released : PX.Data.BQL.BqlString.Constant<released>
			{
				public released() : base(Released) { }
			}
			public class reserved : PX.Data.BQL.BqlString.Constant<reserved>
			{
				public reserved() : base(Reserved) { }
			}
			public class closed : PX.Data.BQL.BqlString.Constant<closed>
			{
				public closed() : base(Closed) { }
			}
		}
		#endregion

		public override void Configure(PXScreenConfiguration config)
		{
			var context = config.GetScreenConfigurationContext<IBProductionOrderMaint, nisyProductionOrder>();

			#region Categories
			var commonCategories = CommonActionCategories.Get(context);
			var processingCategory = commonCategories.Processing;
			#endregion

			context.AddScreenConfigurationFor(screen =>
			{
				return screen
					.StateIdentifierIs<productionOrderStatus>()
					.AddDefaultFlow(flow =>
						flow.WithFlowStates(flowStates =>
							{
								flowStates.Add<States.notSet>(flowState =>
								{
									return flowState
									.IsInitial()
									.WithActions(actions =>
									{
										actions.Add(g => g.Release, a => a.IsDuplicatedInToolbar());
									});
								});
								flowStates.Add<States.released>(flowState =>
								{
									return flowState
										.WithActions(actions =>
										{
											actions.Add(g => g.IssueMaterial, a => a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success));
										})
										.WithFieldStates(states =>
										{
											states.AddField<nisyProductionOrder.orderID>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.productionOrderDate>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.requestedDate>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.productNumber>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.lotSize>(state => state.IsDisabled());
										});
								});
								flowStates.Add<States.reserved>(flowState =>
								{
									return flowState
										.WithActions(actions =>
										{
											actions.Add(g => g.ReceiveShopOrder, a => a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success));
										})
										.WithEventHandlers(handlers =>
										{
											handlers.Add(g => g.OnSaveReceiveStock);
										})
										.WithFieldStates(states =>
										{
											states.AddField<nisyProductionOrder.orderID>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.productionOrderDate>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.requestedDate>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.productNumber>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.lotSize>(state => state.IsDisabled());
										});
								});
								flowStates.Add<States.closed>(flowState =>
								{
									return flowState
										.WithFieldStates(states =>
										{
											states.AddField<nisyProductionOrder.orderID>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.productionOrderDate>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.requestedDate>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.productNumber>(state => state.IsDisabled());
											states.AddField<nisyProductionOrder.lotSize>(state => state.IsDisabled());
										});
								});
							})
							.WithTransitions(transitions =>
							{
								transitions.Add(t => t.From<States.notSet>().To<States.released>().IsTriggeredOn(g => g.Release));
								transitions.Add(t => t.From<States.released>().To<States.reserved>().IsTriggeredOn(g => g.IssueMaterial));
								transitions.Add(t => t.From<States.reserved>().To<States.closed>().IsTriggeredOn(g => g.OnSaveReceiveStock));
							})
					)
					.WithHandlers(handlers =>
					{
						handlers.Add(handler => handler
						.WithTargetOf<NisyReceiveStock>()
						.OfEntityEvent<NisyReceiveStock.Events>(e => e.SaveDocument)
						.Is(g => g.OnSaveReceiveStock)
						.UsesPrimaryEntityGetter<SelectFrom<nisyProductionOrder>.Where<productNumber.IsEqual<NisyReceiveStock.partid.FromCurrent>>>());
					})
					.WithCategories(categories =>
					{
						categories.Add(processingCategory);
					})
					.WithActions(actions =>
					{
						actions.Add(g => g.Release, c => c
							.WithCategory(processingCategory));
						actions.Add(g => g.IssueMaterial);
						actions.Add(g => g.ReceiveShopOrder);

					});
			});
		}
	}
}
