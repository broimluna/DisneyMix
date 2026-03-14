using System;

namespace Mix.Friends
{
	public class GetRecommendedFriendsResult : EventArgs
	{
		public bool Status { get; protected set; }

		public GetRecommendedFriendsResult(bool status)
		{
			Status = status;
		}
	}
}
