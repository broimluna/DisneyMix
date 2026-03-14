using UnityEngine;

namespace Mix.Avatar
{
	public class AvatarObjectSpawner : MonoBehaviour
	{
		public enum AvatarPrefabEnum
		{
			avatar_mp_final = 0,
			avatar_mp_final_flipped = 1,
			avatar_mp_preloader = 2
		}

		public Vector3 AvatarScale = new Vector3(1f, 1f, 1f);

		public Vector3 AvatarPosition = new Vector3(0f, -100f, -100f);

		public Quaternion AvatarRotation = new Quaternion(0f, 180f, 0f, 1f);

		public RuntimeAnimatorController AnimationController;

		public AvatarPrefabEnum AvatarPrefab;

		public GameObject Init()
		{
			return CreateAvatar();
		}

		public GameObject CreateAvatar()
		{
			string path = "Prefabs/Avatar/" + AvatarPrefab;
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>(path));
			if (!gameObject.IsNullOrDisposed())
			{
				gameObject.transform.SetParent(base.gameObject.transform, false);
				gameObject.transform.localScale = AvatarScale;
				gameObject.transform.localPosition = AvatarPosition;
				gameObject.transform.localRotation = AvatarRotation;
				Util.SetLayerRecursively(gameObject, base.gameObject.layer);
				Transform transform = gameObject.transform.Find("cube_rig/grp_offset/shadowPlane");
				if (!transform.IsNullOrDisposed())
				{
					transform.gameObject.SetActive(false);
				}
				GameObject gameObject2 = gameObject.transform.Find("cube_rig").gameObject;
				if (AnimationController != null)
				{
					Animator component = gameObject2.GetComponent<Animator>();
					component.runtimeAnimatorController = AnimationController;
				}
				return gameObject2;
			}
			return null;
		}
	}
}
