using DG.Tweening;
using Mix.Games.Tray.Friendzy.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.Menu
{
	public class FlipPanel : MonoBehaviour
	{
		protected const float HALF_FLIP_DURATION = 0.1f;

		public Transform transformToFollow;

		[Header("References To Panels")]
		public Image CategoryImage;

		public Text QuizText;

		public Renderer FlipBarMesh;

		public Renderer FlipBarMaterialTarget;

		public Material ActiveMaterial;

		public Material InactiveMaterial;

		protected Vector3 HALF_FLIP_VECTOR = new Vector3(-90f, 0f, 0f);

		protected Vector3 FLIP_VECTOR = new Vector3(-180f, 0f, 0f);

		protected bool mIsOnCategorySide = true;

		protected void Update()
		{
			if (transformToFollow != null)
			{
				base.transform.position = transformToFollow.position;
			}
		}

		public Sequence Flip180()
		{
			Sequence sequence = DOTween.Sequence();
			if (FlipBarMesh.isVisible)
			{
				sequence.Append(base.transform.DOLocalRotate(HALF_FLIP_VECTOR, 0.1f, RotateMode.LocalAxisAdd));
				sequence.AppendCallback(ToggleSides);
				sequence.Append(base.transform.DOLocalRotate(HALF_FLIP_VECTOR, 0.1f, RotateMode.LocalAxisAdd));
			}
			else
			{
				sequence.AppendCallback(UnseenFlip);
			}
			return sequence;
		}

		public Sequence Flip180End()
		{
			Sequence sequence = DOTween.Sequence();
			if (FlipBarMesh.isVisible)
			{
				sequence.Append(base.transform.DOLocalRotate(HALF_FLIP_VECTOR, 0.1f, RotateMode.LocalAxisAdd));
				sequence.AppendCallback(ToggleSidesEnd);
				sequence.Append(base.transform.DOLocalRotate(HALF_FLIP_VECTOR, 0.1f, RotateMode.LocalAxisAdd));
			}
			else
			{
				sequence.AppendCallback(UnseenFlipEnd);
			}
			return sequence;
		}

		protected void UnseenFlip()
		{
			ToggleSides();
			base.transform.Rotate(FLIP_VECTOR, Space.Self);
		}

		protected void UnseenFlipEnd()
		{
			ToggleSidesEnd();
			base.transform.Rotate(FLIP_VECTOR, Space.Self);
		}

		protected void ToggleSides()
		{
			ToggleOppositeSide(true);
			mIsOnCategorySide = !mIsOnCategorySide;
			ToggleOppositeSide(false);
		}

		protected void ToggleSidesEnd()
		{
			ToggleOppositeSide(false);
			mIsOnCategorySide = !mIsOnCategorySide;
			ToggleOppositeSide(false);
		}

		public virtual void ToggleOppositeSide(bool aToggle)
		{
			if (mIsOnCategorySide)
			{
				if (!aToggle)
				{
					QuizText.text = string.Empty;
				}
				QuizText.enabled = aToggle;
				FlipBarMaterialTarget.material = ActiveMaterial;
			}
			else
			{
				CategoryImage.enabled = aToggle;
				if (string.IsNullOrEmpty(QuizText.text))
				{
					FlipBarMaterialTarget.material = InactiveMaterial;
				}
			}
		}

		public virtual string GetFrontPanelText()
		{
			string empty = string.Empty;
			if (mIsOnCategorySide)
			{
				return base.name;
			}
			return QuizText.text;
		}

		public void AlterFrontPanelColor(Color aColor)
		{
		}

		public virtual void SetCategory(Category aCategory)
		{
			base.name = aCategory.Name;
			CategoryImage.sprite = aCategory.GetLogoPicture().GetPicture();
		}
	}
}
