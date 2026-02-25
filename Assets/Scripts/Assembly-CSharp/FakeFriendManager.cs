using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Disney.Mix.SDK;
using Mix;
using Mix.Connectivity;
using Mix.DeviceDb;
using Mix.FakeFriend.Datatypes;
using Mix.Localization;
using Mix.Session;
using Mix.Session.Extensions;
using Mix.Session.Local.Messages;
using Mix.Session.Local.Thread;
using UnityEngine;

public class FakeFriendManager : MonoSingleton<FakeFriendManager>
{
	private const string KEY_OA_PREFIX = "fakefriend_oa_messageid_prefix";

	private const string KEY_SCRIPT_DONE_TIME = "fakefriend_script_done_time";

	private const string KEY_TIME_INITED = "fakefriend_time_inited";

	private const string KEY_VISITED_CHAT = "fakefriend_visited_chat";

	private const string MESSAGE_PREFIX = "fakefriend_message_";

	private const string LAST_MESSAGE = "last_message";

	public static string OAID = "OA-72844169130089556";

	private IOfficialAccountChatThread officalAccountThread;

	private IOfficialAccount officalAccount;

	private SdkEvents eventGenerator = new SdkEvents();

	private SdkActions actionGenerator = new SdkActions();

	private bool officalAccountListenersAdded;

	public static string TYPE_ADD_FRIEND = "add_friend";

	public static string TYPE_FRIEND_BUTTON = "friend_button";

	public static string TYPE_CHAT_BACK = "chat_back";

	public static string TYPE_CHAT_INPUT = "chat_input";

	public static string TYPE_MEDIA_STICKER = "media_sticker";

	public static string TYPE_MEDIA_GAG = "media_gag";

	public static string TYPE_MEDIA_GAME = "media_game";

	public static string TYPE_MEDIA_UGC = "media_ugc";

	public FakeFriendData fakeFriend;

	public LocalChatMember fakeFriendAsChatMember;

	public FakeThread fakeThread;

	private IKeyValDatabaseApi databaseApi;

	private int pendingMessages;

	private int lastMessage;

	private Dictionary<string, bool> databaseCache = new Dictionary<string, bool>();

	private Dictionary<string, bool> messagesSent = new Dictionary<string, bool>();

	private List<int> messagesPending = new List<int>();

