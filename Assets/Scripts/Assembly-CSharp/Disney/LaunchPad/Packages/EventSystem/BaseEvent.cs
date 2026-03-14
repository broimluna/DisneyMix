namespace Disney.LaunchPad.Packages.EventSystem
{
	public class BaseEvent
	{
		public string GetName()
		{
			return GetType().ToString();
		}
	}
}
