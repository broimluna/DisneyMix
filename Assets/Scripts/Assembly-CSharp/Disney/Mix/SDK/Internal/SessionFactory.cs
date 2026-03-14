using System;
using System.Collections.Generic;
using System.Linq;
using DeviceDB;

namespace Disney.Mix.SDK.Internal
{
	public class SessionFactory : ISessionFactory
	{
		private readonly AbstractLogger logger;

		private readonly ICoroutineManager coroutineManager;

		private readonly IStopwatch pollCountdownStopwatch;

		private readonly IEpochTime epochTime;

		private readonly DatabaseCorruptionHandler databaseCorruptionHandler;

		private readonly INotificationQueue notificationQueue;

		private readonly INotificationDispatcher notificationDispatcher;

		private readonly ISessionStatus sessionStatus;

		private readonly IMixWebCallFactoryFactory mixWebCallFactoryFactory;

		private readonly IWebCallEncryptorFactory webCallEncryptorFactory;

		private readonly IMixSessionStarter mixSessionStarter;

		private readonly IKeychain keychain;

		private readonly ISessionRefresherFactory sessionRefresherFactory;

		private readonly IGuestControllerClientFactory guestControllerClientFactory;

		private readonly IRandom random;

		private readonly IEncryptor encryptor;

		private readonly IFileSystem fileSystem;

		private readonly IWwwCallFactory wwwCallFactory;

		private readonly string localStorageDirPath;

		private readonly string clientVersion;

		private readonly IDatabaseDirectoryCreator databaseDirectoryCreator;

		private readonly IDocumentCollectionFactory documentCollectionFactory;

		private readonly IDatabase database;

		public SessionFactory(AbstractLogger logger, ICoroutineManager coroutineManager, IStopwatch pollCountdownStopwatch, IEpochTime epochTime, DatabaseCorruptionHandler databaseCorruptionHandler, INotificationQueue notificationQueue, INotificationDispatcher notificationDispatcher, ISessionStatus sessionStatus, IMixWebCallFactoryFactory mixWebCallFactoryFactory, IWebCallEncryptorFactory webCallEncryptorFactory, IMixSessionStarter mixSessionStarter, IKeychain keychain, ISessionRefresherFactory sessionRefresherFactory, IGuestControllerClientFactory guestControllerClientFactory, IRandom random, IEncryptor encryptor, IFileSystem fileSystem, IWwwCallFactory wwwCallFactory, string localStorageDirPath, string clientVersion, IDatabaseDirectoryCreator databaseDirectoryCreator, IDocumentCollectionFactory documentCollectionFactory, IDatabase database)
		{
			this.logger = logger;
			this.coroutineManager = coroutineManager;
			this.pollCountdownStopwatch = pollCountdownStopwatch;
			this.epochTime = epochTime;
			this.databaseCorruptionHandler = databaseCorruptionHandler;
			this.notificationQueue = notificationQueue;
			this.notificationDispatcher = notificationDispatcher;
			this.sessionStatus = sessionStatus;
			this.mixWebCallFactoryFactory = mixWebCallFactoryFactory;
			this.webCallEncryptorFactory = webCallEncryptorFactory;
			this.mixSessionStarter = mixSessionStarter;
			this.keychain = keychain;
			this.sessionRefresherFactory = sessionRefresherFactory;
			this.guestControllerClientFactory = guestControllerClientFactory;
			this.random = random;
			this.encryptor = encryptor;
			this.fileSystem = fileSystem;
			this.wwwCallFactory = wwwCallFactory;
			this.localStorageDirPath = localStorageDirPath;
			this.clientVersion = clientVersion;
			this.databaseDirectoryCreator = databaseDirectoryCreator;
			this.documentCollectionFactory = documentCollectionFactory;
			this.database = database;
		}

