using System;
using System.Collections.Generic;
using Mix.Games.Tray.ObjectPool;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class ColumnGenerator : MonoBehaviour
	{
		public Texture2D ColumnMask;

		public Coordinate2D ColumnOffset;

		[Space(10f)]
		public int MaxColumnDistance;

		public int ColumnPoolSize;

		private List<DropColumn> columns = new List<DropColumn>();

		private bool[] columnData;

		private Coordinate2D centerPoint;

		public DropGame Game { get; private set; }

		private void Start()
		{
			Game = GetComponentInParent<DropGame>();
			if (!ObjectPoolManager.HasPoolForObj(Game.ColumnPrefab.gameObject))
			{
				ObjectPoolManager.CreatePool(Game.ColumnPrefab.gameObject, ColumnPoolSize);
			}
			Color32[] pixels = ColumnMask.GetPixels32();
			columnData = new bool[pixels.Length];
			Color32 color = Color.black;
			for (int i = 0; i < pixels.Length; i++)
			{
				columnData[i] = pixels[i].Equals(color);
			}
		}

		public void Initialize()
		{
			DropPlayer player = Game.Player;
			player.OnPlatformUpdated = (Action<Platform>)Delegate.Combine(player.OnPlatformUpdated, new Action<Platform>(OnPlayerPlatformUpdated));
			PopulateInitialColumns(new Coordinate2D(0, 0));
		}

		public void OnPlayerPlatformUpdated(Platform platform)
		{
			if (platform != null)
			{
				UpdateVisibleColumns(platform.Configuration.GridCoordinates);
			}
		}

		private void PopulateInitialColumns(Coordinate2D initialCenterPoint)
		{
			columns.Clear();
			centerPoint = initialCenterPoint;
			for (int i = centerPoint.x - MaxColumnDistance; i <= centerPoint.x + MaxColumnDistance; i++)
			{
				for (int j = centerPoint.y - MaxColumnDistance; j <= centerPoint.y + MaxColumnDistance; j++)
				{
					Coordinate2D coords = new Coordinate2D(i, j);
					if (IsColumnAtGridCoordinate(coords))
					{
						SpawnColumn(coords);
					}
				}
			}
		}

		private void UpdateVisibleColumns(Coordinate2D newCenterPoint)
		{
			int num = newCenterPoint.x - centerPoint.x;
			int num2 = newCenterPoint.y - centerPoint.y;
			for (int i = 0; i < Mathf.Abs(num); i++)
			{
				ShiftHorizontal((int)Mathf.Sign(num));
			}
			for (int j = 0; j < Mathf.Abs(num2); j++)
			{
				ShiftVertical((int)Mathf.Sign(num2));
			}
		}

		private void ShiftHorizontal(int xOffset)
		{
			xOffset = Mathf.Clamp(xOffset, -1, 1);
			int num = centerPoint.x - MaxColumnDistance + xOffset;
			int num2 = centerPoint.x + MaxColumnDistance + xOffset;
			for (int num3 = columns.Count - 1; num3 >= 0; num3--)
			{
				DropColumn dropColumn = columns[num3];
				if (dropColumn.GridCoords.x < num || dropColumn.GridCoords.x > num2)
				{
					columns.RemoveAt(num3);
					ObjectPoolManager.DestroyPooledObj(dropColumn.gameObject);
				}
			}
			int x = centerPoint.x + MaxColumnDistance * xOffset + xOffset;
			for (int i = centerPoint.y - MaxColumnDistance; i <= centerPoint.y + MaxColumnDistance; i++)
			{
				Coordinate2D coords = new Coordinate2D(x, i);
				if (IsColumnAtGridCoordinate(coords))
				{
					SpawnColumn(coords);
				}
			}
			centerPoint.x += xOffset;
		}

		private void ShiftVertical(int yOffset)
		{
			yOffset = Mathf.Clamp(yOffset, -1, 1);
			int num = centerPoint.y - MaxColumnDistance + yOffset;
			int num2 = centerPoint.y + MaxColumnDistance + yOffset;
			for (int num3 = columns.Count - 1; num3 >= 0; num3--)
			{
				DropColumn dropColumn = columns[num3];
				if (dropColumn.GridCoords.y < num || dropColumn.GridCoords.y > num2)
				{
					columns.RemoveAt(num3);
					ObjectPoolManager.DestroyPooledObj(dropColumn.gameObject);
				}
			}
			int y = centerPoint.y + MaxColumnDistance * yOffset + yOffset;
			for (int i = centerPoint.x - MaxColumnDistance; i <= centerPoint.x + MaxColumnDistance; i++)
			{
				Coordinate2D coords = new Coordinate2D(i, y);
				if (IsColumnAtGridCoordinate(coords))
				{
					SpawnColumn(coords);
				}
			}
			centerPoint.y += yOffset;
		}

		private void SpawnColumn(Coordinate2D coords)
		{
			DropColumn component = ObjectPoolManager.InstantiatePooledObj(Game.ColumnPrefab.gameObject).GetComponent<DropColumn>();
			component.transform.SetParent(base.transform, false);
			component.name = "Column " + coords.ToString();
			component.transform.position = Game.LevelGenerator.GetPositionForCoords(coords);
			component.GridCoords = coords;
			columns.Add(component);
		}

		public bool IsColumnAtGridCoordinate(Coordinate2D coords)
		{
			if (columnData != null && columnData.Length > 0)
			{
				int num = WrapInt(coords.y + ColumnOffset.y, ColumnMask.height);
				int num2 = WrapInt(coords.x + ColumnOffset.x, ColumnMask.width);
				return columnData[num * ColumnMask.width + num2];
			}
			return false;
		}

		private int WrapInt(int val, int range)
		{
			if (val < 0)
			{
				return (val + (Mathf.Abs(val) / range + 1) * range) % range;
			}
			return val % range;
		}
	}
}
