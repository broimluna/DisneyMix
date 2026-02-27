using System;
using System.Collections.Generic;
using Disney.PushNotificationUnityPlugin.Internal;
using UnityEngine;

namespace Disney.PushNotificationUnityPlugin
{
	public class GoogleCloudMessagingClient : IPushNotificationClient
	{
		private enum State
		{
			Idle = 0,
			WaitingForToken = 1,
			WaitingForNotifications = 2
		}

		private State state;

		private readonly string senderId;

		private static readonly AndroidJavaClass UnityApiJavaClass = new AndroidJavaClass("");

		public event EventHandler<AbstractNotificationReceivedEventArgs> OnNotificationReceived = delegate
		{
		};

		public event EventHandler<AbstractTokenGeneratedEventArgs> OnTokenGenerated = delegate
		{
		};

		public GoogleCloudMessagingClient(string senderId)
		{
			this.senderId = senderId;
			state = State.WaitingForNotifications;
		}

		public void Register()
		{
			//UnityApiJavaClass.CallStatic("register", senderId.ToString());
		}

		public void CheckForToken()
		{
			state = State.WaitingForToken;
		}

		public void Unregister()
		{
			state = State.Idle;
			//UnityApiJavaClass.CallStatic("unregister");
		}

		public void Update()
		{
			switch (state)
			{
			default:
				return;
			case State.WaitingForToken:
			{
				return;
			}
			case State.WaitingForNotifications:
				break;
			}
			while (true)
			{
				
			}
		}

		public void OnPause()
		{
			//UnityApiJavaClass.CallStatic("onPause");
		}

		public void OnResume()
		{
			//UnityApiJavaClass.CallStatic("onResume");
		}

		public bool IsNotificationEnabled()
		{
			bool flag = true;
			return flag;
		}
	}
}
