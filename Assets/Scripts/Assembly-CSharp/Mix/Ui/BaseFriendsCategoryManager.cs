using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mix.Ui
{
	public class BaseFriendsCategoryManager
	{
		protected string name;

		protected GameObject headerPrefab;

		protected GameObject itemPrefab;

		protected ScrollView scrollView;

		protected CategoryItem header;

		protected List<BaseFriendsListItem> scrollListItems = new List<BaseFriendsListItem>();

		protected bool visible = true;

		private bool addedToScrollView;

		public BaseFriendsCategoryManager(string aName, GameObject aHeaderPrefab, GameObject aItemPrefab, ScrollView aScrollView, int aCategoryId = -1)
		{
			name = aName;
			headerPrefab = aHeaderPrefab;
			itemPrefab = aItemPrefab;
			scrollView = aScrollView;
			header = new CategoryItem(headerPrefab, name, 0, true, aCategoryId);
			header.OnCategoryToggled += OnCategoryToggled;
		}

		public virtual void Refresh()
		{
		}

		public virtual int GetCount()
		{
			return 0;
		}

		protected virtual void UpdateHeader()
		{
			int count = GetCount();
			header.SetCount(count);
			if (addedToScrollView && count <= 0)
			{
				scrollView.Remove(header);
				addedToScrollView = false;
			}
			else if (!addedToScrollView && scrollListItems.Count() > 0)
			{
				scrollView.AddBefore(scrollListItems.First() as IScrollItem, header);
				addedToScrollView = true;
			}
		}

		protected void OnCategoryToggled(object sender, CategoryToggledEventArgs arguments)
		{
			visible = arguments.State;
			if (arguments.State)
			{
				Refresh();
				return;
			}
			scrollListItems.ForEach(delegate(BaseFriendsListItem item)
			{
				scrollView.Remove(item as IScrollItem);
			});
			scrollListItems.Clear();
		}
	}
}
