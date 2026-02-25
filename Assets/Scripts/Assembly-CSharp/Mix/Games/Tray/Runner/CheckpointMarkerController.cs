using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class CheckpointMarkerController : MonoBehaviour
	{
		private const int AVATAR_SNAPSHOT_SIZE = 100;

		[Header("Internal References")]
		public ParticleSystem triggeredParticles;

		public Transform gemPlaceholder;

		[Header("Head Settings")]
		public Vector3 BaseHeadPosition;

		public Vector3 HeadOffset;

		public float headAppearAnimationDelay = 1f;

		public float headAppearAnimationTime = 0.2f;

		public Ease headAppearAnimationEasing = Ease.OutBack;

		[Range(0f, 1f)]
		public float headFinalScale = 0.8f;

		[Space(10f)]
		public float headWobbleAmount = 1f;

		public float headWobbleFrequencyX = 10f;

		public float headWobbleFrequencyY = 10f;

		public float headWobbleOffsetPerHead = 5f;

		[Header("External References")]
		public GameObject gemPrefab;

		public CheckpointMarkerHead headPrefab;

		private List<CheckpointMarkerHead> mHeads = new List<CheckpointMarkerHead>();

		private CheckpointGem mGem;

		private void Awake()
		{
			mHeads = new List<CheckpointMarkerHead>();
			GameObject gameObject = Object.Instantiate(gemPrefab);
			gameObject.transform.SetParent(base.transform);
			gameObject.transform.position = gemPlaceholder.transform.position;
			mGem = gameObject.GetComponent<CheckpointGem>();
			mGem.name = "Gem";
		}

		private void Update()
		{
			for (int i = 0; i < mHeads.Count; i++)
			{
				if (mHeads[i].gameObject.activeSelf)
				{
					mHeads[i].transform.localPosition = GetHeadLocation(i) + new Vector3(Mathf.Sin(headWobbleOffsetPerHead * (float)i + Time.time * headWobbleFrequencyX), Mathf.Sin(headWobbleOffsetPerHead * (float)i + Time.time * headWobbleFrequencyY), 0f) * headWobbleAmount;
				}
			}
		}

		private Vector3 GetHeadLocation(int index)
		{
			return BaseHeadPosition + HeadOffset * index;
		}

		public void FlyGemToUI()
		{
			mGem.FlyToUI();
		}

		public bool HasHead(string playerID)
		{
			if (mHeads != null)
			{
				return mHeads.Any((CheckpointMarkerHead x) => !x.IsNullOrDisposed() && x.PlayerID == playerID);
			}
			return false;
		}

		public void AddHead(string playerID)
		{
			if (!(headPrefab != null) || HasHead(playerID))
			{
				return;
			}
			CheckpointMarkerHead head = Object.Instantiate(headPrefab);
			head.transform.SetParent(base.transform, false);
			head.transform.localScale = Vector3.zero;
			head.transform.DOScale(headFinalScale, headAppearAnimationTime).SetEase(headAppearAnimationEasing).SetDelay(headAppearAnimationDelay);
			head.PlayerID = playerID;
			mHeads.Add(head);
			BaseGameController.Instance.LoadSnapshot(playerID, 100, delegate(bool success, Sprite snapshot)
			{
				if (!head.IsNullOrDisposed() && !head.AvatarSnapshot.IsNullOrDisposed())
				{
					head.AvatarSnapshot.sprite = snapshot;
				}
			});
		}

		public void HideAllHeads()
		{
			for (int i = 0; i < mHeads.Count; i++)
			{
				mHeads[i].gameObject.SetActive(false);
			}
		}

		public IEnumerator RemoveParticleSystemWhenFinished(ParticleSystem particles)
		{
			while (particles.IsAlive())
			{
				yield return null;
			}
			Object.Destroy(particles.gameObject);
		}

		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.color = Color.white;
			for (int i = 0; i < 5; i++)
			{
				Gizmos.DrawWireCube(BaseHeadPosition + HeadOffset * i, Vector3.one * 0.5f);
			}
		}
	}
}
