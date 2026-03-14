using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class Tinkerbell : MonoBehaviour
	{
		public float mGravity;

		private Rigidbody mRB;

		private void Start()
		{
			mRB = GetComponent<Rigidbody>();
		}

		public void Launch(Vector3 launchVector)
		{
			mRB = GetComponent<Rigidbody>();
			base.transform.localPosition = Vector3.zero;
			mRB.linearVelocity = Vector3.zero;
			mRB.AddForce(launchVector);
		}

		private void FixedUpdate()
		{
			if (base.transform.position.y > -10f)
			{
				mRB.AddForce(new Vector3(0f, 0f - mGravity, 0f));
				return;
			}
			mRB.linearVelocity = Vector3.zero;
			base.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
			base.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>()
				.Stop();
		}
	}
}
