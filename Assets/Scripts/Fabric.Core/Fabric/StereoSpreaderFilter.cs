using UnityEngine;

namespace Fabric
{
	[AddComponentMenu("Fabric/DSP/StereoSpreader")]
	public class StereoSpreaderFilter : DSPComponent
	{
		public override void OnInitialise(bool addToAudioSourceGameObject)
		{
			if (addToAudioSourceGameObject)
			{
				OnInitialise("StereoSpreader");
			}
			base.Type = DSPType.StereoSpreader;
			UpdateParameters();
		}

		public override UnityEngine.Component CreateComponent(GameObject gameObject)
		{
			return gameObject.AddComponent<StereoSpreader>();
		}

		public override string GetTypeByName()
		{
			return "StereoSpreader";
		}

		public override void UpdateParameters()
		{
		}
	}
}
