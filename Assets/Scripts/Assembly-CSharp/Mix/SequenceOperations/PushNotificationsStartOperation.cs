namespace Mix.SequenceOperations
{
	public class PushNotificationsStartOperation : SequenceOperation
	{
		public PushNotificationsStartOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			MonoSingleton<PushNotifications>.Instance.Init();
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}
	}
}
