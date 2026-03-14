using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class StartDifficultyDisplay : MonoBehaviour
	{
		public GameObject skullBlockPrefab;

		public GameObject regularBlockPrefab;

		public int maxSkulls = 5;

		private MainRunnerGame mainController;

		private void Start()
		{
			GameObject gameObject = GameObject.Find("main");
			if ((bool)gameObject)
			{
				mainController = gameObject.GetComponent<MainRunnerGame>();
				if ((bool)mainController)
				{
					SetSkulls(mainController.avgDifficulty);
				}
			}
		}

		private void SetSkulls(int aNum)
		{
			for (int i = 0; i < maxSkulls; i++)
			{
				GameObject gameObject = null;
				if (i < aNum)
				{
					gameObject = Object.Instantiate(skullBlockPrefab);
					gameObject.transform.SetParent(base.transform);
				}
				else
				{
					gameObject = Object.Instantiate(regularBlockPrefab);
					gameObject.transform.SetParent(base.transform);
				}
				gameObject.transform.localPosition = Vector3.right * i;
			}
		}
	}
}
