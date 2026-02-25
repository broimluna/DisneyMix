using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class FriendRecommender
	{
		public static void Recommend(AbstractLogger logger, IMixWebCallFactory mixWebCallFactory, IUserDatabase userDatabase, Action<IEnumerable<IInternalUnidentifiedUser>> successCallback, Action failureCallback)
		{
			try
			{
				BaseUserRequest request = new BaseUserRequest();
				IWebCall<BaseUserRequest, GetFriendshipRecommendationResponse> webCall = mixWebCallFactory.FriendshipRecommendPost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<GetFriendshipRecommendationResponse> e)
				{
					List<FriendRecommendation> friendRecommendations = e.Response.FriendRecommendations;
					if (friendRecommendations == null)
					{
						failureCallback();
					}
					else
					{
						foreach (FriendRecommendation item in friendRecommendations)
						{
							userDatabase.InsertUserDocument(new UserDocument
							{
								AvatarId = item.Avatar.AvatarId.Value,
								DisplayName = item.DisplayName,
								FirstName = item.FirstName,
								Swid = null,
								HashedSwid = null
							});
							userDatabase.InsertAvatar(item.Avatar);
						}
						IInternalUnidentifiedUser[] obj = friendRecommendations.Select((FriendRecommendation item) => RemoteUserFactory.CreateUnidentifiedUser(item.DisplayName, item.FirstName, userDatabase)).ToArray();
						successCallback(obj);
					}
				};
				webCall.OnError += delegate
				{
					failureCallback();
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback();
			}
		}
	}
}
