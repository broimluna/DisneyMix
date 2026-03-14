using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class CategoryListItem : IScrollItem
	{
		private GameObject prefab;

		private ICategoryListItem listener;

		private bool state = true;

		private Transform arrow;

		public CategoryListItem(GameObject aPrefab, ICategoryListItem aListener)
		{
			prefab = aPrefab;
			listener = aListener;
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			GameObject gameObject = Object.Instantiate(prefab);
			gameObject.GetComponent<Button>().onClick.AddListener(OnClicked);
			arrow = gameObject.transform.Find("ExpandArrow");
			return gameObject;
		}

		void IScrollItem.Destroy()
		{
		}

		private void OnClicked()
		{
			if (arrow != null)
			{
				state = !state;
				listener.OnCategoryToggled(state);
				arrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, (!state) ? 180 : 0);
			}
		}
	}
}
