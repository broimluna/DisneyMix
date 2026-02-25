using UnityEngine;

namespace Mix.Assets
{
	public class BundleReference
	{
		public int referenceCount;

		public Object unityObject;

		public BundleReference(Object aUnityObject, int aReferenceCount = 0)
		{
			referenceCount = aReferenceCount;
			unityObject = aUnityObject;
		}

		public void Destroy()
		{
			Object.DestroyImmediate(unityObject, true);
			unityObject = null;
		}
	}
}
