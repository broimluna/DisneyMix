using Disney.Mix.SDK;
using Mix.Data;
using UnityEngine;

namespace Mix.Ui
{
	public class MediaPreview : MonoBehaviour
	{
		public GameObject itemHolder;

		public Transform placeholderLeft;

		public Transform placeholderRight;

		private IMediaPreview caller;

		private BaseContentData entitlement;

		private BasePreview preview;

		private void Start()
		{
			if (itemHolder != null && itemHolder.transform.parent != null && placeholderLeft != null && placeholderRight != null)
			{
				SkinnedMeshRenderer component = placeholderLeft.gameObject.GetComponent<SkinnedMeshRenderer>();
				SkinnedMeshRenderer component2 = placeholderRight.gameObject.GetComponent<SkinnedMeshRenderer>();
				component.sharedMesh = Object.Instantiate(component.sharedMesh);
				component2.sharedMesh = Object.Instantiate(component2.sharedMesh);
			}
		}

		public void OnApplicationPause(bool aState)
		{
			Hide();
		}

		public void Show(IMediaPreview aCaller, BaseContentData aEntitlement, IChatThread aChatThread)
		{
			if (!base.gameObject.activeSelf || entitlement != aEntitlement)
			{
				if (preview != null)
				{
					preview.Remove();
				}
				ClearHolder();
				caller = aCaller;
				entitlement = aEntitlement;
				preview = new BasePreview(itemHolder.transform, entitlement, aChatThread);
				base.gameObject.SetActive(true);
				base.gameObject.GetComponent<Animator>().enabled = true;
			}
		}

		public void Hide()
		{
			if (preview != null)
			{
				preview.Remove();
				preview = null;
			}
			base.gameObject.GetComponent<Animator>().Play("PreviewPanel_Out");
		}

		public void OnAcceptButtonClicked()
		{
			caller.OnSendEntitlement(entitlement);
		}

		public void ClearHolder()
		{
			for (int i = 0; i < itemHolder.transform.childCount; i++)
			{
				if (preview != null && itemHolder.transform.GetChild(i).gameObject != preview.ActiveGameObject)
				{
					Object.Destroy(itemHolder.transform.GetChild(i).gameObject);
				}
			}
		}
	}
}