	private Dictionary<string, string> messages = new Dictionary<string, string>
	{
		{
			"fakefriend_message_" + 1,
			"customtokens.fakefriend.message_1"
		},
		{
			"fakefriend_message_" + 2,
			"customtokens.fakefriend.message_2"
		},
		{
			"fakefriend_message_" + 3,
			"customtokens.fakefriend.message_3"
		},
		{
			"fakefriend_message_" + 4,
			"customtokens.fakefriend.message_4"
		},
		{
			"fakefriend_message_" + 5,
			"customtokens.fakefriend.message_5"
		},
		{
			"fakefriend_message_" + 6,
			"customtokens.fakefriend.message_6"
		},
		{
			"fakefriend_message_" + 7,
			"customtokens.fakefriend.message_7"
		},
		{
			"fakefriend_message_" + 8,
			"customtokens.fakefriend.message_8"
		},
		{
			"fakefriend_message_" + 9,
			"customtokens.fakefriend.message_9"
		},
		{
			"fakefriend_message_" + 10,
			"customtokens.fakefriend.message_10"
		},
		{
			"fakefriend_message_" + 11,
			"customtokens.fakefriend.message_11"
		},
		{
			"fakefriend_message_" + 12,
			"customtokens.fakefriend.message_12"
		},
		{
			"fakefriend_message_" + 14,
			"customtokens.fakefriend.message_14"
		},
		{
			"fakefriend_message_" + 15,
			"customtokens.fakefriend.message_15"
		},
		{
			"fakefriend_message_" + 16,
			"customtokens.fakefriend.message_16"
		},
		{
			"fakefriend_message_" + 17,
			"customtokens.fakefriend.message_17"
		},
		{
			"fakefriend_message_" + 20,
			"customtokens.fakefriend.message_20"
		},
		{
			"fakefriend_message_" + 21,
			"customtokens.fakefriend.message_21"
		},
		{
			"fakefriend_message_" + 22,
			"customtokens.fakefriend.message_22"
		},
		{
			"fakefriend_message_" + 23,
			"customtokens.fakefriend.message_23"
		},
		{
			"fakefriend_message_" + 24,
			"customtokens.fakefriend.message_24"
		},
		{
			"fakefriend_message_" + 25,
			"customtokens.fakefriend.message_25"
		},
		{
			"fakefriend_message_" + 30,
			"customtokens.fakefriend.message_30"
		},
		{
			"fakefriend_message_" + 31,
			"customtokens.fakefriend.message_31"
		},
		{
			"fakefriend_message_" + 32,
			"customtokens.fakefriend.message_32"
		},
		{
			"fakefriend_message_" + 33,
			"customtokens.fakefriend.message_33"
		},
		{
			"fakefriend_message_" + 34,
			"customtokens.fakefriend.message_34"
		},
		{
			"fakefriend_message_" + 35,
			"customtokens.fakefriend.message_35"
		},
		{
			"fakefriend_message_" + 36,
			"customtokens.fakefriend.message_36"
		},
		{
			"fakefriend_message_" + 37,
			"customtokens.fakefriend.message_37"
		},
		{
			"fakefriend_message_" + 38,
			"customtokens.fakefriend.message_38"
		},
		{
			"fakefriend_message_" + 39,
			"customtokens.fakefriend.message_39"
		},
		{
			"fakefriend_message_" + 50,
			"customtokens.fakefriend.message_50"
		},
		{
			"fakefriend_message_" + 51,
			"customtokens.fakefriend.message_51"
		},
		{
			"fakefriend_message_" + 52,
			"customtokens.fakefriend.message_52"
		},
		{
			"fakefriend_message_" + 53,
			"customtokens.fakefriend.message_53"
		},
		{
			"fakefriend_message_" + 54,
			"customtokens.fakefriend.message_54"
		},
		{
			"fakefriend_message_" + 55,
			"customtokens.fakefriend.message_55"
		},
		{
			"fakefriend_message_" + 56,
			"customtokens.fakefriend.message_56"
		},
		{
			"fakefriend_message_" + 57,
			"customtokens.fakefriend.message_57"
		},
		{
			"fakefriend_message_" + 58,
			"customtokens.fakefriend.message_58"
		},
		{
			"fakefriend_message_" + 59,
			"customtokens.fakefriend.message_59"
		},
		{
			"fakefriend_message_" + 60,
			"customtokens.fakefriend.message_60"
		},
		{
			"fakefriend_message_" + 61,
			"customtokens.fakefriend.message_61"
		},
		{
			"fakefriend_message_" + 62,
			"customtokens.fakefriend.message_62"
		},
		{
			"fakefriend_message_" + 63,
			"customtokens.fakefriend.message_63"
		}
	};

	public string DisplayName { get; set; }

	public bool IAmSending { get; set; }

	public void Init(bool aSimple = false)
	{
		DisplayName = "Mixbot";
		IAmSending = false;
		if (aSimple)
		{
			return;
		}
		databaseApi = Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi;
		if (databaseApi == null || MixSession.Session == null)
		{
			return;
		}
		fakeFriend = (FakeFriendData)MixSession.User.Friends.FirstOrDefault((IFriend fr) => fr is FakeFriendData);
		fakeFriendAsChatMember = new LocalChatMember(fakeFriend);
		fakeThread = (FakeThread)MixSession.User.ChatThreadsFromUsers().FirstOrDefault((IChatThread thr) => thr is FakeThread);
		databaseCache["fakefriend_visited_chat"] = databaseApi.LoadUserValueAsBool("fakefriend_visited_chat");
		messagesSent.Clear();
		messagesPending.Clear();
		foreach (KeyValuePair<string, string> message in messages)
		{
			messagesSent.Add(message.Key, false);
		}
		List<string> list = new List<string>(messagesSent.Keys);
		foreach (string item in list)
		{
			messagesSent[item] = databaseApi.LoadUserValueAsBool(item, messagesSent[item]);
		}
		lastMessage = databaseApi.LoadUserValueAsInt("last_message");
		if (string.IsNullOrEmpty(databaseApi.LoadUserValue("fakefriend_time_inited")))
		{
			databaseApi.SaveUserValue("fakefriend_time_inited", DateTime.Now.ToString("O"));
			StartCoroutine(SendMessage(1, 0f));
		}
		MixSession.User.OnAddedToOfficialAccountChatThread += eventGenerator.AddEventHandler<AbstractAddedToOfficialAccountThreadEventArgs>(null, OnAddedToOAThread);
		MixSession.OnGotNewMessages += HandleOnGotNewMessages;
		GetOfficialAccount();
	}

