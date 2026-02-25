using System.Collections.Generic;
using Disney.Mix.SDK;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ParentalControlsPanel : BasePanel
	{
		public interface IParentalControlsPanelListener
		{
			void OnAddChild(ILocalUser aParent, BasePanel fromPanel);
		}

		public GameObject ChildItem;

		public ScrollView ScrollView;

		public Text Email;

		private ILocalUser parent;

		private IEnumerable<ILinkedUser> linkedUsers;

		private IParentalControlsPanelListener currentListener;

		private void Start()
		{
		}

		public void Init(ILocalUser aParent, IParentalControlsPanelListener aListener)
		{
			parent = aParent;
			currentListener = aListener;
			Email.text = parent.RegistrationProfile.Email;
			aParent.GetLinkedChildren(delegate(IGetLinkedUsersResult getLinkedUsersResult)
			{
				if (getLinkedUsersResult.Success)
				{
					linkedUsers = getLinkedUsersResult.LinkedUsers;
					if (linkedUsers != null)
					{
						foreach (ILinkedUser linkedUser in linkedUsers)
						{
							ParentalControlChildOpenChatItem parentalControlChildOpenChatItem = new ParentalControlChildOpenChatItem(ChildItem, parent, linkedUser);
							parentalControlChildOpenChatItem.Id = ScrollView.Add(parentalControlChildOpenChatItem, false);
						}
					}
				}
			});
		}

		public void Toggled(long aId)
		{
		}

		public void OnAddChildClicked()
		{
			currentListener.OnAddChild(parent, this);
		}
	}
}
