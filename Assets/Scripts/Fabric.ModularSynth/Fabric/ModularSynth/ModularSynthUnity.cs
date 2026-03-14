using UnityEngine;

namespace Fabric.ModularSynth
{
	[AddComponentMenu("Fabric/ModularSynth/Synth")]
	public class ModularSynthUnity : MonoBehaviour
	{
		[SerializeField]
		public bool playOnAwake = true;

		[SerializeField]
		public DSPUnit dspUnit = new DSPUnit();

		[SerializeField]
		public ModularSynthGraphUnity graph;

		private void Start()
		{
		}

		private void Update()
		{
			if (dspUnit != null)
			{
				dspUnit.Update();
			}
		}

		private void Destroy()
		{
			DSPUnit dspUnit2 = dspUnit;
		}

		public void LoadDSPUnit()
		{
			if (graph == null)
			{
				return;
			}
			dspUnit.Clear();
			int blockSize = 1024;
			for (int i = 0; i < graph.GetNumModules(); i++)
			{
				ModuleInfo moduleInfoByIndex = graph.GetModuleInfoByIndex(i);
				dspUnit.CreateModule(moduleInfoByIndex.name, moduleInfoByIndex.guid);
			}
			for (int j = 0; j < graph.GetNumModuleLinks(); j++)
			{
				ModuleLinkInfo moduleLinkInfoByIndex = graph.GetModuleLinkInfoByIndex(j);
				dspUnit.LinkModules(moduleLinkInfoByIndex.fromModule, moduleLinkInfoByIndex.fromPin, moduleLinkInfoByIndex.toModule, moduleLinkInfoByIndex.toPin);
			}
			for (int k = 0; k < graph.GetNumModules(); k++)
			{
				ModuleInfo moduleInfoByIndex2 = graph.GetModuleInfoByIndex(k);
				for (int l = 0; l < moduleInfoByIndex2.propertiesInfo.Count; l++)
				{
					dspUnit.SetModuleProperty(moduleInfoByIndex2.guid, moduleInfoByIndex2.propertiesInfo[l].name, moduleInfoByIndex2.propertiesInfo[l].GetObjectValue());
				}
				for (int m = 0; m < moduleInfoByIndex2.parametersInfo.Count; m++)
				{
					dspUnit.SetModuleParameter(moduleInfoByIndex2.guid, moduleInfoByIndex2.parametersInfo[m].name, moduleInfoByIndex2.parametersInfo[m].value);
				}
			}
			dspUnit.SetNumChannels((int)AudioSettings.speakerMode);
			dspUnit.SetBlockSize(blockSize);
			dspUnit.Create();
		}

		public void Play()
		{
			dspUnit.Play();
		}

		public void Stop()
		{
			dspUnit.Stop();
		}

		public void RemoveModuleInfo(string guid)
		{
			if ((bool)graph)
			{
				graph.RemoveModuleInfo(guid);
			}
		}

		public void RemoveModuleLinkInfo(ModuleLinkInfo info)
		{
			if ((bool)graph)
			{
				graph.RemoveModuleLinkInfo(info);
			}
		}

		private void OnAudioFilterRead(float[] data, int channels)
		{
			if (dspUnit != null)
			{
				dspUnit.Process(data, channels, data, channels, data.Length);
			}
		}

		internal void RegisterFactories()
		{
			ModularSynthFactoryUnity[] componentsInChildren = base.gameObject.GetComponentsInChildren<ModularSynthFactoryUnity>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].RegisterFactory();
			}
		}
	}
}
