using System;
using System.Collections.Generic;
using DeviceDB;

namespace Disney.Mix.SDK.Internal
{
	public class DatabaseCorruptionHandler
	{
		private readonly AbstractLogger logger;

		private readonly Dictionary<object, Action> deleters;

		private readonly IFileSystem fileSystem;

		private readonly string sdkStorageDirPath;

		public event EventHandler<CorruptionDetectedEventArgs> OnCorruptionDetected = delegate
		{
		};

		public DatabaseCorruptionHandler(AbstractLogger logger, IFileSystem fileSystem, string sdkStorageDirPath)
		{
			this.logger = logger;
			this.fileSystem = fileSystem;
			this.sdkStorageDirPath = sdkStorageDirPath;
			deleters = new Dictionary<object, Action>();
		}

		public void Add<TDocument>(IDocumentCollection<TDocument> collection) where TDocument : AbstractDocument
		{
			deleters[collection] = collection.Delete;
		}

		public void Remove<TDocument>(IDocumentCollection<TDocument> collection) where TDocument : AbstractDocument
		{
			deleters.Remove(collection);
		}

		public void HandleCorruption(CorruptionException ex)
		{
			logger.Fatal("Corruption detected: " + ex);
			foreach (KeyValuePair<object, Action> deleter in deleters)
			{
				deleter.Value();
			}
			if (fileSystem.DirectoryExists(sdkStorageDirPath))
			{
				fileSystem.DeleteDirectory(sdkStorageDirPath);
			}
			this.OnCorruptionDetected(this, new CorruptionDetectedEventArgs());
		}
	}
}
