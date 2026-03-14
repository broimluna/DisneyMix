using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class StateGetter
	{
		public static void GetState(AbstractLogger logger, IEpochTime epochTime, string clientVersion, IDatabase database, IUserDatabase userDatabase, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, string localUserId, long lastNotificationTime, Action<GetStateResponse> successCallback, Action failureCallback)
		{
			try
			{
				long? num = ((lastNotificationTime != long.MinValue) ? new long?(lastNotificationTime) : ((long?)null));
				GetStateRequest getStateRequest = new GetStateRequest();
				getStateRequest.UserId = localUserId;
				getStateRequest.ClientVersion = clientVersion;
				getStateRequest.GameStateSince = num;
				getStateRequest.AvatarSince = num;
				GetStateRequest request = getStateRequest;
				IWebCall<GetStateRequest, GetStateResponse> webCall = mixWebCallFactory.StatePost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<GetStateResponse> e)
				{
					HandleGetStateSuccess(logger, epochTime, database, userDatabase, notificationQueue, e.Response, mixWebCallFactory, successCallback, failureCallback);
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

		private static void HandleGetStateSuccess(AbstractLogger logger, IEpochTime epochTime, IDatabase database, IUserDatabase userDatabase, INotificationQueue notificationQueue, GetStateResponse response, IMixWebCallFactory mixWebCallFactory, Action<GetStateResponse> successCallback, Action failureCallback)
		{
			try
			{
				if (ValidateResponse(response))
				{
					epochTime.ReferenceTime = response.Timestamp.Value;
					database.SetServerTimeOffsetMillis((long)epochTime.Offset.TotalMilliseconds);
					VerifyOfficialAccounts(logger, response, userDatabase, mixWebCallFactory, delegate
					{
						successCallback(response);
						notificationQueue.LatestSequenceNumber = response.NotificationSequenceCounter.Value;
					}, delegate
					{
						failureCallback();
						notificationQueue.LatestSequenceNumber = response.NotificationSequenceCounter.Value;
					});
				}
				else
				{
					logger.Critical("Error validating get state response: " + JsonParser.ToJson(response));
					failureCallback();
					notificationQueue.Clear();
				}
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback();
				notificationQueue.Clear();
			}
		}

		private static bool ValidateResponse(GetStateResponse response)
		{
			if (response.ChatThreads != null && response.ChatThreads.Any(delegate(ChatThread t)
			{
				long? chatThreadId = t.ChatThreadId;
				int result;
				if (chatThreadId.HasValue && ChatThreadTypeConverter.ValidateServerType(t.ChatThreadType))
				{
					bool? isTrusted = t.IsTrusted;
					if (isTrusted.HasValue)
					{
						result = ((t.ChatThreadType == "OFFICIAL_ACCOUNT" && t.OfficialAccountId == null) ? 1 : 0);
						goto IL_0060;
					}
				}
				result = 1;
				goto IL_0060;
				IL_0060:
				return (byte)result != 0;
			}))
			{
				return false;
			}
			if (response.ChatThreadNicknames != null && response.ChatThreadNicknames.Any(delegate(Disney.Mix.SDK.Internal.MixDomain.ChatThreadNickname n)
			{
				long? chatThreadId = n.ChatThreadId;
				return !chatThreadId.HasValue || n.Nickname == null;
			}))
			{
				return false;
			}
			if (response.FriendshipInvitations != null && response.FriendshipInvitations.Any(delegate(FriendshipInvitation i)
			{
				bool? isTrusted = i.IsTrusted;
				int result;
				if (isTrusted.HasValue)
				{
					long? friendshipInvitationId = i.FriendshipInvitationId;
					result = ((!friendshipInvitationId.HasValue) ? 1 : 0);
				}
				else
				{
					result = 1;
				}
				return (byte)result != 0;
			}))
			{
				return false;
			}
			if (response.Friendships != null && response.Friendships.Any(delegate(Friendship f)
			{
				bool? isTrusted = f.IsTrusted;
				return !isTrusted.HasValue;
			}))
			{
				return false;
			}
			if (response.PollIntervals != null && response.PollIntervals.Any((int? p) => !p.HasValue))
			{
				return false;
			}
			if (response.PokeIntervals != null && response.PokeIntervals.Any((int? p) => !p.HasValue))
			{
				return false;
			}
			long? timestamp = response.Timestamp;
			if (!timestamp.HasValue)
			{
				return false;
			}
			long? notificationSequenceCounter = response.NotificationSequenceCounter;
			if (!notificationSequenceCounter.HasValue)
			{
				return false;
			}
			if (response.OfficialAccounts != null && response.OfficialAccounts.Any((string p) => p == null))
			{
				return false;
			}
			if (response.Users == null || response.Users.Any((User u) => u.HashedUserId == null))
			{
				return false;
			}
			return true;
		}

		private static void VerifyOfficialAccounts(AbstractLogger logger, GetStateResponse response, IUserDatabase userDatabase, IMixWebCallFactory mixWebCallFactory, Action successCallback, Action failureCallback)
		{
			if (!HasAllOfficialAccountsInDatabase(response, userDatabase))
			{
				OfficialAccountGetter.GetAllOfficialAccounts(logger, userDatabase, mixWebCallFactory, delegate
				{
					successCallback();
				}, failureCallback);
			}
			else
			{
				successCallback();
			}
		}

		private static bool HasAllOfficialAccountsInDatabase(GetStateResponse response, IUserDatabase userDatabase)
		{
			List<string> source = ((response.ChatThreads == null) ? new List<string>() : (from t in response.ChatThreads
				select t.OfficialAccountId into s
				where s != null
				select s).ToList());
			return response.OfficialAccounts.All(userDatabase.ContainsOfficialAccount) && source.All(userDatabase.ContainsOfficialAccount);
		}
	}
}
