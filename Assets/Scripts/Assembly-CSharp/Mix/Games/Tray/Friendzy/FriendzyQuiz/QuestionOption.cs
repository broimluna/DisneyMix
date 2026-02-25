using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.FriendzyQuiz
{
	public class QuestionOption : MonoBehaviour
	{
		public Button Button;

		public Image SpriteAnswer;

		public Text TextAnswer;

		public Image BottomBar;

		public Image Border;

		public Transform AppearPos;

		public Transform FinalPos;

		private Vector3 mRestPos;

		private void Awake()
		{
			mRestPos = base.transform.localPosition;
		}

		public void SetColors(Color borderColor, Color textColor, Color bgColor)
		{
			SpriteAnswer.color = bgColor;
			TextAnswer.color = textColor;
			Border.color = borderColor;
			if (BottomBar != null)
			{
				BottomBar.color = borderColor;
			}
		}

		public void SetToAppearPos()
		{
			base.transform.localPosition = mRestPos + AppearPos.localPosition;
		}

		public void SetToRestPos()
		{
			base.transform.localPosition = mRestPos;
		}

		public Tween AnimateIn(float aDuration)
		{
			return base.transform.DOLocalMove(mRestPos, aDuration).OnComplete(SetToRestPos);
		}

		public Tween AnimateOut(float aDuration)
		{
			return base.transform.DOLocalMove(mRestPos + FinalPos.localPosition, aDuration);
		}
	}
}
