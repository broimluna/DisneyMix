using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.SecretWord
{
	public class TileGrid : MonoBehaviour
	{
		public SecretWordGame game;

		public string masterLetterList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public GameObject tilePrefab;

		public float minScaleUpTime = 0.2f;

		public float maxScaleUpTime = 0.5f;

		public float maxScaleUpDelay = 0.5f;

		public float maxRandomRoation = 10f;

		public Vector3 scaleUpBounce = new Vector3(0f, 1f, 0f);

		public float scaleUpBounceTime = 1f;

		public List<int> rowLengths = new List<int>();

		public Vector2 spacing;

		public Transform tileParent
		{
			get
			{
				return base.transform;
			}
		}

		private void Start()
		{
			int num = 0;
			int num2 = 0;
			Vector3 vector = new Vector3(0.5f * spacing.x * (float)(1 - rowLengths[num]), 0f, 0f);
			DOVirtual.DelayedCall(0.01f, delegate
			{
				BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/AllTilesAppear");
			});
			for (int num3 = 0; num3 < masterLetterList.Length; num3++)
			{
				Vector3 vector2 = new Vector3((float)num2 * spacing.x, 0f, (float)(-num) * spacing.y);
				CreateTile(vector2 + vector, masterLetterList[num3], maxScaleUpDelay);
				num2++;
				if (num2 >= rowLengths[num])
				{
					num = Mathf.Min(num + 1, rowLengths.Count - 1);
					num2 = 0;
					vector = new Vector3(0.5f * spacing.x * (float)(1 - rowLengths[num]), 0f, 0f);
				}
			}
		}

		public Tile CreateTile(Vector3 position, char letter, float delay)
		{
			Tile tile = game.CreateNewTileWithPositionAndRotation(base.transform, letter, position, Quaternion.Euler(0f, Random.Range(0f - maxRandomRoation, maxRandomRoation), 0f), false);
			tile.transform.localScale = Vector3.zero;
			Tweener t = tile.transform.DOScale(Vector3.one, Random.Range(minScaleUpTime, maxScaleUpTime)).SetEase(Ease.OutBack);
			if (delay > 0f)
			{
				t.SetDelay(Random.Range(0f, delay));
			}
			return tile;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = new Color(0f, 0f, 0f, 0.5f);
			for (int i = 0; i < rowLengths.Count; i++)
			{
				Vector3 vector = new Vector3((0f - (float)rowLengths[i] / 2f) * spacing.x + spacing.x * 0.5f, 0f, 0f);
				for (int j = 0; j < rowLengths[i]; j++)
				{
					Vector3 vector2 = new Vector3((float)j * spacing.x, 0f, (float)(-i) * spacing.y);
					Gizmos.DrawWireCube(base.transform.position + vector2 + vector, new Vector3(1f, 0.05f, 1f));
				}
			}
		}
	}
}
