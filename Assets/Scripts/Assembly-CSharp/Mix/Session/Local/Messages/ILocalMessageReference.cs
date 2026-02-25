using Disney.Mix.SDK;

namespace Mix.Session.Local.Messages
{
	public interface ILocalMessageReference<T> where T : IChatMessage
	{
		T SdkReference { get; set; }
	}
}