	private void HandleOnGotNewMessages(bool success, bool areNewMessages)
	{
		if (this.IsNullOrDisposed() || !areNewMessages || !success || officalAccountThread == null)
		{
			return;
		}
		IEnumerable<IChatMessage> enumerable = officalAccountThread.ChatMessages.Skip(Math.Max(0, officalAccountThread.ChatMessages.Count() - 25));
		foreach (IChatMessage item in enumerable)
		{
			OnOAMessageAdded(item);
		}
	}

	private void OnAddedToOAThread(object obj, AbstractAddedToOfficialAccountThreadEventArgs args)
	{
		if (!this.IsNullOrDisposed() && args.ChatThread.OfficialAccount.AccountId.Equals(OAID))
		{
			officalAccountThread = args.ChatThread;
			AddThreadListeners();
		}
	}

	private void GetOfficialAccount()
	{
		officalAccount = null;
		RemoveThreadListeners();
		officalAccountThread = MixSession.User.OfficialAccountChatThreads.FirstOrDefault((IOfficialAccountChatThread x) => x.OfficialAccount.AccountId.Equals(OAID));
		if (officalAccountThread != null)
		{
			if (!messagesSent["fakefriend_message_" + 1])
			{
				LocalTextMessage message = new LocalTextMessage(fakeFriendAsChatMember.Id, ParseMessage(Singleton<Localizer>.Instance.getString(messages["fakefriend_message_" + 1]), null));
				fakeThread.FakeMessageIncoming(message);
			}
			officalAccount = officalAccountThread.OfficialAccount;
			KillScript();
			return;
		}
		if (messagesSent["fakefriend_message_" + 12])
		{
			FollowMixbotOA();
		}
		MixSession.User.GetAllOfficialAccounts(actionGenerator.CreateAction(delegate(IGetAllOfficialAccountsResult aResult)
		{
			if (aResult.Success)
			{
				officalAccount = aResult.OfficialAccounts.FirstOrDefault((IOfficialAccount x) => x.AccountId.Equals(OAID));
			}
		}));
	}

	private void AddThreadListeners()
	{
		if (officalAccountThread != null && !officalAccountListenersAdded)
		{
			IChatMessageRetriever chatMessageRetriever = officalAccountThread.CreateChatMessageRetriever(25);
			chatMessageRetriever.RetrieveMessages(actionGenerator.CreateAction<IRetrieveChatThreadMessagesResult>(OARetrieverCallback), actionGenerator.CreateAction<IRetrieveChatThreadMessagesResult>(OARetrieverCallback));
			officalAccountThread.OnTextMessageAdded += eventGenerator.AddEventHandler<AbstractChatThreadTextMessageAddedEventArgs>(officalAccountThread, OnOATextMessageAdded);
			officalAccountThread.OnStickerMessageAdded += eventGenerator.AddEventHandler<AbstractChatThreadStickerMessageAddedEventArgs>(officalAccountThread, OnOAStickerMessageAdded);
			officalAccountThread.OnPhotoMessageAdded += eventGenerator.AddEventHandler<AbstractChatThreadPhotoMessageAddedEventArgs>(officalAccountThread, OnOAPhotoMessageAdded);
			officalAccountThread.OnVideoMessageAdded += eventGenerator.AddEventHandler<AbstractChatThreadVideoMessageAddedEventArgs>(officalAccountThread, OnOAVideoMessageAdded);
			officalAccountListenersAdded = true;
		}
	}

