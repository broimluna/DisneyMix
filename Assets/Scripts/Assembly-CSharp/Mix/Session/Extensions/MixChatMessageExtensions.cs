using System.Linq;
using Disney.Mix.SDK;
using Mix.Session.Local.Messages;

namespace Mix.Session.Extensions
{
	public static class MixChatMessageExtensions
	{
		public static bool IsMediaMessage(this IChatMessage aChatMessage)
		{
			return aChatMessage is IStickerMessage || aChatMessage is IGagMessage || aChatMessage is IPhotoMessage || aChatMessage is IVideoMessage;
		}

		public static bool IsMine(this IChatMessage aChatMessage)
		{
			return aChatMessage.SenderId == null || MixChat.user.Id == aChatMessage.SenderId;
		}

		public static string IdOrSDKRefId(this IChatMessage aChatMessage)
		{
			if (aChatMessage.Id != null)
			{
				return aChatMessage.Id;
			}
			if (aChatMessage is ILocalMessageReference<ITextMessage>)
			{
				ILocalMessageReference<ITextMessage> localMessageReference = (ILocalMessageReference<ITextMessage>)aChatMessage;
				return localMessageReference.SdkReference.Id;
			}
			if (aChatMessage is ILocalMessageReference<IStickerMessage>)
			{
				ILocalMessageReference<IStickerMessage> localMessageReference2 = (ILocalMessageReference<IStickerMessage>)aChatMessage;
				return localMessageReference2.SdkReference.Id;
			}
			if (aChatMessage is ILocalMessageReference<IGagMessage>)
			{
				ILocalMessageReference<IGagMessage> localMessageReference3 = (ILocalMessageReference<IGagMessage>)aChatMessage;
				return localMessageReference3.SdkReference.Id;
			}
			if (aChatMessage is ILocalMessageReference<IGameEventMessage>)
			{
				ILocalMessageReference<IGameEventMessage> localMessageReference4 = (ILocalMessageReference<IGameEventMessage>)aChatMessage;
				return localMessageReference4.SdkReference.Id;
			}
			if (aChatMessage is ILocalMessageReference<IGameStateMessage>)
			{
				ILocalMessageReference<IGameStateMessage> localMessageReference5 = (ILocalMessageReference<IGameStateMessage>)aChatMessage;
				return localMessageReference5.SdkReference.Id;
			}
			if (aChatMessage is ILocalMessageReference<IPhotoMessage>)
			{
				ILocalMessageReference<IPhotoMessage> localMessageReference6 = (ILocalMessageReference<IPhotoMessage>)aChatMessage;
				return localMessageReference6.SdkReference.Id;
			}
			if (aChatMessage is ILocalMessageReference<IVideoMessage>)
			{
				ILocalMessageReference<IVideoMessage> localMessageReference7 = (ILocalMessageReference<IVideoMessage>)aChatMessage;
				return localMessageReference7.SdkReference.Id;
			}
			return null;
		}

		public static IChatMessage GetSDKRef(this IChatMessage aChatMessage)
		{
			if (aChatMessage is LocalTextMessage)
			{
				return ((LocalTextMessage)aChatMessage).SdkReference;
			}
			if (aChatMessage is LocalStickerMessage)
			{
				return ((LocalStickerMessage)aChatMessage).SdkReference;
			}
			if (aChatMessage is LocalGagMessage)
			{
				return ((LocalGagMessage)aChatMessage).SdkReference;
			}
			if (aChatMessage is LocalGameStateMessage)
			{
				return ((LocalGameStateMessage)aChatMessage).SdkReference;
			}
			if (aChatMessage is LocalPhotoMessage)
			{
				return ((LocalPhotoMessage)aChatMessage).SdkReference;
			}
			if (aChatMessage is LocalVideoMessage)
			{
				return ((LocalVideoMessage)aChatMessage).SdkReference;
			}
			return null;
		}

		public static bool IsMessageSentByBot(this IChatMessage message, IChatThread thread)
		{
			IRemoteChatMember remoteChatMember = thread.RemoteMembers.FirstOrDefault((IRemoteChatMember m) => m.Id == message.SenderId) ?? thread.FormerRemoteMembers.FirstOrDefault((IRemoteChatMember m) => m.Id == message.SenderId);
			return remoteChatMember != null && remoteChatMember.ChatMemberType == ChatMemberType.Bot;
		}
	}
}
