using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.FortuneCookie
{
	public class FortunePaper : MonoBehaviour
	{
		public Text messageText;

		private FortuneGenerator mGenerator;

		[Range(0f, 45f)]
		public float angleRandomness;

		public float bounceAmount = 0.2f;

		public float bounceTime = 0.4f;

		public FortuneGenerator generator
		{
			get
			{
				return mGenerator;
			}
			set
			{
				mGenerator = value;
				GenerateRandomFortune();
			}
		}

		private void Start()
		{
		}

		public void GenerateRandomFortune()
		{
			messageText.text = generator.GenerateRandomFortune();
			base.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f - angleRandomness, angleRandomness));
			base.transform.localScale = Vector3.zero;
			base.gameObject.transform.DOScale(Vector3.one, bounceTime);
		}

		public void Remove()
		{
			base.gameObject.transform.DOScale(Vector3.zero, 0.5f);
			Object.Destroy(base.gameObject, 0.5f);
		}
	}
}
