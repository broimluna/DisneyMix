using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.SecretWord
{
	public class WordArea : MonoBehaviour
	{
		private enum Direction
		{
			LEFT = 0,
			RIGHT = 1
		}

		public delegate void TilesUpdatedEvent(int numTiles);

		[Header("Metrics")]
		public float letterSpacing;

		public float slotSnapDistance = 2.5f;

		public float displacementTimeout = 0.25f;

		[Header("External References")]
		public GameObject slotPrefab;

		public SecretWordGame m_game;

		[HideInInspector]
		public List<TileSlot> m_slots = new List<TileSlot>();

		private GameObject m_offsetParent;

		public Transform tileParent
		{
			get
			{
				return m_offsetParent.transform;
			}
		}

		public event TilesUpdatedEvent TilesUpdated;

		private void Initialize(int tileCount)
		{
			m_offsetParent = new GameObject("Offset");
			m_offsetParent.transform.SetParent(base.transform, false);
			for (int i = 0; i < tileCount; i++)
			{
				GameObject gameObject = Object.Instantiate(slotPrefab);
				gameObject.transform.SetParent(m_offsetParent.transform, false);
				gameObject.transform.localPosition = new Vector3((float)i * letterSpacing, 0f);
				gameObject.name = "Slot " + i;
				TileSlot componentInChildren = gameObject.GetComponentInChildren<TileSlot>();
				componentInChildren.slotNumber = i;
				m_slots.Add(componentInChildren);
			}
			UpdatePosition(false);
		}

		public void SetupForWordCreate(int maxWordLength)
		{
			Initialize(maxWordLength);
		}

		public void SetupForWordGuess(int wordLength)
		{
			Initialize(wordLength);
		}

		public TileSlot GetSlotContainingTile(Tile tile)
		{
			for (int i = 0; i < m_slots.Count; i++)
			{
				if (m_slots[i].tile == tile)
				{
					return m_slots[i];
				}
			}
			return null;
		}

		public bool InsertTileAtIndex(int slot, Tile tile)
		{
			bool result = false;
			if (slot >= 0 && slot < m_slots.Count)
			{
				if (m_slots[slot].tile != null)
				{
					int num = CountUnoccupiedSlotsInDirection(slot, Direction.LEFT);
					int num2 = CountUnoccupiedSlotsInDirection(slot, Direction.RIGHT);
					if (num > num2)
					{
						ShiftTileInSlot(slot, Direction.LEFT);
					}
					else if (num2 > 0)
					{
						ShiftTileInSlot(slot, Direction.RIGHT);
					}
				}
				if (m_slots[slot].tile == null)
				{
					m_slots[slot].SetTile(tile);
					tile.transform.parent = tileParent;
					result = true;
					SendTileCount();
				}
			}
			return result;
		}

		private int CountUnoccupiedSlotsInDirection(int index, Direction direction)
		{
			int num = 0;
			int num2;
			if (direction == Direction.LEFT)
			{
				num2 = -1;
				index--;
			}
			else
			{
				num2 = 1;
				index++;
			}
			while (IsInBounds(index, 0, m_slots.Count))
			{
				if (m_slots[index].tile == null)
				{
					num++;
				}
				index += num2;
			}
			return num;
		}

		private int CountOccupiedSlotsInDirection(int index, Direction direction)
		{
			int num = 0;
			int num2;
			if (direction == Direction.LEFT)
			{
				num2 = -1;
				index--;
			}
			else
			{
				num2 = 1;
				index++;
			}
			while (IsInBounds(index, 0, m_slots.Count))
			{
				if (m_slots[index].tile != null)
				{
					num++;
				}
				index += num2;
			}
			return num;
		}

		private bool IsInBounds(int index, int min, int max)
		{
			return index >= min && index < max;
		}

		private void SetLayerRecursively(GameObject obj, int newLayer)
		{
			if (null == obj)
			{
				return;
			}
			obj.layer = newLayer;
			foreach (Transform item in obj.transform)
			{
				if (!(null == item))
				{
					SetLayerRecursively(item.gameObject, newLayer);
				}
			}
		}

		public int GetFirstFreeSlot()
		{
			int num = -1;
			bool flag = false;
			for (int i = 0; i < m_slots.Count; i++)
			{
				if (m_slots[i].tile == null)
				{
					if (flag)
					{
						return i;
					}
					if (num < 0)
					{
						num = i;
					}
				}
				else
				{
					flag = true;
				}
			}
			return num;
		}

		public int GetFirstTile()
		{
			for (int i = 0; i < m_slots.Count; i++)
			{
				if (m_slots[i].tile != null)
				{
					return i;
				}
			}
			return -1;
		}

		private bool ShiftTileInSlot(int slot, Direction direction)
		{
			if (direction == Direction.RIGHT)
			{
				if (slot < m_slots.Count - 1 && m_slots[slot].tile != null && ShiftTileInSlot(slot + 1, direction))
				{
					m_slots[slot + 1].tile = m_slots[slot].tile;
					m_slots[slot].tile = null;
				}
			}
			else if (slot > 0 && m_slots[slot].tile != null && ShiftTileInSlot(slot - 1, direction))
			{
				m_slots[slot - 1].tile = m_slots[slot].tile;
				m_slots[slot].tile = null;
			}
			return m_slots[slot].tile == null;
		}

		private void UnshifTileInSlot(int slot, Direction direction)
		{
			if (direction == Direction.RIGHT)
			{
				if (slot < m_slots.Count - 1 && m_slots[slot].tile == null)
				{
					m_slots[slot].tile = m_slots[slot + 1].tile;
					m_slots[slot + 1].tile = null;
					UnshifTileInSlot(slot + 1, direction);
				}
			}
			else if (slot > 0 && m_slots[slot].tile == null)
			{
				m_slots[slot].tile = m_slots[slot - 1].tile;
				m_slots[slot - 1].tile = null;
				UnshifTileInSlot(slot - 1, direction);
			}
		}

		public void RemoveTileFromSlot(int slot)
		{
			bool flag = GetFirstTile() == slot;
			m_slots[slot].tile = null;
			if (flag && slot < m_slots.Count - 1)
			{
				ShiftTileInSlot(slot + 1, Direction.LEFT);
			}
			EliminateGapsBetweenTiles();
			SendTileCount();
		}

		public void SwapTilesInSlots(int slotA, int slotB)
		{
			Tile tile = m_slots[slotA].tile;
			m_slots[slotA].tile = m_slots[slotB].tile;
			m_slots[slotB].tile = tile;
		}

		public void EliminateGapsBetweenTiles()
		{
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < m_slots.Count - 1; i++)
			{
				if (m_slots[i].tile != null)
				{
					flag = true;
				}
				else
				{
					if (!flag)
					{
						continue;
					}
					for (int j = i + 1; j < m_slots.Count; j++)
					{
						if (m_slots[j].tile != null)
						{
							m_slots[i].tile = m_slots[j].tile;
							m_slots[j].tile = null;
							flag2 = true;
							break;
						}
					}
				}
			}
			if (flag2)
			{
				BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/LettersRearranged");
			}
		}

		public void SendTileCount()
		{
			if (this.TilesUpdated != null)
			{
				this.TilesUpdated(GetTileCount());
			}
		}

		private int GetTileCount()
		{
			int num = 0;
			for (int i = 0; i < m_slots.Count; i++)
			{
				if (m_slots[i].tile != null)
				{
					num++;
				}
			}
			return num;
		}

		public TileSlot GetSlotByIndex(int index)
		{
			return m_slots[index];
		}

		public TileSlot GetClosestSlotToPosition(Vector3 position, Tile forTile, bool canBeOccupied = false)
		{
			TileSlot result = null;
			float num = float.MaxValue;
			bool flag = true;
			for (int i = 0; i < m_slots.Count; i++)
			{
				float num2 = Vector3.Distance(position, m_slots[i].transform.position);
				flag = !(m_slots[i].tile != null) || !(m_slots[i].tile != forTile) || canBeOccupied;
				if (flag && num2 < slotSnapDistance && num2 < num)
				{
					num = num2;
					result = m_slots[i];
				}
			}
			return result;
		}

		public void UpdatePosition(bool aAnimate)
		{
			Vector3 vector = new Vector3(0.5f * letterSpacing * (float)(1 - m_slots.Count), 0f, 0f);
			if (aAnimate)
			{
				m_offsetParent.transform.DOLocalMove(vector, 0.2f).SetEase(Ease.InOutCubic);
			}
			else
			{
				m_offsetParent.transform.localPosition = vector;
			}
		}

		public string GetWord()
		{
			string text = string.Empty;
			for (int i = 0; i < m_slots.Count; i++)
			{
				if (m_slots[i].tile != null)
				{
					text += m_slots[i].tile.letter.text;
				}
			}
			return text;
		}

		public void ExplodeTiles()
		{
			for (int i = 0; i < m_slots.Count; i++)
			{
				TileSlot tileSlot = m_slots[i];
				if (tileSlot.tile != null)
				{
					Tile tile = tileSlot.tile;
					tile.Explode();
					tileSlot.tile = null;
				}
			}
			SendTileCount();
		}

		public void PopInWord(string word, TweenCallback onSequenceEnd)
		{
			Sequence s = DOTween.Sequence();
			for (int i = 0; i < word.Length; i++)
			{
				TileSlot slotByIndex = GetSlotByIndex(i);
				Tile tile = m_game.CreateNewTile(slotByIndex.visual.transform, word[i]);
				tile.isAppearing = true;
				InsertTileAtIndex(i, tile);
				Tween t = slotByIndex.tile.AppearInSlot(slotByIndex);
				s.Insert((float)i * tile.appearDelay, t);
			}
			if (onSequenceEnd != null)
			{
				s.AppendCallback(onSequenceEnd);
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = new Color(0f, 0f, 0f, 0.5f);
			int num = 8;
			if ((bool)m_game)
			{
				num = m_game.maxWordLength;
			}
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = new Vector3((0f - (float)num / 2f) * letterSpacing + letterSpacing * 0.5f, 0f, 0f);
				Vector3 vector2 = new Vector3((float)i * letterSpacing, 0f, 0f);
				Gizmos.DrawWireCube(base.transform.position + vector + vector2, new Vector3(1f, 0.05f, 1f));
			}
			Gizmos.color = Color.red;
			for (int j = 0; j < m_slots.Count; j++)
			{
				if (m_slots[j].tile != null)
				{
					Gizmos.DrawLine(m_slots[j].visual.transform.position, m_slots[j].tile.offsetParent.transform.position);
				}
			}
		}

		public bool CanAddNewTileToWord()
		{
			int num = 0;
			for (int i = 0; i < m_slots.Count; i++)
			{
				if (m_slots[i].tile == null)
				{
					num++;
				}
			}
			return num > 0;
		}

		public void CelebrateAllTiles()
		{
			for (int i = 0; i < m_slots.Count; i++)
			{
				if (m_slots[i].tile != null)
				{
					SetLayerRecursively(m_slots[i].tile.gameObject, LayerMask.NameToLayer("Game3D A"));
					m_slots[i].tile.celebrationJumpIndex = i;
					m_slots[i].tile.SetState(TileStates.CELEBRATING);
				}
			}
		}
	}
}
