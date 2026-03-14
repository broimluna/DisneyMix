using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.ResultShow
{
	public class ResultCardBehavior : MonoBehaviour
	{
		private const float SCALE_FACTOR = 0.01f;

		private const float LERP_RATE = 0.5f;

		private const float SCALE_STRENGTH = 0.1f;

		private const float ROTATION_STRENGTH = 10f;

		[SerializeField]
		private Image mResultPic;

		private Vector3 mTargetScale;

		public Sprite ResultSprite
		{
			get
			{
				return mResultPic.sprite;
			}
			set
			{
				mResultPic.sprite = value;
			}
		}

		public Tween SetLerpPoint(Vector3 aLerpPoint, bool aSetImmediate = false)
		{
			if (aSetImmediate)
			{
				base.transform.localPosition = aLerpPoint;
			}
			return base.transform.DOLocalMove(aLerpPoint, 0.5f);
		}

		public Tween ResetTargetScale(Vector2 aTargetWorldSize, bool aSetImmediate = false)
		{
			aTargetWorldSize *= 0.01f;
			mTargetScale = new Vector3(aTargetWorldSize.x, aTargetWorldSize.y, 1f);
			if (aSetImmediate)
			{
				base.transform.localScale = mTargetScale;
			}
			return base.transform.DOScale(mTargetScale, 0.5f);
		}

		public Sequence ShowNormalSprite(float aDuration)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(base.transform.DOPunchScale(base.transform.localScale * 0.1f, aDuration, 1));
			sequence.Join(base.transform.DOPunchRotation(mResultPic.transform.forward * 10f, aDuration));
			sequence.Join(mResultPic.DOColor(Color.white, aDuration));
			return sequence;
		}

		public Sequence SelectChoice(float aDuration, Vector3 aLerpPoint)
		{
			FriendzyGame.PlaySound("ResultShake", FriendzyGame.SOUND_PREFIX);
			Sequence sequence = DOTween.Sequence();
			sequence.Append(base.transform.DOLocalMove(new Vector3(aLerpPoint.x, aLerpPoint.y, aLerpPoint.z), aDuration));
			sequence.Append(base.transform.DOPunchScale(base.transform.localScale * 0.1f, aDuration, 1));
			sequence.Join(base.transform.DOPunchRotation(mResultPic.transform.forward * 10f, aDuration));
			return sequence;
		}
	}
}
