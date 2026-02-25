using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class CategoryItem : IScrollItem
	{
		public interface ICategoryListener
		{
			void OnCategoryClicked(string category);
		}

		private int categoryId;

		private GameObject item;

		private string categoryName;

		private int count;

		private bool expanded;

		private GameObject inst;

		public int CategoryId
		{
			get
			{
				return categoryId;
			}
			set
			{
				categoryId = value;
			}
		}

		public event EventHandler<CategoryToggledEventArgs> OnCategoryToggled = delegate
		{
		};

		public CategoryItem(GameObject aCategoryItem, string aName, int aCount, bool aExpanded, int aCategoryId = -1)
		{
			item = aCategoryItem;
			categoryName = aName;
			count = aCount;
			expanded = aExpanded;
			categoryId = aCategoryId;
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			inst = UnityEngine.Object.Instantiate(item);
			inst.transform.Find("CategoryNameText").GetComponent<Text>().text = categoryName + " (" + count + ")";
			if (!expanded)
			{
				inst.transform.Find("ExpandArrow").GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, (!expanded) ? 180 : 0);
			}
			UnityAction call = onClicked;
			inst.GetComponent<Button>().onClick.AddListener(call);
			return inst;
		}

		void IScrollItem.Destroy()
		{
		}

		public void SetCount(int newCount)
		{
			if (this != null)
			{
				count = newCount;
				if (!inst.IsNullOrDisposed())
				{
					inst.transform.Find("CategoryNameText").GetComponent<Text>().text = categoryName + " (" + count + ")";
				}
			}
		}

		private void onClicked()
		{
			Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/FriendPageArrows");
			expanded = !expanded;
			if (inst != null)
			{
				inst.transform.Find("ExpandArrow").GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, (!expanded) ? 180 : 0);
			}
			this.OnCategoryToggled(this, new CategoryToggledEventArgs(this, expanded));
		}
	}
}
