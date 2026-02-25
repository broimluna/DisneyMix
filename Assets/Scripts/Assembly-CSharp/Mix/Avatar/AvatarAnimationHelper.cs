using System;
using System.Collections;
using UnityEngine;

namespace Mix.Avatar
{
	public class AvatarAnimationHelper : MonoBehaviour
	{
		private Hashtable triggersQueued;

		private Animator animator;

		private float randomSeed;

		private float BaseCooldown;

		private float CooldownRange;

		private float percentIgnored;

		private void Start()
		{
			triggersQueued = new Hashtable();
		}

		private void Update()
		{
		}

		public void SetRandomDelay(float baseCooldown, float cooldownRange, float percentIgnored)
		{
			BaseCooldown = baseCooldown;
			CooldownRange = cooldownRange;
			this.percentIgnored = percentIgnored;
		}

		public IEnumerator DelayedInvocation(Action action)
		{
			if (Mathf.Abs(BaseCooldown) <= float.Epsilon && Mathf.Abs(CooldownRange) <= float.Epsilon)
			{
				yield return new WaitForEndOfFrame();
				action();
			}
			else
			{
				float totalWaitTime = BaseCooldown + UnityEngine.Random.Range(0f, CooldownRange);
				yield return new WaitForSeconds(totalWaitTime);
				action();
			}
		}

		public void SetAnimator(Animator anim)
		{
			animator = anim;
		}

		public void SetTrigger(string name, bool alwaysRun = false)
		{
			if (!(animator != null))
			{
				return;
			}
			float num = UnityEngine.Random.Range(0f, 100f);
			if (!(num > percentIgnored) && !alwaysRun)
			{
				return;
			}
			randomSeed = UnityEngine.Random.Range(0f, 1f);
			if (triggersQueued == null || triggersQueued.ContainsKey(name))
			{
				return;
			}
			triggersQueued.Add(name, StartCoroutine(DelayedInvocation(delegate
			{
				if (triggersQueued != null && triggersQueued.ContainsKey(name))
				{
					triggersQueued.Remove(name);
				}
			})));
			animator.SetFloat("RandomSeed", randomSeed);
			animator.SetTrigger(name);
		}

		public void SetBool(string name, bool value)
		{
			if (animator != null)
			{
				randomSeed = UnityEngine.Random.Range(0f, 1f);
				animator.SetFloat("RandomSeed", randomSeed);
				animator.SetBool(name, value);
			}
		}
	}
}
