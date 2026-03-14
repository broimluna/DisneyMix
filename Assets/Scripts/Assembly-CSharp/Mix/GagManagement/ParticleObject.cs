using System;
using UnityEngine;

namespace Mix.GagManagement
{
	[Serializable]
	public class ParticleObject
	{
		public GameObject Particle;

		public GameObject Emitter;

		public GameObject EmitterRig;

		public int Start;

		public bool Follow;

		[HideInInspector]
		public bool isPlaying;

		[HideInInspector]
		public Vector3 startPos;

		private GameObject pInst;

		public void Flip(bool flipIt)
		{
			if (flipIt)
			{
				pInst.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
			}
		}

		public void Play(Vector3 aStartPos)
		{
			startPos = aStartPos;
			pInst = UnityEngine.Object.Instantiate(Particle, Emitter.transform.position, Quaternion.identity) as GameObject;
			pInst.transform.parent = Emitter.transform;
			Util.SetLayerRecursively(pInst, Emitter.layer);
			Animator component = EmitterRig.GetComponent<Animator>();
			component.Play("Take 001");
			isPlaying = true;
		}

		public void Destroy()
		{
			Particle = null;
			Emitter = null;
			if (pInst != null)
			{
				UnityEngine.Object.Destroy(pInst);
			}
			pInst = null;
		}
	}
}
