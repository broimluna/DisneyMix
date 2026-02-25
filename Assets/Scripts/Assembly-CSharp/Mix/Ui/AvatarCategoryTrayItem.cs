using UnityEngine;

namespace Mix.Ui
{
	public class AvatarCategoryTrayItem : ISubSelectorEntry<AvatarCategoryTrayInfo>
	{
		public GameObject mainObj;

		public string disableId;

		public AvatarCategoryTrayInfo trayData;

		public AvatarCategoryTrayItem(GameObject go, string catName, string locToken, string aDisableId)
		{
			mainObj = go;
			disableId = aDisableId;
			trayData = new AvatarCategoryTrayInfo(catName, locToken, disableId);
		}

		public AvatarCategoryTrayInfo GetContent()
		{
			return trayData;
		}

		public void Clean()
		{
			mainObj = null;
			trayData = null;
		}

		public U GetThumbComponent<U>() where U : Component
		{
			return mainObj.GetComponent<U>();
		}

		public string GetDeselectedEntitlement()
		{
			return disableId;
		}
	}
}
