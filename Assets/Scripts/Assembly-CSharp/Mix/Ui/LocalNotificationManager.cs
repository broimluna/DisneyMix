using System;
using System.Collections;
using System.Collections.Generic;
using Disney.Mix.SDK;
using Mix.Session;
using UnityEngine;

namespace Mix.Ui
{
	public class LocalNotificationManager : MonoSingleton<LocalNotificationManager>
	{
		private const float STARTUP_DELAY = 3f;

		private const float CHECK_DELAY = 2f;

		private Queue<BaseNofitication> notificationQueue = new Queue<BaseNofitication>();

		private bool notificationShowing;

		private SdkEvents eventGenerator = new SdkEvents();

		public void Init()
		{
			notificationQueue.Clear();
			foreach (IAlert alert in MixSession.User.Alerts)
			{
				notificationQueue.Enqueue(new AlertLocalNotification(alert));
			}
			MixSession.User.OnAlertsAdded += AddAlertNotifications;
			MixSession.User.OnReceivedIncomingFriendInvitation += OnReceivedIncomingFriendInvitation;
			MixSession.User.OnReceivedOutgoingFriendInvitation += OnReceivedOutgoingFriendInvitation;
			foreach (IOutgoingFriendInvitation outgoingFriendInvitation in MixSession.User.OutgoingFriendInvitations)
			{
				outgoingFriendInvitation.OnAccepted += AddFriendAcceptedNotification;
			}
			Invoke("RunQueue", 3f);
		}

		public void RemoveNotificationsForThread(IChatThread aThread)
		{
			Queue<BaseNofitication> queue = new Queue<BaseNofitication>();
			foreach (BaseNofitication item in notificationQueue)
			{
				if (item is MessageLocalNotification && ((MessageLocalNotification)item).ToThread != aThread)
				{
					queue.Enqueue(item);
				}
			}
			if (queue.Count < notificationQueue.Count)
			{
				notificationQueue = queue;
			}
		}

		private void RunQueue()
		{
			if (!this.IsNullOrDisposed())
			{
				StopAllCoroutines();
				StartCoroutine(ProcessQueue());
			}
		}

		private IEnumerator ProcessQueue()
		{
			while (true)
			{
				if (notificationShowing || notificationQueue.Count <= 0)
				{
					yield return new WaitForSeconds(2f);
					continue;
				}
				notificationShowing = true;
				BaseNofitication notification = notificationQueue.Dequeue();
				GameObject note = notification.GenerateGameObject();
				note.GetComponent<AnimationEvents>().OnDestroyed += delegate
				{
					notificationShowing = false;
					notification.Destroy();
				};
				note.transform.SetParent(GameObject.Find("Persistent_Holder").transform, false);
				note.SetActive(true);
				yield return new WaitForSeconds(2f);
			}
		}

		private void OnReceivedIncomingFriendInvitation(object sender, AbstractReceivedIncomingFriendInvitationEventArgs arguments)
		{
		}

		private void OnReceivedOutgoingFriendInvitation(object sender, AbstractReceivedOutgoingFriendInvitationEventArgs arguments)
		{
			arguments.Invitation.OnAccepted += eventGenerator.AddEventHandler<AbstractFriendInvitationAcceptedEventArgs>(arguments.Invitation, AddFriendAcceptedNotification);
		}

		private void AddFriendAcceptedNotification(object sender, AbstractFriendInvitationAcceptedEventArgs arguments)
		{
			if (MixSession.IsValidSession && !this.IsNullOrDisposed())
			{
				notificationQueue.Enqueue(new FriendAcceptLocalNotification(arguments.Friend));
			}
		}

		public void AddAlertNotifications(object aSender, AbstractAlertsAddedEventArgs aArgs)
		{
			foreach (IAlert alert in aArgs.Alerts)
			{
				notificationQueue.Enqueue(new AlertLocalNotification(alert));
			}
		}

		public void AddChatMessageNotification(IChatThread fromThread, IChatThread toThread, IChatMessage aMessage, Action<IChatThread, IChatThread> aOnClickedAction)
		{
			if (!(toThread is IOfficialAccountChatThread) || !((IOfficialAccountChatThread)toThread).OfficialAccount.AccountId.Equals(FakeFriendManager.OAID))
			{
				notificationQueue.Enqueue(new MessageLocalNotification(fromThread, toThread, aMessage, aOnClickedAction));
			}
		}
	}
}
