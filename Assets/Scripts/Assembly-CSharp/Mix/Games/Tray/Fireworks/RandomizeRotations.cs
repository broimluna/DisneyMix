using System;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	[RequireComponent(typeof(Firework))]
	public class RandomizeRotations : MonoBehaviour
	{
		public Transform[] AffectedObjects;

		[Space(10f)]
		public bool RotateX;

		public float RotateXMin;

		public float RotateXMax;

		[Space(10f)]
		public bool RotateY;

		public float RotateYMin;

		public float RotateYMax;

		[Space(10f)]
		public bool RotateZ = true;

		public float RotateZMin;

		public float RotateZMax;

		private Firework mFirework;

		private void OnEnable()
		{
			if (mFirework == null)
			{
				mFirework = GetComponent<Firework>();
			}
			Firework firework = mFirework;
			firework.OnFireworkExplode = (Action)Delegate.Combine(firework.OnFireworkExplode, new Action(SetRotation));
		}

		private void OnDisable()
		{
			Firework firework = mFirework;
			firework.OnFireworkExplode = (Action)Delegate.Remove(firework.OnFireworkExplode, new Action(SetRotation));
		}

		private void SetRotation()
		{
			Transform[] affectedObjects = AffectedObjects;
			foreach (Transform transform in affectedObjects)
			{
				Vector3 eulerAngles = transform.eulerAngles;
				if (RotateX)
				{
					eulerAngles.x = UnityEngine.Random.Range(RotateXMin, RotateXMax);
				}
				if (RotateY)
				{
					eulerAngles.y = UnityEngine.Random.Range(RotateYMin, RotateYMax);
				}
				if (RotateZ)
				{
					eulerAngles.z = UnityEngine.Random.Range(RotateZMin, RotateZMax);
				}
				transform.eulerAngles = eulerAngles;
			}
		}
	}
}
