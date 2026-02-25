using System.Collections.Generic;
using Mix.Data;
using Mix.Entitlements;

namespace Mix.Ui
{
	public class AvatarCategoryTrayInfo
	{
		public string categoryName;

		public string locCategoryName;

		public string disableId;

		public AvatarCategoryTrayInfo(string aCategoryName, string aLocText, string aDisableId)
		{
			categoryName = aCategoryName;
			locCategoryName = aLocText;
			disableId = aDisableId;
		}

		public List<BaseContentData> GetThumbContent()
		{
			List<BaseContentData> list = new List<BaseContentData>();
			List<Avatar_Multiplane> myAvatarDataByCategory = Singleton<EntitlementsManager>.Instance.GetMyAvatarDataByCategory(categoryName);
			if (myAvatarDataByCategory != null)
			{
				for (int i = 0; i < myAvatarDataByCategory.Count; i++)
				{
					if (myAvatarDataByCategory[i].GetUid() != disableId && !myAvatarDataByCategory[i].GetHidden())
					{
						list.Add(myAvatarDataByCategory[i]);
					}
				}
			}
			return list;
		}

		public string GetDefaultUid()
		{
			if (!string.IsNullOrEmpty(disableId))
			{
				return disableId;
			}
			List<Avatar_Multiplane> myAvatarDataByCategory = Singleton<EntitlementsManager>.Instance.GetMyAvatarDataByCategory(categoryName);
			if (myAvatarDataByCategory != null)
			{
				return myAvatarDataByCategory[0].GetUid();
			}
			return null;
		}
	}
}
