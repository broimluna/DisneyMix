using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class CameraController : MonoBehaviour
	{
		public Transform player;

		public float heightThreshold = 6f;

		public float distanceThreshold = 0.2f;

		public float moveSpeed = 1f;

		public Animator animController;

		public float upperLevelHeight = 20f;

		private Vector3 target;

		public Vector3 offset;

		private bool isFollowingPlayer = true;

		private Vector3 startPos;

		private void Start()
		{
			startPos = base.transform.localPosition;
		}

		private void Update()
		{
			if (player != null && isFollowingPlayer)
			{
				SetLocalPos(GetCameraPosFromTargetPos(player.transform.localPosition));
			}
		}

		private void SetLocalPos(Vector3 pos)
		{
			base.transform.localPosition = pos;
		}

		private Vector3 GetCameraPosFromTargetPos(Vector3 target)
		{
			return new Vector3(target.x + offset.x, startPos.y + offset.y, startPos.z + offset.z);
		}

		public void Shake()
		{
			animController.Play("runnerCamera_shake");
		}

		public void SetPlayer(Transform t)
		{
			player = t;
		}

		public void SetFollowPlayer(bool flag)
		{
			isFollowingPlayer = flag;
		}

		public void Interp(Vector3 pos, float transitionDuration, float delay)
		{
			SetFollowPlayer(false);
			Sequence s = DOTween.Sequence();
			s.AppendInterval(delay);
			s.Append(base.transform.DOLocalMove(GetCameraPosFromTargetPos(pos), transitionDuration));
		}
	}
}
