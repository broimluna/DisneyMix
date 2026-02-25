using System;
using UnityEngine;

namespace Mix.GagManagement
{
	[Serializable]
	public class OffsetData
	{
		public GameObject OffsetObject;

		[HideInInspector]
		public Vector3 startPos;

		public void SetStartPosition(Vector3 aStartPos)
		{
			startPos = aStartPos;
			OffsetObject.transform.position = startPos;
		}

		public void Destroy()
		{
			OffsetObject = null;
		}
	}
}
