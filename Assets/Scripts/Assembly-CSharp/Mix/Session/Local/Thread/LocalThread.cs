using System;
using System.Collections;
using System.Collections.Generic;
using Disney.Mix.SDK;
using Mix.FakeFriend.Results;
using Mix.Session.Local.Messages;
using Mix.Session.Local.Results;
using UnityEngine;

namespace Mix.Session.Local.Thread
{
	public abstract class LocalThread : IChatThread
	{
		protected class ActionAndCallback
		{
			public Action<bool, Action<bool>> sendAction;

			public Action<bool> resendCallback;

			public bool isPendingSend;

			public ActionAndCallback(Action<bool, Action<bool>> aAction, Action<bool> aInvokeCallback)
			{
				sendAction = aAction;
				resendCallback = aInvokeCallback;
				isPendingSend = true;
			}
		}

		public delegate void ThreadCreated(object sender);

		protected ILocalUser User;

		private LocalTrustLevel trustLevel;

		private LocalThreadNickname nickname;

		protected IEnumerable<IFriend> members;

		private List<IRemoteChatMember> membersAsUsers = new List<IRemoteChatMember>();

		private List<IChatMessage> messages;

		protected bool IsCreating;

		protected Dictionary<IChatMessage, ActionAndCallback> localSends;

		public IChatThread realThread;

		protected SdkActions actionGenerator = new SdkActions();

		public IChatThreadTrustLevel TrustLevel
		{
			get
			{
				IChatThreadTrustLevel result;
				if (realThread == null)
				{
					IChatThreadTrustLevel chatThreadTrustLevel = trustLevel;
					result = chatThreadTrustLevel;
				}
				else
				{
					result = realThread.TrustLevel;
				}
				return result;
			}
		}

		public IEnumerable<IChatMessage> ChatMessages
		{
			get
			{
				IEnumerable<IChatMessage> result;
				if (realThread == null)
				{
					IEnumerable<IChatMessage> enumerable = messages;
					result = enumerable;
				}
				else
				{
					result = realThread.ChatMessages;
				}
				return result;
			}
		}

		public IChatThreadNickname Nickname
		{
			get
			{
				IChatThreadNickname result;
				if (realThread == null)
				{
					IChatThreadNickname chatThreadNickname = nickname;
					result = chatThreadNickname;
				}
				else
				{
					result = GetThreadNickname(realThread);
				}
				return result;
			}
		}

		public string Id
		{
			get
			{
				return (realThread != null) ? realThread.Id : "localId";
			}
		}

		public IEnumerable<IRemoteChatMember> RemoteMembers
		{
			get
			{
				if (realThread == null)
				{
					return membersAsUsers;
				}
				return realThread.RemoteMembers;
			}
		}

		public IEnumerable<IRemoteChatMember> FormerRemoteMembers
		{
			get
			{
				if (realThread == null)
				{
					return new List<IRemoteChatMember>();
				}
				return realThread.FormerRemoteMembers;
			}
		}

		public uint UnreadMessageCount
		{
			get
			{
				if (realThread == null)
				{
					return 0u;
				}
				return realThread.UnreadMessageCount;
			}
		}

		public event ThreadCreated OnThreadCreated = delegate
		{
		};

		public event EventHandler<AbstractChatThreadTrustStatusChangedEventArgs> OnTrustStatusChanged = delegate
		{
		};

		public event EventHandler<AbstractChatThreadNicknameChangedEventArgs> OnNicknameChanged = delegate
		{
		};

		public event EventHandler<AbstractChatThreadMemberAddedEventArgs> OnMemberAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadMemberRemovedEventArgs> OnMemberRemoved = delegate
		{
		};

		public event EventHandler<AbstractChatThreadMemberAddedMessageAddedEventArgs> OnMemberAddedMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadMemberRemovedMessageAddedEventArgs> OnMemberRemovedMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadStickerMessageAddedEventArgs> OnStickerMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadTextMessageAddedEventArgs> OnTextMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadGameStateMessageAddedEventArgs> OnGameStateMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadGameEventMessageAddedEventArgs> OnGameEventMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadGameStateMessageUpdatedEventArgs> OnGameStateMessageUpdated = delegate
		{
		};

		public event EventHandler<AbstractChatThreadGagMessageAddedEventArgs> OnGagMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadPhotoMessageAddedEventArgs> OnPhotoMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadVideoMessageAddedEventArgs> OnVideoMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractUnreadMessageCountChangedEventArgs> OnUnreadMessageCountChanged = delegate
		{
		};