	private void RemoveThreadListeners()
	{
		if (officalAccountThread != null && officalAccountListenersAdded)
		{
			officalAccountThread.OnTextMessageAdded -= eventGenerator.GetEventHandler<AbstractChatThreadTextMessageAddedEventArgs>(officalAccountThread, OnOATextMessageAdded);
			officalAccountThread.OnStickerMessageAdded -= eventGenerator.GetEventHandler<AbstractChatThreadStickerMessageAddedEventArgs>(officalAccountThread, OnOAStickerMessageAdded);
			officalAccountThread.OnPhotoMessageAdded -= eventGenerator.GetEventHandler<AbstractChatThreadPhotoMessageAddedEventArgs>(officalAccountThread, OnOAPhotoMessageAdded);
			officalAccountThread.OnVideoMessageAdded -= eventGenerator.GetEventHandler<AbstractChatThreadVideoMessageAddedEventArgs>(officalAccountThread, OnOAVideoMessageAdded);
			officalAccountListenersAdded = false;
		}
	}

	private void OARetrieverCallback(IRetrieveChatThreadMessagesResult aResult)
	{
		if (aResult == null || !aResult.Success)
		{
			return;
		}
		foreach (IChatMessage chatMessage in aResult.ChatMessages)
		{
			OnOAMessageAdded(chatMessage);
		}
	}

	private void OnOAMessageAdded(IChatMessage aMessage)
	{
		if (aMessage.IsMine() || !messagesSent["fakefriend_message_" + 12])
		{
			return;
		}
		string text = databaseApi.LoadUserValue("fakefriend_script_done_time");
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		DateTime value = DateTime.Parse(text).ToUniversalTime().Add(MixSession.Session.ServerTimeOffset);
		if (aMessage.TimeSent.CompareTo(value) < 0)
		{
			return;
		}
		string text2 = databaseApi.LoadUserValue("fakefriend_oa_messageid_prefix_" + aMessage.Id);
		if (text2 == null)
		{
			databaseApi.SaveUserValue("fakefriend_oa_messageid_prefix_" + aMessage.Id, "Sent");
			IChatMessage chatMessage = null;
			if (aMessage is ITextMessage)
			{
				chatMessage = new LocalTextMessage(fakeFriendAsChatMember.Id, ((ITextMessage)aMessage).Text, aMessage.TimeSent);
			}
			else if (aMessage is IStickerMessage)
			{
				chatMessage = new LocalStickerMessage(((IStickerMessage)aMessage).ContentId, fakeFriendAsChatMember.Id, aMessage.TimeSent);
			}
			else if (aMessage is IPhotoMessage)
			{
				chatMessage = new LocalPhotoMessage(aMessage.Id, fakeFriendAsChatMember.Id, aMessage.TimeSent);
			}
			else if (aMessage is IVideoMessage)
			{
				chatMessage = new LocalVideoMessage(aMessage.Id, fakeFriendAsChatMember.Id, aMessage.TimeSent);
			}
			if (chatMessage != null)
			{
				fakeThread.FakeMessageIncoming(chatMessage);
			}
		}
	}

	private void OnOATextMessageAdded(object s, AbstractChatThreadTextMessageAddedEventArgs args)
	{
		OnOAMessageAdded(args.Message);
	}

	private void OnOAStickerMessageAdded(object s, AbstractChatThreadStickerMessageAddedEventArgs args)
	{
		OnOAMessageAdded(args.Message);
	}

	private void OnOAPhotoMessageAdded(object s, AbstractChatThreadPhotoMessageAddedEventArgs args)
	{
		OnOAMessageAdded(args.Message);
	}

	private void OnOAVideoMessageAdded(object s, AbstractChatThreadVideoMessageAddedEventArgs args)
	{
		OnOAMessageAdded(args.Message);
	}

	public bool IsFake(IChatThread aThread)
	{
		Init(true);
		return aThread is FakeThread;
	}

