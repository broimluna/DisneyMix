using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class CameraZoom : MonoBehaviour
	{
		public float Zoom
		{
			get
			{
				return base.transform.localPosition.z;
			}
			set
			{
				base.transform.localPosition = new Vector3(0f, 0f, value);
			}
		}
	}
}
