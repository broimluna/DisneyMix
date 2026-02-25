using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	[RequireComponent(typeof(ParticleSystem))]
	public class ParticleManager : MonoBehaviour
	{
		private ParticleSystem mParticle;

		private float mCurrentPlayback;

		private bool mIsPlaying;

		private void Awake()
		{
			mParticle = GetComponent<ParticleSystem>();
		}

		private void OnEnable()
		{
			if (mIsPlaying && (mCurrentPlayback < mParticle.duration || mParticle.loop))
			{
				mParticle.Simulate(mCurrentPlayback, false);
				mParticle.Play(false);
			}
		}

		private void OnDisable()
		{
			mIsPlaying = mParticle.emission.enabled;
			mParticle.playOnAwake = false;
			mCurrentPlayback = mParticle.time;
		}
	}
}
