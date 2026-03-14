using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class AttachToPlayer : MonoBehaviour
	{
		public Transform PlayerCubeRig;

		public string AttachPoint;

		[Space(10f)]
		public Vector3 LocalPosition;

		public Vector3 LocalEulerAngles;

		private void Start()
		{
			if (!(base.transform != null))
			{
				return;
			}
			if (PlayerCubeRig != null)
			{
				Transform[] componentsInChildren = PlayerCubeRig.GetComponentsInChildren<Transform>();
				if (componentsInChildren.Length > 0)
				{
					IEnumerable<Transform> enumerable = componentsInChildren.Where((Transform x) => x.name.Equals(AttachPoint));
					if (enumerable != null)
					{
						base.transform.parent = enumerable.FirstOrDefault();
					}
				}
			}
			base.transform.localEulerAngles = LocalEulerAngles;
			base.transform.localPosition = LocalPosition;
		}
	}
}
