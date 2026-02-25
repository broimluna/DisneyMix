using System.Collections.Generic;
using UnityEngine;

namespace Mix.Assets
{
	public class ReferenceCounter
	{
		public Dictionary<string, BundleReference> BundleInstances = new Dictionary<string, BundleReference>();

		public int GetRefCount(string aKey)
		{
			if (BundleInstances.ContainsKey(aKey))
			{
				return BundleInstances[aKey].referenceCount;
			}
			return -1;
		}

		public void DestroyInstance(string aKey, Object aObject = null)
		{
			if (!(aObject is Texture))
			{
				Object.Destroy(aObject);
			}
			DecrementReferenceCount(aKey);
		}

		public Object GetInstance(string aKey)
		{
			if (BundleInstances.ContainsKey(aKey))
			{
				Object obj = null;
				obj = ((!(BundleInstances[aKey].unityObject is Texture2D)) ? Object.Instantiate(BundleInstances[aKey].unityObject) : BundleInstances[aKey].unityObject);
				if (obj != null)
				{
					IncrementReferenceCount(aKey);
				}
				return obj;
			}
			return null;
		}

		public void AddBundleInstance(string aKey, Object aObject, int aReferenceCount = 0)
		{
			if (aObject != null && !BundleInstances.ContainsKey(aKey))
			{
				BundleInstances.Add(aKey, new BundleReference(aObject, aReferenceCount));
			}
		}

		public string FormatKey(string aKey)
		{
			return aKey;
		}

		public void IncrementReferenceCount(string aKey)
		{
			if (BundleInstances.ContainsKey(aKey))
			{
				BundleInstances[aKey].referenceCount++;
			}
		}

		public void DecrementReferenceCount(string aKey)
		{
			if (BundleInstances.ContainsKey(aKey))
			{
				BundleInstances[aKey].referenceCount--;
				if (BundleInstances[aKey].referenceCount <= 0)
				{
					RemoveReference(aKey);
				}
			}
		}

		public void RemoveReference(string aKey)
		{
			if (BundleInstances.ContainsKey(aKey))
			{
				if (BundleInstances[aKey].unityObject is GameObject)
				{
					AssetUtils.DestroyGameObject((GameObject)BundleInstances[aKey].unityObject);
				}
				BundleInstances[aKey].Destroy();
				BundleInstances.Remove(aKey);
			}
		}
	}
}