	public bool IsFake(IFriend aUser)
	{
		Init(true);
		return aUser is FakeFriendData;
	}

	public void ChatVisited(IChatThread aChatThread)
	{
		if (IsFake(aChatThread))
		{
			databaseCache["fakefriend_visited_chat"] = true;
			databaseApi.SaveUserValueFromBool("fakefriend_visited_chat", true);
		}
	}

	public bool ProcessChatMessage(IChatThread aThread, IChatMessage aMessage, Action<bool> callback)
	{
		if (IsFake(aThread))
		{
			if (aMessage is LocalTextMessage)
			{
				LocalTextMessage txtMsg = (LocalTextMessage)aMessage;
				string text = txtMsg.Text;
				aThread.ModerateTextMessage(text, delegate(ITextModerationResult result)
				{
					if (result.Success)
					{
						txtMsg.Text = result.ModeratedText;
						txtMsg.Sent = true;
						MonoSingleton<FakeFriendManager>.Instance.MessageSent(aMessage);
					}
					StartCoroutine(RunCallback(result.Success, callback));
				});
			}
			else
			{
				if (MonoSingleton<ConnectionManager>.Instance.IsConnected)
				{
					MonoSingleton<FakeFriendManager>.Instance.MessageSent(aMessage);
				}
				StartCoroutine(RunCallback(MonoSingleton<ConnectionManager>.Instance.IsConnected, callback));
			}
			if (officalAccountThread != null)
			{
				if (aMessage is ITextMessage)
				{
					officalAccountThread.SendTextMessage(((ITextMessage)aMessage).Text, actionGenerator.CreateAction<ISendTextMessageResult>(delegate
					{
					}));
				}
				else if (aMessage is IStickerMessage)
				{
					officalAccountThread.SendStickerMessage(((IStickerMessage)aMessage).ContentId, actionGenerator.CreateAction<ISendStickerMessageResult>(delegate
					{
					}));
				}
				else if (aMessage is IGameStateMessage)
				{
					IGameStateMessage gameStateMessage = (IGameStateMessage)aMessage;
					officalAccountThread.SendGameStateMessage(gameStateMessage.GameName, gameStateMessage.State, actionGenerator.CreateAction<ISendGameStateMessageResult>(delegate
					{
					}));
				}
			}
			return true;
		}
		return false;
	}

	public IEnumerator RunCallback(bool success, Action<bool> callback)
	{
		yield return new WaitForEndOfFrame();
		callback(success);
	}

