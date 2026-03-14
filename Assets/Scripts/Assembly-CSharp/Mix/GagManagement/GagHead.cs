using UnityEngine;

namespace Mix.GagManagement
{
	public class GagHead
	{
		private Vector3 headStartLocalPos;

		private RuntimeAnimatorController previousAnimatorController;

		public GameObject Head { get; private set; }

		public GameObject mainObject { get; private set; }

		public Vector3 worldPosition { get; private set; }

		public Animator animator { get; private set; }

		public Vector3 HeadStartPos { get; private set; }

		public GagHead(GameObject aHead, RuntimeAnimatorController aAvatarAnimatorController)
		{
			Head = aHead;
			HeadStartPos = Head.transform.position;
			headStartLocalPos = Head.transform.localPosition;
			mainObject = Head.transform.Find("cube_rig").gameObject;
			worldPosition = mainObject.transform.position;
			animator = mainObject.GetComponent<Animator>();
			previousAnimatorController = animator.runtimeAnimatorController;
			animator.runtimeAnimatorController = aAvatarAnimatorController;
		}

		public void Destroy()
		{
			Vector3 localPosition = Head.transform.localPosition;
			Head.transform.localPosition = new Vector3(headStartLocalPos.x, localPosition.y, headStartLocalPos.z);
			Head = null;
			mainObject = null;
			animator.runtimeAnimatorController = previousAnimatorController;
			previousAnimatorController = null;
		}
	}
}
