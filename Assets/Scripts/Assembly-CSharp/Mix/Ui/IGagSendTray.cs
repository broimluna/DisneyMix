using Disney.Mix.SDK;
using Mix.Data;

namespace Mix.Ui
{
	public interface IGagSendTray
	{
		void OnSendGroupGag(Gag aGag, IRemoteChatMember aTarget);
	}
}
