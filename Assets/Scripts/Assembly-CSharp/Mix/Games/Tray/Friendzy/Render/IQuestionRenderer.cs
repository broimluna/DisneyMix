using Mix.Games.Tray.Friendzy.Data;
using UnityEngine;

namespace Mix.Games.Tray.Friendzy.Render
{
	public interface IQuestionRenderer
	{
		void RenderQuestion(Question questionToRender);

		QUESTION_TYPE GetRenderType();

		void SetColors(Color mainColor, Color questionFontColor, Color answerFontColor, Color answerBgColor);
	}
}
