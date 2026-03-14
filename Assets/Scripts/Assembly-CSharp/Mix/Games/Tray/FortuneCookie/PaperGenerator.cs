using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.FortuneCookie
{
	public class PaperGenerator : MonoBehaviour
	{
		public FortuneGenerator fortuneGenerator;

		public GameObject prefab;

		[Range(0f, 1f)]
		public float cooldownBetweenFortunes;

		public float maxPositionOffset;

		public int maxFortunes;

		public Vector3 shiftAmount;

		public float shiftTime;

		private List<FortunePaper> mFortunes = new List<FortunePaper>();

		public string GenerateRandomFortune()
		{
			ShiftAllFortunes();
			GameObject gameObject = Object.Instantiate(prefab);
			gameObject.transform.SetParent(base.transform, false);
			gameObject.transform.localPosition = Random.insideUnitCircle * maxPositionOffset;
			FortunePaper component = gameObject.GetComponent<FortunePaper>();
			component.generator = fortuneGenerator;
			mFortunes.Add(component);
			if (mFortunes.Count > maxFortunes)
			{
				mFortunes[0].Remove();
				mFortunes.RemoveAt(0);
			}
			StartCoroutine(CooldownRoutine());
			return mFortunes[0].messageText.text;
		}

		public void RegenerateFortune(string aMessage)
		{
			GameObject gameObject = Object.Instantiate(prefab);
			gameObject.transform.SetParent(base.transform, false);
			gameObject.transform.localPosition = Random.insideUnitCircle * maxPositionOffset;
			FortunePaper component = gameObject.GetComponent<FortunePaper>();
			component.messageText.text = aMessage;
			base.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f - component.angleRandomness, component.angleRandomness));
			mFortunes.Add(component);
		}

		private void ShiftAllFortunes()
		{
			for (int i = 0; i < mFortunes.Count; i++)
			{
				mFortunes[i].gameObject.transform.DOMove(shiftAmount, shiftTime);
			}
		}

		private IEnumerator CooldownRoutine()
		{
			yield return new WaitForSeconds(cooldownBetweenFortunes);
		}
	}
}
