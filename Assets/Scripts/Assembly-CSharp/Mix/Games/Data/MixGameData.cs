using System;
using System.Collections.Generic;

namespace Mix.Games.Data
{
	public class MixGameData
	{
		public const int NO_RESPONSE = -1;

		public string Entitlement { get; set; }

		public virtual string GameProcessor
		{
			get
			{
				return "noOpGame";
			}
		}

		public MixGameSessionStatus Status { get; set; }

		public MixGameDataType Type { get; set; }

		public Version Version { get; set; }

		public T CreateNewReponse<T>(List<T> aResponses, string myUserSwid) where T : MixGameResponse, new()
		{
			T val = new T
			{
				PlayerSwid = myUserSwid
			};
			aResponses.Add(val);
			return val;
		}

		public T GetMyResponse<T>(List<T> aResponses, string myUserSwid) where T : MixGameResponse, new()
		{
			T val = (T)null;
			foreach (T aResponse in aResponses)
			{
				T current = aResponse;
				if (string.Equals(current.PlayerSwid, myUserSwid))
				{
					val = current;
					break;
				}
			}
			if (val == null)
			{
				val = CreateNewReponse(aResponses, myUserSwid);
			}
			return val;
		}

		public T GetMyLastResponse<T>(List<T> aResponses, string myUserSwid) where T : MixGameResponse, new()
		{
			T val = (T)null;
			for (int num = aResponses.Count - 1; num >= 0; num--)
			{
				T val2 = aResponses[num];
				if (string.Equals(val2.PlayerSwid, myUserSwid))
				{
					val = val2;
					break;
				}
			}
			if (val == null)
			{
				val = CreateNewReponse(aResponses, myUserSwid);
			}
			return val;
		}

		public bool HasResponse<T>(List<T> aResponses, string aSwid) where T : MixGameResponse
		{
			foreach (T aResponse in aResponses)
			{
				if (string.Equals(aResponse.PlayerSwid, aSwid))
				{
					return true;
				}
			}
			return false;
		}
	}
}
