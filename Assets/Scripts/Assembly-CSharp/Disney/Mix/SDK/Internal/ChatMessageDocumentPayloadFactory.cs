using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ChatMessageDocumentPayloadFactory
	{
		public static string Create(GagChatMessage gagMessage)
		{
			GagMessageDocumentPayload gagMessageDocumentPayload = new GagMessageDocumentPayload();
			gagMessageDocumentPayload.ContentId = gagMessage.ContentId;
			gagMessageDocumentPayload.TargetUserId = gagMessage.TargetUserId;
			return JsonParser.ToJson(gagMessageDocumentPayload);
		}

		public static string Create(IGagMessage gagMessage)
		{
			GagMessageDocumentPayload gagMessageDocumentPayload = new GagMessageDocumentPayload();
			gagMessageDocumentPayload.ContentId = gagMessage.ContentId;
			gagMessageDocumentPayload.TargetUserId = gagMessage.TargetUserId;
			return JsonParser.ToJson(gagMessageDocumentPayload);
		}

		public static string Create(GameEventChatMessage gameEventMessage)
		{
			GameEventMessageDocumentPayload gameEventMessageDocumentPayload = new GameEventMessageDocumentPayload();
			gameEventMessageDocumentPayload.GameStateMessageId = gameEventMessage.GameStateMessageId;
			gameEventMessageDocumentPayload.GameName = gameEventMessage.GameName;
			gameEventMessageDocumentPayload.Payload = gameEventMessage.Payload;
			return JsonParser.ToJson(gameEventMessageDocumentPayload);
		}

		public static string Create(GameStateChatMessage gameStateMessage)
		{
			GameStateMessageDocumentPayload gameStateMessageDocumentPayload = new GameStateMessageDocumentPayload();
			gameStateMessageDocumentPayload.GameName = gameStateMessage.GameName;
			gameStateMessageDocumentPayload.State = gameStateMessage.State;
			return JsonParser.ToJson(gameStateMessageDocumentPayload);
		}

		public static string Create(IGameStateMessage gameStateMessage)
		{
			GameStateMessageDocumentPayload gameStateMessageDocumentPayload = new GameStateMessageDocumentPayload();
			gameStateMessageDocumentPayload.GameName = gameStateMessage.GameName;
			gameStateMessageDocumentPayload.State = JsonParser.ToJson(gameStateMessage.State);
			return JsonParser.ToJson(gameStateMessageDocumentPayload);
		}

		public static string Create(MemberListChangedChatMessage memberListChangedMessage)
		{
			MemberListChangedMessageDocumentPayload memberListChangedMessageDocumentPayload = new MemberListChangedMessageDocumentPayload();
			memberListChangedMessageDocumentPayload.MemberUserIds = memberListChangedMessage.MemberUserIds;
			memberListChangedMessageDocumentPayload.IsAdded = memberListChangedMessage.IsAdded;
			return JsonParser.ToJson(memberListChangedMessageDocumentPayload);
		}

		public static string Create(PhotoChatMessage photoMessage)
		{
			return JsonParser.ToJson(ChatMessagePayloadConverter.ConvertPhotoChatMessageToDocumentPayload(photoMessage));
		}

		public static string Create(IPhotoMessage photoMessage)
		{
			return JsonParser.ToJson(new PhotoMessageDocumentPayload());
		}

		public static string Create(StickerChatMessage stickerMessage)
		{
			StickerMessageDocumentPayload stickerMessageDocumentPayload = new StickerMessageDocumentPayload();
			stickerMessageDocumentPayload.ContentId = stickerMessage.ContentId;
			return JsonParser.ToJson(stickerMessageDocumentPayload);
		}

		public static string Create(IStickerMessage stickerMessage)
		{
			StickerMessageDocumentPayload stickerMessageDocumentPayload = new StickerMessageDocumentPayload();
			stickerMessageDocumentPayload.ContentId = stickerMessage.ContentId;
			return JsonParser.ToJson(stickerMessageDocumentPayload);
		}

		public static string Create(TextChatMessage textMessage)
		{
			TextMessageDocumentPayload textMessageDocumentPayload = new TextMessageDocumentPayload();
			textMessageDocumentPayload.Text = textMessage.Text;
			textMessageDocumentPayload.IsModerated = textMessage.IsModerated;
			return JsonParser.ToJson(textMessageDocumentPayload);
		}

		public static string Create(ITextMessage textMessage)
		{
			TextMessageDocumentPayload textMessageDocumentPayload = new TextMessageDocumentPayload();
			textMessageDocumentPayload.Text = textMessage.Text;
			textMessageDocumentPayload.IsModerated = false;
			return JsonParser.ToJson(textMessageDocumentPayload);
		}

		public static string Create(VideoChatMessage videoMessage)
		{
			return JsonParser.ToJson(ChatMessagePayloadConverter.ConvertVideoChatMessageToDocumentPayload(videoMessage));
		}

		public static string Create(IVideoMessage videoMessage)
		{
			return JsonParser.ToJson(new VideoMessageDocumentPayload());
		}
	}
}
