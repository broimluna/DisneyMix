using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class CheckpointGem : MonoBehaviour
	{
		public Transform gemTransform;

		public AnimationCurve gemWobble;

		public AnimationCurve gemRotation;

		public float flyToUITime = 2f;

		public float flyToUIDeviationAmount = 5f;

		public TrailRenderer trail;

		private Vector3 mOrigin;

		private MainRunnerGame game;

		private void Start()
		{
			game = GetComponentInParent<MainRunnerGame>();
			mOrigin = gemTransform.localPosition;
			if ((bool)trail)
			{
				trail.enabled = false;
			}
		}

		private void Update()
		{
			gemTransform.localPosition = mOrigin + Vector3.up * gemWobble.Evaluate(Time.time);
			gemTransform.localEulerAngles = new Vector3(0f, gemRotation.Evaluate(Time.time), 0f);
		}

		public void FlyToUI()
		{
			float distance = Vector3.Distance(base.transform.position, game.GameController.MixGameCamera.transform.position);
			Vector2 vector = game.GameController.MixGameCamera.WorldToScreenPoint(base.transform.position);
			base.transform.SetParent(game.flyingGemHolder);
			base.transform.position = game.flyingGemCamera.ScreenPointToRay(vector).GetPoint(distance);
			Util.SetLayerRecursively(base.gameObject, LayerMask.NameToLayer("Game3D A"));
			PlayUIPanel playUIPanel = game.uiController.CurrentUIPanel as PlayUIPanel;
			Vector2 vector2 = game.GameController.MixGameCamera.WorldToScreenPoint(playUIPanel.gemIconLocation);
			Vector3 point = game.flyingGemCamera.ScreenPointToRay(vector2).GetPoint(distance);
			Vector3[] array = new Vector3[2];
			Vector3 normalized = Vector3.Cross(point - base.transform.position, Vector3.forward).normalized;
			array[0] = Vector3.Lerp(base.transform.position, point, 0.5f) + normalized * flyToUIDeviationAmount;
			array[1] = point;
			base.transform.DOPath(array, flyToUITime, PathType.CatmullRom, PathMode.Sidescroller2D, 5).SetEase(Ease.Linear).OnComplete(delegate
			{
				Object.Destroy(base.gameObject, trail.time + 0.2f);
			});
			base.transform.DOScale(Vector3.zero, flyToUITime * 0.2f).SetDelay(flyToUITime * 0.8f).SetEase(Ease.InQuad);
			StartCoroutine(ResetTrailRenderer());
		}

		private IEnumerator ResetTrailRenderer()
		{
			float originalTime = trail.time;
			trail.time = -1f;
			yield return null;
			trail.time = originalTime;
			trail.enabled = true;
		}
	}
}
