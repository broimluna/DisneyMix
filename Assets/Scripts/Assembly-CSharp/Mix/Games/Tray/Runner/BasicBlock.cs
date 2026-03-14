using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class BasicBlock : MonoBehaviour
	{
		private Vector3 mMaxOffset = new Vector3(0f, 0f, 0.2f);

		private float randomOffsetFrequency = 10f;

		private void Start()
		{
			float f = (base.transform.localPosition.y * 10f + base.transform.localPosition.x) * randomOffsetFrequency;
			Vector3 localPosition = base.transform.localPosition;
			localPosition += Mathf.Sin(f) * mMaxOffset;
			base.transform.localPosition = localPosition;
		}
	}
}