		public IInternalSession Create(string swid)
		{
			byte[] localStorageKey = keychain.LocalStorageKey;
			IDocumentCollection<AlertDocument> documentCollection = GetDocumentCollection<AlertDocument>(swid, "Alerts", databaseDirectoryCreator, localStorageKey, documentCollectionFactory);
			IDocumentCollection<AvatarDocument> documentCollection2 = GetDocumentCollection<AvatarDocument>(swid, "Avatar", databaseDirectoryCreator, localStorageKey, documentCollectionFactory);
			IDocumentCollection<ChatMemberDocument> documentCollection3 = GetDocumentCollection<ChatMemberDocument>(swid, "ChatMembers", databaseDirectoryCreator, localStorageKey, documentCollectionFactory);
			IDocumentCollection<ChatThreadDocument> documentCollection4 = GetDocumentCollection<ChatThreadDocument>(swid, "ChatThreads", databaseDirectoryCreator, localStorageKey, documentCollectionFactory);
			IDocumentCollection<FriendDocument> documentCollection5 = GetDocumentCollection<FriendDocument>(swid, "Friends", databaseDirectoryCreator, localStorageKey, documentCollectionFactory);
			IDocumentCollection<FriendInvitationDocument> documentCollection6 = GetDocumentCollection<FriendInvitationDocument>(swid, "FriendInvitations", databaseDirectoryCreator, localStorageKey, documentCollectionFactory);
			IDocumentCollection<UserDocument> documentCollection7 = GetDocumentCollection<UserDocument>(swid, "Users", databaseDirectoryCreator, localStorageKey, documentCollectionFactory);
			databaseCorruptionHandler.Add(documentCollection7);
			string dirPath = BuildDocCollectionPath(databaseDirectoryCreator, swid);
			UserDatabase userDatabase = new UserDatabase(documentCollection, documentCollection2, documentCollection3, documentCollection4, documentCollection5, documentCollection6, documentCollection7, localStorageKey, dirPath, epochTime, documentCollectionFactory, databaseCorruptionHandler, coroutineManager);
			database.ClearServerTimeOffsetMillis();
			IEpochTime obj = epochTime;
			long? serverTimeOffsetMillis = database.GetServerTimeOffsetMillis();
			obj.OffsetMilliseconds = ((!serverTimeOffsetMillis.HasValue) ? 0 : serverTimeOffsetMillis.Value);
			SessionDocument sessionDocument = database.GetSessionDocument(swid);
			keychain.PushNotificationKey = sessionDocument.CurrentSymmetricEncryptionKey;
			IWebCallEncryptor webCallEncryptor = webCallEncryptorFactory.Create(sessionDocument.CurrentSymmetricEncryptionKey, sessionDocument.SessionId);
			IGuestControllerClient guestControllerClient = guestControllerClientFactory.Create(swid);
			ISessionRefresher sessionRefresher = sessionRefresherFactory.Create(mixSessionStarter, guestControllerClient);
			IMixWebCallFactory mixWebCallFactory = mixWebCallFactoryFactory.Create(webCallEncryptor, swid, sessionDocument.GuestControllerAccessToken, sessionRefresher);
			guestControllerClient.OnAccessTokenChanged += delegate(object sender, AbstractGuestControllerAccessTokenChangedEventArgs e)
			{
				mixWebCallFactory.GuestControllerAccessToken = e.GuestControllerAccessToken;
			};
			ChatMessageFactory chatMessageFactory = new ChatMessageFactory(epochTime, random);
			PhotoStorage photoStorage = new PhotoStorage(fileSystem, localStorageDirPath);
			AssetLoader assetLoader = new AssetLoader(logger, wwwCallFactory);
			PhotoFlavorFactory photoFlavorFactory = new PhotoFlavorFactory(logger, photoStorage, assetLoader);
			VideoFlavorUrlGetter videoFlavorUrlGetter = new VideoFlavorUrlGetter(logger);
			VideoFlavorFactory videoFlavorFactory = new VideoFlavorFactory(videoFlavorUrlGetter);
			ChatMessageParser chatMessageParser = new ChatMessageParser(logger, swid, chatMessageFactory, userDatabase, epochTime, photoFlavorFactory, videoFlavorFactory, mixWebCallFactory);
			ChatMessageHandler chatMessageHandler = new ChatMessageHandler(swid, userDatabase, epochTime);
			IList<IInternalFriend> friends = CreateFriends(userDatabase);
			ChatMessageRetrieverFactory chatMessageRetrieverFactory = new ChatMessageRetrieverFactory(logger, epochTime, sessionStatus);
			ChatMessageSender messageSender = new ChatMessageSender(logger, random, notificationQueue);
			ChatThreadFactory chatThreadFactory = new ChatThreadFactory(logger, chatMessageRetrieverFactory, messageSender, chatMessageFactory, notificationQueue);
			IList<IInternalOneOnOneChatThread> oneOnOneChatThreads;
			IList<IInternalGroupChatThread> groupChatThreads;
			IList<IInternalOfficialAccountChatThread> officialAccountChatThreads;
			CreateChatThreads(swid, userDatabase, mixWebCallFactory, chatMessageParser, sessionDocument, chatMessageHandler, chatThreadFactory, out oneOnOneChatThreads, out groupChatThreads, out officialAccountChatThreads);
			AgeBandType ageBandType = AgeBandTypeConverter.Convert(sessionDocument.AgeBand);
			DateTime lastRefreshTime = epochTime.FromSeconds(sessionDocument.LastProfileRefreshTime);
			RegistrationProfile registrationProfile = new RegistrationProfile(logger, sessionDocument.DisplayNameText, sessionDocument.ProposedDisplayName, sessionDocument.ProposedDisplayNameStatus, sessionDocument.FirstName, sessionDocument.AccountStatus, lastRefreshTime, sessionDocument.CountryCode);
			IList<IOfficialAccount> followships = CreateFollowships(userDatabase);
			GetStateResponseParser getStateResponseParser = new GetStateResponseParser(logger, chatThreadFactory);
			NotificationPoller notificationPoller = new NotificationPoller(logger, mixWebCallFactory, notificationQueue, pollCountdownStopwatch, getStateResponseParser, epochTime, random, database, swid);
			DisplayName displayName = new DisplayName(sessionDocument.DisplayNameText);
			ChatThreadCreator chatThreadCreator = new ChatThreadCreator(logger, mixWebCallFactory, chatThreadFactory, swid, userDatabase, chatMessageParser, chatMessageHandler, notificationQueue);
			LocalUser localUser = new LocalUser(logger, displayName, swid, chatThreadCreator, friends, oneOnOneChatThreads, groupChatThreads, officialAccountChatThreads, followships, ageBandType, chatMessageFactory, database, userDatabase, chatMessageParser, registrationProfile, mixWebCallFactory, photoFlavorFactory, videoFlavorFactory, guestControllerClient, notificationQueue, encryptor, epochTime);
			Session session = new Session(logger, localUser, sessionDocument.GuestControllerAccessToken, sessionDocument.PushNotificationToken != null, notificationPoller, coroutineManager, database, userDatabase, guestControllerClient, chatMessageParser, chatMessageHandler, mixWebCallFactory, epochTime, databaseCorruptionHandler, sessionStatus, keychain, getStateResponseParser, clientVersion, notificationQueue);
			try
			{
				NotificationHandler.Handle(notificationDispatcher, userDatabase, localUser, epochTime);
				notificationQueue.LatestSequenceNumber = sessionDocument.LatestNotificationSequenceNumber;
				IEnumerable<IncomingFriendInvitation> incomingFriendInvitations = GetIncomingFriendInvitations(userDatabase, localUser);
				foreach (IncomingFriendInvitation item in incomingFriendInvitations)
				{
					localUser.AddIncomingFriendInvitation(item);
				}
				IEnumerable<OutgoingFriendInvitation> outgoingFriendInvitations = GetOutgoingFriendInvitations(userDatabase, localUser);
				foreach (OutgoingFriendInvitation item2 in outgoingFriendInvitations)
				{
					localUser.AddOutgoingFriendInvitation(item2);
				}
				return session;
			}
			catch (Exception)
			{
				session.Dispose();
				throw;
			}
		}

