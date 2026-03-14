using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray
{
	[RequireComponent(typeof(Animator))]
	public class PreserveAnimationControllerState : MonoBehaviour
	{
		public struct LayerStateInfo
		{
			public AnimatorStateInfo CurrentStateInfo;

			public AnimatorStateInfo NextStateInfo;

			public LayerStateInfo(AnimatorStateInfo current, AnimatorStateInfo next)
			{
				CurrentStateInfo = current;
				NextStateInfo = next;
			}
		}

		[Tooltip("When the animator is restored this boolean parameter is set to true.  This allows the animator to know it was restored.")]
		public string RestoredParameter;

		private Dictionary<int, LayerStateInfo> layerStateInfos = new Dictionary<int, LayerStateInfo>();

		private Dictionary<int, bool> boolParemeters = new Dictionary<int, bool>();

		private Dictionary<int, float> floatParameters = new Dictionary<int, float>();

		private Dictionary<int, int> intParameters = new Dictionary<int, int>();

		private Dictionary<int, bool> triggerParameters = new Dictionary<int, bool>();

		private bool isQuitting;

		private bool isRestoring;

		private void OnValidate()
		{
			isRestoring = false;
		}

		private void OnEnable()
		{
			if (isRestoring)
			{
				Animator component = GetComponent<Animator>();
				foreach (int key in boolParemeters.Keys)
				{
					component.SetBool(key, boolParemeters[key]);
				}
				foreach (int key2 in floatParameters.Keys)
				{
					component.SetFloat(key2, floatParameters[key2]);
				}
				foreach (int key3 in intParameters.Keys)
				{
					component.SetInteger(key3, intParameters[key3]);
				}
				foreach (int key4 in triggerParameters.Keys)
				{
					if (triggerParameters[key4])
					{
						component.SetTrigger(key4);
					}
				}
				foreach (int key5 in layerStateInfos.Keys)
				{
					component.Play(layerStateInfos[key5].CurrentStateInfo.shortNameHash, key5, layerStateInfos[key5].CurrentStateInfo.normalizedTime);
					if (layerStateInfos[key5].NextStateInfo.shortNameHash != 0)
					{
						component.Play(layerStateInfos[key5].NextStateInfo.shortNameHash, key5, layerStateInfos[key5].NextStateInfo.normalizedTime);
					}
				}
				boolParemeters.Clear();
				intParameters.Clear();
				floatParameters.Clear();
				triggerParameters.Clear();
				layerStateInfos.Clear();
				if (!string.IsNullOrEmpty(RestoredParameter))
				{
					component.SetBool(RestoredParameter, true);
				}
			}
			isRestoring = false;
		}

		private void OnDisable()
		{
			if (isQuitting)
			{
				return;
			}
			if (base.gameObject.activeSelf)
			{
				Animator component = GetComponent<Animator>();
				layerStateInfos.Clear();
				for (int i = 0; i < component.layerCount; i++)
				{
					layerStateInfos.Add(i, new LayerStateInfo(component.GetCurrentAnimatorStateInfo(i), component.GetNextAnimatorStateInfo(i)));
				}
				for (int j = 0; j < component.parameterCount; j++)
				{
					AnimatorControllerParameter parameter = component.GetParameter(j);
					if (parameter.type == AnimatorControllerParameterType.Bool)
					{
						boolParemeters.Add(parameter.nameHash, component.GetBool(parameter.nameHash));
					}
					else if (parameter.type == AnimatorControllerParameterType.Float)
					{
						floatParameters.Add(parameter.nameHash, component.GetFloat(parameter.nameHash));
					}
					else if (parameter.type == AnimatorControllerParameterType.Int)
					{
						intParameters.Add(parameter.nameHash, component.GetInteger(parameter.nameHash));
					}
					else if (parameter.type == AnimatorControllerParameterType.Trigger)
					{
						triggerParameters.Add(parameter.nameHash, component.GetBool(parameter.nameHash));
					}
				}
				isRestoring = true;
			}
			else
			{
				isRestoring = false;
			}
		}

		private void OnApplicationQuit()
		{
			isQuitting = true;
			isRestoring = false;
		}
	}
}
