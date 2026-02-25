using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class GetChatThreadMessagesResponse : BaseResponse
	{
		public string Cursor;

		public List<TextChatMessage> Text;

		public List<StickerChatMessage> Sticker;

		public List<GagChatMessage> Gag;

		public List<VideoChatMessage> Video;

		public List<PhotoChatMessage> Photo;

		public List<GameStateChatMessage> GameState;

		public List<GameEventChatMessage> GameEvent;

		public List<MemberListChangedChatMessage> MemberListChanged;
	}
}
