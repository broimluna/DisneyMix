using System;
using UnityEngine;

namespace Mix.GagManagement
{
	[Serializable]
	public class GagObject
	{
		public GameObject GagMesh;

		public OffsetData[] OffsetGroup = new OffsetData[0];

		public void play(Vector3 startPos)
		{
			GagMesh.transform.position = startPos;
			GagMesh.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
			Animator component = GagMesh.GetComponent<Animator>();
			component.Play("Take 001");
		}

		public void Destroy()
		{
			GagMesh = null;
			OffsetData[] offsetGroup = OffsetGroup;
			foreach (OffsetData offsetData in offsetGroup)
			{
				offsetData.Destroy();
			}
			OffsetGroup = null;
		}
	}
}
