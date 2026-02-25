using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Drop
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Text))]
	public class BestFitStaticFont : MonoBehaviour
	{
		public int MinFontSize;

		public int MaxFontSize;

		private Text textComponent;

		private bool isFittingText;

		private bool TextFits
		{
			get
			{
				return LayoutUtility.GetPreferredWidth(textComponent.rectTransform) <= textComponent.rectTransform.rect.width && LayoutUtility.GetPreferredHeight(textComponent.rectTransform) <= textComponent.rectTransform.rect.height;
			}
		}

		private void Awake()
		{
			textComponent = GetComponent<Text>();
			textComponent.RegisterDirtyLayoutCallback(FitTextToBox);
			isFittingText = false;
		}

		private IEnumerator Start()
		{
			yield return null;
			FitTextToBox();
		}

		private void FitTextToBox()
		{
			if (!isFittingText)
			{
				isFittingText = true;
				textComponent.fontSize = MaxFontSize;
				while (!TextFits && textComponent.fontSize > MinFontSize)
				{
					textComponent.fontSize--;
				}
				isFittingText = false;
			}
		}
	}
}
