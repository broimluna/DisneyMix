using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray.Common
{
	public class MeshRandomizer : MonoBehaviour
	{
		public MeshFilter meshFilter;

		public List<Mesh> meshVariants;

		private void Start()
		{
			if (meshFilter != null && meshVariants.Count > 1)
			{
				meshFilter.sharedMesh = meshVariants[Random.Range(0, meshVariants.Count)];
			}
		}
	}
}
