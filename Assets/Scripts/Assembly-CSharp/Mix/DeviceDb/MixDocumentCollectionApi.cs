using System;
using System.Collections.Generic;
using System.IO;
using DeviceDB;
using Mix.Assets;
using Mix.Session;

namespace Mix.DeviceDb
{
	public abstract class MixDocumentCollectionApi<TDocument> : IMixDocumentCollection<TDocument> where TDocument : AbstractDocument, new()
	{
		protected virtual string CollectionName
		{
			get
			{
				return null;
			}
		}

		public Dictionary<string, IDocumentCollection<TDocument>> collections { get; protected set; }

		public MixDocumentCollectionApi()
		{
			collections = new Dictionary<string, IDocumentCollection<TDocument>>();
		}

		public virtual void LogOut()
		{
		}

		public virtual void Delete()
		{
			foreach (KeyValuePair<string, IDocumentCollection<TDocument>> collection in collections)
			{
				if (collection.Value != null)
				{
					collection.Value.Delete();
				}
			}
			if (CollectionName != null && Directory.Exists(Singleton<MixDocumentCollections>.Instance.DirPath + "/" + CollectionName + "/"))
			{
				Directory.Delete(Singleton<MixDocumentCollections>.Instance.DirPath + "/" + CollectionName + "/", true);
			}
			collections = new Dictionary<string, IDocumentCollection<TDocument>>();
		}

		public void Clear()
		{
			foreach (KeyValuePair<string, IDocumentCollection<TDocument>> collection in collections)
			{
				if (collection.Value != null)
				{
					collection.Value.Drop();
					collection.Value.Dispose();
				}
			}
			collections = new Dictionary<string, IDocumentCollection<TDocument>>();
		}

		public string GetUserId()
		{
			if (MixSession.User == null)
			{
				return "user";
			}
			return AssetManager.GetShaString(MixSession.User.Id);
		}

		public List<uint> GetDocumentIdsGreaterThenInt(string key, int limit, IDocumentCollection<TDocument> collection)
		{
			List<uint> list = new List<uint>();
			try
			{
				foreach (uint item in collection.FindDocumentIdsGreaterThan(key, limit))
				{
					list.Add(item);
				}
			}
			catch (CorruptionException exception)
			{
				Log.Exception(string.Empty, exception);
				Singleton<MixDocumentCollections>.Instance.DeleteAll();
			}
			catch (Exception exception2)
			{
				Log.Exception(string.Empty, exception2);
			}
			return list;
		}

		public TDocument FindDocById(IDocumentCollection<TDocument> collection, uint id)
		{
			try
			{
				return collection.Find(id);
			}
			catch (CorruptionException exception)
			{
				Log.Exception(string.Empty, exception);
				Singleton<MixDocumentCollections>.Instance.DeleteAll();
			}
			catch (Exception exception2)
			{
				Log.Exception(string.Empty, exception2);
			}
			return (TDocument)null;
		}

		public void DeleteDocument(IDocumentCollection<TDocument> collection, TDocument doc)
		{
			try
			{
				if (doc != null)
				{
					collection.Delete(doc.Id);
				}
			}
			catch (Exception exception)
			{
				Log.Exception(string.Empty, exception);
			}
		}

		public List<TDocument> GetAllDocs(List<IDocumentCollection<TDocument>> collections)
		{
			List<TDocument> list = new List<TDocument>();
			try
			{
				foreach (IDocumentCollection<TDocument> collection in collections)
				{
					foreach (TDocument item in collection)
					{
						list.Add(item);
					}
				}
			}
			catch (Exception exception)
			{
				Log.Exception(string.Empty, exception);
			}
			return list;
		}

		public TDocument GetDocumentByFieldAndKey(IDocumentCollection<TDocument> collection, string fieldName, string key)
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(fieldName) || collection == null)
			{
				return (TDocument)null;
			}
			try
			{
				using (IEnumerator<uint> enumerator = collection.FindDocumentIdsEqual(fieldName, key).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						uint current = enumerator.Current;
						return collection.Find(current);
					}
				}
			}
			catch (CorruptionException exception)
			{
				Log.Exception(string.Empty, exception);
				Singleton<MixDocumentCollections>.Instance.DeleteAll();
			}
			catch (Exception exception2)
			{
				Log.Exception(string.Empty, exception2);
			}
			return (TDocument)null;
		}

		public IDocumentCollection<TDocument> GetCollection(string collectionName, string aDirPath)
		{
			if (string.IsNullOrEmpty(collectionName) || string.IsNullOrEmpty(aDirPath))
			{
				return null;
			}
			if (!collections.ContainsKey(collectionName))
			{
				try
				{
					collections[collectionName] = Singleton<MixDocumentCollections>.Instance.factory.CreateHighSecurityFileSystemCollection<TDocument>(Singleton<MixDocumentCollections>.Instance.DirPath + aDirPath, Singleton<MixDocumentCollections>.Instance.key);
				}
				catch (Exception exception)
				{
					Log.Exception(string.Empty, exception);
					return null;
				}
			}
			return collections[collectionName];
		}
	}
}
