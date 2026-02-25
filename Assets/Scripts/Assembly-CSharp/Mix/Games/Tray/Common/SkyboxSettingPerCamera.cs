using UnityEngine;

namespace Mix.Games.Tray.Common
{
	[RequireComponent(typeof(Camera))]
	public class SkyboxSettingPerCamera : MonoBehaviour
	{
		public Material skyboxMaterial;

		private Material mOriginalSkyboxMaterial;

		private void OnPreRender()
		{
			mOriginalSkyboxMaterial = RenderSettings.skybox;
			RenderSettings.skybox = skyboxMaterial;
		}

		private void OnPostRender()
		{
			RenderSettings.skybox = mOriginalSkyboxMaterial;
		}
	}
}