		private static void CreateChatThreads(string localUserSwid, IUserDatabase userDatabase, IMixWebCallFactory mixWebCallFactory, IChatMessageParser chatMessageParser, SessionDocument lastSessionDoc, IChatMessageHandler chatMessageHandler, IChatThreadFactory chatThreadFactory, out IList<IInternalOneOnOneChatThread> oneOnOneChatThreads, out IList<IInternalGroupChatThread> groupChatThreads, out IList<IInternalOfficialAccountChatThread> officialAccountChatThreads)
		{
			oneOnOneChatThreads = (from chatThreadDoc in userDatabase.GetAllChatThreadDocuments()
				where chatThreadDoc.ChatThreadType == 0
				select CreateOneOnOneChatThread(chatThreadDoc, localUserSwid, userDatabase, mixWebCallFactory, chatMessageParser, lastSessionDoc, chatMessageHandler, chatThreadFactory)).ToList();
			groupChatThreads = (from chatThreadDoc in userDatabase.GetAllChatThreadDocuments()
				where chatThreadDoc.ChatThreadType == 1
				select CreateGroupChatThread(chatThreadDoc, localUserSwid, userDatabase, mixWebCallFactory, chatMessageParser, lastSessionDoc, chatMessageHandler, chatThreadFactory)).ToList();
			officialAccountChatThreads = (from d in userDatabase.GetAllChatThreadDocuments()
				where d.ChatThreadType == 2
				select CreateOfficialAccountChatThread(d, userDatabase, mixWebCallFactory, chatMessageParser, lastSessionDoc.Swid, chatMessageHandler, chatThreadFactory)).ToList();
		}