		public LocalThread(ILocalUser user, IEnumerable<IFriend> members)
		{
			User = user;
			trustLevel = new LocalTrustLevel();
			trustLevel.AllMembersTrustEachOther = false;
			nickname = new LocalThreadNickname();
			this.members = members;
			foreach (IFriend member in members)
			{
				membersAsUsers.Add(new LocalChatMember(member));
			}
			messages = new List<IChatMessage>();
			localSends = new Dictionary<IChatMessage, ActionAndCallback>();
		}

		public IChatThreadNickname GetThreadNickname(IChatThread aIChatThread)
		{
			if (aIChatThread is IOneOnOneChatThread)
			{
				return ((IOneOnOneChatThread)aIChatThread).Nickname;
			}
			if (aIChatThread is IGroupChatThread)
			{
				return ((IGroupChatThread)aIChatThread).Nickname;
			}
			return null;
		}

		public IChatMessageRetriever CreateChatMessageRetriever(int maximumMessageCount)
		{
			if (realThread != null)
			{
				return realThread.CreateChatMessageRetriever(maximumMessageCount);
			}
			return new LocalChatMessageRetriever(maximumMessageCount, new List<IChatMessage>());
		}

		public IChatThreadNickname SetNickname(string nickname, Action<ISetChatThreadNicknameResult> callback)
		{
			if (realThread != null)
			{
				if (realThread is IGroupChatThread)
				{
					return ((IGroupChatThread)realThread).SetNickname(nickname, callback);
				}
				if (realThread is IOneOnOneChatThread)
				{
					return ((IOneOnOneChatThread)realThread).SetNickname(nickname, callback);
				}
				return null;
			}
			return null;
		}

		public void RemoveNickname(Action<IRemoveChatThreadNicknameResult> callback)
		{
			if (realThread != null)
			{
				if (realThread is IGroupChatThread)
				{
					((IGroupChatThread)realThread).RemoveNickname(callback);
				}
				else if (realThread is IOneOnOneChatThread)
				{
					((IOneOnOneChatThread)realThread).RemoveNickname(callback);
				}
			}
			else
			{
				callback(null);
			}
		}

		protected void handlePreSendResponse<T, U>(T fakeMsg, U message) where T : LocalChatMessage<U>, ILocalMessageReference<U> where U : IChatMessage
		{
			fakeMsg.SdkReference = message;
		}

		protected void handleSendResponse<T, U, V>(T fakeMsg, U message, V res, bool success, Action<bool> resendCallback, Action<V> callback) where T : LocalChatMessage<U>, ILocalMessageReference<U> where U : IChatMessage
		{
			fakeMsg.Id = message.Id;
			fakeMsg.Sent = success;
			fakeMsg.TimeSent = message.TimeSent;
			fakeMsg.SdkReference = message;
			if (success)
			{
				localSends.Remove(fakeMsg);
			}
			if (resendCallback != null)
			{
				resendCallback(success);
			}
			else
			{
				callback(res);
			}
		}

		protected void handleFailedThreadCreation<T>(Action<bool> resendCallback, Action<T> callback)
		{
			if (resendCallback != null)
			{
				resendCallback(false);
			}
			else
			{
				callback(default(T));
			}
		}

		public ITextMessage SendTextMessage(string text, Action<ISendTextMessageResult> callback)
		{
			if (realThread != null)
			{
				return realThread.SendTextMessage(text, callback);
			}
			LocalTextMessage fakeMsg = new LocalTextMessage(User.Id, text);
			localSends[fakeMsg] = new ActionAndCallback(delegate(bool result, Action<bool> resendCallback)
			{
				if (result)
				{
					ITextMessage message = null;
					message = realThread.SendTextMessage(text, delegate(ISendTextMessageResult res)
					{
						handleSendResponse(fakeMsg, message, res, res.Success, resendCallback, callback);
					});
					handlePreSendResponse(fakeMsg, message);
				}
				else
				{
					handleFailedThreadCreation(resendCallback, callback);
				}
			}, null);
			CreateThread(InternalSetRealThread);
			return fakeMsg;
		}

