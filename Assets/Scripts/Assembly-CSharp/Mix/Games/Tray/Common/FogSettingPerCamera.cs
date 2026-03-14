using UnityEngine;

namespace Mix.Games.Tray.Common
{
	[RequireComponent(typeof(Camera))]
	public class FogSettingPerCamera : MonoBehaviour
	{
		public bool enableFog;

		[Space(10f)]
		public Color fogColor = Color.white;

		public float fogDensity = 0.01f;

		private bool mOriginalFogState;

		private Color mOriginalFogColor;

		private float mOriginalFogDensity;

		private void OnPreRender()
		{
			mOriginalFogState = RenderSettings.fog;
			mOriginalFogColor = RenderSettings.fogColor;
			mOriginalFogDensity = RenderSettings.fogDensity;
			RenderSettings.fog = enableFog;
			RenderSettings.fogColor = fogColor;
			RenderSettings.fogDensity = fogDensity;
		}

		private void OnPostRender()
		{
			RenderSettings.fog = mOriginalFogState;
			RenderSettings.fogColor = mOriginalFogColor;
			RenderSettings.fogDensity = mOriginalFogDensity;
		}
	}
}
