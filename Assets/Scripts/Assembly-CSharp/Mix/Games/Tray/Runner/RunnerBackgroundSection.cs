using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class RunnerBackgroundSection : MonoBehaviour
	{
		[Header("Internal References")]
		public MeshFilter meshFilter;

		[Space(10f)]
		public List<Mesh> backgroundMeshes;

		public bool useRandomBackground;

		private void Start()
		{
			if (useRandomBackground)
			{
				meshFilter.sharedMesh = backgroundMeshes[Random.Range(0, backgroundMeshes.Count)];
			}
		}

		public void SetBackgroundMesh(Mesh mesh)
		{
			meshFilter.sharedMesh = mesh;
			useRandomBackground = false;
		}
	}
}
