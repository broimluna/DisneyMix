using UnityEngine;

namespace Mix.Games.Tray.Common
{
	public class GizmoExtensions
	{
		public static void DrawArrowGizmo(Vector3 from, Vector3 to, float endGap = 0f)
		{
			float num = 0.3f;
			to -= (to - from).normalized * endGap;
			Vector3 vector = (from - to).normalized * num;
			Vector3 vector2 = Vector3.Cross(vector, Vector3.up);
			Gizmos.DrawLine(from, to);
			Gizmos.DrawLine(to, to + vector + vector2);
			Gizmos.DrawLine(to, to + vector - vector2);
		}
	}
}
