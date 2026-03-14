using UnityEngine;

namespace Mix.Games.Avatar
{
	public class FakeAvatarDna : MonoBehaviour
	{
		private const string HEAD_NODE = "head";

		private const string BROW_NODE = "face_brow_";

		private const string EYE_NODE = "face_eye_";

		private const string MOUTH_NODE = "face_mouth";

		private const string EYE_JOINT_CONTROLLER_L = "tex_l_eye";

		private const string EYE_JOINT_CONTROLLER_R = "tex_r_eye";

		private const string MOUTH_JOINT_CONTROLLER = "tex_mouth";

		public Texture2D FaceTex;

		public Texture2D BrowTex;

		public Texture2D EyeTex;

		public Texture2D MouthTex;

		public bool ManualBlendShapes;

		private void Start()
		{
			SetAvatarTextures();
		}

		public void SetAvatarTextures()
		{
			SkinnedMeshRenderer[] componentsInChildren = base.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in componentsInChildren)
			{
				string text = skinnedMeshRenderer.name;
				if (text == "head")
				{
					skinnedMeshRenderer.material.mainTexture = FaceTex;
				}
				else if (text.StartsWith("face_brow_"))
				{
					skinnedMeshRenderer.material.mainTexture = BrowTex;
				}
				else if (text.StartsWith("face_eye_"))
				{
					skinnedMeshRenderer.material.mainTexture = EyeTex;
					string aJointControllerName = ((!text.EndsWith("left")) ? "tex_r_eye" : "tex_l_eye");
					AddAnimatedTextureController(skinnedMeshRenderer.gameObject, aJointControllerName, 0.25f, 0.25f);
				}
				else if (text == "face_mouth")
				{
					skinnedMeshRenderer.material.mainTexture = MouthTex;
					AddAnimatedTextureController(skinnedMeshRenderer.gameObject, "tex_mouth", 0.333f, 0.25f);
				}
				if (!ManualBlendShapes)
				{
					for (int j = 0; j < skinnedMeshRenderer.sharedMesh.blendShapeCount; j++)
					{
						skinnedMeshRenderer.SetBlendShapeWeight(j, 50f);
					}
				}
			}
		}

		private void AddAnimatedTextureController(GameObject aTarget, string aJointControllerName, float aOffsetRatioX, float aOffsetRatioY)
		{
			AnimatedTextureController animatedTextureController = aTarget.AddComponent<AnimatedTextureController>();
			Transform transform = aTarget.transform.parent.Find(aJointControllerName);
			if (transform != null)
			{
				animatedTextureController.jointController = transform.gameObject;
			}
			else
			{
				Debug.LogWarningFormat("Could not find joint controller named \"{0}\"", aJointControllerName);
			}
			animatedTextureController.offsetRatioX = aOffsetRatioX;
			animatedTextureController.offsetRatioY = aOffsetRatioY;
		}
	}
}
