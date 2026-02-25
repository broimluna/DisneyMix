using System.Collections.Generic;

namespace Mix.DeviceDb
{
	public interface IFakeFirstFriendDocumentCollectionApi
	{
		void AddGag(bool aIsFromFriend, long aTimeSent, bool sent, bool read, string aUniqueId, string aContentId);

		BaseFakeFirstFriendDocument GetGag(string aUniqueId);

		List<BaseFakeFirstFriendDocument> GetAllGags();

		void AddText(bool aIsFromFriend, long aTimeSent, bool sent, bool read, string aUniqueId, string aText);

		BaseFakeFirstFriendDocument GetText(string aUniqueId);

		List<BaseFakeFirstFriendDocument> GetAllTexts();

		void AddSticker(bool aIsFromFriend, long aTimeSent, bool sent, bool read, string aUniqueId, string aContentId);

		BaseFakeFirstFriendDocument GetSticker(string aUniqueId);

		List<BaseFakeFirstFriendDocument> GetAllStickers();

		void AddGameState(bool aIsFromFriend, long aTimeSent, bool sent, bool read, string aUniqueId, string aGameName, string aGameData);

		BaseFakeFirstFriendDocument GetGameState(string aUniqueId);

		List<BaseFakeFirstFriendDocument> GetAllGameStates();

		void AddPhoto(bool aIsFromFriend, long aTimeSent, bool sent, bool read, string aUniqueId, string aMessageId);

		BaseFakeFirstFriendDocument GetPhoto(string aUniqueId);

		List<BaseFakeFirstFriendDocument> GetAllPhotos();

		void AddVideo(bool aIsFromFriend, long aTimeSent, bool sent, bool read, string aUniqueId, string aMessageId);

		BaseFakeFirstFriendDocument GetVideo(string aUniqueId);

		List<BaseFakeFirstFriendDocument> GetAllVideos();

		List<BaseFakeFirstFriendDocument> GetAllDocuments();

		void RemoveDocument(string aUniqueId);

		void RemoveDocumentByType(string aCollectionName, string aUniqueId);
	}
}
