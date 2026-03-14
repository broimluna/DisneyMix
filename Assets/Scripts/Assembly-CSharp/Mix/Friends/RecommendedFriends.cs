using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.CustomExtensions;
using Mix.Session;
using Mix.Ui;

namespace Mix.Friends
{
	public class RecommendedFriends : Singleton<RecommendedFriends>
	{
		private const int NUM_RECOMMENDED_FRIENDS_FROM_SERVER = 20;

		private List<IUnidentifiedUser> recommendedFriends = new List<IUnidentifiedUser>();

		private bool shouldGetMoreRecommendedFriends;

		private SdkActions actionGenerator = new SdkActions();

		private SdkEvents eventGenerator = new SdkEvents();

		public IEnumerable<IUnidentifiedUser> Users
		{
			get
			{
				return recommendedFriends.ToArray();
			}
		}

		public RecommendedFriends()
		{
			shouldGetMoreRecommendedFriends = true;
			SetupEvents();
		}

		public void HandleConnectionChange(MixSession.ConnectionState newState, MixSession.ConnectionState oldState)
		{
			if (newState == MixSession.ConnectionState.ONLINE)
			{
				NavigationRequest lastProcessedRequest = MonoSingleton<NavigationManager>.Instance.GetLastProcessedRequest();
				if (lastProcessedRequest != null && lastProcessedRequest.PrefabPath != "Prefabs/Screens/Friends/FriendsScreen")
				{
					CleanupEvents();
					recommendedFriends.Clear();
					shouldGetMoreRecommendedFriends = true;
					SetupEvents();
				}
			}
		}

		private void SetupEvents()
		{
			MixSession.OnConnectionChanged += HandleConnectionChange;
			MixSession.User.OnReceivedIncomingFriendInvitation += eventGenerator.AddEventHandler<AbstractReceivedIncomingFriendInvitationEventArgs>(this, ReceivedIncomingFriendInvitationFromApi);
			MixSession.User.OnReceivedOutgoingFriendInvitation += eventGenerator.AddEventHandler<AbstractReceivedOutgoingFriendInvitationEventArgs>(this, ReceivedOutgoingFriendInvitationFromApi);
		}

		private void CleanupEvents()
		{
			MixSession.OnConnectionChanged -= HandleConnectionChange;
			MixSession.User.OnReceivedIncomingFriendInvitation -= eventGenerator.GetEventHandler<AbstractReceivedIncomingFriendInvitationEventArgs>(this, ReceivedIncomingFriendInvitationFromApi);
			MixSession.User.OnReceivedOutgoingFriendInvitation -= eventGenerator.GetEventHandler<AbstractReceivedOutgoingFriendInvitationEventArgs>(this, ReceivedOutgoingFriendInvitationFromApi);
		}

		private void ReceivedIncomingFriendInvitationFromApi(object sender, AbstractReceivedIncomingFriendInvitationEventArgs args)
		{
			if (!(MixSession.User.DisplayName.Text != args.Invitation.Invitee.DisplayName.Text))
			{
				RemoveRecommendedFriend(args.Invitation.Inviter.DisplayName.Text);
			}
		}

		private void ReceivedOutgoingFriendInvitationFromApi(object sender, AbstractReceivedOutgoingFriendInvitationEventArgs args)
		{
			if (!(MixSession.User.DisplayName.Text != args.Invitation.Inviter.DisplayName.Text))
			{
				RemoveRecommendedFriend(args.Invitation.Invitee.DisplayName.Text);
			}
		}

		public void RemoveRecommendedFriend(string displayName)
		{
			IUnidentifiedUser unidentifiedUser = FindRecommendedFriend(displayName);
			if (unidentifiedUser != null)
			{
				recommendedFriends.Remove(unidentifiedUser);
			}
		}

		public void GetRecommendedFriends(Action<GetRecommendedFriendsResult> callback, bool forceGetFromServer = false)
		{
			if (shouldGetMoreRecommendedFriends || forceGetFromServer)
			{
				shouldGetMoreRecommendedFriends = false;
				MixSession.User.GetRecommendedFriends(actionGenerator.CreateAction(delegate(IGetRecommendedFriendsResult result)
				{
					shouldGetMoreRecommendedFriends = true;
					if (result != null && result.Success && result.Users.Count() > 0)
					{
						shouldGetMoreRecommendedFriends = ((result.Users.Count() >= 20) ? true : false);
						recommendedFriends.Clear();
						recommendedFriends = result.Users.Shuffle().ToList();
						callback(new GetRecommendedFriendsResult(true));
					}
					else
					{
						callback(new GetRecommendedFriendsResult(false));
					}
				}));
			}
			else
			{
				callback(new GetRecommendedFriendsResult(false));
			}
		}

		private IUnidentifiedUser FindRecommendedFriend(string displayName)
		{
			foreach (IUnidentifiedUser recommendedFriend in recommendedFriends)
			{
				if (recommendedFriend.DisplayName.Text == displayName)
				{
					return recommendedFriend;
				}
			}
			return null;
		}
	}
}