		private static IList<IOfficialAccount> CreateFollowships(IUserDatabase userDatabase)
		{
			return (from doc in userDatabase.GetAllOfficialAccounts()
				where doc.IsFollowing
				select OfficialAccountFactory.CreateOfficialAccount(doc.AccountId, doc.DisplayName, doc.IsAvailable, doc.CanUnfollow)).ToList();
		}

		private static IInternalOneOnOneChatThread CreateOneOnOneChatThread(ChatThreadDocument chatThreadDoc, string localUserSwid, IUserDatabase userDatabase, IMixWebCallFactory mixWebCallFactory, IChatMessageParser chatMessageParser, SessionDocument lastSessionDoc, IChatMessageHandler chatMessageHandler, IChatThreadFactory chatThreadFactory)
		{
			IList<IInternalRemoteChatMember> members = CreateChatMembers(chatThreadDoc, localUserSwid, userDatabase);
			IInternalOneOnOneChatThread internalOneOnOneChatThread = chatThreadFactory.CreateOneOnOneChatThread(mixWebCallFactory, chatThreadDoc.ChatThreadId, new ChatThreadTrustLevel(chatThreadDoc.IsTrusted), lastSessionDoc.Swid, chatThreadDoc.LatestSequenceNumber, chatThreadDoc.AreSequenceNumbersIndexed, members, userDatabase, chatMessageParser, chatMessageHandler);
			internalOneOnOneChatThread.UpdateNickname(chatThreadDoc.Nickname);
			internalOneOnOneChatThread.UpdateUnreadMessageCount(chatThreadDoc.UnreadMessageCount);
			return internalOneOnOneChatThread;
		}

		private static IInternalGroupChatThread CreateGroupChatThread(ChatThreadDocument chatThreadDoc, string localUserSwid, IUserDatabase userDatabase, IMixWebCallFactory mixWebCallFactory, IChatMessageParser chatMessageParser, SessionDocument lastSessionDoc, IChatMessageHandler chatMessageHandler, IChatThreadFactory chatThreadFactory)
		{
			IList<IInternalRemoteChatMember> members = CreateChatMembers(chatThreadDoc, localUserSwid, userDatabase);
			IInternalGroupChatThread internalGroupChatThread = chatThreadFactory.CreateGroupChatThread(mixWebCallFactory, chatThreadDoc.ChatThreadId, new ChatThreadTrustLevel(chatThreadDoc.IsTrusted), lastSessionDoc.Swid, chatThreadDoc.LatestSequenceNumber, chatThreadDoc.AreSequenceNumbersIndexed, members, userDatabase, chatMessageParser, chatMessageHandler);
			internalGroupChatThread.UpdateNickname(chatThreadDoc.Nickname);
			internalGroupChatThread.UpdateUnreadMessageCount(chatThreadDoc.UnreadMessageCount);
			return internalGroupChatThread;
		}

		private static IInternalOfficialAccountChatThread CreateOfficialAccountChatThread(ChatThreadDocument chatThreadDoc, IUserDatabase userDatabase, IMixWebCallFactory mixWebCallFactory, IChatMessageParser chatMessageParser, string swid, IChatMessageHandler chatMessageHandler, IChatThreadFactory chatThreadFactory)
		{
			IList<IInternalRemoteChatMember> members = CreateChatMembers(chatThreadDoc, swid, userDatabase);
			ChatThreadTrustLevel trustLevel = new ChatThreadTrustLevel(chatThreadDoc.IsTrusted);
			IInternalOfficialAccountChatThread internalOfficialAccountChatThread = chatThreadFactory.CreateOfficialAccountChatThread(mixWebCallFactory, chatThreadDoc.ChatThreadId, trustLevel, swid, chatThreadDoc.OfficialAccountId, chatThreadDoc.LatestSequenceNumber, chatThreadDoc.AreSequenceNumbersIndexed, members, userDatabase, chatMessageParser, chatMessageHandler);
			internalOfficialAccountChatThread.UpdateUnreadMessageCount(chatThreadDoc.UnreadMessageCount);
			return internalOfficialAccountChatThread;
		}

