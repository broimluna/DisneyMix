using System;
using Disney.Mix.SDK;

namespace Mix.Session.Local.Messages
{
	public class LocalGagMessage : LocalChatMessage<IGagMessage>, ILocalMessageReference<IGagMessage>, IChatMessage, IGagMessage, IReadable
	{
		public string ContentId { get; set; }

		public string TargetUserId { get; set; }

		public IGagMessage SdkReference { get; set; }

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

		public LocalGagMessage(string contentId, string targetUserId, string senderId)
		{
			ContentId = contentId;
			TargetUserId = targetUserId;
			base.SenderId = senderId;
			base.TimeSent = DateTime.UtcNow;
			base.Sent = false;
			base.Read = false;
			base.Id = LocalUserWrapper.fakeObjectIds.ToString();
			LocalUserWrapper.fakeObjectIds++;
		}

		public LocalGagMessage(string contentId, string targetUserId, string senderId, DateTime time, bool sent, bool read, string Id)
		{
			ContentId = contentId;
			TargetUserId = targetUserId;
			base.SenderId = senderId;
			base.TimeSent = time;
			base.Sent = sent;
			base.Read = read;
			base.Id = Id;
		}

		public override bool Equals(IChatMessage other)
		{
			if (other != null && other is LocalGagMessage && other.SenderId == SenderId && TimeSent.Equals(other.TimeSent))
			{
				LocalGagMessage localGagMessage = (LocalGagMessage)other;
				return ContentId == localGagMessage.ContentId && TargetUserId == localGagMessage.TargetUserId;
			}
			return false;
		}
	}
}
