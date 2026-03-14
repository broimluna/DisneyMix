using System;
using System.Collections.Generic;
using Disney.Mix.SDK;

namespace Mix.FakeFriend.Messages
{
	public class FakeGameEventMessage : IChatMessage, IGameEventMessage
	{
		private DateTime timeSent;

		public string SenderId { get; private set; }

		public bool Sent { get; set; }

		public DateTime TimeSent
		{
			get
			{
				return timeSent;
			}
		}

		public string Id { get; set; }

		public IGameStateMessage GameStateMessage
		{
			get
			{
				return null;
			}
		}

		public string GameName
		{
			get
			{
				return null;
			}
		}

		public Dictionary<string, object> EventState
		{
			get
			{
				return null;
			}
		}

		public long SequenceNumber
		{
			get
			{
				return 0L;
			}
		}

		public FakeGameEventMessage(string senderId)
		{
			SenderId = senderId;
			timeSent = DateTime.UtcNow;
			Id = "FakeGameResponseMessage";
			Sent = false;
		}

		public bool Equals(IChatMessage other)
		{
			if (other != null && other is FakeGameEventMessage && SenderId == other.SenderId && timeSent.Equals(other.TimeSent))
			{
				return true;
			}
			return false;
		}
	}
}
