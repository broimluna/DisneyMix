using System;
using DeviceDB;

namespace Disney.Mix.SDK.Internal
{
	public class OfflineSessionCreator : IOfflineSessionCreator
	{
		private readonly AbstractLogger logger;

		private readonly ISessionFactory sessionFactory;

		private readonly IDatabase database;

		public OfflineSessionCreator(AbstractLogger logger, ISessionFactory sessionFactory, IDatabase database)
		{
			this.logger = logger;
			this.sessionFactory = sessionFactory;
			this.database = database;
		}

		public IInternalOfflineLastSessionResult Create()
		{
			try
			{
				SessionDocument lastLoggedInSessionDocument = database.GetLastLoggedInSessionDocument();
				if (lastLoggedInSessionDocument == null)
				{
					return new OfflineLastSessionNotFoundResult();
				}
				IInternalSession session = sessionFactory.Create(lastLoggedInSessionDocument.Swid);
				return new OfflineLastSessionResult(true, session);
			}
			catch (CorruptionException ex)
			{
				logger.Fatal("Corruption detected during offline session creation: " + ex);
				return new OfflineLastSessionCorruptionDetectedResult();
			}
			catch (Exception ex2)
			{
				logger.Critical("Error creating session: " + ex2);
				return new OfflineLastSessionResult(false, null);
			}
		}
	}
}
