using System;
using System.Collections;
using System.Collections.Generic;

namespace DeviceDB
{
	public interface IDocumentCollection<TDocument> : IEnumerable<TDocument>, IEnumerable, IDisposable where TDocument : AbstractDocument
	{
		LogHandler DebugLogHandler { get; }

		void Insert(TDocument document);

		void Update(TDocument document);

		void Delete(uint documentId);

		void Delete();

		void Drop();

		TDocument Find(uint documentId);

		IEnumerable<uint> FindDocumentIds<TField>(string fieldName, TField value, Predicate<TField> matcher) where TField : IComparable<TField>;

		uint? FindDocumentIdMax<TField>(string fieldName) where TField : IComparable<TField>;

		void IndexField(string fieldName);
	}
}
