using UnityEngine;

namespace Mix.Games.Tray.SecretWord
{
	public class TileSlot : MonoBehaviour
	{
		public GameObject visual;

		public int slotNumber;

		private Tile mTile;

		public Tile tile
		{
			get
			{
				return mTile;
			}
			set
			{
				SetTile(value);
			}
		}

		public void SetTile(Tile aTile)
		{
			if (mTile != null)
			{
				mTile.myCurrentSlot = null;
			}
			mTile = aTile;
			if (mTile != null)
			{
				aTile.myCurrentSlot = this;
			}
		}
	}
}
