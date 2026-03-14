using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.SecretWord
{
	public class SecretWordGameOverPanel : MonoBehaviour
	{
		[Header("Internal References")]
		public GameObject[] heartFragments;

		public GameObject heartHolder;

		public Image background;

		public Text message;

		[Header("Show Animation")]
		public float heartScaleInTime = 0.5f;

		public float heartScaleInDelay = 0.2f;

		public Ease heartScaleInEaseType;

		public float hearteRotationRandonmess = 10f;

		public float messageScaleInTime = 0.5f;

		public float messageScaleInDelay = 0.3f;

		public Ease messageScaleInEaseType;

		public float messageRotationRandonmess = 10f;

		public float backgroundFadeInTime = 0.5f;

		public float backgroundFadeInDelay;

		public Color backgroundFadeInitialColor;

		public Color backgroudnFadeFinalColor;

		public float heartFragmentShakeAmount;

		private List<Vector3> mOriginalHeartFragmentPositions;

		public void Awake()
		{
			mOriginalHeartFragmentPositions = new List<Vector3>();
			for (int i = 0; i < heartFragments.Length; i++)
			{
				mOriginalHeartFragmentPositions.Add(heartFragments[i].transform.localPosition);
			}
		}

		public void Update()
		{
			for (int i = 0; i < heartFragments.Length; i++)
			{
				heartFragments[i].transform.localPosition = mOriginalHeartFragmentPositions[i] + (Vector3)Random.insideUnitCircle * heartFragmentShakeAmount;
			}
		}

		[ContextMenu("Show")]
		public void Show()
		{
			base.gameObject.SetActive(true);
			heartHolder.transform.localScale = Vector3.zero;
			message.transform.localScale = Vector3.zero;
			background.color = backgroundFadeInitialColor;
			background.DOColor(backgroudnFadeFinalColor, backgroundFadeInTime).SetDelay(backgroundFadeInDelay);
			heartHolder.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f - hearteRotationRandonmess, hearteRotationRandonmess));
			Sequence s = DOTween.Sequence();
			s.AppendInterval(heartScaleInDelay);
			s.Append(heartHolder.transform.DOScale(Vector3.one, heartScaleInTime).SetEase(heartScaleInEaseType));
			message.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f - messageRotationRandonmess, messageRotationRandonmess));
			Sequence s2 = DOTween.Sequence();
			s2.AppendInterval(messageScaleInDelay);
			s2.Append(message.transform.DOScale(Vector3.one, messageScaleInTime).SetEase(messageScaleInEaseType));
		}

		[ContextMenu("Hide")]
		public void Hide()
		{
			background.DOColor(backgroundFadeInitialColor, backgroundFadeInTime);
			heartHolder.transform.DOScale(Vector3.zero, heartScaleInTime).SetEase(heartScaleInEaseType);
			message.transform.DOScale(Vector3.zero, messageScaleInTime).SetEase(messageScaleInEaseType);
		}
	}
}
