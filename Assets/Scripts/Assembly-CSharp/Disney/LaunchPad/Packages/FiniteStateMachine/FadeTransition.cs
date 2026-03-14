using System.Collections;
using UnityEngine;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class FadeTransition : Transition
	{
		[SerializeField]
		public float fadeOutSeconds = 0.5f;

		[SerializeField]
		public float fadeInSeconds = 0.5f;

		[SerializeField]
		public float waitSeconds = 0.5f;

		[SerializeField]
		public Color fadeColor = Color.black;

		[SerializeField]
		public Texture2D texture;

		private float mTimeElapsed;

		private Color mCurrentFadeColor = Color.clear;

		private Texture2D mRenderTexture;

		public override void Perform(StateChangeArgs stateChangeDetails)
		{
			m_stateChangeDetails = stateChangeDetails;
			SetTransitionEnabled(true);
			CreateTexture();
			m_stateChangeDetails.StartState.RaisePreExitEvent(m_stateChangeDetails);
			StartCoroutine(FadeOutScreen());
		}

		public override void Reset()
		{
			m_stateChangeDetails = null;
			mTimeElapsed = 0f;
			EventDispatcher.ClearAll();
			SetTransitionEnabled(false);
		}

		public void OnGUI()
		{
			GUI.depth = -1000;
			GUI.color = mCurrentFadeColor;
			Rect position = new Rect(0f, 0f, Screen.width, Screen.height);
			if (mRenderTexture != null)
			{
				GUI.DrawTexture(position, mRenderTexture);
			}
			GUI.color = Color.white;
		}

		private Color CalculateFadeColor(Color startColor, Color endColor, float timeInterval)
		{
			Color result = startColor;
			result.r = Mathf.SmoothStep(result.r, endColor.r, timeInterval);
			result.g = Mathf.SmoothStep(result.g, endColor.g, timeInterval);
			result.b = Mathf.SmoothStep(result.b, endColor.b, timeInterval);
			result.a = Mathf.SmoothStep(result.a, endColor.a, timeInterval);
			return result;
		}

		private IEnumerator FadeOutScreen()
		{
			mTimeElapsed = 0f;
			while (mTimeElapsed < fadeOutSeconds)
			{
				mCurrentFadeColor = CalculateFadeColor(Color.clear, Color.white, mTimeElapsed / fadeOutSeconds);
				mTimeElapsed += Time.deltaTime;
				yield return null;
			}
			mCurrentFadeColor = Color.white;
			m_stateChangeDetails.StartState.RaiseExitEvent(m_stateChangeDetails);
			m_stateChangeDetails.StartState.RaisePostExitEvent(m_stateChangeDetails);
			yield return StartCoroutine(OnFullyFadedOut());
			StartCoroutine(FadeInScreen());
		}

		protected virtual IEnumerator OnFullyFadedOut()
		{
			yield return new WaitForSeconds(waitSeconds);
		}

		private IEnumerator FadeInScreen()
		{
			m_stateChangeDetails.EndState.RaisePreEnterEvent(m_stateChangeDetails);
			mTimeElapsed = 0f;
			while (mTimeElapsed < fadeInSeconds)
			{
				mCurrentFadeColor = CalculateFadeColor(Color.white, Color.clear, mTimeElapsed / fadeInSeconds);
				mTimeElapsed += Time.deltaTime;
				yield return null;
			}
			DestroyTexture();
			m_stateChangeDetails.EndState.RaiseEnterEvent(m_stateChangeDetails);
			m_stateChangeDetails.EndState.RaisePostEnterEvent(m_stateChangeDetails);
			RaiseTransitionCompletedEvent();
		}

		private void CreateTexture()
		{
			if (texture == null)
			{
				mRenderTexture = new Texture2D(1, 1);
				mRenderTexture.SetPixels(new Color[1] { fadeColor });
				mRenderTexture.Apply();
			}
			else
			{
				mRenderTexture = texture;
			}
		}

		private void DestroyTexture()
		{
			if (mRenderTexture != texture)
			{
				Object.Destroy(mRenderTexture);
			}
			mRenderTexture = null;
		}
	}
}
