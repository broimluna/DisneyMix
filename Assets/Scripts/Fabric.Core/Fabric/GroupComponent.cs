using System;
using UnityEngine;

namespace Fabric
{
	[AddComponentMenu("Fabric/Components/GroupComponent")]
	public class GroupComponent : Component
	{
		private bool _solo;

		[SerializeField]
		[HideInInspector]
		public bool _showInMixerView = true;

		[HideInInspector]
		[SerializeField]
		public string _targetGroupComponentPath;

		[NonSerialized]
		[HideInInspector]
		public bool _isRegisteredWithMainHierarchy;

		[HideInInspector]
		[SerializeField]
		public bool _ignoreUnloadUnusedAssets = true;

		[NonSerialized]
		[HideInInspector]
		public int _registeredWithMainRefCount;

		[NonSerialized]
		[HideInInspector]
		public static bool _createProxy = true;

		private bool finishedPlayingOncePerFrame;

		public bool Solo
		{
			get
			{
				return _solo;
			}
			set
			{
				_solo = value;
			}
		}

		public bool IsFabricHierarchyPresent()
		{
			if (base.transform.parent != null && (base.transform.parent.gameObject.GetComponent<FabricManager>() != null || base.transform.parent.gameObject.GetComponent<Component>() != null))
			{
				return true;
			}
			return false;
		}

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

		public void IncRef()
		{
			_registeredWithMainRefCount++;
		}

		public void DecRef()
		{
			_registeredWithMainRefCount--;
		}

		public void Awake()
		{
			if (!Component._initializationInProgress && !(FabricManager.Instance == null) && !IsFabricHierarchyPresent() && !_isInstance && _targetGroupComponentPath != null)
			{
				_isRegisteredWithMainHierarchy = FabricManager.Instance.RegisterGroupComponent(this, _targetGroupComponentPath, _createProxy);
			}
		}

		private void OnDestroy()
		{
			if (!_quitting && !IsFabricHierarchyPresent() && !(FabricManager.Instance == null) && _isRegisteredWithMainHierarchy)
			{
				_isRegisteredWithMainHierarchy = FabricManager.Instance.UnregisterGroupComponent(this);
				if (!_ignoreUnloadUnusedAssets)
				{
					Resources.UnloadUnusedAssets();
				}
			}
		}
	}
}
