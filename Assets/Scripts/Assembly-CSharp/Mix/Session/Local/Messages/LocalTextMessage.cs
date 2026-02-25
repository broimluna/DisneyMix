using System;
using Disney.Mix.SDK;

namespace Mix.Session.Local.Messages
{
	public class LocalTextMessage : LocalChatMessage<ITextMessage>, ILocalMessageReference<ITextMessage>, IChatMessage, ITextMessage, IReadable
	{
		public string Text { get; set; }

		public ITextMessage SdkReference { get; set; }

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

		public LocalTextMessage(string senderId, string text, DateTime? timeSent = null)
		{
			base.SenderId = senderId;
			base.TimeSent = ((!timeSent.HasValue) ? DateTime.UtcNow : timeSent.Value);
			Text = text;
			base.Id = LocalUserWrapper.fakeObjectIds.ToString();
			LocalUserWrapper.fakeObjectIds++;
			base.Sent = false;
			base.Read = false;
		}

		public LocalTextMessage(string senderId, string text, DateTime timeSent, bool sent, bool read, string id)
		{
			base.SenderId = senderId;
			base.TimeSent = timeSent;
			Text = text;
			base.Id = id;
			base.Sent = sent;
			base.Read = read;
		}

		public override bool Equals(IChatMessage other)
		{
			if (other != null && other is LocalTextMessage && SenderId == other.SenderId && TimeSent.Equals(other.TimeSent))
			{
				return true;
			}
			return false;
		}
	}
}