		public IStickerMessage SendStickerMessage(string contentId, Action<ISendStickerMessageResult> callback)
		{
			if (realThread != null)
			{
				return realThread.SendStickerMessage(contentId, callback);
			}
			LocalStickerMessage fakeMsg = new LocalStickerMessage(contentId, User.Id);
			localSends[fakeMsg] = new ActionAndCallback(delegate(bool result, Action<bool> resendCallback)
			{
				if (result)
				{
					IStickerMessage message = null;
					message = realThread.SendStickerMessage(contentId, delegate(ISendStickerMessageResult res)
					{
						handleSendResponse(fakeMsg, message, res, res.Success, resendCallback, callback);
					});
					handlePreSendResponse(fakeMsg, message);
				}
				else
				{
					handleFailedThreadCreation(resendCallback, callback);
				}
			}, null);
			CreateThread(InternalSetRealThread);
			return fakeMsg;
		}

		public IGameStateMessage SendGameStateMessage(string gameName, Dictionary<string, object> payload, Action<ISendGameStateMessageResult> callback)
		{
			if (realThread != null)
			{
				if (realThread is IGroupChatThread)
				{
					return ((IGroupChatThread)realThread).SendGameStateMessage(gameName, payload, callback);
				}
				if (realThread is IOneOnOneChatThread)
				{
					return ((IOneOnOneChatThread)realThread).SendGameStateMessage(gameName, payload, callback);
				}
				return null;
			}
			LocalGameStateMessage fakeMsg = new LocalGameStateMessage(gameName, payload, User.Id);
			localSends[fakeMsg] = new ActionAndCallback(delegate(bool result, Action<bool> resendCallback)
			{
				if (result)
				{
					IGameStateMessage message = null;
					if (realThread is IGroupChatThread)
					{
						message = ((IGroupChatThread)realThread).SendGameStateMessage(gameName, payload, delegate(ISendGameStateMessageResult res)
						{
							handleSendResponse(fakeMsg, message, res, res.Success, resendCallback, callback);
						});
						handlePreSendResponse(fakeMsg, message);
					}
					if (realThread is IOneOnOneChatThread)
					{
						message = ((IOneOnOneChatThread)realThread).SendGameStateMessage(gameName, payload, delegate(ISendGameStateMessageResult res)
						{
							handleSendResponse(fakeMsg, message, res, res.Success, resendCallback, callback);
						});
						handlePreSendResponse(fakeMsg, message);
					}
				}
				else
				{
					handleFailedThreadCreation(resendCallback, callback);
				}
			}, null);
			CreateThread(InternalSetRealThread);
			return fakeMsg;
		}

		public void UpdateGameStateMessage(IGameStateMessage message, Dictionary<string, object> payload, Action<IUpdateGameStateMessageResult> callback)
		{
			if (realThread != null)
			{
				if (realThread is IGroupChatThread)
				{
					((IGroupChatThread)realThread).UpdateGameStateMessage(message, payload, callback);
				}
				if (realThread is IOneOnOneChatThread)
				{
					((IOneOnOneChatThread)realThread).UpdateGameStateMessage(message, payload, callback);
				}
				if (!(realThread is IOfficialAccountChatThread))
				{
				}
				return;
			}
			localSends[message] = new ActionAndCallback(delegate(bool result, Action<bool> resendCallback)
			{
				if (result)
				{
					if (realThread is IGroupChatThread)
					{
						((IGroupChatThread)realThread).UpdateGameStateMessage(message, payload, delegate(IUpdateGameStateMessageResult res)
						{
							callback(res);
						});
					}
					if (realThread is IOneOnOneChatThread)
					{
						((IOneOnOneChatThread)realThread).UpdateGameStateMessage(message, payload, delegate(IUpdateGameStateMessageResult res)
						{
							callback(res);
						});
					}
				}
				else
				{
					callback(null);
				}
			}, null);
		}

		public IPhotoMessage SendPhotoMessage(string photoPath, PhotoEncoding encoding, int width, int height, Action<ISendPhotoMessageResult> callback)
		{
			if (realThread != null)
			{
				return realThread.SendPhotoMessage(photoPath, encoding, width, height, callback);
			}
			LocalPhotoMessage fakeMsg = new LocalPhotoMessage(photoPath, encoding, width, height, User.Id);
			localSends[fakeMsg] = new ActionAndCallback(delegate(bool result, Action<bool> resendCallback)
			{
				if (result)
				{
					IPhotoMessage message = null;
					message = realThread.SendPhotoMessage(photoPath, encoding, width, height, delegate(ISendPhotoMessageResult res)
					{
						handleSendResponse(fakeMsg, message, res, res.Success, resendCallback, callback);
					});
					handlePreSendResponse(fakeMsg, message);
				}
				else
				{
					handleFailedThreadCreation(resendCallback, callback);
				}
			}, null);
			CreateThread(InternalSetRealThread);
			return fakeMsg;
		}

