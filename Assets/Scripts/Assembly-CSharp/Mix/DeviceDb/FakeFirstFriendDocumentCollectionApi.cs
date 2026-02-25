using System.Collections.Generic;
using System.IO;
using DeviceDB;
using Disney.Mix.SDK;

namespace Mix.DeviceDb
{
	public class FakeFirstFriendDocumentCollectionApi : MixDocumentCollectionApi<BaseFakeFirstFriendDocument>, IFakeFirstFriendDocumentCollectionApi
	{
		public const string GagCollection = "FakeGagDb";

		public const string TextCollection = "FakeTextDb";

		public const string StickerCollection = "FakeStickerDb";

		public const string GameStateCollection = "FakeGameStateDb";

		public const string PhotoCollection = "FakePhotoDb";

		public const string VideoCollection = "FakeVideoDb";

		public override void LogOut()
		{
			foreach (KeyValuePair<string, IDocumentCollection<BaseFakeFirstFriendDocument>> collection in base.collections)
			{
				if (collection.Value != null)
				{
					collection.Value.Dispose();
				}
			}
			base.collections = new Dictionary<string, IDocumentCollection<BaseFakeFirstFriendDocument>>();
		}

		public override void Delete()
		{
			base.Delete();
			DeleteDBOnDisk(Singleton<MixDocumentCollections>.Instance.DirPath + "/FakeGagDb/");
			DeleteDBOnDisk(Singleton<MixDocumentCollections>.Instance.DirPath + "/FakeTextDb/");
			DeleteDBOnDisk(Singleton<MixDocumentCollections>.Instance.DirPath + "/FakeStickerDb/");
			DeleteDBOnDisk(Singleton<MixDocumentCollections>.Instance.DirPath + "/FakeGameStateDb/");
			DeleteDBOnDisk(Singleton<MixDocumentCollections>.Instance.DirPath + "/FakePhotoDb/");
			DeleteDBOnDisk(Singleton<MixDocumentCollections>.Instance.DirPath + "/FakeVideoDb/");
		}

		private void DeleteDBOnDisk(string aPath)
		{
			if (Directory.Exists(aPath))
			{
				Directory.Delete(aPath, true);
			}
		}

		protected string GetCollecitonName(string aCollectionName)
		{
			return aCollectionName + "/" + GetUserId();
		}

		protected string GetCollectionPath(string aCollectionName)
		{
			return "/" + aCollectionName + "/" + GetUserId();
		}

		public List<BaseFakeFirstFriendDocument> GetAllDocuments()
		{
			List<IDocumentCollection<BaseFakeFirstFriendDocument>> list = new List<IDocumentCollection<BaseFakeFirstFriendDocument>>();
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeTextDb"), GetCollectionPath("FakeTextDb"));
			if (collection != null)
			{
				list.Add(collection);
			}
			collection = GetCollection(GetCollecitonName("FakeGagDb"), GetCollectionPath("FakeGagDb"));
			if (collection != null)
			{
				list.Add(collection);
			}
			collection = GetCollection(GetCollecitonName("FakeStickerDb"), GetCollectionPath("FakeStickerDb"));
			if (collection != null)
			{
				list.Add(collection);
			}
			collection = GetCollection(GetCollecitonName("FakeGameStateDb"), GetCollectionPath("FakeGameStateDb"));
			if (collection != null)
			{
				list.Add(collection);
			}
			collection = GetCollection(GetCollecitonName("FakePhotoDb"), GetCollectionPath("FakePhotoDb"));
			if (collection != null)
			{
				list.Add(collection);
			}
			collection = GetCollection(GetCollecitonName("FakeVideoDb"), GetCollectionPath("FakeVideoDb"));
			if (collection != null)
			{
				list.Add(collection);
			}
			return GetAllDocs(list);
		}

		public void RemoveDocumentByType(string aCollectionName, string aUniqueId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName(aCollectionName), GetCollectionPath(aCollectionName));
			if (collection != null)
			{
				BaseFakeFirstFriendDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
				DeleteDocument(collection, documentByFieldAndKey);
			}
		}

