using System.Collections.Generic;

namespace Mix.Assets
{
	public interface IAssetDatabaseApi
	{
		long GetAccessCount(string aSha);

		int IncrementAccessCount(string aSha);

		void ClearAssets();

		void CreateAssetsTable();

		void CreateAssetsUniqueIndex();

		Dictionary<string, string> GetHeader(string aSha);

		bool CheckForRecord(string aSha);

		Record GetRecord(string aSha);

		void AddRecord(Record aRecord);

		void UpdateRecord(Record aRecord);

		List<uint> GetIdsByUseCountOrdered(float aPercentToReturn = 1f);

		void RemoveRecordByIndex(uint aIndex);

		Record GetRecordByIndex(uint index);

		bool DeleteRecordBySha(string aSha);
	}
}
