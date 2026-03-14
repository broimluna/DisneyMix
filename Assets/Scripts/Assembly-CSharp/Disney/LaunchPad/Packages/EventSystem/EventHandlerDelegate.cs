namespace Disney.LaunchPad.Packages.EventSystem
{
	public delegate bool EventHandlerDelegate<T>(T evt) where T : BaseEvent;
}
