using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class RunnerGeneratedBackground : MonoBehaviour
	{
		public Mesh startMesh;

		public Mesh endMesh;

		public GameObject backgroundSectionPrefab;

		public int sectionsToGenerate;

		public Vector3 offsetBetweenSections;

		private void Start()
		{
			GameObject gameObject = Object.Instantiate(backgroundSectionPrefab);
			gameObject.transform.SetParent(base.transform);
			gameObject.name = "Level Section Start";
			gameObject.transform.localPosition = offsetBetweenSections * 0.5f;
			gameObject.GetComponent<RunnerBackgroundSection>().SetBackgroundMesh(startMesh);
			for (int i = 0; i < sectionsToGenerate; i++)
			{
				GameObject gameObject2 = Object.Instantiate(backgroundSectionPrefab);
				gameObject2.transform.SetParent(base.transform);
				gameObject2.name = "Level Section " + i;
				gameObject2.transform.localPosition = offsetBetweenSections * (i + 1) + offsetBetweenSections * 0.5f;
			}
			GameObject gameObject3 = Object.Instantiate(backgroundSectionPrefab);
			gameObject3.transform.SetParent(base.transform);
			gameObject3.name = "Level Section End";
			gameObject3.transform.localPosition = offsetBetweenSections * (sectionsToGenerate + 1) + offsetBetweenSections * 0.5f;
			gameObject3.GetComponent<RunnerBackgroundSection>().SetBackgroundMesh(endMesh);
		}
	}
}
