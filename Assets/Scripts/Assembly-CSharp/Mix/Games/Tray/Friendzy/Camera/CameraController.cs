using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Friendzy.Camera
{
	public class CameraController : MonoBehaviour
	{
		[HideInInspector]
		public Animator CamAnimator;

		private void Awake()
		{
			CamAnimator = GetComponent<Animator>();
		}

		public Sequence IntroZoom(float aDuration = 0f)
		{
			FriendzyGame.PlaySound("LightApplause", FriendzyGame.SOUND_PREFIX);
			Sequence sequence = DOTween.Sequence();
			sequence.AppendCallback(delegate
			{
				CamAnimator.SetTrigger("ZoomIn");
			});
			sequence.AppendInterval(aDuration);
			return sequence;
		}

		public void OutroZoomOut()
		{
			CamAnimator.SetTrigger("ZoomOut");
		}

		public void ResultSlowZoom()
		{
			CamAnimator.SetTrigger("SlowZoom");
		}

		public void ResultZoomOut()
		{
			CamAnimator.SetTrigger("ZoomOut");
		}

		public void ComparisonZoomIn()
		{
			CamAnimator.SetTrigger("RelationshipZoomIn");
		}

		public void ComparisonZoomOut()
		{
			CamAnimator.SetTrigger("RelationshipZoomOut");
		}

		public bool CheckState(string aString)
		{
			return CamAnimator.GetCurrentAnimatorStateInfo(0).IsName(aString);
		}
	}
}
