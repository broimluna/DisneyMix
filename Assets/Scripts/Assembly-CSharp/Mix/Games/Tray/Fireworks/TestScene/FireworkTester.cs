using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks.TestScene
{
	public class FireworkTester : MonoBehaviour
	{
		public FireworkGesture gesture;

		public List<Firework> fireworks;

		public float fireInterval = 1f;

		public Vector3 spacing = new Vector3(10f, 0f, 0f);

		[Header("Trail Fireworks")]
		public Vector2 trailAmplitude;

		public Vector2 trailFrequency;

		private void Start()
		{
			DOVirtual.DelayedCall(0.1f, TriggerAllFireworks);
			DOVirtual.DelayedCall(0.5f, gesture.Play);
		}

		private void Update()
		{
			gesture.transform.localPosition = Vector2.Scale(new Vector2(Mathf.Sin(Time.time * trailFrequency.x), Mathf.Cos(Time.time * trailFrequency.y)), trailAmplitude);
		}

		private void TriggerAllFireworks()
		{
			for (int i = 0; i < fireworks.Count; i++)
			{
				fireworks[i].Launch((i + 1) * spacing);
			}
			DOVirtual.DelayedCall(fireInterval, TriggerAllFireworks);
		}
	}
}