		private static IList<IInternalRemoteChatMember> CreateChatMembers(ChatThreadDocument chatThreadDoc, string localUserSwid, IUserDatabase userDatabase)
		{
			List<IInternalRemoteChatMember> list = new List<IInternalRemoteChatMember>();
			ChatMemberDocument[] membersInThread = userDatabase.GetMembersInThread(chatThreadDoc.ChatThreadId);
			ChatMemberDocument[] array = membersInThread;
			foreach (ChatMemberDocument chatMemberDocument in array)
			{
				if (!(chatMemberDocument.Swid == localUserSwid))
				{
					string swid = chatMemberDocument.Swid;
					UserDocument userBySwid = userDatabase.GetUserBySwid(swid);
					string displayName = userBySwid.DisplayName;
					IInternalRemoteChatMember item = RemoteUserFactory.CreateRemoteChatMember(swid, displayName, userBySwid.FirstName, userDatabase);
					list.Add(item);
				}
			}
			return list;
		}

		private static IEnumerable<IncomingFriendInvitation> GetIncomingFriendInvitations(IUserDatabase userDatabase, IInternalLocalUser localUser)
		{
			return (from friendInvitationDoc in userDatabase.GetFriendInvitationDocuments(false)
				where userDatabase.GetUserByDisplayName(friendInvitationDoc.DisplayName) != null
				select friendInvitationDoc).Select(delegate(FriendInvitationDocument friendInvitationDoc)
			{
				UserDocument userByDisplayName = userDatabase.GetUserByDisplayName(friendInvitationDoc.DisplayName);
				string displayName = userByDisplayName.DisplayName;
				IInternalUnidentifiedUser inviter = RemoteUserFactory.CreateUnidentifiedUser(displayName, userByDisplayName.FirstName, userDatabase);
				bool isTrusted = friendInvitationDoc.IsTrusted;
				IncomingFriendInvitation incomingFriendInvitation = new IncomingFriendInvitation(inviter, localUser, isTrusted);
				incomingFriendInvitation.SendComplete(friendInvitationDoc.FriendInvitationId);
				return incomingFriendInvitation;
			}).ToList();
		}

		private static IEnumerable<OutgoingFriendInvitation> GetOutgoingFriendInvitations(IUserDatabase userDatabase, IInternalLocalUser localUser)
		{
			return (from friendInvitationDoc in userDatabase.GetFriendInvitationDocuments(true)
				where userDatabase.GetUserByDisplayName(friendInvitationDoc.DisplayName) != null
				select friendInvitationDoc).Select(delegate(FriendInvitationDocument friendInvitationDoc)
			{
				UserDocument userByDisplayName = userDatabase.GetUserByDisplayName(friendInvitationDoc.DisplayName);
				string displayName = userByDisplayName.DisplayName;
				IInternalUnidentifiedUser invitee = RemoteUserFactory.CreateUnidentifiedUser(displayName, userByDisplayName.FirstName, userDatabase);
				bool isTrusted = friendInvitationDoc.IsTrusted;
				OutgoingFriendInvitation outgoingFriendInvitation = new OutgoingFriendInvitation(localUser, invitee, isTrusted);
				outgoingFriendInvitation.SendComplete(friendInvitationDoc.FriendInvitationId);
				return outgoingFriendInvitation;
			}).ToList();
		}

		private static IList<IInternalFriend> CreateFriends(IUserDatabase userDatabase)
		{
			return userDatabase.GetAllFriendDocuments().Select(delegate(FriendDocument friendDoc)
			{
				UserDocument userBySwid = userDatabase.GetUserBySwid(friendDoc.Swid);
				IInternalFriend internalFriend = RemoteUserFactory.CreateFriend(friendDoc.Swid, friendDoc.IsTrusted, userBySwid.DisplayName, userBySwid.FirstName, userDatabase);
				if (friendDoc.Nickname != null)
				{
					UserNickname nickname = new UserNickname(friendDoc.Nickname);
					internalFriend.UpdateNickname(nickname);
				}
				return internalFriend;
			}).ToList();
		}

		private static IDocumentCollection<TDocument> GetDocumentCollection<TDocument>(string userSwid, string entityName, IDatabaseDirectoryCreator directoryCreator, byte[] encryptionKey, IDocumentCollectionFactory documentCollectionFactory) where TDocument : AbstractDocument, new()
		{
			string dir = BuildDocCollectionPath(directoryCreator, userSwid);
			string path = HashedPathGenerator.GetPath(dir, entityName);
			return documentCollectionFactory.CreateHighSecurityFileSystemCollection<TDocument>(path, encryptionKey);
		}

		private static string BuildDocCollectionPath(IDatabaseDirectoryCreator directoryCreator, string userSwid)
		{
			string dir = directoryCreator.CreateUserDirectory();
			return HashedPathGenerator.GetPath(dir, userSwid);
		}
	}
}
