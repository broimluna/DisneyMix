using Mix.Data;
using Mix.Games.Data;
using UnityEngine;

namespace Mix.DataEntry
{
	public class OverlayUI : MonoBehaviour
	{
		public static SharedEntitlement sharedEntitlement;

		public static GameObject targetGameObject;

		protected GameObject overlay;

		public static void SetEntitlement(BaseContentData aBaseContentData, GameObject aGameObject)
		{
			sharedEntitlement = new SharedEntitlement(aBaseContentData);
			targetGameObject = aGameObject;
		}

		public static void SetEntitlement(MixGameContentData aGameContentData, GameObject aGameObject)
		{
			sharedEntitlement = new SharedEntitlement(aGameContentData);
			targetGameObject = aGameObject;
		}

		public void Init(object aPrefab)
		{
			overlay = Object.Instantiate(aPrefab as GameObject);
			overlay.transform.SetParent(base.gameObject.transform);
			overlay.transform.localScale = new Vector3(1f, 1f, 1f);
			overlay.transform.localPosition = new Vector3(0f, 0f, 0f);
		}

		public void Clear()
		{
			if (overlay != null)
			{
				Object.Destroy(overlay);
				overlay = null;
			}
			Object.Destroy(this);
		}
	}
}
