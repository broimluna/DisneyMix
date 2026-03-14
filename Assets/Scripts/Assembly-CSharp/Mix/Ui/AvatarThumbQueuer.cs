using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mix.Ui
{
	public class AvatarThumbQueuer
	{
		private const int MaxInstantiationsAFrame = 4;

		private MonoBehaviour coroutineRunner;

		private List<Action> actionsToRun = new List<Action>();

		private Coroutine actionCoroutine;

		public AvatarThumbQueuer(MonoBehaviour parent)
		{
			coroutineRunner = parent;
		}

		public void Cancel(Action routineToRun)
		{
			actionsToRun.Remove(routineToRun);
		}

		public void InvokeWhenAvailable(Action routineToRun)
		{
			actionsToRun.Add(routineToRun);
			if (actionCoroutine == null)
			{
				actionCoroutine = coroutineRunner.StartCoroutine(processActions());
			}
		}

		public IEnumerator processActions()
		{
			while (actionsToRun.Count != 0)
			{
				for (int count = 0; count < 4; count++)
				{
					if (actionsToRun.Count <= 0)
					{
						break;
					}
					Action nextAction = actionsToRun[0];
					actionsToRun.RemoveAt(0);
					nextAction();
				}
				yield return new WaitForEndOfFrame();
			}
			actionCoroutine = null;
		}
	}
}
