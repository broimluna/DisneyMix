using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class CameraShake : MonoBehaviour
	{
		public void ShakeCamera(float amount, float duration, int vibrato = 10, float elasticity = 1f)
		{
			base.transform.DOPunchPosition(Vector3.one * amount, duration, vibrato, elasticity);
		}
	}
}
