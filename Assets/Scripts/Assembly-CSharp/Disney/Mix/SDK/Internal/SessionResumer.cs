using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class SessionResumer
	{
		public static void Resume(AbstractLogger logger, IGetStateResponseParser getStateResponseParser, IEpochTime epochTime, string clientVersion, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, IInternalLocalUser localUser, IDatabase database, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, INotificationPoller notificationPoller, IChatMessageHandler chatMessageHandler, Action<IResumeSessionResult> callback)
		{
			long? serverTimeOffsetMillis = database.GetServerTimeOffsetMillis();
			epochTime.OffsetMilliseconds = ((!serverTimeOffsetMillis.HasValue) ? 0 : serverTimeOffsetMillis.Value);
			SessionDocument sessionDocument = database.GetSessionDocument(localUser.Swid);
			long lastNotificationTime = sessionDocument.LastNotificationTime;
			StateGetter.GetState(logger, epochTime, clientVersion, database, userDatabase, notificationQueue, mixWebCallFactory, localUser.Swid, lastNotificationTime, delegate(GetStateResponse response)
			{
				HandleGetStateSuccess(logger, getStateResponseParser, response, mixWebCallFactory, localUser, userDatabase, chatMessageParser, notificationPoller, chatMessageHandler, callback);
			}, delegate
			{
				callback(new ResumeSessionResult(false));
			});
		}

		private static void HandleGetStateSuccess(AbstractLogger logger, IGetStateResponseParser getStateResponseParser, GetStateResponse response, IMixWebCallFactory mixWebCallFactory, IInternalLocalUser localUser, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, INotificationPoller notificationPoller, IChatMessageHandler chatMessageHandler, Action<IResumeSessionResult> callback)
		{
			try
			{
				userDatabase.SyncToGetStateResponse(response, delegate
				{
					HandleGetStateSynced(getStateResponseParser, response, mixWebCallFactory, localUser, userDatabase, chatMessageParser, notificationPoller, chatMessageHandler, callback);
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(new ResumeSessionResult(false));
			}
		}

		private static void HandleGetStateSynced(IGetStateResponseParser getStateResponseParser, GetStateResponse response, IMixWebCallFactory mixWebCallFactory, IInternalLocalUser localUser, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, INotificationPoller notificationPoller, IChatMessageHandler chatMessageHandler, Action<IResumeSessionResult> callback)
		{
			int[] pollIntervals;
			int[] pokeIntervals;
			getStateResponseParser.ParsePollIntervals(response, out pollIntervals, out pokeIntervals);
			notificationPoller.PollIntervals = pollIntervals;
			notificationPoller.PokeIntervals = pokeIntervals;
			notificationPoller.Jitter = response.NotificationIntervalsJitter.Value;
			notificationPoller.MaximumMissingNotificationTime = response.NotificationSequenceThreshold.Value;
			getStateResponseParser.ReconcileWithLocalUser(mixWebCallFactory, response, localUser, userDatabase, chatMessageParser, chatMessageHandler);
			callback(new ResumeSessionResult(true));
		}
	}
}
