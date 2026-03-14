using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Friendzy.Menu
{
	public class LerpColorSwitch : MonoBehaviour
	{
		private const float CATEGORY_BLEND_VALUE = 0f;

		private const float QUIZ_BLEND_VALUE = 1f;

		public Material MenuMaterial;

		public Texture MainTexture;

		public Texture DefaultTexture;

		public Material BladeMaterial;

		private float BlendValue;

		private float TargetValue;

		public Color BaseColor = Color.white;

		private Color mIPColor = Color.black;

		private bool mIsOnCategorySide = true;

		public Texture IPTexture { private get; set; }

		private void Awake()
		{
			Initialize();
		}

		public void Initialize()
		{
			BlendValue = 0f;
			TargetValue = 1f;
			mIsOnCategorySide = true;
			IPTexture = DefaultTexture;
			MenuMaterial.SetTexture("_MainTex", MainTexture);
			MenuMaterial.SetTexture("_Texture2", DefaultTexture);
		}

		public void SetIPTexture(Texture aIPTexture)
		{
			IPTexture = ((!(aIPTexture == null)) ? aIPTexture : DefaultTexture);
			MenuMaterial.SetTexture("_Texture2", IPTexture);
		}

		public void SetIPColor(Color aTarget)
		{
			mIPColor = aTarget;
			mIPColor.a = 0.5f;
		}

		private void Update()
		{
			MenuMaterial.SetFloat("_Blend", BlendValue);
			Color color = Color.Lerp(BaseColor, mIPColor, BlendValue);
			BladeMaterial.SetColor("_TintColor", color);
		}

		public Tween BlendColors(float aDuration)
		{
			TargetValue = ((!mIsOnCategorySide) ? 0f : 1f);
			mIsOnCategorySide = !mIsOnCategorySide;
			return TweenLerpShaderFloat(TargetValue, aDuration);
		}

		private Tween TweenLerpShaderFloat(float aLerpPosition, float aDuration)
		{
			return DOTween.To(() => BlendValue, delegate(float x)
			{
				BlendValue = x;
			}, aLerpPosition, aDuration);
		}
	}
}
