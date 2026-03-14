using System;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class IntroScene : MonoBehaviour
	{
		public Action OnIntroComplete;

		public void IntroFinished()
		{
			if (OnIntroComplete != null)
			{
				OnIntroComplete();
			}
		}
	}
}