		public IVideoMessage SendVideoMessage(string videoPath, VideoFormat format, int bitrate, int duration, int videoWidth, int videoHeight, string thumbnailPath, PhotoEncoding thumbnailEncoding, int thumbnailWidth, int thumbnailHeight, Action<ISendVideoMessageResult> callback)
		{
			if (realThread != null)
			{
				return realThread.SendVideoMessage(videoPath, format, bitrate, duration, videoWidth, videoHeight, thumbnailPath, thumbnailEncoding, thumbnailWidth, thumbnailHeight, callback);
			}
			LocalVideoMessage fakeMsg = new LocalVideoMessage(videoPath, format, bitrate, duration, videoWidth, videoHeight, thumbnailPath, thumbnailEncoding, thumbnailWidth, thumbnailHeight, User.Id);
			localSends[fakeMsg] = new ActionAndCallback(delegate(bool result, Action<bool> resendCallback)
			{
				if (result)
				{
					IVideoMessage message = null;
					message = realThread.SendVideoMessage(videoPath, format, bitrate, duration, videoWidth, videoHeight, thumbnailPath, thumbnailEncoding, thumbnailWidth, thumbnailHeight, delegate(ISendVideoMessageResult res)
					{
						handleSendResponse(fakeMsg, message, res, res.Success, resendCallback, callback);
					});
					handlePreSendResponse(fakeMsg, message);
				}
				else
				{
					handleFailedThreadCreation(resendCallback, callback);
				}
			}, null);
			CreateThread(InternalSetRealThread);
			return fakeMsg;
		}

		public void ModerateTextMessage(string text, Action<ITextModerationResult> callback)
		{
			if (realThread != null)
			{
				realThread.ModerateTextMessage(text, callback);
			}
			else
			{
				User.ModerateText(text, false, callback);
			}
		}

		public void ResendChatMessage(IChatMessage message, Action<IResendChatMessageResult> callback)
		{
			if (realThread != null)
			{
				if (localSends.ContainsKey(message))
				{
					localSends[message].sendAction(true, delegate(bool didSend)
					{
						callback(new FakeResendChatMessageResult(didSend));
					});
				}
				else
				{
					realThread.ResendChatMessage(message, callback);
				}
			}
			else
			{
				localSends[message].resendCallback = delegate(bool didSend)
				{
					callback(new FakeResendChatMessageResult(didSend));
				};
				localSends[message].isPendingSend = true;
				CreateThread(InternalSetRealThread);
			}
		}

		public void ClearUnreadMessageCount(Action<IClearUnreadMessageCountResult> callback)
		{
			if (realThread != null)
			{
				realThread.ClearUnreadMessageCount(delegate(IClearUnreadMessageCountResult e)
				{
					callback(e);
				});
			}
		}

		public void GetGameStateMessage(IGameEventMessage message, Action<IGetGameStateMessageResult> callback)
		{
			if (realThread != null)
			{
				if (realThread is IGroupChatThread)
				{
					((IGroupChatThread)realThread).GetGameStateMessage(message, callback);
				}
				if (realThread is IOneOnOneChatThread)
				{
					((IOneOnOneChatThread)realThread).GetGameStateMessage(message, callback);
				}
				if (!(realThread is IOfficialAccountChatThread))
				{
				}
			}
		}

		protected abstract void CreateThread(Action<IChatThread> newThread);

		protected void InternalSetRealThread(IChatThread chatThread)
		{
			SetRealThread(chatThread);
		}

