using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class DnaBurst : MonoBehaviour
	{
		public float mAcceleration;

		public float mMinMaxX;

		public float mSpeed;

		public bool mIsGoingLeft;

		private bool on;

		private void Start()
		{
			if (mIsGoingLeft)
			{
				mSpeed = 2f;
			}
			else
			{
				mSpeed = -2f;
			}
			on = false;
		}

		public void StartOff()
		{
			if (mIsGoingLeft)
			{
				mSpeed = 2f;
			}
			else
			{
				mSpeed = -2f;
			}
			on = true;
		}

		public void Return()
		{
			on = false;
		}

		private void FixedUpdate()
		{
			if (!on)
			{
				return;
			}
			if (mIsGoingLeft)
			{
				if (base.transform.localPosition.x > 0f)
				{
					mSpeed -= mAcceleration;
				}
				else
				{
					mIsGoingLeft = !mIsGoingLeft;
				}
			}
			else if (0f > base.transform.localPosition.x)
			{
				mSpeed += mAcceleration;
			}
			else
			{
				mIsGoingLeft = !mIsGoingLeft;
			}
			if (mSpeed > mMinMaxX)
			{
				mSpeed = mMinMaxX;
			}
			else if (mSpeed < 0f - mMinMaxX)
			{
				mSpeed = 0f - mMinMaxX;
			}
			base.transform.localPosition += new Vector3(mSpeed, 0f, 0f);
		}
	}
}
