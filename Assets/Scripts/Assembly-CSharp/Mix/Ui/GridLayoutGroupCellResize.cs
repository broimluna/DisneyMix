using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class GridLayoutGroupCellResize : GridLayoutGroup
	{
		private float GetLayoutDimension(bool getHeight)
		{
			if (getHeight)
			{
				return base.rectTransform.rect.height - ((float)(base.padding.top + base.padding.bottom) + base.spacing.y);
			}
			return base.rectTransform.rect.width - ((float)(base.padding.left + base.padding.right) + base.spacing.x);
		}

		private float GetNewCellDimension(bool getHeight)
		{
			float result = ((!getHeight) ? base.cellSize.x : base.cellSize.y);
			float layoutDimension = GetLayoutDimension(getHeight);
			if (layoutDimension > 0f)
			{
				result = layoutDimension / (float)base.constraintCount;
			}
			return result;
		}

		private void ResizeCells()
		{
			if (base.constraint == Constraint.FixedRowCount)
			{
				float newCellDimension = GetNewCellDimension(true);
				base.cellSize = new Vector2(newCellDimension / (base.cellSize.y / base.cellSize.x), newCellDimension);
			}
			else if (base.constraint == Constraint.FixedColumnCount)
			{
				float newCellDimension = GetNewCellDimension(false);
				base.cellSize = new Vector2(newCellDimension, newCellDimension * (base.cellSize.y / base.cellSize.x));
			}
		}

		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			ResizeCells();
		}
	}
}
