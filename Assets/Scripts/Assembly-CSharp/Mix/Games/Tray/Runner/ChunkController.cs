using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class ChunkController : MonoBehaviour
	{
		private MainRunnerGame main;

		private GameObject mainObj;

		public Vector3 RESPAWN_POSITION = new Vector3(0f, 10f, 0f);

		public Vector3 endLink;

		public float gizmoSize = 1f;

		public List<LevelPieceData> levelPieces = new List<LevelPieceData>();

		public bool hasGeneratedPieces;

		public int difficulty = 1;

		public GameObject nextChunk;

		public bool centerChunk;

		[Space(10f)]
		[Tooltip("This chunks index in the game's chunk array.  This is assigned at runtime.")]
		public int ChunkIndex;

		private Transform mCenterTransform;

		private void Start()
		{
			InitializeCenterChunk();
			GenerateLevelPieces();
			mainObj = GameObject.Find("main");
			if (mainObj == null)
			{
				Debug.LogWarning("Unable to find main game object");
				return;
			}
			main = mainObj.GetComponent<MainRunnerGame>();
			if (main == null)
			{
				Debug.LogWarning("Unable to find main game controller");
			}
		}

		public void InitializeCenterChunk()
		{
			if (centerChunk)
			{
				if (mCenterTransform == null)
				{
					GameObject gameObject = new GameObject("Center Anchor");
					mCenterTransform = gameObject.transform;
					mCenterTransform.SetParent(base.transform, false);
				}
			}
			else
			{
				mCenterTransform = base.transform;
			}
		}

		public void GenerateLevelPieces(bool levelEditor = false)
		{
			if (hasGeneratedPieces)
			{
				return;
			}
			if (levelEditor)
			{
				InitializeCenterChunk();
			}
			foreach (LevelPieceData levelPiece in levelPieces)
			{
				if (!levelEditor)
				{
					GameObject gameObject = Object.Instantiate(levelPiece.objReference);
					gameObject.transform.localPosition = levelPiece.position;
					gameObject.transform.localRotation = levelPiece.rotation;
					gameObject.transform.SetParent(mCenterTransform, false);
				}
			}
			if (centerChunk)
			{
				mCenterTransform.localPosition = -endLink * 0.5f;
			}
			hasGeneratedPieces = true;
		}

		private void Update()
		{
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(endLink + base.transform.position, gizmoSize);
		}
	}
}
