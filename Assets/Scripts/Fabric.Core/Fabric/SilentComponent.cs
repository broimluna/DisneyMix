using UnityEngine;

namespace Fabric
{
	[AddComponentMenu("Fabric/Components/SilentComponent")]
	public class SilentComponent : AudioComponent
	{
		[Range(0f, 120f)]
		[SerializeField]
		public float _silenceLength = 1f;

		[SerializeField]
		[Range(0f, 120f)]
		public float _randomizeSilenceLength;

		protected override void OnInitialise(bool inPreviewMode = false)
		{
			float num = _silenceLength + (float)(_random.NextDouble() * (double)_randomizeSilenceLength);
			int num2 = 8000;
			AudioClip audioClip = AudioClip.Create("silenceAudioClip", (int)((float)num2 * num), 1, num2, false, false);
			base.AudioClip = audioClip;
			base.OnInitialise(inPreviewMode);
		}
	}
}
