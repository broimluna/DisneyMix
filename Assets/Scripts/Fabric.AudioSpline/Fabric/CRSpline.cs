using System;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class CRSpline
	{
		public AudioSplinePoint[] pts;

		private Vector3[] _bakedPoints;

		private Vector3 _prevBakedPoint;

		public void AddPoint(int index, AudioSplinePoint point)
		{
			index++;
			pts = MyArray<AudioSplinePoint>.InsertAt(pts, index, point);
			if (pts[index - 1] != null && pts[index + 1] != null)
			{
				Vector3 position = Vector3.Lerp(pts[index - 1].transform.position, pts[index + 1].transform.position, 0.5f);
				point.transform.position = position;
			}
			RefreshPointNames();
		}

		public AudioSplinePoint RemovePoint(int index)
		{
			AudioSplinePoint result = pts[index];
			pts = MyArray<AudioSplinePoint>.RemoveAt(pts, index);
			RefreshPointNames();
			return result;
		}

		public void RefreshPointNames()
		{
			for (int i = 0; i < pts.Length; i++)
			{
				if (i == 0)
				{
					pts[i].name = "Point_Start";
				}
				else if (i == pts.Length - 1)
				{
					pts[i].name = "Point_End";
				}
				else
				{
					pts[i].name = "Point_" + i;
				}
			}
		}

		public void BakeSpline(float resolution)
		{
			int num = 5000;
			if (resolution > float.MinValue)
			{
				num = Math.Min(num, (int)(1f / resolution) + 1);
			}
			_bakedPoints = new Vector3[num];
			float num2 = 0f;
			for (int i = 0; i < num; i++)
			{
				_bakedPoints[i] = Interp(num2);
				num2 += resolution;
			}
		}

		public Vector3 GetNearestPointToListener(Vector3 position, ref float t)
		{
			float num = float.MaxValue;
			int num2 = 0;
			for (int i = 0; i < _bakedPoints.Length; i++)
			{
				float num3 = Vector3.Distance(_bakedPoints[i], position);
				if (num3 < num)
				{
					num2 = i;
					num = num3;
				}
			}
			Vector3 result = (_prevBakedPoint = Vector3.Lerp(_bakedPoints[num2], _prevBakedPoint, 0.9f));
			t = (float)num2 / (float)_bakedPoints.Length;
			return result;
		}

		public void UpdateEventTriggers(Vector3 position)
		{
			for (int i = 0; i < pts.Length; i++)
			{
				AudioSplinePoint audioSplinePoint = pts[i];
				if (audioSplinePoint.HasEventTrigger())
				{
					float num = Vector3.Distance(audioSplinePoint.transform.position, position);
					if (num < audioSplinePoint._radius && !audioSplinePoint.IsEntered())
					{
						audioSplinePoint.OnEnter();
					}
					else if (num > audioSplinePoint._radius && audioSplinePoint.IsEntered())
					{
						audioSplinePoint.OnExit();
					}
				}
			}
		}

		public Vector3 Interp(float t)
		{
			if (pts.Length >= 3)
			{
				int num = pts.Length - 3;
				int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
				float num3 = t * (float)num - (float)num2;
				if (pts[num2] != null)
				{
					Vector3 position = pts[num2].transform.position;
					Vector3 position2 = pts[num2 + 1].transform.position;
					Vector3 position3 = pts[num2 + 2].transform.position;
					Vector3 position4 = pts[num2 + 3].transform.position;
					return 0.5f * ((-position + 3f * position2 - 3f * position3 + position4) * (num3 * num3 * num3) + (2f * position - 5f * position2 + 4f * position3 - position4) * (num3 * num3) + (-position + position3) * num3 + 2f * position2);
				}
			}
			return default(Vector3);
		}

		public void GizmoDraw(float t, Color splineColor)
		{
			if (pts != null)
			{
				Gizmos.color = splineColor;
				Vector3 to = Interp(0f);
				for (int i = 1; i <= 60; i++)
				{
					float t2 = (float)i / 60f;
					Vector3 vector = Interp(t2);
					Gizmos.DrawLine(vector, to);
					to = vector;
				}
			}
		}
	}
}
