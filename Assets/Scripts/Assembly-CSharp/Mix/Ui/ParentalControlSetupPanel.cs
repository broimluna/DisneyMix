using System.Collections.Generic;
using Disney.Mix.SDK;
using Mix.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ParentalControlSetupPanel : BasePanel, ParentControlAddChildItem.IParentControlAddChildItemListener, ParentalControlChildItem.IParentalControlChildItemListener
	{
		public interface IParentalControlSetupPanelListener
		{
			void OnAddChildManually(ILocalUser aParent);

			void OnVerifyAdult(ILocalUser aParent);
		}

		public GameObject ChildItem;

		public GameObject AddChildItem;

		public ScrollView ScrollView;

		public Text Email;

		private IParentalControlSetupPanelListener currentListener;

		private IEnumerable<ILinkedUser> claimableUsers;

		private ILocalUser parent;

		public void Init(ILocalUser aParent, IEnumerable<ILinkedUser> aClaimableUsers, IEnumerable<ILinkedUser> aLinkedUsers, IParentalControlSetupPanelListener aListener)
		{
			parent = aParent;
			claimableUsers = aClaimableUsers;
			currentListener = aListener;
			Email.text = MixSession.User.RegistrationProfile.Email;
			if (claimableUsers != null)
			{
				foreach (ILinkedUser claimableUser in claimableUsers)
				{
					ParentalControlChildItem parentalControlChildItem = new ParentalControlChildItem(ChildItem, claimableUser, false, this);
					parentalControlChildItem.Id = ScrollView.Add(parentalControlChildItem, false);
				}
			}
			if (aLinkedUsers != null)
			{
				foreach (ILinkedUser aLinkedUser in aLinkedUsers)
				{
					ParentalControlChildItem parentalControlChildItem2 = new ParentalControlChildItem(ChildItem, aLinkedUser, true, this);
					parentalControlChildItem2.Id = ScrollView.Add(parentalControlChildItem2, false);
				}
			}
			ParentControlAddChildItem parentControlAddChildItem = new ParentControlAddChildItem(AddChildItem, this);
			parentControlAddChildItem.Id = ScrollView.Add(parentControlAddChildItem, false);
		}

		public void OnAddChildItemClicked()
		{
			currentListener.OnAddChildManually(parent);
		}

		public void LinkClaimableChild(ILinkedUser aChild, ParentalControlChildItem aItem)
		{
			ILinkedUser[] children = new ILinkedUser[1] { aChild };
			parent.LinkClaimableChildAccounts(children, delegate(ILinkChildResult linkChildResult)
			{
				if (linkChildResult.Success)
				{
					aItem.DisableToggle();
				}
			});
		}

		public void OnSetupPCClicked()
		{
			currentListener.OnVerifyAdult(parent);
		}
	}
}
