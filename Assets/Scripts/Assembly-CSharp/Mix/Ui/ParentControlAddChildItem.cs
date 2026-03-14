using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ParentControlAddChildItem : IScrollItem
	{
		public interface IParentControlAddChildItemListener
		{
			void OnAddChildItemClicked();
		}

		private long id;

		private GameObject item;

		private GameObject inst;

		private IParentControlAddChildItemListener currentListener;

		public long Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public ParentControlAddChildItem(GameObject aAddChildItem, IParentControlAddChildItemListener aListener)
		{
			item = aAddChildItem;
			currentListener = aListener;
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			inst = Object.Instantiate(item);
			UnityAction call = onClicked;
			inst.GetComponent<Button>().onClick.AddListener(call);
			return inst;
		}

		void IScrollItem.Destroy()
		{
		}

		private void onClicked()
		{
			currentListener.OnAddChildItemClicked();
		}
	}
}
