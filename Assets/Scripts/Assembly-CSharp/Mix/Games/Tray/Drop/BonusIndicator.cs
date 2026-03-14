using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class BonusIndicator : MonoBehaviour
	{
		public Vector3 InitialOffset;

		public float MaxScale;

		public float ScaleInTime;

		public Ease ScaleInEaseType;

		public Vector3 TravelDistance;

		public float TravelTime;

		public Ease TravelEaseType;

		public float LingerBeforeHiding;

		public float HideTime;

		public Ease HideEaseType;

		public ParticleSystem BurstParticles;

		private Sequence sequence;

		public Camera GameCamera { get; set; }

		private void OnDisable()
		{
			if (sequence != null)
			{
				sequence.Kill();
				sequence = null;
			}
		}

		public void Show(Vector3 position)
		{
			base.transform.position = position + InitialOffset;
			sequence = DOTween.Sequence();
			sequence.Insert(0f, base.transform.DOScale(Vector3.one * MaxScale, ScaleInTime).SetEase(ScaleInEaseType));
			sequence.Insert(0f, base.transform.DOMove(base.transform.position + TravelDistance, TravelTime).SetEase(TravelEaseType));
			sequence.Insert(TravelTime + LingerBeforeHiding, base.transform.DOScale(Vector3.zero, HideTime).SetEase(HideEaseType));
			sequence.AppendCallback(delegate
			{
				base.gameObject.SetActive(false);
			});
			base.gameObject.SetActive(true);
			BurstParticles.Play(true);
		}

		private void LateUpdate()
		{
			if (GameCamera == null)
			{
				GameCamera = DropGame.Instance.GameController.MixGameCamera;
			}
			if ((bool)GameCamera)
			{
				base.transform.LookAt(GameCamera.transform.position, Vector3.up);
				base.transform.Rotate(new Vector3(0f, 180f, 0f), Space.Self);
			}
		}
	}
}