	public void HighlightAnimator(string aType, Animator aAnimator, bool aDisable = true)
	{
		if (this.IsNullOrDisposed() || !MixSession.IsValidSession || MixSession.User == null || MixSession.User.Friends == null || aAnimator == null)
		{
			return;
		}
		bool flag = false;
		string stateName = "Highlight";
		if (aType.Equals(TYPE_ADD_FRIEND) && MixSession.User.Friends.Count() < 2)
		{
			stateName = (aDisable ? "idle" : "Highlight");
			flag = true;
		}
		if (aType.Equals(TYPE_FRIEND_BUTTON) && ShouldHighlightFriends())
		{
			flag = true;
		}
		if (aType.Equals(TYPE_CHAT_BACK) && ShouldHighlightChatBackButton())
		{
			flag = true;
		}
		if (aType.StartsWith("media") && ShouldHighlightMediaTray(aType))
		{
			stateName = (aDisable ? "idle" : "Highlight");
			flag = true;
		}
		if (aType.Equals(TYPE_CHAT_INPUT) && ShouldHighlightChatBar())
		{
			flag = true;
			stateName = "Highlight";
		}
		if (flag)
		{
			aAnimator.enabled = true;
			if (!aAnimator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
			{
				aAnimator.Play(stateName);
			}
		}
		else if (aDisable)
		{
			aAnimator.enabled = false;
		}
		else if (!aAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
		{
			aAnimator.Play("idle");
		}
	}

	public bool ShouldHighlightChatBar()
	{
		bool flag = messagesSent["fakefriend_message_" + 2];
		bool flag2 = messagesSent["fakefriend_message_" + 5];
		bool flag3 = messagesSent["fakefriend_message_" + 7];
		bool flag4 = messagesSent["fakefriend_message_" + 9];
		bool flag5 = messagesSent["fakefriend_message_" + 10];
		bool flag6 = messagesSent["fakefriend_message_" + 11];
		bool flag7 = messagesSent["fakefriend_message_" + 12];
		if (!flag && !flag2 && !flag3 && !flag4 && !flag5)
		{
			return true;
		}
		if (flag6 && !flag7)
		{
			return true;
		}
		return false;
	}

	public bool ShouldHighlightChatBackButton()
	{
		if (!MixSession.IsValidSession || databaseApi == null)
		{
			return false;
		}
		return messagesSent["fakefriend_message_" + 12] && MixSession.User.Friends.Count() < 2;
	}

	public bool ShouldHighlightMediaTray(string aType)
	{
		bool flag = messagesSent["fakefriend_message_" + 4];
		bool flag2 = messagesSent["fakefriend_message_" + 5];
		bool flag3 = messagesSent["fakefriend_message_" + 7];
		bool flag4 = messagesSent["fakefriend_message_" + 9];
		bool flag5 = messagesSent["fakefriend_message_" + 11];
		if (lastMessage > 0 && !flag5)
		{
			if (lastMessage == 14 && aType.Equals(TYPE_MEDIA_STICKER))
			{
				return true;
			}
			if (lastMessage == 15 && aType.Equals(TYPE_MEDIA_GAG))
			{
				return true;
			}
			if (lastMessage == 16 && aType.Equals(TYPE_MEDIA_GAME))
			{
				return true;
			}
		}
		if (flag && !flag2 && !flag3 && !flag4 && aType.Equals(TYPE_MEDIA_STICKER))
		{
			return true;
		}
		return false;
	}

	public bool ShouldHighlightFriends()
	{
		if (this.IsNullOrDisposed() || !MixSession.IsValidSession || MixSession.User == null || MixSession.User.Friends == null || databaseApi == null || !databaseCache.ContainsKey("fakefriend_visited_chat"))
		{
			return false;
		}
		return MixSession.User.Friends.Count() < 2 && databaseCache["fakefriend_visited_chat"];
	}

	public void MessageSent(IChatMessage aMessage)
	{
		if (ShouldSendMessageTwo(aMessage))
		{
			StartCoroutine(SendMessage(2, 1.5f, aMessage));
			StartCoroutine(SendMessage(3, 3f));
			StartCoroutine(SendMessage(4, 4.5f));
		}
		else if (ShouldSendMessageFiveSevenNineOrTen(aMessage))
		{
			float aDelay = 4.5f;
			if (aMessage is IStickerMessage)
			{
				if (messagesSent["fakefriend_message_" + 5])
				{
					aDelay = 1.5f;
				}
				else
				{
					StartCoroutine(SendMessage(5));
					StartCoroutine(SendMessage(6, 3f));
				}
			}
			else if (aMessage is IGagMessage)
			{
				if (messagesSent["fakefriend_message_" + 7])
				{
					aDelay = 1.5f;
				}
				else
				{
					StartCoroutine(SendMessage(7));
					StartCoroutine(SendMessage(8, 5f));
					aDelay = 10f;
				}
			}
			else if (aMessage is IGameStateMessage)
			{
				if (messagesSent["fakefriend_message_" + 9])
				{
					aDelay = 1.5f;
				}
				else
				{
					StartCoroutine(SendMessage(9));
					aDelay = 3f;
				}
			}
			else if (aMessage is IVideoMessage || aMessage is IPhotoMessage)
			{
				if (messagesSent["fakefriend_message_" + 10])
				{
					aDelay = 1.5f;
				}
				else
				{
					StartCoroutine(SendMessage(10));
					aDelay = 3f;
				}
			}
			int nextMessageToSend = GetNextMessageToSend();
			if (nextMessageToSend > 0)
			{
				StartCoroutine(SendMessage(nextMessageToSend, aDelay));
			}
		}
		else if (ShouldSendMessageTwelve(aMessage))
		{
			StartCoroutine(SendMessage(12));
		}
		else if (ShouldSendRandomReply())
		{
			int aMessageId = ((UnityEngine.Random.Range(0, 100) <= 80) ? UnityEngine.Random.Range(50, 64) : UnityEngine.Random.Range(30, 37));
			StartCoroutine(SendMessage(aMessageId, 1.5f, null, true));
		}
	}

	private int GetNextMessageToSend()
	{
		if (!messagesSent["fakefriend_message_" + 5] && !messagesPending.Contains(5))
		{
			return (!messagesSent["fakefriend_message_" + 14]) ? 14 : 0;
		}
		if (!messagesSent["fakefriend_message_" + 7] && !messagesPending.Contains(7))
		{
			return (!messagesSent["fakefriend_message_" + 15]) ? 15 : 0;
		}
		if (!messagesSent["fakefriend_message_" + 9] && !messagesPending.Contains(9))
		{
			return (!messagesSent["fakefriend_message_" + 16]) ? 16 : 0;
		}
		if (!messagesSent["fakefriend_message_" + 11])
		{
			return 11;
		}
		return 0;
	}

	private bool ShouldSendRandomReply()
	{
		return false;
	}

	private bool ShouldSendMessageTwelve(IChatMessage aMessage)
	{
		if (messagesSent["fakefriend_message_" + 12])
		{
			return false;
		}
		if (!messagesSent["fakefriend_message_" + 11])
		{
			return false;
		}
		return true;
	}

	private bool ShouldSendMessageFiveSevenNineOrTen(IChatMessage aMessage)
	{
		if ((aMessage is IStickerMessage && messagesSent["fakefriend_message_" + 5]) || (aMessage is IGagMessage && messagesSent["fakefriend_message_" + 7]) || (aMessage is IGameStateMessage && messagesSent["fakefriend_message_" + 9]) || ((aMessage is IVideoMessage || aMessage is IPhotoMessage) && messagesSent["fakefriend_message_" + 10]))
		{
			return false;
		}
		if (aMessage is ITextMessage)
		{
			return false;
		}
		return true;
	}

	private bool ShouldSendMessageTwo(IChatMessage aMessage)
	{
		if (messagesSent["fakefriend_message_" + 2] || messagesSent["fakefriend_message_" + 5] || messagesSent["fakefriend_message_" + 7] || messagesSent["fakefriend_message_" + 9] || messagesSent["fakefriend_message_" + 10])
		{
			return false;
		}
		if (!(aMessage is ITextMessage))
		{
			return false;
		}
		return true;
	}

	public void CheckTimeBasedMessages()
	{
		if (databaseApi != null && MixSession.IsValidSession)
		{
			string text = databaseApi.LoadUserValue("fakefriend_time_inited");
			DateTime dateTime = DateTime.Now;
			if (!string.IsNullOrEmpty(text))
			{
				dateTime = DateTime.ParseExact(text, "O", CultureInfo.InvariantCulture);
			}
			if (MixSession.User.Friends.Count() < 2 && dateTime.AddHours(12.0) < DateTime.Now)
			{
				StartCoroutine(SendMessage(21, 0f));
			}
			if (MixSession.User.Friends.Count() < 2 && dateTime.AddHours(24.0) < DateTime.Now)
			{
				StartCoroutine(SendMessage(22, 0f));
			}
			if (dateTime.AddHours(24.0) < DateTime.Now && !messagesSent["fakefriend_message_" + 12])
			{
				KillScript();
			}
			if (MixSession.User.Friends.Count() < 2 && dateTime.AddHours(72.0) < DateTime.Now)
			{
				StartCoroutine(SendMessage(23, 0f));
			}
			if (MixSession.User.Friends.Count() < 2 && dateTime.AddDays(7.0) < DateTime.Now)
			{
				StartCoroutine(SendMessage(24, 0f));
			}
			if (MixSession.User.Friends.Count() < 2 && dateTime.AddDays(14.0) < DateTime.Now)
			{
				StartCoroutine(SendMessage(25, 0f));
			}
		}
	}

	private string ParseMessage(string aMessage, IChatMessage aLastMessage)
	{
		Regex regex = new Regex("\\[Y:[^\\]]+\\]");
		Regex regex2 = new Regex("\\[N:[^\\]]+\\]");
		bool flag = true;
		if (aLastMessage != null && aLastMessage is ITextMessage)
		{
			ITextMessage textMessage = (ITextMessage)aLastMessage;
			if (textMessage != null)
			{
				flag = !textMessage.Text.ToLower().StartsWith("n");
			}
		}
		string text = ((!flag) ? regex.Replace(aMessage, string.Empty) : regex2.Replace(aMessage, string.Empty));
		return text.Replace("[Y:", string.Empty).Replace("[N:", string.Empty).Replace("]", string.Empty);
	}

	private IEnumerator SendMessage(int aMessageId, float aDelay = 1.5f, IChatMessage aLastMessage = null, bool aAllowMultiple = false)
	{
		pendingMessages++;
		messagesPending.Add(aMessageId);
		string messageKey = "fakefriend_message_" + aMessageId;
		yield return new WaitForSeconds(aDelay);
		if (!aAllowMultiple && messagesSent[messageKey])
		{
			pendingMessages--;
			messagesPending.Remove(aMessageId);
			yield break;
		}
		IAmSending = true;
		databaseApi.SaveUserValueFromBool(messageKey, true);
		messagesSent[messageKey] = true;
		if (aMessageId == 12)
		{
			KillScript();
		}
		if (aMessageId >= 14 && aMessageId <= 17)
		{
			databaseApi.SaveUserValueFromInt("last_message", aMessageId);
			lastMessage = aMessageId;
		}
		switch (aMessageId)
		{
		case 3:
		case 6:
		case 30:
		case 31:
		case 32:
		case 33:
		case 34:
		case 35:
		case 36:
		case 37:
		case 38:
		case 39:
		{
			LocalStickerMessage message3 = new LocalStickerMessage(Singleton<Localizer>.Instance.getString(messages[messageKey]), fakeFriendAsChatMember.Id);
			fakeThread.FakeMessageIncoming(message3);
			break;
		}
		default:
			if (aMessageId == 8)
			{
				LocalGagMessage message = new LocalGagMessage(Singleton<Localizer>.Instance.getString(messages[messageKey]), null, fakeFriendAsChatMember.Id);
				fakeThread.FakeMessageIncoming(message);
			}
			else
			{
				LocalTextMessage message2 = new LocalTextMessage(fakeFriendAsChatMember.Id, ParseMessage(Singleton<Localizer>.Instance.getString(messages[messageKey]), aLastMessage));
				fakeThread.FakeMessageIncoming(message2);
			}
			break;
		}
		IAmSending = false;
		pendingMessages--;
		messagesPending.Remove(aMessageId);
	}

	private void KillScript()
	{
		for (int i = 1; i <= 12; i++)
		{
			databaseApi.SaveUserValueFromBool("fakefriend_message_" + i, true);
			messagesSent["fakefriend_message_" + i] = true;
		}
		if (string.IsNullOrEmpty(databaseApi.LoadUserValue("fakefriend_script_done_time")))
		{
			databaseApi.SaveUserValue("fakefriend_script_done_time", DateTime.UtcNow.ToString("u"));
		}
		FollowMixbotOA();
	}

	private void FollowMixbotOA()
	{
		officalAccountThread = MixSession.User.OfficialAccountChatThreads.FirstOrDefault((IOfficialAccountChatThread x) => x.OfficialAccount.AccountId.Equals(OAID));
		if (officalAccountThread != null)
		{
			AddThreadListeners();
		}
		else
		{
			if (officalAccount == null)
			{
				return;
			}
			MixSession.User.FollowOfficialAccount(officalAccount, actionGenerator.CreateAction(delegate(IFollowOfficialAccountResult aResult)
			{
				if (aResult.Success)
				{
					Analytics.LogFollowOfficialAccount(officalAccount);
					AddThreadListeners();
				}
			}));
		}
	}
}
