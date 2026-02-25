using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.FriendzyQuiz;
using Mix.Games.Tray.Friendzy.Render;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.FriendzyRenderer
{
	[RequireComponent(typeof(TransitionAnimator))]
	public class QuestionTextRenderer : MonoBehaviour, IQuestionRenderer
	{
		public QUESTION_TYPE Type;

		public Text QuestionText;

		public QuestionOption[] Answers;

		private TransitionAnimator mTransitionAnim;

		private void Awake()
		{
			mTransitionAnim = GetComponent<TransitionAnimator>();
			mTransitionAnim.SetButtons(Answers);
		}

		public void RenderQuestion(Question inQuestionToRender)
		{
			QuestionText.text = inQuestionToRender.Content;
			for (int i = 0; i < Answers.Length; i++)
			{
				Answers[i].TextAnswer.text = inQuestionToRender.Answers[i];
			}
			mTransitionAnim.OptionsEnter();
		}

		public QUESTION_TYPE GetRenderType()
		{
			return Type;
		}

		public void SetColors(Color mainColor, Color questionFontColor, Color answerFontColor, Color answerBgColor)
		{
			for (int i = 0; i < Answers.Length; i++)
			{
				Answers[i].SetColors(mainColor, questionFontColor, answerBgColor);
			}
		}
	}
}
