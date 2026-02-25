using System.Collections.Generic;
using Mix.Assets;

namespace Mix.Games.Data
{
	public static class MixGameContentDataHelper
	{
		public static void SetVisibleAndOwnedContent(List<MixGameContentData> aSource, ref List<MixGameContentData> aVisibleList)
		{
			if (aSource == null)
			{
				return;
			}
			foreach (MixGameContentData item in aSource)
			{
				if (ShouldShowItem(item) && DoesUserOwnItem(item))
				{
					aVisibleList.Add(item);
				}
			}
		}

		public static bool ShouldShowItem(MixGameContentData data)
		{
			if (MonoSingleton<AssetManager>.Instance.GetEnvironment() == CpipeEnvironment.Dev)
			{
				return true;
			}
			return !data.preview;
		}

		public static bool DoesUserOwnItem(MixGameContentData item)
		{
			return !item.hidden;
		}
	}
}
