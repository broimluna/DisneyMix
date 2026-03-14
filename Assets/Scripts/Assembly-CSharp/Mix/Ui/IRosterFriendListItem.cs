using Disney.Mix.SDK;

namespace Mix.Ui
{
	public interface IRosterFriendListItem
	{
		void OnToggleScroll(bool aState);

		void OnPanelToggled(bool isShowing);

		void OnFriendInvited(IOutgoingFriendInvitation aInvite);
	}
}