		public void SetRealThread(IChatThread thread)
		{
			if (this == null || thread == null || (realThread != null && thread.Equals(realThread)))
			{
				return;
			}
			realThread = thread;
			if (realThread is IOneOnOneChatThread)
			{
				((IOneOnOneChatThread)thread).OnGagMessageAdded += delegate(object sender, AbstractChatThreadGagMessageAddedEventArgs e)
				{
					this.OnGagMessageAdded(this, e);
				};
				((IOneOnOneChatThread)thread).OnNicknameChanged += delegate(object sender, AbstractChatThreadNicknameChangedEventArgs e)
				{
					this.OnNicknameChanged(this, e);
				};
			}
			if (realThread is IGroupChatThread)
			{
				((IGroupChatThread)thread).OnGagMessageAdded += delegate(object sender, AbstractChatThreadGagMessageAddedEventArgs e)
				{
					this.OnGagMessageAdded(this, e);
				};
				((IGroupChatThread)thread).OnNicknameChanged += delegate(object sender, AbstractChatThreadNicknameChangedEventArgs e)
				{
					this.OnNicknameChanged(this, e);
				};
			}
			thread.OnGameEventMessageAdded += delegate(object sender, AbstractChatThreadGameEventMessageAddedEventArgs e)
			{
				this.OnGameEventMessageAdded(this, e);
			};
			thread.OnGameStateMessageAdded += delegate(object sender, AbstractChatThreadGameStateMessageAddedEventArgs e)
			{
				this.OnGameStateMessageAdded(this, e);
			};
			thread.OnMemberAdded += delegate(object sender, AbstractChatThreadMemberAddedEventArgs e)
			{
				this.OnMemberAdded(this, e);
			};
			thread.OnMemberAddedMessageAdded += delegate(object sender, AbstractChatThreadMemberAddedMessageAddedEventArgs e)
			{
				this.OnMemberAddedMessageAdded(this, e);
			};
			thread.OnMemberRemoved += delegate(object sender, AbstractChatThreadMemberRemovedEventArgs e)
			{
				this.OnMemberRemoved(this, e);
			};
			thread.OnMemberRemovedMessageAdded += delegate(object sender, AbstractChatThreadMemberRemovedMessageAddedEventArgs e)
			{
				this.OnMemberRemovedMessageAdded(this, e);
			};
			thread.OnPhotoMessageAdded += delegate(object sender, AbstractChatThreadPhotoMessageAddedEventArgs e)
			{
				this.OnPhotoMessageAdded(this, e);
			};
			thread.OnStickerMessageAdded += delegate(object sender, AbstractChatThreadStickerMessageAddedEventArgs e)
			{
				this.OnStickerMessageAdded(this, e);
			};
			thread.OnTextMessageAdded += delegate(object sender, AbstractChatThreadTextMessageAddedEventArgs e)
			{
				this.OnTextMessageAdded(this, e);
			};
			thread.OnTrustStatusChanged += delegate(object sender, AbstractChatThreadTrustStatusChangedEventArgs e)
			{
				this.OnTrustStatusChanged(this, e);
			};
			thread.OnVideoMessageAdded += delegate(object sender, AbstractChatThreadVideoMessageAddedEventArgs e)
			{
				this.OnVideoMessageAdded(this, e);
			};
			MonoSingleton<ChatPrimerManager>.Instance.StartCoroutine(SendPendingLocal());
			this.OnThreadCreated(this);
		}

		public IEnumerator SendPendingLocal()
		{
			List<IChatMessage> keys = new List<IChatMessage>(localSends.Keys);
			foreach (IChatMessage key in keys)
			{
				if (localSends.ContainsKey(key))
				{
					ActionAndCallback message = localSends[key];
					if (message.isPendingSend)
					{
						message.sendAction(true, message.resendCallback);
						message.isPendingSend = false;
					}
				}
				yield return new WaitForSeconds(0.5f);
			}
		}

		public IChatThread GetRealThread()
		{
			return realThread;
		}

		public void AddLocalThreadMembers(IEnumerable<IFriend> membersToAdd, Action<IAddChatThreadMemberResult> callback)
		{
			List<IFriend> list = new List<IFriend>();
			foreach (IFriend member in members)
			{
				list.Add(member);
			}
			List<Action> list2 = new List<Action>();
			foreach (IFriend item in membersToAdd)
			{
				list.Add(item);
				LocalChatMember newMemberAsChatMember = new LocalChatMember(item);
				membersAsUsers.Add(new LocalChatMember(item));
				list2.Add(delegate
				{
					this.OnMemberAdded(this, new LocalChatThreadMemberAddedArgs(newMemberAsChatMember.Id));
				});
			}
			members = list;
			callback(new LocalAddChatThreadMemberResult());
			foreach (Action item2 in list2)
			{
				item2();
			}
		}
	}
}
