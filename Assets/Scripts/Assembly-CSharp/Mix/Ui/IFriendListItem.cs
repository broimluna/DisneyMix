using Disney.Mix.SDK;
using UnityEngine;

namespace Mix.Ui
{
	public interface IFriendListItem
	{
		void OnFriendListSendTrustInviteClicked(IFriend friend);

		void OnFriendListRemoveClicked(IFriend friend);

		void OnFriendListItemClicked(IFriend friend);

		void OnToggleScroll(bool aState);

		void OnScrollToListItem(RectTransform aTransform, int aHeight);
	}
}
