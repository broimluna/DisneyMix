using System;
using Disney.Mix.SDK;

namespace Mix.Session.Local.Messages
{
	public class LocalStickerMessage : LocalChatMessage<IStickerMessage>, ILocalMessageReference<IStickerMessage>, IChatMessage, IStickerMessage, IReadable
	{
		public string ContentId { get; set; }

		public IStickerMessage SdkReference { get; set; }

		 bool IReadable.Read
		{
			get
			{
				return base.Read;
			}
			set
			{
				base.Read = value;
			}
		}

		public LocalStickerMessage(string contentId, string senderId, DateTime? timeSent = null)
		{
			ContentId = contentId;
			base.SenderId = senderId;
			base.TimeSent = ((!timeSent.HasValue) ? DateTime.UtcNow : timeSent.Value);
			base.Sent = false;
			base.Read = false;
			base.Id = LocalUserWrapper.fakeObjectIds.ToString();
			LocalUserWrapper.fakeObjectIds++;
		}

		public LocalStickerMessage(string contentId, string senderId, DateTime timesent, bool sent, bool read, string id)
		{
			ContentId = contentId;
			base.SenderId = senderId;
			base.TimeSent = timesent;
			base.Sent = sent;
			base.Read = read;
			base.Id = id;
		}

		public override bool Equals(IChatMessage other)
		{
			if (other != null && other is LocalStickerMessage && SenderId == other.SenderId && TimeSent.Equals(other.TimeSent))
			{
				LocalStickerMessage localStickerMessage = other as LocalStickerMessage;
				return localStickerMessage.ContentId == ContentId;
			}
			return false;
		}
	}
}
