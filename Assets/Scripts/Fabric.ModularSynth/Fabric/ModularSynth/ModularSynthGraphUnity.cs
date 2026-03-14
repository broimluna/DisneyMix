using System.Collections.Generic;
using UnityEngine;

namespace Fabric.ModularSynth
{
	[AddComponentMenu("Fabric/ModularSynth/Graph")]
	public class ModularSynthGraphUnity : MonoBehaviour
	{
		[SerializeField]
		private List<ModuleInfo> moduleInfoList = new List<ModuleInfo>();

		[SerializeField]
		private List<ModuleLinkInfo> moduleLinkInfoList = new List<ModuleLinkInfo>();

		public void Clear()
		{
			moduleInfoList.Clear();
			moduleLinkInfoList.Clear();
		}

		public int GetNumModules()
		{
			return moduleInfoList.Count;
		}

		public ModuleInfo GetModuleInfoByIndex(int index)
		{
			return moduleInfoList[index];
		}

		public ModuleInfo GetModuleInfoByGuid(string guid)
		{
			for (int i = 0; i < moduleInfoList.Count; i++)
			{
				ModuleInfo moduleInfo = moduleInfoList[i];
				if (moduleInfo.guid == guid)
				{
					return moduleInfo;
				}
			}
			return null;
		}

		public void AddModuleInfo(ModuleInfo info)
		{
			moduleInfoList.Add(info);
		}

		public void RemoveModuleInfo(string guid)
		{
			for (int i = 0; i < moduleInfoList.Count; i++)
			{
				ModuleInfo moduleInfo = moduleInfoList[i];
				if (!(moduleInfo.guid == guid))
				{
					continue;
				}
				moduleInfoList.Remove(moduleInfo);
				int count = moduleLinkInfoList.Count;
				for (int num = count - 1; num >= 0; num--)
				{
					ModuleLinkInfo moduleLinkInfo = moduleLinkInfoList[num];
					if (moduleLinkInfo.fromModule == moduleInfo.guid || moduleLinkInfo.toModule == moduleInfo.guid)
					{
						moduleLinkInfoList.RemoveAt(num);
					}
				}
			}
		}

		public int GetNumModuleLinks()
		{
			return moduleLinkInfoList.Count;
		}

		public ModuleLinkInfo GetModuleLinkInfoByIndex(int index)
		{
			return moduleLinkInfoList[index];
		}

		public void AddModuleLinkInfo(ModuleLinkInfo info)
		{
			moduleLinkInfoList.Add(info);
		}

		public void RemoveModuleLinkInfo(ModuleLinkInfo info)
		{
			moduleLinkInfoList.Remove(info);
		}
	}
}
