using UnityEngine;

namespace Fabric.ModularSynth
{
	[AddComponentMenu("Fabric/ModularSynth/Manager")]
	public class ModularSynthManagerUnity : MonoBehaviour
	{
		[SerializeField]
		private SampleManager _sampleManager = new SampleManager();

		public void RegisterFactories()
		{
			ModularSynthFactoryUnity[] componentsInChildren = base.gameObject.GetComponentsInChildren<ModularSynthFactoryUnity>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].RegisterFactory();
			}
		}

		private void Awake()
		{
			RegisterFactories();
		}

		private void Start()
		{
			_sampleManager.Start();
		}

		private void Destroy()
		{
			_sampleManager.Destroy();
		}
	}
}
