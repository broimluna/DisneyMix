using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace DeviceDB
{
	internal class DocumentCollection<TDocument> : IEnumerable<TDocument>, IEnumerable, IDocumentCollection<TDocument>, IDisposable where TDocument : AbstractDocument, new()
	{
		private readonly PackedFile packedFile;

		private readonly JournalPlayer journalPlayer;

		private readonly JournalWriter journalWriter;

		private readonly FieldIndexes<TDocument> fieldIndexes;

		private readonly object transactionLockObject;

		private Aes256Encryptor encryptor;

		private uint metadataDocumentId;

		private FieldInfo[] fields;

		private bool isFieldIndexesInUse;

		private bool isDisposed;

		private Type documentType;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private LogHandler _003CDebugLogHandler_003Ek__BackingField;

		public LogHandler DebugLogHandler
		{
			[CompilerGenerated]
			get
			{
				return _003CDebugLogHandler_003Ek__BackingField;
			}
		}

		public DocumentCollection(PackedFile packedFile, IndexFactory indexFactory, byte[] key, JournalPlayer journalPlayer, JournalWriter journalWriter)
		{
			transactionLockObject = new object();
			EnsureValidKey(key);
			documentType = typeof(TDocument);
			try
			{
				journalPlayer.Play();
				SerializerReflectionCache.AddTypes(typeof(MetadataDocument), documentType);
				this.packedFile = packedFile;
				this.journalPlayer = journalPlayer;
				this.journalWriter = journalWriter;
				byte[] initializationVector;
				if (packedFile.IsEmpty)
				{
					initializationVector = CryptoRandomNumberGenerator.GenerateBytes(16u);
					journalWriter.Start();
					WriteMetadataDocument(initializationVector);
					journalWriter.Finish();
					journalPlayer.Play();
				}
				else
				{
					KeyValuePair<uint, byte[]> keyValuePair = packedFile.Documents.First();
					metadataDocumentId = keyValuePair.Key;
					MetadataDocument metadataDocument = BinarySerializer.Deserialize<MetadataDocument>(keyValuePair.Value);
					initializationVector = metadataDocument.InitializationVector;
				}
				encryptor = new Aes256Encryptor(key, initializationVector);
				fieldIndexes = new FieldIndexes<TDocument>(indexFactory, encryptor);
			}
			catch (Exception)
			{
				journalWriter.Discard();
				throw;
			}
		}

		public void Insert(TDocument document)
		{
			lock (transactionLockObject)
			{
				if (isDisposed)
				{
					throw new ObjectDisposedException("Can't use Insert() after Dispose() or Delete()");
				}
				if (isFieldIndexesInUse)
				{
					throw new InvalidOperationException("Can't use Insert() before disposing the enumerable from a FindDocumentIds call");
				}
				if (document == null)
				{
					if (DebugLogHandler != null)
					{
						DebugLogHandler("Insert: null");
					}
					throw new ArgumentException("Document to insert can't be null", "document");
				}
				if (DebugLogHandler != null)
				{
					if (fields == null)
					{
						fields = documentType.GetFields();
					}
					StringBuilder stringBuilder = new StringBuilder("Insert: document=\n");
					FieldInfo[] array = fields;
					foreach (FieldInfo fieldInfo in array)
					{
						stringBuilder.Append(fieldInfo.Name);
						stringBuilder.Append(" = ");
						stringBuilder.Append(fieldInfo.GetValue(document));
						stringBuilder.Append('\n');
					}
					DebugLogHandler(stringBuilder.ToString());
				}
				if (document.Id != 0)
				{
					throw new ArgumentException("Document already has an ID. Update Update() instead.");
				}
				byte[] document2 = SerializeAndEncrypt(document);
				journalWriter.Start();
				try
				{
					document.Id = packedFile.Insert(document2);
					fieldIndexes.Insert(document);
					journalWriter.Finish();
					journalPlayer.Play();
				}
				catch (Exception)
				{
					journalWriter.Discard();
					throw;
				}
			}
		}

		public void Update(TDocument document)
		{
			lock (transactionLockObject)
			{
				if (isDisposed)
				{
					throw new ObjectDisposedException("Can't use Update() after Dispose() or Delete()");
				}
				if (isFieldIndexesInUse)
				{
					throw new InvalidOperationException("Can't use Update() before disposing the enumerable from a FindDocumentIds call");
				}
				if (document == null)
				{
					if (DebugLogHandler != null)
					{
						DebugLogHandler("Update: null");
					}
					throw new ArgumentException("Can't update a null document");
				}
				if (DebugLogHandler != null)
				{
					if (fields == null)
					{
						fields = documentType.GetFields();
					}
					StringBuilder stringBuilder = new StringBuilder("Update: document=\n");
					FieldInfo[] array = fields;
					foreach (FieldInfo fieldInfo in array)
					{
						stringBuilder.Append(fieldInfo.Name);
						stringBuilder.Append(" = ");
						stringBuilder.Append(fieldInfo.GetValue(document));
						stringBuilder.Append('\n');
					}
					DebugLogHandler(stringBuilder.ToString());
				}
				if (document.Id == metadataDocumentId)
				{
					throw new ArgumentException("Document " + document.Id + " not found");
				}
				byte[] array2 = packedFile.Find(document.Id);
				if (array2 == null)
				{
					throw new ArgumentException("Document " + document.Id + " not found");
				}
				TDocument oldDocument = DecryptAndDeserialize(array2, document.Id);
				byte[] document2 = SerializeAndEncrypt(document);
				journalWriter.Start();
				try
				{
					document.Id = packedFile.Update(document.Id, document2);
					fieldIndexes.Update(document, oldDocument);
					journalWriter.Finish();
					journalPlayer.Play();
				}
				catch (Exception)
				{
					journalWriter.Discard();
					throw;
				}
			}
		}

		public void Delete(uint documentId)
		{
			lock (transactionLockObject)
			{
				if (isDisposed)
				{
					throw new ObjectDisposedException("Can't use Delete(id) after Dispose() or Delete()");
				}
				if (isFieldIndexesInUse)
				{
					throw new InvalidOperationException("Can't use Delete(id) before disposing the enumerable from a FindDocumentIds call");
				}
				if (DebugLogHandler != null)
				{
					DebugLogHandler("Delete: documentId=" + documentId);
				}
				if (documentId == metadataDocumentId)
				{
					throw new ArgumentException("Document " + documentId + " not found");
				}
				TDocument val = Find(documentId);
				if (val == null)
				{
					throw new ArgumentException("Document " + documentId + " not found");
				}
				journalWriter.Start();
				try
				{
					packedFile.Remove(documentId);
					fieldIndexes.Delete(val);
					journalWriter.Finish();
					journalPlayer.Play();
				}
				catch (Exception)
				{
					journalWriter.Discard();
					throw;
				}
			}
		}

		public void Delete()
		{
			lock (transactionLockObject)
			{
				if (!isDisposed)
				{
					isDisposed = true;
					if (DebugLogHandler != null)
					{
						DebugLogHandler("Delete: whole collection");
					}
					packedFile.Delete();
					fieldIndexes.Delete();
					journalWriter.Delete();
				}
			}
		}

		public void Drop()
		{
			lock (transactionLockObject)
			{
				if (isDisposed)
				{
					throw new ObjectDisposedException("Can't use Drop() after Dispose() or Delete()");
				}
				if (isFieldIndexesInUse)
				{
					throw new InvalidOperationException("Can't use Drop() before disposing the enumerable from a FindDocumentIds call");
				}
				if (DebugLogHandler != null)
				{
					DebugLogHandler("Drop");
				}
				try
				{
					journalWriter.Start();
					packedFile.JournaledClear();
					fieldIndexes.JournaledClear();
					journalWriter.Finish();
					journalPlayer.Play();
					metadataDocumentId = 0u;
					journalWriter.Start();
					WriteMetadataDocument(encryptor.InitializationVector);
					journalWriter.Finish();
					journalPlayer.Play();
				}
				catch (Exception)
				{
					journalWriter.Discard();
					throw;
				}
			}
		}

		public TDocument Find(uint documentId)
		{
			lock (transactionLockObject)
			{
				if (isDisposed)
				{
					throw new ObjectDisposedException("Can't use Find() after Dispose() or Delete()");
				}
				if (DebugLogHandler != null)
				{
					DebugLogHandler("Find: documentId=" + documentId);
				}
				if (documentId == metadataDocumentId)
				{
					return (TDocument)null;
				}
				byte[] array = packedFile.Find(documentId);
				return (array != null) ? DecryptAndDeserialize(array, documentId) : ((TDocument)null);
			}
		}

		public IEnumerable<uint> FindDocumentIds<TField>(string fieldName, TField value, Predicate<TField> matcher) where TField : IComparable<TField>
		{
			lock (transactionLockObject)
			{
				if (isDisposed)
				{
					throw new ObjectDisposedException("Can't use FindDocumentIds() after Dispose() or Delete()");
				}
				if (isFieldIndexesInUse)
				{
					throw new InvalidOperationException("Can't use FindDocumentIds() before disposing the enumerable from a previous FindDocumentIds or FindDocuments call");
				}
				if (DebugLogHandler != null)
				{
					DebugLogHandler(string.Concat("FindDocumentIds: fieldName=", fieldName, ", value=", value, ", matcher=", matcher));
				}
				Index<TField> index = fieldIndexes.GetIndex<TField>(fieldName);
				isFieldIndexesInUse = true;
				try
				{
					foreach (KeyValuePair<uint, TField> item in index.Find(value, matcher))
					{
						yield return item.Key;
						if (isDisposed)
						{
							break;
						}
					}
				}
				finally
				{
					isFieldIndexesInUse = false;
				}
			}
		}

		public uint? FindDocumentIdMax<TField>(string fieldName) where TField : IComparable<TField>
		{
			lock (transactionLockObject)
			{
				if (isDisposed)
				{
					throw new ObjectDisposedException("Can't use FindDocumentIdMax() after Dispose() or Delete()");
				}
				if (isFieldIndexesInUse)
				{
					throw new InvalidOperationException("Can't use FindDocumentIdMax() before disposing the enumerable from a FindDocumentIds or FindDocuments call");
				}
				if (DebugLogHandler != null)
				{
					DebugLogHandler("FindDocumentIdMax: fieldName=" + fieldName);
				}
				Index<TField> index = fieldIndexes.GetIndex<TField>(fieldName);
				return index.FindMaxId();
			}
		}

		IEnumerator<TDocument> IEnumerable<TDocument>.GetEnumerator()
		{
			lock (transactionLockObject)
			{
				if (isDisposed)
				{
					throw new ObjectDisposedException("Can't use GetEnumerator() or foreach after Dispose() or Delete()");
				}
				if (DebugLogHandler != null)
				{
					DebugLogHandler("GetEnumerator<TDocument>");
				}
				foreach (KeyValuePair<uint, byte[]> pair in packedFile.Documents)
				{
					if (pair.Key != metadataDocumentId)
					{
						yield return DecryptAndDeserialize(pair.Value, pair.Key);
					}
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			lock (transactionLockObject)
			{
				if (isDisposed)
				{
					throw new ObjectDisposedException("Can't use GetEnumerator() or foreach after Dispose() or Delete()");
				}
				if (DebugLogHandler != null)
				{
					DebugLogHandler("GetEnumerator");
				}
				foreach (KeyValuePair<uint, byte[]> pair in packedFile.Documents)
				{
					if (pair.Key != metadataDocumentId)
					{
						yield return DecryptAndDeserialize(pair.Value, pair.Key);
					}
				}
			}
		}

		public void Dispose()
		{
			lock (transactionLockObject)
			{
				if (!isDisposed)
				{
					isDisposed = true;
					if (DebugLogHandler != null)
					{
						DebugLogHandler("Dispose");
					}
					packedFile.Dispose();
					fieldIndexes.Dispose();
					journalWriter.Dispose();
					journalPlayer.Dispose();
				}
			}
		}

		public void IndexField(string fieldName)
		{
			lock (transactionLockObject)
			{
				if (isDisposed)
				{
					throw new ObjectDisposedException("Can't use IndexField() after Dispose() or Delete()");
				}
				if (isFieldIndexesInUse)
				{
					throw new InvalidOperationException("Can't use IndexField() before disposing the enumerable from a FindDocumentIds call");
				}
				if (DebugLogHandler != null)
				{
					DebugLogHandler("IndexField: fieldName=" + fieldName);
				}
				journalWriter.Start();
				try
				{
					fieldIndexes.ClearField(fieldName);
					List<KeyValuePair<uint, IComparable>> list = new List<KeyValuePair<uint, IComparable>>();
					foreach (KeyValuePair<uint, byte[]> document2 in packedFile.Documents)
					{
						if (document2.Key != metadataDocumentId)
						{
							TDocument document = DecryptAndDeserialize(document2.Value, document2.Key);
							IComparable fieldValue = fieldIndexes.GetFieldValue(fieldName, document);
							list.Add(new KeyValuePair<uint, IComparable>(document2.Key, fieldValue));
						}
					}
					list.Sort((KeyValuePair<uint, IComparable> a, KeyValuePair<uint, IComparable> b) => NullSafeComparer.Compare(a.Value, b.Value));
					fieldIndexes.InsertPreSorted(fieldName, list);
					journalWriter.Finish();
					journalPlayer.Play();
				}
				catch (Exception)
				{
					journalWriter.Discard();
					throw;
				}
			}
		}

		private byte[] SerializeAndEncrypt(TDocument document)
		{
			return SerializeAndEncrypt(document, encryptor);
		}

		private byte[] SerializeAndEncrypt(TDocument document, Aes256Encryptor encryptor)
		{
			byte[] bytes = BinarySerializer.Serialize(document, documentType);
			return encryptor.Encrypt(bytes);
		}

		private TDocument DecryptAndDeserialize(byte[] inBytes, uint documentId)
		{
			byte[] bytes = encryptor.Decrypt(inBytes);
			TDocument val = BinarySerializer.Deserialize<TDocument>(bytes);
			val.Id = documentId;
			return val;
		}

		private void WriteMetadataDocument(byte[] initializationVector)
		{
			MetadataDocument metadataDocument = new MetadataDocument();
			metadataDocument.InitializationVector = initializationVector;
			MetadataDocument obj = metadataDocument;
			byte[] document = BinarySerializer.Serialize(obj, typeof(MetadataDocument));
			metadataDocumentId = ((metadataDocumentId == 0) ? packedFile.Insert(document) : packedFile.Update(metadataDocumentId, document));
		}

		private static void EnsureValidKey(byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new ArgumentException("key is null or the wrong size");
			}
		}
	}
}
