using DG.Tweening;
using Mix.Games.Tray.ObjectPool;
using UnityEngine;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveHitMarker : MonoBehaviour
	{
		public float GrowTime = 0.25f;

		public float HoldTime = 0.25f;

		public float ShrinkTime = 0.12f;

		private Transform mTransform;

		private SpriteRenderer mSprite;

		private Sequence mAnimSequence;

		private TextMesh[] ScoreText;

		public void SetPointValue(int aVal)
		{
			string text = aVal.ToString();
			for (int i = 0; i < ScoreText.Length; i++)
			{
				ScoreText[i].text = text;
			}
		}

		private void Awake()
		{
			mTransform = base.transform;
			mSprite = GetComponentInChildren<SpriteRenderer>();
			ScoreText = mTransform.GetComponentsInChildren<TextMesh>();
			SetupAnim();
		}

		private void Start()
		{
			mTransform.localScale = Vector3.zero;
			mSprite.color = Color.white;
			mAnimSequence.Play();
		}

		private void SetupAnim()
		{
			mAnimSequence = DOTween.Sequence();
			Tween t = mTransform.DOScale(Vector3.one, GrowTime).SetEase(Ease.OutBack);
			mAnimSequence.Append(t);
			mAnimSequence.AppendInterval(HoldTime);
			t = mTransform.DOScale(Vector3.zero, ShrinkTime).SetEase(Ease.InCubic);
			mAnimSequence.Append(t);
			t = mSprite.DOFade(0f, ShrinkTime);
			mAnimSequence.Join(t);
			mAnimSequence.AppendCallback(EndAnim);
			mAnimSequence.SetAutoKill(false);
			mAnimSequence.Pause();
		}

		private void EndAnim()
		{
			mAnimSequence.Rewind();
			ObjectPoolManager.DestroyPooledObj(base.gameObject);
		}
	}
}