		public void RemoveDocument(string aUniqueId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeTextDb"), GetCollectionPath("FakeTextDb"));
			if (collection == null)
			{
				return;
			}
			BaseFakeFirstFriendDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
			if (documentByFieldAndKey == null)
			{
				collection = GetCollection(GetCollecitonName("FakeGagDb"), GetCollectionPath("FakeGagDb"));
				documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
				if (documentByFieldAndKey == null)
				{
					collection = GetCollection(GetCollecitonName("FakeStickerDb"), GetCollectionPath("FakeStickerDb"));
					documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
					if (documentByFieldAndKey == null)
					{
						collection = GetCollection(GetCollecitonName("FakeGameStateDb"), GetCollectionPath("FakeGameStateDb"));
						documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
						if (documentByFieldAndKey == null)
						{
							collection = GetCollection(GetCollecitonName("FakePhotoDb"), GetCollectionPath("FakePhotoDb"));
							documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
							if (documentByFieldAndKey == null)
							{
								collection = GetCollection(GetCollecitonName("FakeVideoDb"), GetCollectionPath("FakeVideoDb"));
								documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
							}
						}
					}
				}
			}
			DeleteDocument(collection, documentByFieldAndKey);
		}

		public void AddText(bool aIsFromFriend, long aTimeSent, bool aSent, bool aRead, string aUniqueId, string aText)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeTextDb"), GetCollectionPath("FakeTextDb"));
			if (collection != null)
			{
				BaseFakeFirstFriendDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new BaseFakeFirstFriendDocument();
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aText;
					documentByFieldAndKey.EntryTypeId = 57;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aText;
					documentByFieldAndKey.EntryTypeId = 57;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		public BaseFakeFirstFriendDocument GetText(string aUniqueId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeTextDb"), GetCollectionPath("FakeTextDb"));
			if (collection == null)
			{
				return null;
			}
			return GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
		}

		public List<BaseFakeFirstFriendDocument> GetAllTexts()
		{
			List<BaseFakeFirstFriendDocument> list = new List<BaseFakeFirstFriendDocument>();
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeTextDb"), GetCollectionPath("FakeTextDb"));
			if (collection == null)
			{
				return list;
			}
			List<IDocumentCollection<BaseFakeFirstFriendDocument>> list2 = new List<IDocumentCollection<BaseFakeFirstFriendDocument>>();
			if (collection != null)
			{
				list2.Add(collection);
			}
			List<BaseFakeFirstFriendDocument> allDocs = GetAllDocs(list2);
			foreach (BaseFakeFirstFriendDocument item in allDocs)
			{
				list.Add(item);
			}
			return list;
		}

		public void AddGag(bool aIsFromFriend, long aTimeSent, bool aSent, bool aRead, string aUniqueId, string aContentId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeGagDb"), GetCollectionPath("FakeGagDb"));
			if (collection != null)
			{
				BaseFakeFirstFriendDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new BaseFakeFirstFriendDocument();
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aContentId;
					documentByFieldAndKey.EntryTypeId = 55;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aContentId;
					documentByFieldAndKey.EntryTypeId = 55;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		public BaseFakeFirstFriendDocument GetGag(string aUniqueId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeGagDb"), GetCollectionPath("FakeGagDb"));
			if (collection == null)
			{
				return null;
			}
			return GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
		}

		public List<BaseFakeFirstFriendDocument> GetAllGags()
		{
			List<BaseFakeFirstFriendDocument> list = new List<BaseFakeFirstFriendDocument>();
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeGagDb"), GetCollectionPath("FakeGagDb"));
			if (collection == null)
			{
				return list;
			}
			List<IDocumentCollection<BaseFakeFirstFriendDocument>> list2 = new List<IDocumentCollection<BaseFakeFirstFriendDocument>>();
			if (collection != null)
			{
				list2.Add(collection);
			}
			List<BaseFakeFirstFriendDocument> allDocs = GetAllDocs(list2);
			foreach (BaseFakeFirstFriendDocument item in allDocs)
			{
				list.Add(item);
			}
			return list;
		}

		public void AddSticker(bool aIsFromFriend, long aTimeSent, bool aSent, bool aRead, string aUniqueId, string aContentId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeStickerDb"), GetCollectionPath("FakeStickerDb"));
			if (collection != null)
			{
				BaseFakeFirstFriendDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new BaseFakeFirstFriendDocument();
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aContentId;
					documentByFieldAndKey.EntryTypeId = 58;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aContentId;
					documentByFieldAndKey.EntryTypeId = 58;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		public BaseFakeFirstFriendDocument GetSticker(string aUniqueId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeStickerDb"), GetCollectionPath("FakeStickerDb"));
			if (collection == null)
			{
				return null;
			}
			return GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
		}

		public List<BaseFakeFirstFriendDocument> GetAllStickers()
		{
			List<BaseFakeFirstFriendDocument> list = new List<BaseFakeFirstFriendDocument>();
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeStickerDb"), GetCollectionPath("FakeStickerDb"));
			if (collection == null)
			{
				return list;
			}
			List<IDocumentCollection<BaseFakeFirstFriendDocument>> list2 = new List<IDocumentCollection<BaseFakeFirstFriendDocument>>();
			if (collection != null)
			{
				list2.Add(collection);
			}
			List<BaseFakeFirstFriendDocument> allDocs = GetAllDocs(list2);
			foreach (BaseFakeFirstFriendDocument item in allDocs)
			{
				list.Add(item);
			}
			return list;
		}

		public void AddGameState(bool aIsFromFriend, long aTimeSent, bool aSent, bool aRead, string aUniqueId, string aGameName, string aGameData)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeGameStateDb"), GetCollectionPath("FakeGameStateDb"));
			if (collection != null)
			{
				BaseFakeFirstFriendDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new BaseFakeFirstFriendDocument();
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aGameName;
					documentByFieldAndKey.GameData_or_FilePath_or_VideoPath = aGameData;
					documentByFieldAndKey.EntryTypeId = 59;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aGameName;
					documentByFieldAndKey.GameData_or_FilePath_or_VideoPath = aGameData;
					documentByFieldAndKey.EntryTypeId = 59;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		public BaseFakeFirstFriendDocument GetGameState(string aUniqueId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeGameStateDb"), GetCollectionPath("FakeGameStateDb"));
			if (collection == null)
			{
				return null;
			}
			return GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
		}

		public List<BaseFakeFirstFriendDocument> GetAllGameStates()
		{
			List<BaseFakeFirstFriendDocument> list = new List<BaseFakeFirstFriendDocument>();
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeGameStateDb"), GetCollectionPath("FakeGameStateDb"));
			if (collection == null)
			{
				return list;
			}
			List<IDocumentCollection<BaseFakeFirstFriendDocument>> list2 = new List<IDocumentCollection<BaseFakeFirstFriendDocument>>();
			if (collection != null)
			{
				list2.Add(collection);
			}
			List<BaseFakeFirstFriendDocument> allDocs = GetAllDocs(list2);
			foreach (BaseFakeFirstFriendDocument item in allDocs)
			{
				list.Add(item);
			}
			return list;
		}

		public void AddPhoto(bool aIsFromFriend, long aTimeSent, bool aSent, bool aRead, string aUniqueId, string aFilePath, PhotoEncoding aEncoding, int aWidth, int aHeight)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakePhotoDb"), GetCollectionPath("FakePhotoDb"));
			if (collection != null)
			{
				BaseFakeFirstFriendDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new BaseFakeFirstFriendDocument();
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.GameData_or_FilePath_or_VideoPath = aFilePath;
					documentByFieldAndKey.Encoding_or_Format = aEncoding.ToString();
					documentByFieldAndKey.Width_or_Bitrate = aWidth;
					documentByFieldAndKey.Height_or_Duration = aHeight;
					documentByFieldAndKey.EntryTypeId = 63;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.GameData_or_FilePath_or_VideoPath = aFilePath;
					documentByFieldAndKey.Encoding_or_Format = aEncoding.ToString();
					documentByFieldAndKey.Width_or_Bitrate = aWidth;
					documentByFieldAndKey.Height_or_Duration = aHeight;
					documentByFieldAndKey.EntryTypeId = 63;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		public void AddPhoto(bool aIsFromFriend, long aTimeSent, bool aSent, bool aRead, string aUniqueId, string aMessageId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakePhotoDb"), GetCollectionPath("FakePhotoDb"));
			if (collection != null)
			{
				BaseFakeFirstFriendDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new BaseFakeFirstFriendDocument();
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.EntryTypeId = 63;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aMessageId;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.EntryTypeId = 63;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aMessageId;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		public BaseFakeFirstFriendDocument GetPhoto(string aUniqueId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakePhotoDb"), GetCollectionPath("FakePhotoDb"));
			if (collection == null)
			{
				return null;
			}
			return GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
		}

		public List<BaseFakeFirstFriendDocument> GetAllPhotos()
		{
			List<BaseFakeFirstFriendDocument> list = new List<BaseFakeFirstFriendDocument>();
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakePhotoDb"), GetCollectionPath("FakePhotoDb"));
			if (collection == null)
			{
				return list;
			}
			List<IDocumentCollection<BaseFakeFirstFriendDocument>> list2 = new List<IDocumentCollection<BaseFakeFirstFriendDocument>>();
			if (collection != null)
			{
				list2.Add(collection);
			}
			List<BaseFakeFirstFriendDocument> allDocs = GetAllDocs(list2);
			foreach (BaseFakeFirstFriendDocument item in allDocs)
			{
				list.Add(item);
			}
			return list;
		}

		public void AddVideo(bool aIsFromFriend, long aTimeSent, bool aSent, bool aRead, string aUniqueId, string videoPath, VideoFormat format, int bitrate, int duration, int videoWidth, int videoHeight, string thumbnailPath, PhotoEncoding thumbnailEncoding, int thumbnailWidth, int thumbnailHeight)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeVideoDb"), GetCollectionPath("FakeVideoDb"));
			if (collection != null)
			{
				BaseFakeFirstFriendDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new BaseFakeFirstFriendDocument();
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.GameData_or_FilePath_or_VideoPath = videoPath;
					documentByFieldAndKey.Encoding_or_Format = format.ToString();
					documentByFieldAndKey.Width_or_Bitrate = bitrate;
					documentByFieldAndKey.Height_or_Duration = duration;
					documentByFieldAndKey.VideoWidth = videoWidth;
					documentByFieldAndKey.VideoHeight = videoHeight;
					documentByFieldAndKey.ThumbnailPath = thumbnailPath;
					documentByFieldAndKey.ThumbnailEncoding = thumbnailEncoding.ToString();
					documentByFieldAndKey.ThumbnailWidth = thumbnailWidth;
					documentByFieldAndKey.ThumbnailHeight = thumbnailHeight;
					documentByFieldAndKey.EntryTypeId = 64;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.GameData_or_FilePath_or_VideoPath = videoPath;
					documentByFieldAndKey.Encoding_or_Format = format.ToString();
					documentByFieldAndKey.Width_or_Bitrate = bitrate;
					documentByFieldAndKey.Height_or_Duration = duration;
					documentByFieldAndKey.VideoWidth = videoWidth;
					documentByFieldAndKey.VideoHeight = videoHeight;
					documentByFieldAndKey.ThumbnailPath = thumbnailPath;
					documentByFieldAndKey.ThumbnailEncoding = thumbnailEncoding.ToString();
					documentByFieldAndKey.ThumbnailWidth = thumbnailWidth;
					documentByFieldAndKey.ThumbnailHeight = thumbnailHeight;
					documentByFieldAndKey.EntryTypeId = 64;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		public void AddVideo(bool aIsFromFriend, long aTimeSent, bool aSent, bool aRead, string aUniqueId, string aMessageId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeVideoDb"), GetCollectionPath("FakeVideoDb"));
			if (collection != null)
			{
				BaseFakeFirstFriendDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new BaseFakeFirstFriendDocument();
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aMessageId;
					documentByFieldAndKey.EntryTypeId = 64;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.isFromFriend = aIsFromFriend;
					documentByFieldAndKey.timeSent = aTimeSent;
					documentByFieldAndKey.uniqueId = aUniqueId;
					documentByFieldAndKey.isSent = aSent;
					documentByFieldAndKey.isRead = aRead;
					documentByFieldAndKey.ContentId_or_GameName_or_Text_or_MessageId = aMessageId;
					documentByFieldAndKey.EntryTypeId = 64;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		public BaseFakeFirstFriendDocument GetVideo(string aUniqueId)
		{
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeVideoDb"), GetCollectionPath("FakeVideoDb"));
			if (collection == null)
			{
				return null;
			}
			return GetDocumentByFieldAndKey(collection, "uniqueId", aUniqueId);
		}

		public List<BaseFakeFirstFriendDocument> GetAllVideos()
		{
			List<BaseFakeFirstFriendDocument> list = new List<BaseFakeFirstFriendDocument>();
			IDocumentCollection<BaseFakeFirstFriendDocument> collection = GetCollection(GetCollecitonName("FakeVideoDb"), GetCollectionPath("FakeVideoDb"));
			if (collection == null)
			{
				return list;
			}
			List<IDocumentCollection<BaseFakeFirstFriendDocument>> list2 = new List<IDocumentCollection<BaseFakeFirstFriendDocument>>();
			if (collection != null)
			{
				list2.Add(collection);
			}
			List<BaseFakeFirstFriendDocument> allDocs = GetAllDocs(list2);
			foreach (BaseFakeFirstFriendDocument item in allDocs)
			{
				list.Add(item);
			}
			return list;
		}
	}
}
