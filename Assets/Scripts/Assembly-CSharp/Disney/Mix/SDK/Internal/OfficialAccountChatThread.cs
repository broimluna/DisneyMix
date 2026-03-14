using System;
using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public class OfficialAccountChatThread : AbstractChatThread, IInternalChatThread, IInternalOfficialAccountChatThread, IChatThread, IOfficialAccountChatThread
	{
		private readonly AbstractLogger logger;

		private readonly IUserDatabase userDatabase;

		private readonly INotificationQueue notificationQueue;

		public IOfficialAccount OfficialAccount { get; private set; }

		 IChatThreadTrustLevel IChatThread.TrustLevel
		{
			get
			{
				return base.TrustLevel;
			}
		}

		 IEnumerable<IChatMessage> IChatThread.ChatMessages
		{
			get
			{
				return base.ChatMessages;
			}
		}

		 IEnumerable<IRemoteChatMember> IChatThread.RemoteMembers
		{
			get
			{
				return base.RemoteMembers;
			}
		}

		 IEnumerable<IRemoteChatMember> IChatThread.FormerRemoteMembers
		{
			get
			{
				return base.FormerRemoteMembers;
			}
		}

		 uint IChatThread.UnreadMessageCount
		{
			get
			{
				return base.UnreadMessageCount;
			}
		}

		 string IChatThread.Id
		{
			get
			{
				return base.Id;
			}
		}

		public event EventHandler<AbstractChatHistoryClearedEventArgs> OnChatHistoryCleared = delegate
		{
		};

		public OfficialAccountChatThread(AbstractLogger logger, IChatMessageRetrieverFactory chatMessageRetrieverFactory, long chatThreadId, string hashedChatThreadId, IChatMessageSender messageSender, IOfficialAccount officialAccount, IList<IInternalRemoteChatMember> remoteMembers, IChatThreadTrustLevel trustLevel, string localUserId, long latestSequenceNumber, bool areSequenceNumbersIndexed, IChatMessageFactory chatMessageFactory, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler, IMixWebCallFactory mixWebCallFactory, INotificationQueue notificationQueue)
			: base(logger, chatMessageRetrieverFactory, chatThreadId, hashedChatThreadId, messageSender, remoteMembers, trustLevel, localUserId, latestSequenceNumber, areSequenceNumbersIndexed, chatMessageFactory, userDatabase, chatMessageParser, chatMessageHandler, mixWebCallFactory, notificationQueue)
		{
			this.logger = logger;
			this.userDatabase = userDatabase;
			this.notificationQueue = notificationQueue;
			OfficialAccount = officialAccount;
		}

		public void ClearChatHistory(Action<IClearChatHistoryResult> callback)
		{
			ChatHistoryClearer.ClearChatHistory(logger, notificationQueue, mixWebCallFactory, chatThreadId, delegate
			{
				try
				{
					messages.Clear();
					userDatabase.DeleteChatMessages(chatThreadId);
				}
				catch (Exception ex)
				{
					logger.Critical("Unhandled exception: " + ex);
					callback(new ClearChatHistoryResult(false));
					return;
				}
				callback(new ClearChatHistoryResult(true));
				this.OnChatHistoryCleared(this, new ChatHistoryClearedEventArgs());
			}, delegate
			{
				callback(new ClearChatHistoryResult(false));
			});
		}

		public void ClearChatHistory()
		{
			messages.Clear();
			this.OnChatHistoryCleared(this, new ChatHistoryClearedEventArgs());
		}

		public void OfficialAccountTextMessageSend(string text, Action<IOfficialAccountTextMessageSenderResult> callback)
		{
			OfficialAccountTextMessageSender.Publish(logger, OfficialAccount.AccountId, text, mixWebCallFactory, delegate
			{
				callback(new OfficialAccountTextMessageSenderResult(true));
			}, delegate
			{
				callback(new OfficialAccountTextMessageSenderResult(false));
			});
		}

		public void OfficialAccountPhotoMessageSend(string caption, Action<IOfficialAccountPhotoMessageSenderResult> callback)
		{
			OfficialAccountPhotoMessageSender.Publish(logger, OfficialAccount.AccountId, caption, mixWebCallFactory, delegate
			{
				callback(new OfficialAccountPhotoMessageSenderResult(true));
			}, delegate
			{
				callback(new OfficialAccountPhotoMessageSenderResult(false));
			});
		}

		public void OfficialAccountVideoMessageSend(string caption, Action<IOfficialAccountVideoMessageSenderResult> callback)
		{
			OfficialAccountVideoMessageSender.Publish(logger, OfficialAccount.AccountId, caption, mixWebCallFactory, delegate
			{
				callback(new OfficialAccountVideoMessageSenderResult(true));
			}, delegate
			{
				callback(new OfficialAccountVideoMessageSenderResult(false));
			});
		}

		 IChatMessageRetriever IChatThread.CreateChatMessageRetriever(int maximumMessageCount)
		{
			return CreateChatMessageRetriever(maximumMessageCount);
		}

		 IPhotoMessage IChatThread.SendPhotoMessage(string photoPath, PhotoEncoding encoding, int width, int height, Action<ISendPhotoMessageResult> callback)
		{
			return SendPhotoMessage(photoPath, encoding, width, height, callback);
		}

		 ITextMessage IChatThread.SendTextMessage(string text, Action<ISendTextMessageResult> callback)
		{
			return SendTextMessage(text, callback);
		}

		 IVideoMessage IChatThread.SendVideoMessage(string videoPath, VideoFormat format, int bitrate, int duration, int videoWidth, int videoHeight, string thumbnailPath, PhotoEncoding thumbnailEncoding, int thumbnailWidth, int thumbnailHeight, Action<ISendVideoMessageResult> callback)
		{
			return SendVideoMessage(videoPath, format, bitrate, duration, videoWidth, videoHeight, thumbnailPath, thumbnailEncoding, thumbnailWidth, thumbnailHeight, callback);
		}

		 IStickerMessage IChatThread.SendStickerMessage(string contentId, Action<ISendStickerMessageResult> callback)
		{
			return SendStickerMessage(contentId, callback);
		}

		 IGameStateMessage IChatThread.SendGameStateMessage(string gameName, Dictionary<string, object> payload, Action<ISendGameStateMessageResult> callback)
		{
			return SendGameStateMessage(gameName, payload, callback);
		}

		 void IChatThread.UpdateGameStateMessage(IGameStateMessage message, Dictionary<string, object> payload, Action<IUpdateGameStateMessageResult> callback)
		{
			UpdateGameStateMessage(message, payload, callback);
		}

		 void IChatThread.GetGameStateMessage(IGameEventMessage message, Action<IGetGameStateMessageResult> callback)
		{
			GetGameStateMessage(message, callback);
		}

		 void IChatThread.ResendChatMessage(IChatMessage message, Action<IResendChatMessageResult> callback)
		{
			ResendChatMessage(message, callback);
		}

		 void IChatThread.ModerateTextMessage(string text, Action<ITextModerationResult> callback)
		{
			ModerateTextMessage(text, callback);
		}

		 void IChatThread.ClearUnreadMessageCount(Action<IClearUnreadMessageCountResult> callback)
		{
			ClearUnreadMessageCount(callback);
		}

		 event EventHandler<AbstractChatThreadTrustStatusChangedEventArgs> IChatThread.OnTrustStatusChanged
		{
			add { base.OnTrustStatusChanged += value; }
			remove { base.OnTrustStatusChanged -= value; }
		}

		 event EventHandler<AbstractChatThreadMemberAddedEventArgs> IChatThread.OnMemberAdded
		{
			add { base.OnMemberAdded += value; }
			remove { base.OnMemberAdded -= value; }
		}

		 event EventHandler<AbstractChatThreadMemberRemovedEventArgs> IChatThread.OnMemberRemoved
		{
			add { base.OnMemberRemoved += value; }
			remove { base.OnMemberRemoved -= value; }
		}

		 event EventHandler<AbstractChatThreadMemberAddedMessageAddedEventArgs> IChatThread.OnMemberAddedMessageAdded
		{
			add { base.OnMemberAddedMessageAdded += value; }
			remove { base.OnMemberAddedMessageAdded -= value; }
		}

		 event EventHandler<AbstractChatThreadMemberRemovedMessageAddedEventArgs> IChatThread.OnMemberRemovedMessageAdded
		{
			add { base.OnMemberRemovedMessageAdded += value; }
			remove { base.OnMemberRemovedMessageAdded -= value; }
		}

		 event EventHandler<AbstractChatThreadStickerMessageAddedEventArgs> IChatThread.OnStickerMessageAdded
		{
			add { base.OnStickerMessageAdded += value; }
			remove { base.OnStickerMessageAdded -= value; }
		}

		 event EventHandler<AbstractChatThreadTextMessageAddedEventArgs> IChatThread.OnTextMessageAdded
		{
			add { base.OnTextMessageAdded += value; }
			remove { base.OnTextMessageAdded -= value; }
		}

		 event EventHandler<AbstractChatThreadPhotoMessageAddedEventArgs> IChatThread.OnPhotoMessageAdded
		{
			add { base.OnPhotoMessageAdded += value; }
			remove { base.OnPhotoMessageAdded -= value; }
		}

		 event EventHandler<AbstractChatThreadVideoMessageAddedEventArgs> IChatThread.OnVideoMessageAdded
		{
			add { base.OnVideoMessageAdded += value; }
			remove { base.OnVideoMessageAdded -= value; }
		}

		 event EventHandler<AbstractChatThreadGameStateMessageAddedEventArgs> IChatThread.OnGameStateMessageAdded
		{
			add { base.OnGameStateMessageAdded += value; }
			remove { base.OnGameStateMessageAdded -= value; }
		}

		 event EventHandler<AbstractChatThreadGameEventMessageAddedEventArgs> IChatThread.OnGameEventMessageAdded
		{
			add { base.OnGameEventMessageAdded += value; }
			remove { base.OnGameEventMessageAdded -= value; }
		}

		 event EventHandler<AbstractUnreadMessageCountChangedEventArgs> IChatThread.OnUnreadMessageCountChanged
		{
			add { base.OnUnreadMessageCountChanged += value; }
			remove { base.OnUnreadMessageCountChanged -= value; }
		}
	}
}
