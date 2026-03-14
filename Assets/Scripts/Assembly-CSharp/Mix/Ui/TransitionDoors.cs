using Avatar;
using Mix.Avatar;
using Mix.Session;
using UnityEngine;

namespace Mix.Ui
{
	public class TransitionDoors : BaseNavigationTransition
	{
		private ScreenHolder newScreen;

		private ScreenHolder oldScreen;

		private GameObject gameObject;

		private GameObject animation;

		private Animator animator;

		private GameObject loaderPrefab;

		private GameObject loader;

		private bool animationStartDone;

		private float delay;

		public override void Setup(ScreenHolder aOldScreen)
		{
			delay = 0f;
			animationStartDone = false;
			newScreen = null;
			oldScreen = aOldScreen;
			animation = Resources.Load<GameObject>("Prefabs/Ui/transitionMesh");
			loaderPrefab = Resources.Load<GameObject>("Prefabs/Ui/loaders/TransitionLoader");
			gameObject = Object.Instantiate(animation);
			animator = gameObject.GetComponent<Animator>();
			animator.Play("transitionStart");
		}

		public override void Start(ScreenHolder aNewScreen)
		{
			newScreen = aNewScreen;
			newScreen.Hide();
		}

		public override bool Update()
		{
			bool result = false;
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && delay > 0.1f)
			{
				if (!animationStartDone && newScreen != null)
				{
					if (loader != null)
					{
						Object.Destroy(loader);
					}
					animationStartDone = true;
					delay = 0f;
					newScreen.Show();
					oldScreen.Hide();
					animator.Play("transitionEnd");
				}
				else if (newScreen == null && loader == null)
				{
					loader = Object.Instantiate(loaderPrefab);
					loader.transform.SetParent(GameObject.Find("Overlay_Holder").transform, false);
					SkinnedMeshRenderer component = loader.transform.Find("3DTracker/cube_rig/grp_mesh/avatar_cube").GetComponent<SkinnedMeshRenderer>();
					if (component != null)
					{
						MonoSingleton<AvatarManager>.Instance.SkinAvatar(component.gameObject, MixSession.User.Avatar, (AvatarFlags)0, null);
					}
				}
				else if (animationStartDone)
				{
					Object.Destroy(gameObject);
					result = true;
				}
			}
			delay += Time.deltaTime;
			return result;
		}
	}
}
