using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class NotificationDispatcher : INotificationDispatcher
	{
		public event EventHandler<AbstractAddChatThreadGagMessageNotificationEventArgs> OnChatThreadGagMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractAddChatThreadMembershipNotificationEventArgs> OnChatThreadMembershipAdded = delegate
		{
		};

		public event EventHandler<AbstractAddChatThreadNotificationEventArgs> OnChatThreadAdded = delegate
		{
		};

		public event EventHandler<AbstractAddChatThreadPhotoMessageNotificationEventArgs> OnChatThreadPhotoMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractAddChatThreadStickerMessageNotificationEventArgs> OnChatThreadStickerMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractAddChatThreadTextMessageNotificationEventArgs> OnChatThreadTextMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractAddChatThreadVideoMessageNotificationEventArgs> OnChatThreadVideoMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractAddChatThreadMemberListChangedMessageNotificationEventArgs> OnChatThreadMemberListChangedMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractAddChatThreadGameStateMessageNotificationEventArgs> OnChatThreadGameStateMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractUpdateChatThreadGameStateMessageNotificationEventArgs> OnChatThreadGameStateMessageUpdated = delegate
		{
		};

		public event EventHandler<AbstractAddChatThreadGameEventMessageNotificationEventArgs> OnChatThreadGameEventMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractAddFriendshipNotificationEventArgs> OnFriendshipAdded = delegate
		{
		};

		public event EventHandler<AbstractAddFriendshipInvitationNotificationEventArgs> OnFriendshipInvitationAdded = delegate
		{
		};

		public event EventHandler<AbstractAddNicknameNotificationEventArgs> OnNicknameAdded = delegate
		{
		};

		public event EventHandler<AbstractRemoveChatThreadMembershipNotificationEventArgs> OnChatThreadMembershipRemoved = delegate
		{
		};

		public event EventHandler<AbstractRemoveFriendshipInvitationNotificationEventArgs> OnFriendshipInvitationRemoved = delegate
		{
		};

		public event EventHandler<AbstractRemoveFriendshipNotificationEventArgs> OnFriendshipRemoved = delegate
		{
		};

		public event EventHandler<AbstractRemoveFriendshipTrustNotificationEventArgs> OnFriendshipTrustRemoved = delegate
		{
		};

		public event EventHandler<AbstractRemoveNicknameNotificationEventArgs> OnNicknameRemoved = delegate
		{
		};

		public event EventHandler<AbstractSetAvatarNotificationEventArgs> OnAvatarSet = delegate
		{
		};

		public event EventHandler<AbstractUpdateChatThreadTrustStatusNotificationEventArgs> OnChatThreadTrustStatusUpdate = delegate
		{
		};

		public event EventHandler<AbstractAddChatThreadNicknameNotificationEventArgs> OnChatThreadNicknameAdded = delegate
		{
		};

		public event EventHandler<AbstractRemoveChatThreadNicknameNotificationEventArgs> OnChatThreadNicknameRemoved = delegate
		{
		};

		public event EventHandler<AbstractClearMemberChatHistoryNotificationEventArgs> OnChatThreadHistoryCleared = delegate
		{
		};

		public event EventHandler<AbstractClearUnreadMessageCountNotificationEventArgs> OnUnreadMessageCountCleared = delegate
		{
		};

		public event EventHandler<AbstractAddFollowshipNotificationEventArgs> OnOfficialAccountFollowed = delegate
		{
		};

		public event EventHandler<AbstractRemoveFollowshipNotificationEventArgs> OnOfficialAccountUnfollowed = delegate
		{
		};

		public event EventHandler<AbstractAddAlertNotificationEventArgs> OnAlertAdded = delegate
		{
		};

		public event EventHandler<AbstractClearAlertNotificationEventArgs> OnAlertCleared = delegate
		{
		};

		public void Dispatch(BaseNotification notification)
		{
			Action action = GetDispatchFunc<AddChatThreadNotification>(notification, Dispatch) ?? GetDispatchFunc<AddChatThreadMembershipNotification>(notification, Dispatch) ?? GetDispatchFunc<AddChatThreadGagMessageNotification>(notification, Dispatch) ?? GetDispatchFunc<AddChatThreadGameEventMessageNotification>(notification, Dispatch) ?? GetDispatchFunc<AddChatThreadGameStateMessageNotification>(notification, Dispatch) ?? GetDispatchFunc<UpdateChatThreadGameStateMessageNotification>(notification, Dispatch) ?? GetDispatchFunc<AddChatThreadMemberListChangedMessageNotification>(notification, Dispatch) ?? GetDispatchFunc<AddChatThreadPhotoMessageNotification>(notification, Dispatch) ?? GetDispatchFunc<AddChatThreadStickerMessageNotification>(notification, Dispatch) ?? GetDispatchFunc<AddChatThreadTextMessageNotification>(notification, Dispatch) ?? GetDispatchFunc<AddChatThreadVideoMessageNotification>(notification, Dispatch) ?? GetDispatchFunc<RemoveChatThreadMembershipNotification>(notification, Dispatch) ?? GetDispatchFunc<AddFriendshipInvitationNotification>(notification, Dispatch) ?? GetDispatchFunc<AddFriendshipNotification>(notification, Dispatch) ?? GetDispatchFunc<RemoveFriendshipInvitationNotification>(notification, Dispatch) ?? GetDispatchFunc<RemoveFriendshipNotification>(notification, Dispatch) ?? GetDispatchFunc<RemoveFriendshipTrustNotification>(notification, Dispatch) ?? GetDispatchFunc<AddNicknameNotification>(notification, Dispatch) ?? GetDispatchFunc<RemoveNicknameNotification>(notification, Dispatch) ?? GetDispatchFunc<SetAvatarNotification>(notification, Dispatch) ?? GetDispatchFunc<UpdateChatThreadTrustStatusNotification>(notification, Dispatch) ?? GetDispatchFunc<AddChatThreadNicknameNotification>(notification, Dispatch) ?? GetDispatchFunc<RemoveChatThreadNicknameNotification>(notification, Dispatch) ?? GetDispatchFunc<ClearMemberChatHistoryNotification>(notification, Dispatch) ?? GetDispatchFunc<ClearUnreadMessageCountNotification>(notification, Dispatch) ?? GetDispatchFunc<AddFollowshipNotification>(notification, Dispatch) ?? GetDispatchFunc<RemoveFollowshipNotification>(notification, Dispatch) ?? GetDispatchFunc<AddAlertNotification>(notification, Dispatch) ?? GetDispatchFunc<ClearAlertNotification>(notification, Dispatch);
			action();
		}

		private static Action GetDispatchFunc<TNotification>(BaseNotification notification, Action<TNotification> dispatch) where TNotification : BaseNotification
		{
			TNotification castedNotification = notification as TNotification;
			return (castedNotification == null) ? null : ((Action)delegate
			{
				dispatch(castedNotification);
			});
		}

		private void Dispatch(AddChatThreadGagMessageNotification notification)
		{
			AddChatThreadGagMessageNotificationEventArgs e = new AddChatThreadGagMessageNotificationEventArgs(notification);
			this.OnChatThreadGagMessageAdded(this, e);
		}

		private void Dispatch(AddChatThreadMembershipNotification notification)
		{
			AddChatThreadMembershipNotificationEventArgs e = new AddChatThreadMembershipNotificationEventArgs(notification);
			this.OnChatThreadMembershipAdded(this, e);
		}

		private void Dispatch(AddChatThreadNotification notification)
		{
			AddChatThreadNotificationEventArgs e = new AddChatThreadNotificationEventArgs(notification);
			this.OnChatThreadAdded(this, e);
		}

		private void Dispatch(AddChatThreadPhotoMessageNotification notification)
		{
			AddChatThreadPhotoMessageNotificationEventArgs e = new AddChatThreadPhotoMessageNotificationEventArgs(notification);
			this.OnChatThreadPhotoMessageAdded(this, e);
		}

		private void Dispatch(AddChatThreadStickerMessageNotification notification)
		{
			AddChatThreadStickerMessageNotificationEventArgs e = new AddChatThreadStickerMessageNotificationEventArgs(notification);
			this.OnChatThreadStickerMessageAdded(this, e);
		}

		private void Dispatch(AddChatThreadTextMessageNotification notification)
		{
			AddChatThreadTextMessageNotificationEventArgs e = new AddChatThreadTextMessageNotificationEventArgs(notification);
			this.OnChatThreadTextMessageAdded(this, e);
		}

		private void Dispatch(AddChatThreadVideoMessageNotification notification)
		{
			AddChatThreadVideoMessageNotificationEventArgs e = new AddChatThreadVideoMessageNotificationEventArgs(notification);
			this.OnChatThreadVideoMessageAdded(this, e);
		}

		private void Dispatch(AddChatThreadMemberListChangedMessageNotification notification)
		{
			AddChatThreadMemberListChangedMessageNotificationEventArgs e = new AddChatThreadMemberListChangedMessageNotificationEventArgs(notification);
			this.OnChatThreadMemberListChangedMessageAdded(this, e);
		}

		private void Dispatch(AddChatThreadGameStateMessageNotification notification)
		{
			AddChatThreadGameStateMessageNotificationEventArgs e = new AddChatThreadGameStateMessageNotificationEventArgs(notification);
			this.OnChatThreadGameStateMessageAdded(this, e);
		}

		private void Dispatch(UpdateChatThreadGameStateMessageNotification notification)
		{
			UpdateChatThreadGameStateMessageNotificationEventArgs e = new UpdateChatThreadGameStateMessageNotificationEventArgs(notification);
			this.OnChatThreadGameStateMessageUpdated(this, e);
		}

		private void Dispatch(AddChatThreadGameEventMessageNotification notification)
		{
			AddChatThreadGameEventMessageNotificationEventArgs e = new AddChatThreadGameEventMessageNotificationEventArgs(notification);
			this.OnChatThreadGameEventMessageAdded(this, e);
		}

		private void Dispatch(AddFriendshipNotification notification)
		{
			AddFriendshipNotificationEventArgs e = new AddFriendshipNotificationEventArgs(notification);
			this.OnFriendshipAdded(this, e);
		}

		private void Dispatch(AddFriendshipInvitationNotification notification)
		{
			AddFriendshipInvitationNotificationEventArgs e = new AddFriendshipInvitationNotificationEventArgs(notification);
			this.OnFriendshipInvitationAdded(this, e);
		}

		private void Dispatch(AddNicknameNotification notification)
		{
			AddNicknameNotificationEventArgs e = new AddNicknameNotificationEventArgs(notification);
			this.OnNicknameAdded(this, e);
		}

		private void Dispatch(RemoveChatThreadMembershipNotification notification)
		{
			RemoveChatThreadMembershipNotificationEventArgs e = new RemoveChatThreadMembershipNotificationEventArgs(notification);
			this.OnChatThreadMembershipRemoved(this, e);
		}

		private void Dispatch(RemoveFriendshipInvitationNotification notification)
		{
			RemoveFriendshipInvitationNotificationEventArgs e = new RemoveFriendshipInvitationNotificationEventArgs(notification);
			this.OnFriendshipInvitationRemoved(this, e);
		}

		private void Dispatch(RemoveFriendshipNotification notification)
		{
			RemoveFriendshipNotificationEventArgs e = new RemoveFriendshipNotificationEventArgs(notification);
			this.OnFriendshipRemoved(this, e);
		}

		private void Dispatch(RemoveFriendshipTrustNotification notification)
		{
			RemoveFriendshipTrustNotificationEventArgs e = new RemoveFriendshipTrustNotificationEventArgs(notification);
			this.OnFriendshipTrustRemoved(this, e);
		}

		private void Dispatch(RemoveNicknameNotification notification)
		{
			RemoveNicknameNotificationEventArgs e = new RemoveNicknameNotificationEventArgs(notification);
			this.OnNicknameRemoved(this, e);
		}

		private void Dispatch(SetAvatarNotification notification)
		{
			SetAvatarNotificationEventArgs e = new SetAvatarNotificationEventArgs(notification);
			this.OnAvatarSet(this, e);
		}

		private void Dispatch(UpdateChatThreadTrustStatusNotification notification)
		{
			UpdateChatThreadTrustStatusNotificationEventArgs e = new UpdateChatThreadTrustStatusNotificationEventArgs(notification);
			this.OnChatThreadTrustStatusUpdate(this, e);
		}

		private void Dispatch(AddChatThreadNicknameNotification notification)
		{
			AddChatThreadNicknameNotificationEventArgs e = new AddChatThreadNicknameNotificationEventArgs(notification);
			this.OnChatThreadNicknameAdded(this, e);
		}

		private void Dispatch(RemoveChatThreadNicknameNotification notification)
		{
			RemoveChatThreadNicknameNotificationEventArgs e = new RemoveChatThreadNicknameNotificationEventArgs(notification);
			this.OnChatThreadNicknameRemoved(this, e);
		}

		private void Dispatch(ClearMemberChatHistoryNotification notification)
		{
			ClearMemberChatHistoryNotificationEventArgs e = new ClearMemberChatHistoryNotificationEventArgs(notification);
			this.OnChatThreadHistoryCleared(this, e);
		}

		private void Dispatch(ClearUnreadMessageCountNotification notification)
		{
			ClearUnreadMessageCountNotificationEventArgs e = new ClearUnreadMessageCountNotificationEventArgs(notification);
			this.OnUnreadMessageCountCleared(this, e);
		}

		private void Dispatch(AddFollowshipNotification notification)
		{
			AddFollowshipNotificationEventArgs e = new AddFollowshipNotificationEventArgs(notification);
			this.OnOfficialAccountFollowed(this, e);
		}

		private void Dispatch(RemoveFollowshipNotification notification)
		{
			RemoveFollowshipNotificationEventArgs e = new RemoveFollowshipNotificationEventArgs(notification);
			this.OnOfficialAccountUnfollowed(this, e);
		}

		private void Dispatch(AddAlertNotification notification)
		{
			AddAlertNotificationEventArgs e = new AddAlertNotificationEventArgs(notification);
			this.OnAlertAdded(this, e);
		}

		private void Dispatch(ClearAlertNotification notification)
		{
			ClearAlertNotificationEventArgs e = new ClearAlertNotificationEventArgs(notification);
			this.OnAlertCleared(this, e);
		}
	}
}
