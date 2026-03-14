using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class FireworkTinkerbell : Firework
	{
		public Tinkerbell Tinkerbell;

		private bool isLeft;

		private float mXDistance;

		public float YOffset;

		public Vector3 ExplodeVector;

		public float Speed;

		private void Start()
		{
		}

		public override bool Launch(Vector3 launchVector)
		{
			Camera mixGameCamera = Toolbox.Instance.mFireworkManager.mController.MixGameCamera;
			float z = base.transform.parent.GetComponent<BoxCollider>().center.z;
			mXDistance = 0.5f + (mixGameCamera.ScreenToWorldPoint(new Vector3(mixGameCamera.pixelWidth, mixGameCamera.pixelHeight, z)) - mixGameCamera.ScreenToWorldPoint(new Vector3(0f, 0f, z))).x * 1.7f / 2f;
			ExplodeVector.Normalize();
			bool result = !mIsPlaying;
			if (!mIsPlaying)
			{
				base.gameObject.SetActive(true);
				base.transform.localPosition = launchVector;
				isLeft = Random.Range(0, 100) % 2 == 0;
				if (isLeft)
				{
					base.transform.localPosition = new Vector3(0f - mXDistance, base.transform.localPosition.y + YOffset, z);
				}
				else
				{
					base.transform.localPosition = new Vector3(mXDistance, base.transform.localPosition.y + YOffset, z);
				}
				foreach (ParticleSystem trail in Trails)
				{
					trail.Play();
				}
				GetComponent<Animator>().Play(LaunchName, 0);
				mIsPlaying = true;
				if ((bool)Toolbox.Instance.mFireworkGame && TrailingSoundEvent.Length > 0 && LaunchTime > 0f && Toolbox.Instance.mFireworkGame.mGameStage != FireworksGame.FireworksStage.Creation)
				{
					BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent(TrailingSoundEvent, this);
				}
			}
			return result;
		}

		public override void Explode()
		{
			base.Explode();
			Tinkerbell.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
			if (isLeft)
			{
				Tinkerbell.Launch(ExplodeVector * Speed);
				return;
			}
			Vector3 vector = new Vector3(0f - ExplodeVector.x, ExplodeVector.y, ExplodeVector.z);
			Tinkerbell.Launch(vector * Speed);
		}

		public override void Return()
		{
			base.Return();
			Tinkerbell.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
		}
	}
}
