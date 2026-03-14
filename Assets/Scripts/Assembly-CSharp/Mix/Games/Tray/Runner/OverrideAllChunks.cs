using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	[RequireComponent(typeof(MainRunnerGame))]
	public class OverrideAllChunks : MonoBehaviour
	{
		public bool overrideChunks;

		public ChunkController chunk;

		private void Awake()
		{
			if (overrideChunks && UnityEngine.Application.isEditor)
			{
				MainRunnerGame component = GetComponent<MainRunnerGame>();
				for (int i = 0; i < component.chunks.Count; i++)
				{
					component.chunks[i] = chunk;
				}
				component.maxLives = 999;
			}
		}
	}
}
