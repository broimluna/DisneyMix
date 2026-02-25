using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DeviceDB;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class UserDatabase : IDisposable, IUserDatabase
	{
		private readonly IDocumentCollection<AlertDocument> alerts;

		private readonly IDocumentCollection<AvatarDocument> avatars;

		private readonly IDocumentCollection<ChatMemberDocument> chatMembers;

		private readonly IDocumentCollection<ChatThreadDocument> chatThreads;

		private readonly IDocumentCollection<FriendDocument> friends;

		private readonly IDocumentCollection<FriendInvitationDocument> friendInvitations;

		private readonly IDocumentCollection<UserDocument> users;

		private readonly byte[] encryptionKey;

		private readonly string dirPath;

		private readonly IEpochTime epochTime;

		private readonly IDocumentCollectionFactory documentCollectionFactory;

		private readonly DatabaseCorruptionHandler databaseCorruptionHandler;

		private readonly ICoroutineManager coroutineManager;

		private readonly IDictionary<long, Action> callbacks;

		private readonly IList<long> pendingIndexedThreads;

		private bool isIndexingThread;

		public UserDatabase(IDocumentCollection<AlertDocument> alerts, IDocumentCollection<AvatarDocument> avatars, IDocumentCollection<ChatMemberDocument> chatMembers, IDocumentCollection<ChatThreadDocument> chatThreads, IDocumentCollection<FriendDocument> friends, IDocumentCollection<FriendInvitationDocument> friendInvitations, IDocumentCollection<UserDocument> users, byte[] encryptionKey, string dirPath, IEpochTime epochTime, IDocumentCollectionFactory documentCollectionFactory, DatabaseCorruptionHandler databaseCorruptionHandler, ICoroutineManager coroutineManager)
		{
			this.alerts = alerts;
			this.avatars = avatars;
			this.chatMembers = chatMembers;
			this.chatThreads = chatThreads;
			this.friends = friends;
			this.friendInvitations = friendInvitations;
			this.users = users;
			this.encryptionKey = encryptionKey;
			this.dirPath = dirPath;
			this.epochTime = epochTime;
			this.documentCollectionFactory = documentCollectionFactory;
			this.databaseCorruptionHandler = databaseCorruptionHandler;
			this.coroutineManager = coroutineManager;
			callbacks = new Dictionary<long, Action>();
			pendingIndexedThreads = new List<long>();
		}

		public void Dispose()
		{
			alerts.Dispose();
			avatars.Dispose();
			chatMembers.Dispose();
			chatThreads.Dispose();
			friends.Dispose();
			friendInvitations.Dispose();
			users.Dispose();
		}

		public IInternalAlert GetAlert(long alertId)
		{
			AlertDocument alertDocument = GetAlertDocument(alertId);
			return new Alert(alertDocument);
		}

		private AlertDocument GetAlertDocument(long alertId)
		{
			try
			{
				return (from id in alerts.FindDocumentIdsEqual(AlertDocument.AlertIdFieldName, alertId)
					select alerts.Find(id)).FirstOrDefault();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public IList<IInternalAlert> GetAlerts()
		{
			try
			{
				return ((IEnumerable<AlertDocument>)alerts).Select((Func<AlertDocument, IInternalAlert>)((AlertDocument doc) => new Alert(doc))).ToList();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void AddAlert(IInternalAlert alert)
		{
			try
			{
				string type = AlertTypeUtils.ToString(alert.Type);
				string level = alert.Level.ToString();
				AlertDocument alertDocument = GetAlertDocument(alert.AlertId);
				if (alertDocument == null)
				{
					AlertDocument alertDocument2 = new AlertDocument();
					alertDocument2.AlertId = alert.AlertId;
					alertDocument2.Type = type;
					alertDocument2.Level = level;
					alertDocument = alertDocument2;
					alerts.Insert(alertDocument);
				}
				else
				{
					alertDocument.Type = type;
					alertDocument.Level = level;
					alerts.Update(alertDocument);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void RemoveAlert(long alertId)
		{
			try
			{
				uint[] array = EnumerateAlertIds(alertId).ToArray();
				foreach (uint documentId in array)
				{
					alerts.Delete(documentId);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		private IEnumerable<uint> EnumerateAlertIds(long alertId)
		{
			return alerts.FindDocumentIdsEqual(AlertDocument.AlertIdFieldName, alertId);
		}

		public AvatarDocument GetAvatar(long avatarId)
		{
			try
			{
				return (from id in avatars.FindDocumentIdsEqual(AvatarDocument.AvatarIdFieldName, avatarId)
					select avatars.Find(id)).FirstOrDefault();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void InsertAvatar(Disney.Mix.SDK.Internal.MixDomain.Avatar avatar)
		{
			try
			{
				AvatarDocument avatarDocument = new AvatarDocument();
				avatarDocument.AvatarId = avatar.AvatarId.Value;
				AvatarDocument document = avatarDocument;
				SetAvatarDocumentFields(document, avatar);
				avatars.Insert(document);
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void UpdateAvatar(Disney.Mix.SDK.Internal.MixDomain.Avatar avatar)
		{
			try
			{
				AvatarDocument avatar2 = GetAvatar(avatar.AvatarId.Value);
				if (avatar2 != null)
				{
					SetAvatarDocumentFields(avatar2, avatar);
					avatars.Update(avatar2);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void InsertOrUpdateAvatar(Disney.Mix.SDK.Internal.MixDomain.Avatar avatar)
		{
			try
			{
				AvatarDocument avatar2 = GetAvatar(avatar.AvatarId.Value);
				if (avatar2 == null)
				{
					InsertAvatar(avatar);
					return;
				}
				SetAvatarDocumentFields(avatar2, avatar);
				avatars.Update(avatar2);
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		private static void SetAvatarDocumentFields(AvatarDocument document, Disney.Mix.SDK.Internal.MixDomain.Avatar avatar)
		{
			document.AccessoryPropertySelectionKey = avatar.Accessory.SelectionKey;
			document.AccessoryPropertyTintIndex = avatar.Accessory.TintIndex.Value;
			document.AccessoryPropertyXOffset = avatar.Accessory.XOffset.Value;
			document.AccessoryPropertyYOffset = avatar.Accessory.YOffset.Value;
			document.BrowPropertySelectionKey = avatar.Brow.SelectionKey;
			document.BrowPropertyTintIndex = avatar.Brow.TintIndex.Value;
			document.BrowPropertyXOffset = avatar.Brow.XOffset.Value;
			document.BrowPropertyYOffset = avatar.Brow.YOffset.Value;
			document.CostumePropertySelectionKey = avatar.Costume.SelectionKey;
			document.CostumePropertyTintIndex = avatar.Costume.TintIndex.Value;
			document.CostumePropertyXOffset = avatar.Costume.XOffset.Value;
			document.CostumePropertyYOffset = avatar.Costume.YOffset.Value;
			document.EyesPropertySelectionKey = avatar.Eyes.SelectionKey;
			document.EyesPropertyTintIndex = avatar.Eyes.TintIndex.Value;
			document.EyesPropertyXOffset = avatar.Eyes.XOffset.Value;
			document.EyesPropertyYOffset = avatar.Eyes.YOffset.Value;
			document.HairPropertySelectionKey = avatar.Hair.SelectionKey;
			document.HairPropertyTintIndex = avatar.Hair.TintIndex.Value;
			document.HairPropertyXOffset = avatar.Hair.XOffset.Value;
			document.HairPropertyYOffset = avatar.Hair.YOffset.Value;
			document.NosePropertySelectionKey = avatar.Nose.SelectionKey;
			document.NosePropertyTintIndex = avatar.Nose.TintIndex.Value;
			document.NosePropertyXOffset = avatar.Nose.XOffset.Value;
			document.NosePropertyYOffset = avatar.Nose.YOffset.Value;
			document.MouthPropertySelectionKey = avatar.Mouth.SelectionKey;
			document.MouthPropertyTintIndex = avatar.Mouth.TintIndex.Value;
			document.MouthPropertyXOffset = avatar.Mouth.XOffset.Value;
			document.MouthPropertyYOffset = avatar.Mouth.YOffset.Value;
			document.SkinPropertySelectionKey = avatar.Skin.SelectionKey;
			document.SkinPropertyTintIndex = avatar.Skin.TintIndex.Value;
			document.SkinPropertyXOffset = avatar.Skin.XOffset.Value;
			document.SkinPropertyYOffset = avatar.Skin.YOffset.Value;
			if (avatar.Hat != null)
			{
				document.HatPropertySelectionKey = avatar.Hat.SelectionKey;
				document.HatPropertyTintIndex = avatar.Hat.TintIndex.Value;
				document.HatPropertyXOffset = avatar.Hat.XOffset.Value;
				document.HatPropertyYOffset = avatar.Hat.YOffset.Value;
			}
			int? slotId = avatar.SlotId;
			document.SlotId = (slotId.HasValue ? slotId.Value : 0);
		}

		public ChatMemberDocument[] GetMembersInThread(long chatThreadId)
		{
			try
			{
				return (from chatMemberDocId in chatMembers.FindDocumentIdsEqual(ChatMemberDocument.ChatThreadIdFieldName, chatThreadId)
					select chatMembers.Find(chatMemberDocId)).ToArray();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void InsertChatMember(long chatThreadId, string memberId)
		{
			try
			{
				if (!Contains(chatThreadId, memberId))
				{
					chatMembers.Insert(new ChatMemberDocument
					{
						ChatThreadId = chatThreadId,
						Swid = memberId
					});
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void ClearChatMembers()
		{
			try
			{
				chatMembers.Drop();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void DeleteChatMember(long chatThreadId, string memberId)
		{
			try
			{
				uint[] array = (from id in chatMembers.FindDocumentIdsEqual(ChatMemberDocument.ChatThreadIdFieldName, chatThreadId)
					select chatMembers.Find(id) into doc
					where doc.Swid == memberId
					select doc.Id).ToArray();
				uint[] array2 = array;
				foreach (uint documentId in array2)
				{
					chatMembers.Delete(documentId);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		private bool Contains(long chatThreadId, string memberId)
		{
			return (from id in chatMembers.FindDocumentIdsEqual(ChatMemberDocument.ChatThreadIdFieldName, chatThreadId)
				select chatMembers.Find(id)).Any((ChatMemberDocument doc) => doc.Swid == memberId);
		}

		public void InsertChatMessage(long chatThreadId, ChatMessageDocument document)
		{
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				if (document.ChatMessageId == 0L || !Contains(chatMessages, document.ChatMessageId))
				{
					chatMessages.Insert(document);
				}
			});
		}

		public void UpdateChatMessage(long chatThreadId, long chatMessageId, string payload)
		{
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				ChatMessageDocument chatMessage = GetChatMessage(chatMessages, chatMessageId);
				if (chatMessage != null)
				{
					chatMessage.Payload = payload;
					chatMessages.Update(chatMessage);
				}
			});
		}

		public void UpdateChatMessageSequenceNumbers(long chatThreadId, IList<long> sequenceNumberList)
		{
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				for (int i = 0; i < sequenceNumberList.Count; i += 2)
				{
					long chatMessageId = sequenceNumberList[i];
					long num = sequenceNumberList[i + 1];
					ChatMessageDocument chatMessage = GetChatMessage(chatMessages, chatMessageId);
					if (chatMessage != null && chatMessage.SequenceNumber != num)
					{
						chatMessage.SequenceNumber = num;
						chatMessages.Update(chatMessage);
					}
				}
				IOrderedEnumerable<ChatMessageDocument> orderedEnumerable = from id in chatMessages.FindDocumentIdsEqual(ChatMessageDocument.SequenceNumberFieldName, 0L)
					select chatMessages.Find(id) into d
					orderby d.Created descending
					select d;
				int num2 = -1;
				foreach (ChatMessageDocument item in orderedEnumerable)
				{
					item.SequenceNumber = num2;
					chatMessages.Update(item);
					num2--;
				}
			});
		}

		public ChatMessageDocument GetChatMessage(long chatThreadId, long chatMessageId)
		{
			ChatMessageDocument doc = null;
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				doc = GetChatMessage(chatMessages, chatMessageId);
			});
			return doc;
		}

		public void DeleteChatMessageDocument(long chatThreadId, uint documentId)
		{
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				chatMessages.Delete(documentId);
			});
		}

		public void DeleteChatMessages(long chatThreadId)
		{
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				chatMessages.Delete();
			});
		}

		public ChatMessageDocument[] GetMessagesCreatedBefore(long chatThreadId, long created)
		{
			ChatMessageDocument[] before = null;
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				before = (from id in chatMessages.FindDocumentIdsLessThanOrEqualTo(ChatMessageDocument.CreatedFieldName, created)
					select chatMessages.Find(id)).ToArray();
			});
			return before;
		}

		public ChatMessageDocument[] GetMessagesBeforeMaxSequenceNumberInclusive(long chatThreadId, int maxMessageCount)
		{
			ChatMessageDocument[] messages = new ChatMessageDocument[0];
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				uint? num = chatMessages.FindDocumentIdMax<long>(ChatMessageDocument.SequenceNumberFieldName);
				if (num.HasValue)
				{
					ChatMessageDocument chatMessageDocument = chatMessages.Find(num.Value);
					long sequenceNumber = chatMessageDocument.SequenceNumber;
					messages = GetMessagesBeforeSequenceNumberInclusive(chatMessages, sequenceNumber, maxMessageCount);
				}
			});
			return messages;
		}

		public ChatMessageDocument[] GetMessagesBeforeSequenceNumberInclusive(long chatThreadId, long maxSequenceNumber, int maxMessageCount)
		{
			ChatMessageDocument[] messages = null;
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				messages = GetMessagesBeforeSequenceNumberInclusive(chatMessages, maxSequenceNumber, maxMessageCount);
			});
			return messages;
		}

		private static ChatMessageDocument[] GetMessagesBeforeSequenceNumberInclusive(IDocumentCollection<ChatMessageDocument> chatMessages, long maxSequenceNumber, int maxMessageCount)
		{
			return (from id in chatMessages.FindDocumentIdsLessThanOrEqualTo(ChatMessageDocument.SequenceNumberFieldName, maxSequenceNumber)
				select chatMessages.Find(id)).Take(maxMessageCount).ToArray();
		}

		public void InsertAndUpdateChatMessages(long chatThreadId, IEnumerable<ChatMessageDocument> insertDocuments, IEnumerable<ChatMessageDocument> updateDocuments)
		{
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				foreach (ChatMessageDocument insertDocument in insertDocuments)
				{
					bool flag = insertDocument.ChatMessageId == 0;
					if (!flag)
					{
						ChatMessageDocument chatMessage = GetChatMessage(chatMessages, insertDocument.ChatMessageId);
						if (chatMessage == null)
						{
							flag = true;
						}
					}
					if (flag)
					{
						chatMessages.Insert(insertDocument);
					}
				}
				foreach (ChatMessageDocument updateDocument in updateDocuments)
				{
					ChatMessageDocument chatMessage2 = GetChatMessage(chatMessages, updateDocument.ChatMessageId);
					if (chatMessage2 != null)
					{
						chatMessage2.Payload = updateDocument.Payload;
						chatMessages.Update(chatMessage2);
					}
				}
			});
		}

		public bool ContainsZeroSequenceNumber(long chatThreadId)
		{
			bool containsZero = false;
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				containsZero = chatMessages.FindDocumentIdsEqual(ChatMessageDocument.SequenceNumberFieldName, 0L).Any();
			});
			return containsZero;
		}

		public void IndexSequenceNumberField(long chatThreadId)
		{
			CreateChatMessages(chatThreadId, delegate(IDocumentCollection<ChatMessageDocument> chatMessages)
			{
				chatMessages.IndexField(ChatMessageDocument.SequenceNumberFieldName);
			});
		}

		private static ChatMessageDocument GetChatMessage(IDocumentCollection<ChatMessageDocument> chatMessages, long chatMessageId)
		{
			return (from id in chatMessages.FindDocumentIdsEqual(ChatMessageDocument.ChatMessageIdFieldName, chatMessageId)
				select chatMessages.Find(id)).FirstOrDefault();
		}

		private static bool Contains(IDocumentCollection<ChatMessageDocument> chatMessages, long chatMessageId)
		{
			return chatMessages.FindDocumentIdsEqual(ChatMessageDocument.ChatMessageIdFieldName, chatMessageId).Any();
		}

		private void CreateChatMessages(long chatThreadId, Action<IDocumentCollection<ChatMessageDocument>> operation)
		{
			string path = HashedPathGenerator.GetPath(Path.Combine(dirPath, chatThreadId.ToString()), "ChatMessages");
			try
			{
				using (IDocumentCollection<ChatMessageDocument> documentCollection = documentCollectionFactory.CreateHighSecurityFileSystemCollection<ChatMessageDocument>(path, encryptionKey))
				{
					databaseCorruptionHandler.Add(documentCollection);
					operation(documentCollection);
					databaseCorruptionHandler.Remove(documentCollection);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public ChatThreadDocument[] GetAllChatThreadDocuments()
		{
			try
			{
				return chatThreads.ToArray();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public bool ContainsChatThread(long chatThreadId)
		{
			try
			{
				return chatThreads.FindDocumentIdsEqual(ChatThreadDocument.ChatThreadIdFieldName, chatThreadId).Any();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void InsertChatThread(ChatThreadDocument doc)
		{
			try
			{
				if (!ContainsChatThread(doc.ChatThreadId))
				{
					chatThreads.Insert(doc);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void UpdateChatThreadDocument(ChatThreadDocument doc)
		{
			try
			{
				chatThreads.Update(doc);
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void DeleteChatThreadDocument(ChatThreadDocument doc)
		{
			try
			{
				chatThreads.Delete(doc.Id);
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void SetChatThreadIsTrusted(long chatThreadId, bool isTrusted)
		{
			try
			{
				ChatThreadDocument chatThreadDocument = GetChatThreadDocument(chatThreadId);
				if (chatThreadDocument != null)
				{
					chatThreadDocument.IsTrusted = isTrusted;
					chatThreads.Update(chatThreadDocument);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void SetChatThreadNickname(long chatThreadId, string nickname)
		{
			try
			{
				ChatThreadDocument chatThreadDocument = GetChatThreadDocument(chatThreadId);
				if (chatThreadDocument != null)
				{
					chatThreadDocument.Nickname = nickname;
					chatThreads.Update(chatThreadDocument);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void SetChatThreadUnreadMessageCount(long chatThreadId, uint unreadMessageCount)
		{
			try
			{
				ChatThreadDocument chatThreadDocument = GetChatThreadDocument(chatThreadId);
				if (chatThreadDocument != null)
				{
					chatThreadDocument.UnreadMessageCount = unreadMessageCount;
					chatThreads.Update(chatThreadDocument);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void IncrementChatThreadUnreadMessageCountAndUpdateLatestSequenceNumber(long chatThreadId, long sequenceNumber)
		{
			try
			{
				ChatThreadDocument chatThreadDocument = GetChatThreadDocument(chatThreadId);
				if (chatThreadDocument != null)
				{
					chatThreadDocument.UnreadMessageCount++;
					if (sequenceNumber > chatThreadDocument.LatestSequenceNumber)
					{
						chatThreadDocument.LatestSequenceNumber = sequenceNumber;
					}
					chatThreads.Update(chatThreadDocument);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void UpdateLatestSequenceNumber(long chatThreadId, long sequenceNumber)
		{
			try
			{
				ChatThreadDocument chatThreadDocument = GetChatThreadDocument(chatThreadId);
				if (chatThreadDocument != null)
				{
					if (sequenceNumber > chatThreadDocument.LatestSequenceNumber)
					{
						chatThreadDocument.LatestSequenceNumber = sequenceNumber;
					}
					chatThreads.Update(chatThreadDocument);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void SetChatThreadSequenceNumbersIndexed(long chatThreadId)
		{
			try
			{
				ChatThreadDocument chatThreadDocument = GetChatThreadDocument(chatThreadId);
				if (chatThreadDocument != null)
				{
					chatThreadDocument.AreSequenceNumbersIndexed = true;
					chatThreads.Update(chatThreadDocument);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void DeleteChatThread(long chatThreadId)
		{
			try
			{
				ChatThreadDocument chatThreadDocument = GetChatThreadDocument(chatThreadId);
				if (chatThreadDocument != null)
				{
					chatThreads.Delete(chatThreadDocument.Id);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public ChatThreadDocument GetChatThreadDocument(long chatThreadId)
		{
			try
			{
				return (from id in chatThreads.FindDocumentIdsEqual(ChatThreadDocument.ChatThreadIdFieldName, chatThreadId)
					select chatThreads.Find(id)).FirstOrDefault();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public FriendDocument[] GetAllFriendDocuments()
		{
			try
			{
				return friends.ToArray();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void DeleteFriend(string swid)
		{
			try
			{
				uint[] array = EnumerateFriendDocumentIds(friends, swid).ToArray();
				foreach (uint documentId in array)
				{
					friends.Delete(documentId);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void SetFriendIsTrusted(string swid, bool isTrusted)
		{
			try
			{
				UpdateFriend(friends, swid, delegate(FriendDocument doc)
				{
					doc.IsTrusted = isTrusted;
				});
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void SetFriendNickname(string swid, string nickname)
		{
			try
			{
				UpdateFriend(friends, swid, delegate(FriendDocument doc)
				{
					doc.Nickname = nickname;
				});
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void InsertFriend(FriendDocument doc)
		{
			try
			{
				if (!Contains(friends, doc.Swid))
				{
					friends.Insert(doc);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		private static void UpdateFriend(IDocumentCollection<FriendDocument> friends, string swid, Action<FriendDocument> update)
		{
			FriendDocument[] array = (from id in EnumerateFriendDocumentIds(friends, swid)
				select friends.Find(id)).ToArray();
			foreach (FriendDocument friendDocument in array)
			{
				update(friendDocument);
				friends.Update(friendDocument);
			}
		}

		public void ClearFriends()
		{
			try
			{
				friends.Drop();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		private static IEnumerable<uint> EnumerateFriendDocumentIds(IDocumentCollection<FriendDocument> friends, string swid)
		{
			return friends.FindDocumentIdsEqual(FriendDocument.SwidFieldName, swid);
		}

		private static bool Contains(IDocumentCollection<FriendDocument> friends, string swid)
		{
			return friends.FindDocumentIdsEqual(FriendDocument.SwidFieldName, swid).Any();
		}

		public void DeleteFriendInvitation(long invitationId)
		{
			try
			{
				uint[] array = friendInvitations.FindDocumentIdsEqual(FriendInvitationDocument.FriendInvitationIdFieldName, invitationId).ToArray();
				uint[] array2 = array;
				foreach (uint documentId in array2)
				{
					friendInvitations.Delete(documentId);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void InsertFriendInvitation(FriendInvitationDocument doc)
		{
			try
			{
				friendInvitations.Insert(doc);
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void InsertOrUpdateFriendInvitation(FriendInvitationDocument doc)
		{
			try
			{
				FriendInvitationDocument friendInvitationDocument = (from id in friendInvitations.FindDocumentIdsEqual(FriendInvitationDocument.FriendInvitationIdFieldName, doc.FriendInvitationId)
					select friendInvitations.Find(id)).FirstOrDefault();
				if (friendInvitationDocument == null)
				{
					friendInvitations.Insert(doc);
					return;
				}
				doc.Id = friendInvitationDocument.Id;
				friendInvitations.Update(doc);
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void ClearFriendInvitations()
		{
			try
			{
				friendInvitations.Drop();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public FriendInvitationDocument[] GetFriendInvitationDocuments(bool isInviter)
		{
			try
			{
				return friendInvitations.Where((FriendInvitationDocument friendInvitationDoc) => friendInvitationDoc.IsInviter == isInviter).ToArray();
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void InsertOfficialAccount(string accountId, string name, bool isFollowing, bool isAvailable, bool canUnfollow)
		{
			CreateOfficialAccounts(delegate(IDocumentCollection<OfficialAccountDocument> officialAccounts)
			{
				if (!Contains(officialAccounts, accountId))
				{
					OfficialAccountDocument document = new OfficialAccountDocument(accountId, name, isFollowing, isAvailable, canUnfollow);
					officialAccounts.Insert(document);
				}
			});
		}

		public bool ContainsOfficialAccount(string accountId)
		{
			bool contains = false;
			CreateOfficialAccounts(delegate(IDocumentCollection<OfficialAccountDocument> officialAccounts)
			{
				contains = Contains(officialAccounts, accountId);
			});
			return contains;
		}

		public OfficialAccountDocument GetOfficialAccount(string accountId)
		{
			OfficialAccountDocument doc = null;
			CreateOfficialAccounts(delegate(IDocumentCollection<OfficialAccountDocument> officialAccounts)
			{
				doc = (from id in officialAccounts.FindDocumentIdsEqual(OfficialAccountDocument.AccountIdFieldName, accountId)
					select officialAccounts.Find(id)).FirstOrDefault();
			});
			return doc;
		}

		public OfficialAccountDocument[] GetAllOfficialAccounts()
		{
			OfficialAccountDocument[] allOfficialAccounts = null;
			CreateOfficialAccounts(delegate(IDocumentCollection<OfficialAccountDocument> officialAccounts)
			{
				allOfficialAccounts = officialAccounts.ToArray();
			});
			return allOfficialAccounts;
		}

		public void UpdateOfficialAccount(string accountId, Action<OfficialAccountDocument> updateCallback)
		{
			CreateOfficialAccounts(delegate(IDocumentCollection<OfficialAccountDocument> officialAccounts)
			{
				Update(officialAccounts, accountId, updateCallback);
			});
		}

		public void DeleteOfficialAccount(string accountId)
		{
			CreateOfficialAccounts(delegate(IDocumentCollection<OfficialAccountDocument> officialAccounts)
			{
				IEnumerable<uint> enumerable = officialAccounts.FindDocumentIdsEqual(OfficialAccountDocument.AccountIdFieldName, accountId);
				foreach (uint item in enumerable)
				{
					officialAccounts.Delete(item);
				}
			});
		}

		public void InsertOrUpdateOfficialAccounts(IEnumerable<GuestOfficialAccount> officialAccounts)
		{
			GuestOfficialAccount account;
			foreach (GuestOfficialAccount officialAccount2 in officialAccounts)
			{
				account = officialAccount2;
				string oaId = account.OaId;
				OfficialAccountDocument officialAccount = GetOfficialAccount(oaId);
				if (officialAccount == null)
				{
					InsertOfficialAccount(account.OaId, account.OaName, false, account.IsAvailable.Value, account.CanUnfollow.Value);
				}
				else if (officialAccount.DisplayName != account.OaName)
				{
					UpdateOfficialAccount(oaId, delegate(OfficialAccountDocument oaDoc)
					{
						oaDoc.DisplayName = account.OaName;
						oaDoc.LastUpdated = epochTime.RawMillisecondsSince(epochTime.UtcNow);
					});
				}
			}
		}

		private void CreateOfficialAccounts(Action<IDocumentCollection<OfficialAccountDocument>> operation)
		{
			string path = HashedPathGenerator.GetPath(dirPath, "OfficialAccounts");
			try
			{
				using (IDocumentCollection<OfficialAccountDocument> documentCollection = documentCollectionFactory.CreateHighSecurityFileSystemCollection<OfficialAccountDocument>(path, encryptionKey))
				{
					databaseCorruptionHandler.Add(documentCollection);
					operation(documentCollection);
					databaseCorruptionHandler.Remove(documentCollection);
				}
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		private static bool Contains(IDocumentCollection<OfficialAccountDocument> docs, string accountId)
		{
			return docs.FindDocumentIdsEqual(OfficialAccountDocument.AccountIdFieldName, accountId).Any();
		}

		private static void Update(IDocumentCollection<OfficialAccountDocument> docs, string accountId, Action<OfficialAccountDocument> updateCallback)
		{
			OfficialAccountDocument officialAccountDocument = (from id in docs.FindDocumentIdsEqual(OfficialAccountDocument.AccountIdFieldName, accountId)
				select docs.Find(id)).FirstOrDefault();
			if (officialAccountDocument != null)
			{
				updateCallback(officialAccountDocument);
			}
			docs.Update(officialAccountDocument);
		}

		public UserDocument GetUserBySwid(string swid)
		{
			try
			{
				return (swid != null) ? (from id in users.FindDocumentIdsEqual(UserDocument.SwidFieldName, swid)
					select users.Find(id)).FirstOrDefault() : null;
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public UserDocument GetUserByDisplayName(string displayName)
		{
			try
			{
				return (displayName != null) ? (from id in users.FindDocumentIdsEqual(UserDocument.DisplayNameFieldName, displayName)
					select users.Find(id)).FirstOrDefault() : null;
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void InsertUserDocument(UserDocument userDocument)
		{
			try
			{
				users.Insert(userDocument);
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void UpdateUserDocument(UserDocument userDocument)
		{
			try
			{
				users.Update(userDocument);
			}
			catch (CorruptionException ex)
			{
				databaseCorruptionHandler.HandleCorruption(ex);
				throw;
			}
		}

		public void PersistUser(string swid, string hashedSwid, string displayName, string firstName, Disney.Mix.SDK.Internal.MixDomain.Avatar avatar, string status)
		{
			UserDocument userDocumentBySwidOrDisplayName = GetUserDocumentBySwidOrDisplayName(swid, displayName);
			if (userDocumentBySwidOrDisplayName == null)
			{
				UserDocument userDocument = new UserDocument();
				userDocument.Swid = swid;
				userDocument.HashedSwid = hashedSwid;
				userDocument.DisplayName = displayName;
				userDocument.FirstName = firstName;
				userDocument.Status = status;
				userDocumentBySwidOrDisplayName = userDocument;
				if (avatar != null)
				{
					userDocumentBySwidOrDisplayName.AvatarId = avatar.AvatarId.Value;
				}
				InsertUserDocument(userDocumentBySwidOrDisplayName);
			}
			else
			{
				bool flag = false;
				if (swid != null && swid != userDocumentBySwidOrDisplayName.Swid)
				{
					userDocumentBySwidOrDisplayName.Swid = swid;
					flag = true;
				}
				if (hashedSwid != null && hashedSwid != userDocumentBySwidOrDisplayName.HashedSwid)
				{
					userDocumentBySwidOrDisplayName.HashedSwid = hashedSwid;
					flag = true;
				}
				if (displayName != null && displayName != userDocumentBySwidOrDisplayName.DisplayName)
				{
					userDocumentBySwidOrDisplayName.DisplayName = displayName;
					flag = true;
				}
				if (firstName != null && firstName != userDocumentBySwidOrDisplayName.FirstName)
				{
					userDocumentBySwidOrDisplayName.FirstName = firstName;
					flag = true;
				}
				if (avatar != null)
				{
					long? avatarId = avatar.AvatarId;
					if (avatarId.HasValue)
					{
						long? avatarId2 = avatar.AvatarId;
						if (avatarId2.GetValueOrDefault() != userDocumentBySwidOrDisplayName.AvatarId || !avatarId2.HasValue)
						{
							userDocumentBySwidOrDisplayName.AvatarId = avatar.AvatarId.Value;
							flag = true;
						}
					}
				}
				if (status != null && status != userDocumentBySwidOrDisplayName.Status)
				{
					userDocumentBySwidOrDisplayName.Status = status;
					flag = true;
				}
				if (flag)
				{
					UpdateUserDocument(userDocumentBySwidOrDisplayName);
				}
			}
			if (avatar != null)
			{
				InsertOrUpdateAvatar(avatar);
			}
		}

		private UserDocument GetUserDocumentBySwidOrDisplayName(string swid, string displayName)
		{
			UserDocument userDocument = null;
			if (swid != null)
			{
				userDocument = GetUserBySwid(swid);
			}
			if (userDocument == null && displayName != null)
			{
				userDocument = GetUserByDisplayName(displayName);
			}
			return userDocument;
		}

		public void SyncToGetStateResponse(GetStateResponse response, Action callback)
		{
			if (response.Alerts != null)
			{
				foreach (Disney.Mix.SDK.Internal.MixDomain.Alert alert in response.Alerts)
				{
					AddAlert(new Alert(alert));
				}
			}
			ClearChatMembers();
			if (response.ChatThreads != null)
			{
				foreach (ChatThread chatThread in response.ChatThreads)
				{
					long value = chatThread.ChatThreadId.Value;
					foreach (User member in chatThread.Members)
					{
						PersistUser(member.UserId, member.HashedUserId, member.DisplayName, member.FirstName, member.Avatar, member.Status);
						InsertChatMember(value, member.UserId);
					}
				}
			}
			if (response.ChatThreads != null)
			{
				List<ChatThreadDocument> list = GetAllChatThreadDocuments().ToList();
				List<Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname> chatThreadNicknames = response.ChatThreadNicknames;
				IDictionary<long, long> dictionary = SequenceNumberUtils.CreateSequenceNumberDictionary(response.ChatThreadLatestMessageSequenceNumbers);
				IDictionary<long, long> dictionary2 = SequenceNumberUtils.CreateSequenceNumberDictionary(response.ChatThreadLastSeenMessageSequenceNumbers);
				foreach (ChatThread chatThread2 in response.ChatThreads)
				{
					long chatThreadId = chatThread2.ChatThreadId.Value;
					string nickname = null;
					uint unreadMessageCount = 0u;
					if (chatThreadNicknames != null)
					{
						Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname chatThreadNickname = chatThreadNicknames.FirstOrDefault(delegate(Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname n)
						{
							long? chatThreadId3 = n.ChatThreadId;
							return chatThreadId3.GetValueOrDefault() == chatThreadId && chatThreadId3.HasValue;
						});
						if (chatThreadNickname != null)
						{
							nickname = chatThreadNickname.Nickname;
						}
					}
					long value2;
					if (dictionary.TryGetValue(chatThreadId, out value2))
					{
						long value3;
						dictionary2.TryGetValue(chatThreadId, out value3);
						unreadMessageCount = (uint)(value2 - value3);
					}
					ChatThreadDocument chatThreadDocument = list.FirstOrDefault((ChatThreadDocument d) => d.ChatThreadId == chatThreadId);
					if (chatThreadDocument != null)
					{
						list.Remove(chatThreadDocument);
						chatThreadDocument.IsTrusted = chatThread2.IsTrusted.Value;
						chatThreadDocument.Nickname = nickname;
						chatThreadDocument.UnreadMessageCount = unreadMessageCount;
						chatThreadDocument.LatestSequenceNumber = value2;
						UpdateChatThreadDocument(chatThreadDocument);
					}
					ChatThreadDatabaseType chatThreadType = ChatThreadTypeConverter.ConvertServerTypeToDatabaseType(chatThread2.ChatThreadType);
					ChatThreadDocument chatThreadDocument2 = new ChatThreadDocument();
					chatThreadDocument2.ChatThreadId = chatThreadId;
					chatThreadDocument2.ChatThreadType = (byte)chatThreadType;
					chatThreadDocument2.IsTrusted = chatThread2.IsTrusted.Value;
					chatThreadDocument2.UnreadMessageCount = unreadMessageCount;
					chatThreadDocument2.OfficialAccountId = chatThread2.OfficialAccountId;
					chatThreadDocument2.LatestSequenceNumber = value2;
					chatThreadDocument2.AreSequenceNumbersIndexed = true;
					chatThreadDocument2.Nickname = nickname;
					ChatThreadDocument doc = chatThreadDocument2;
					InsertChatThread(doc);
				}
				foreach (ChatThreadDocument item in list)
				{
					DeleteChatThreadDocument(item);
				}
			}
			ClearFriends();
			if (response.Friendships != null)
			{
				List<Disney.Mix.SDK.Internal.MixDomain.UserNickname> userNicknames = response.UserNicknames;
				foreach (Friendship friendship in response.Friendships)
				{
					string userId = friendship.FriendUserId;
					PersistUser(friendship.FriendUserId, null, null, null, null, null);
					string nickname2 = null;
					if (userNicknames != null)
					{
						Disney.Mix.SDK.Internal.MixDomain.UserNickname userNickname = userNicknames.FirstOrDefault((Disney.Mix.SDK.Internal.MixDomain.UserNickname n) => n.NicknamedUserId == userId);
						if (userNickname != null)
						{
							nickname2 = userNickname.Nickname;
						}
					}
					FriendDocument friendDocument = new FriendDocument();
					friendDocument.Swid = friendship.FriendUserId;
					friendDocument.IsTrusted = friendship.IsTrusted.Value;
					friendDocument.Nickname = nickname2;
					FriendDocument doc2 = friendDocument;
					InsertFriend(doc2);
				}
			}
			ClearFriendInvitations();
			if (response.FriendshipInvitations != null)
			{
				FriendshipInvitation friendshipInvitation;
				foreach (FriendshipInvitation friendshipInvitation2 in response.FriendshipInvitations)
				{
					friendshipInvitation = friendshipInvitation2;
					User user = response.Users.FirstOrDefault((User user2) => user2.DisplayName == friendshipInvitation.FriendDisplayName);
					if (user != null)
					{
						string firstName = user.FirstName;
						string userId2 = user.UserId;
						string hashedUserId = user.HashedUserId;
						Disney.Mix.SDK.Internal.MixDomain.Avatar avatar = user.Avatar;
						string status = user.Status;
						PersistUser(userId2, hashedUserId, friendshipInvitation.FriendDisplayName, firstName, avatar, status);
					}
					FriendInvitationDocument friendInvitationDocument = new FriendInvitationDocument();
					friendInvitationDocument.FriendInvitationId = friendshipInvitation.FriendshipInvitationId.Value;
					friendInvitationDocument.IsInviter = friendshipInvitation.IsInviter.Value;
					friendInvitationDocument.IsTrusted = friendshipInvitation.IsTrusted.Value;
					friendInvitationDocument.DisplayName = friendshipInvitation.FriendDisplayName;
					FriendInvitationDocument doc3 = friendInvitationDocument;
					InsertFriendInvitation(doc3);
				}
			}
			if (response.OfficialAccounts != null)
			{
				foreach (string officialAccount2 in response.OfficialAccounts)
				{
					OfficialAccountDocument officialAccount = GetOfficialAccount(officialAccount2);
					if (officialAccount != null)
					{
						long? timestamp = response.Timestamp;
						long lastUpdated = ((!timestamp.HasValue) ? epochTime.RawMillisecondsSince(epochTime.UtcNow) : timestamp.Value);
						UpdateOfficialAccount(officialAccount2, delegate(OfficialAccountDocument officialAccountDoc)
						{
							officialAccountDoc.IsFollowing = true;
							officialAccountDoc.LastUpdated = lastUpdated;
						});
					}
				}
			}
			if (response.Users != null)
			{
				foreach (User user2 in response.Users)
				{
					PersistUser(user2.UserId, user2.HashedUserId, user2.DisplayName, user2.FirstName, user2.Avatar, user2.Status);
				}
			}
			if (response.GameStateChatMessages != null)
			{
				int count = response.GameStateChatMessages.Count;
				if (count > 0)
				{
					int numCallbacks = 0;
					{
						foreach (GameStateChatMessage message in response.GameStateChatMessages)
						{
							string payload = ChatMessageDocumentPayloadFactory.Create(message);
							long chatThreadId2 = message.ChatThreadId.Value;
							IndexSequenceNumberField(chatThreadId2, delegate
							{
								UpdateChatMessage(chatThreadId2, message.ChatMessageId.Value, payload);
								numCallbacks++;
								if (numCallbacks == count)
								{
									callback();
								}
							});
						}
						return;
					}
				}
				callback();
			}
			else
			{
				callback();
			}
		}

		public void IndexSequenceNumberField(long chatThreadId, Action callback)
		{
			if (callbacks.ContainsKey(chatThreadId))
			{
				IDictionary<long, Action> dictionary2;
				IDictionary<long, Action> dictionary = (dictionary2 = callbacks);
				long key2;
				long key = (key2 = chatThreadId);
				Action a = dictionary2[key2];
				dictionary[key] = (Action)Delegate.Combine(a, callback);
				return;
			}
			ChatThreadDocument chatThreadDocument = GetChatThreadDocument(chatThreadId);
			if (chatThreadDocument != null && !chatThreadDocument.AreSequenceNumbersIndexed)
			{
				callbacks.Add(chatThreadId, callback);
				pendingIndexedThreads.Add(chatThreadId);
				if (!isIndexingThread)
				{
					isIndexingThread = true;
					coroutineManager.Start(IndexSequenceFieldAsync());
				}
			}
			else
			{
				callback();
			}
		}

		private IEnumerator IndexSequenceFieldAsync()
		{
			while (pendingIndexedThreads.Count > 0)
			{
				yield return null;
				long chatThreadId = pendingIndexedThreads[0];
				IndexSequenceNumberField(chatThreadId);
				SetChatThreadSequenceNumbersIndexed(chatThreadId);
				pendingIndexedThreads.RemoveAt(0);
				Action callback = callbacks[chatThreadId];
				callbacks.Remove(chatThreadId);
				callback();
			}
			isIndexingThread = false;
		}
	}
}
