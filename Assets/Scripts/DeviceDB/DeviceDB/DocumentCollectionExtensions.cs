using System;
using System.Collections.Generic;

namespace DeviceDB
{
	public static class DocumentCollectionExtensions
	{
		public static IEnumerable<uint> FindDocumentIdsEqual<TDocument, TField>(this IDocumentCollection<TDocument> collection, string fieldName, TField value) where TDocument : AbstractDocument, new() where TField : IComparable<TField>
		{
			return collection.FindDocumentIds(fieldName, value, (TField v) => NullSafeComparer.Compare(v, value) == 0);
		}

		public static IEnumerable<uint> FindDocumentIdsLessThanOrEqualTo<TDocument, TField>(this IDocumentCollection<TDocument> collection, string fieldName, TField value) where TDocument : AbstractDocument, new() where TField : IComparable<TField>
		{
			return collection.FindDocumentIds(fieldName, value, (TField v) => NullSafeComparer.Compare(v, value) <= 0);
		}

		public static IEnumerable<uint> FindDocumentIdsGreaterThan<TDocument, TField>(this IDocumentCollection<TDocument> collection, string fieldName, TField value) where TDocument : AbstractDocument, new() where TField : IComparable<TField>
		{
			return collection.FindDocumentIds(fieldName, value, (TField v) => NullSafeComparer.Compare(v, value) > 0);
		}
	}
}
