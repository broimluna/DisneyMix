using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class Snowflake : MonoBehaviour
	{
		public float MinRot;

		public float MaxRot;

		public float Gravity;

		public float WaitTime = 1.24f;

		public float ScaleUpLerpSpeed = 0.1f;

		public Vector2 RandomScaleMinMax;

		public Vector2 RandomAlphaLerpSpeedMinMax = new Vector2(0.012f, 0.02f);

		private float mScaleDownAlphaLerpSpeed;

		private Rigidbody mRB;

		private float mTime;

		private float mTime2;

		private Vector3 mScale;

		private bool mIsSpinning;

		private void Start()
		{
			mRB = GetComponent<Rigidbody>();
		}

		public void Launch(Vector3 launchVector)
		{
			mTime = 0f;
			mTime2 = 0f;
			float num = Random.Range(RandomScaleMinMax.x, RandomScaleMinMax.y);
			mScaleDownAlphaLerpSpeed = Random.Range(RandomAlphaLerpSpeedMinMax.x, RandomAlphaLerpSpeedMinMax.y);
			mScale = new Vector3(num, num, num);
			base.transform.rotation = Quaternion.Euler(Random.Range(-50f, 50f), Random.Range(-50f, 50f), Random.Range(-50f, 50f));
			mRB = GetComponent<Rigidbody>();
			GetComponent<SpriteRenderer>().color = Color.white;
			mRB.linearVelocity = Vector3.zero;
			mRB.angularVelocity = Vector3.zero;
			base.transform.localScale = Vector3.zero;
			mRB.AddForce(launchVector * 4f);
		}

		private void FixedUpdate()
		{
			if (GetComponent<SpriteRenderer>().color.a > 0.1f)
			{
				if (mTime < WaitTime)
				{
					base.transform.localScale = Vector3.Lerp(base.transform.localScale, mScale, ScaleUpLerpSpeed);
					mRB.linearVelocity *= 0.91f;
					mRB.AddForce(0f, (0f - Gravity) * 2f, 0f);
				}
				mRB.AddForce(new Vector3(0f, 0f - Gravity, 0f));
				if (mRB.linearVelocity.y < 0f)
				{
					mRB.linearVelocity = new Vector3(mRB.linearVelocity.x * 0.98f, mRB.linearVelocity.y, mRB.linearVelocity.z);
					if (mRB.linearVelocity.y < -2f)
					{
						GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, new Color(0f, 0f, 0f, 0f), mTime2 * mScaleDownAlphaLerpSpeed);
						mTime2 += Time.deltaTime;
					}
					int num = (int)mTime % 2;
					if (num == 1)
					{
						if (!mIsSpinning)
						{
							mRB.angularVelocity = Vector3.zero;
							mRB.AddTorque(new Vector3(Random.Range(MinRot, MaxRot), Random.Range(MinRot, MaxRot), Random.Range(MinRot, MaxRot)));
							mIsSpinning = true;
						}
					}
					else
					{
						mIsSpinning = false;
					}
				}
				mTime += Time.deltaTime;
			}
			else
			{
				mRB.linearVelocity = Vector3.zero;
				base.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
				base.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>()
					.Stop();
			}
		}
	}
}
