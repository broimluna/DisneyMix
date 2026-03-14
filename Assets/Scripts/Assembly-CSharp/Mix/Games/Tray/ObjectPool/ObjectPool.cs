using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray.ObjectPool
{
	public class ObjectPool : MonoBehaviour
	{
		private Transform mTransform;

		private GameObject mPrefab;

		private List<GameObject> mPool;

		private Dictionary<GameObject, int> mInstantiateCounters;

		public int MaxPoolSize { get; private set; }

		public int AvailableObjects
		{
			get
			{
				return mPool.Count;
			}
		}

		public int MinAvailable { get; private set; }

		public GameObject Prefab
		{
			get
			{
				return mPrefab;
			}
		}

		public static ObjectPool CreateNewPool(GameObject aPrefab, int aInitSize)
		{
			GameObject gameObject = new GameObject();
			ObjectPool objectPool = gameObject.AddComponent<ObjectPool>();
			objectPool.Initialize(aPrefab, aInitSize);
			return objectPool;
		}

		public void Expand(int aNewItems)
		{
			for (int i = 0; i < aNewItems; i++)
			{
				GameObject gameObject = InstantiateHelper(mTransform.position, mTransform.rotation);
				mInstantiateCounters.Add(gameObject, 0);
				Recycle(gameObject);
			}
			MaxPoolSize += aNewItems;
		}

		public void Clear()
		{
			for (int i = 0; i < mPool.Count; i++)
			{
				ObjectPoolManager.Instance.UnregisterObject(mPool[i]);
				Object.Destroy(mPool[i]);
			}
			mPool.Clear();
			MaxPoolSize = 0;
		}

		public GameObject InstantiatePooled(Vector3 aPosition, Quaternion aRotation)
		{
			if (mPool.Count == 0)
			{
				Debug.LogWarningFormat("ObjectPool \"{0}\" is out of recycled objects. Instantiating a new one.", base.name);
				Expand(1);
			}
			int index = mPool.Count - 1;
			GameObject gameObject = mPool[index];
			mPool.RemoveAt(index);
			MinAvailable = Mathf.Min(MinAvailable, mPool.Count);
			gameObject.transform.parent = null;
			gameObject.transform.position = aPosition;
			gameObject.transform.rotation = aRotation;
			gameObject.SetActive(true);
			gameObject.SendMessage("Awake", SendMessageOptions.DontRequireReceiver);
			if (mInstantiateCounters[gameObject] > 0)
			{
				StartCoroutine(SendStartMessage(gameObject));
			}
			Dictionary<GameObject, int> dictionary2;
			Dictionary<GameObject, int> dictionary = (dictionary2 = mInstantiateCounters);
			GameObject key2;
			GameObject key = (key2 = gameObject);
			int num = dictionary2[key2];
			dictionary[key] = num + 1;
			return gameObject;
		}

		private IEnumerator SendStartMessage(GameObject go)
		{
			yield return new WaitForEndOfFrame();
			go.SendMessage("Start", SendMessageOptions.DontRequireReceiver);
		}

		public void DestroyPooled(GameObject aObj)
		{
			Recycle(aObj);
		}

		private void Initialize(GameObject aPrefab, int aInitSize)
		{
			mTransform = base.transform;
			mPrefab = aPrefab;
			base.name = string.Format("{0} (ObjectPool)", aPrefab.name);
			MaxPoolSize = 0;
			mPool = new List<GameObject>(aInitSize);
			mInstantiateCounters = new Dictionary<GameObject, int>();
			Expand(aInitSize);
			MinAvailable = aInitSize;
		}

		private GameObject InstantiateHelper()
		{
			GameObject gameObject = Object.Instantiate(mPrefab);
			gameObject.name = string.Format("{0} (clone)", mPrefab.name);
			ObjectPoolManager.Instance.RegisterObjectToPrefab(gameObject, mPrefab);
			return gameObject;
		}

		private GameObject InstantiateHelper(Vector3 aPosition, Quaternion aRotation)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(mPrefab, aPosition, aRotation);
			gameObject.name = string.Format("{0} (clone)", mPrefab.name);
			ObjectPoolManager.Instance.RegisterObjectToPrefab(gameObject, mPrefab);
			return gameObject;
		}

		private void Recycle(GameObject aObj)
		{
			aObj.SetActive(false);
			aObj.transform.parent = mTransform;
			mPool.Add(aObj);
		}
	}
}
