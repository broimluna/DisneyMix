using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray.WouldYouRather
{
	public class LineSlider : MonoBehaviour
	{
		public List<Vector3> linePoints;

		public LineSliderAxisEnabled enabledAxis = new LineSliderAxisEnabled();

		private void OnDrawGizmosSelected()
		{
			if (linePoints != null && linePoints.Count > 1)
			{
				Gizmos.color = Color.white;
				for (int i = 0; i < linePoints.Count - 1; i++)
				{
					Gizmos.DrawLine(base.transform.TransformPoint(linePoints[i]), base.transform.TransformPoint(linePoints[i + 1]));
				}
			}
		}

		public void SetPointAtIndex(int index, Vector3 worldPosition)
		{
			if (index < linePoints.Count)
			{
				linePoints[index] = Vector3.Scale(base.transform.InverseTransformPoint(worldPosition), enabledAxis.mask);
			}
		}

		public void AddPointAtIndex(int index, Vector3 worldPosition)
		{
			if (index < linePoints.Count)
			{
				linePoints.Insert(index, Vector3.Scale(base.transform.InverseTransformPoint(worldPosition), enabledAxis.mask));
			}
		}

		public Vector3 GetPointAtIndex(int index)
		{
			if (index < linePoints.Count)
			{
				return base.transform.TransformPoint(linePoints[index]);
			}
			return Vector3.zero;
		}
	}
}
