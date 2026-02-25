using UnityEngine;

namespace Fabric
{
	[AddComponentMenu("Fabric/Components/BlendComponent")]
	public class BlendComponent : Component
	{
		private bool finishedPlayingOncePerFrame;

		internal override void PlayInternal(ComponentInstance zComponentInstance, float target, float curve, bool dontPlayComponents = false)
		{
			finishedPlayingOncePerFrame = false;
			base.PlayInternal(zComponentInstance, target, curve, dontPlayComponents);
		}

		internal override void OnFinishPlaying(double time)
		{
			if (!finishedPlayingOncePerFrame)
			{
				base.OnFinishPlaying(time);
				finishedPlayingOncePerFrame = true;
			}
		}
	}
}
