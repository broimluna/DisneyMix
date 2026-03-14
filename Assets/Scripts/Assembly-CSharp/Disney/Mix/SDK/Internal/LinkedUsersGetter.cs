using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.GuestControllerDomain;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class LinkedUsersGetter
	{
		public static void Get(AbstractLogger logger, IMixWebCallFactory mixWebCallFactory, IList<Profile> profiles, Action<LinkedUser[]> callback)
		{
			IWebCall<GetUsersByUserIdRequest, GetUsersResponse> webCall = mixWebCallFactory.UsersByUserIdPost(new GetUsersByUserIdRequest
			{
				UserIds = profiles.Select((Profile u) => u.swid).ToList()
			});
			webCall.OnResponse += delegate(object sender, WebCallEventArgs<GetUsersResponse> e)
			{
				HandleGetUsersByIdSuccess(logger, e.Response, profiles, callback);
			};
			webCall.OnError += delegate
			{
				callback(null);
			};
			webCall.Execute();
		}

		private static void HandleGetUsersByIdSuccess(AbstractLogger logger, GetUsersResponse response, IList<Profile> profiles, Action<LinkedUser[]> callback)
		{
			try
			{
				if (response.Users == null)
				{
					logger.Critical("Returned users array is null");
					callback(null);
					return;
				}
				Dictionary<Profile, User> profileToUser = new Dictionary<Profile, User>();
				Profile profile;
				foreach (Profile profile2 in profiles)
				{
					profile = profile2;
					User user = response.Users.FirstOrDefault((User u) => profile.swid == u.UserId);
					if (user == null)
					{
						logger.Critical("Returned users doesn't have " + profile.swid + ": " + response.Users);
						callback(null);
						return;
					}
					profileToUser[profile] = user;
				}
				LinkedUser[] obj = profiles.Select(delegate(Profile profile2)
				{
					DateTime? dateOfBirth = GuestControllerUtils.ParseDateTime(logger, profile2.dateOfBirth);
					AgeBandType ageBand = AgeBandTypeConverter.Convert(profile2.ageBand);
					User user2 = profileToUser[profile2];
					DisplayName displayName = new DisplayName(user2.DisplayName);
					IInternalAvatar internalAvatar2;
					if (user2.Avatar == null)
					{
						IInternalAvatar internalAvatar = null;
						internalAvatar2 = internalAvatar;
					}
					else
					{
						internalAvatar2 = AvatarBuilder.Build(user2.Avatar);
					}
					IInternalAvatar avatar = internalAvatar2;
					return new LinkedUser(profile2.username, profile2.firstName, profile2.lastName, displayName, avatar, profile2.email, profile2.parentEmail, ageBand, dateOfBirth, profile2.swid);
				}).ToArray();
				callback(obj);
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(null);
			}
		}
	}
}
