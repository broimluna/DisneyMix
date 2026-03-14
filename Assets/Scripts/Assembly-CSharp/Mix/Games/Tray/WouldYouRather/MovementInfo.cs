using System;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.WouldYouRather
{
	[Serializable]
	public class MovementInfo
	{
		public Transform target;

		public float moveDelay;

		public float moveTime;

		public float rotateDelay;

		public float rotateTime;

		public Ease moveEaseType = Ease.InOutCubic;
	}
}
