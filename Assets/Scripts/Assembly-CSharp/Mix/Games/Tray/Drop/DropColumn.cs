using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	[SelectionBase]
	public class DropColumn : MonoBehaviour
	{
		public Material SolidMaterial;

		public Material TransparentMaterial;

		public MeshRenderer Mesh;

		public Color TransparentColor;

		public float TransparencyRampTime;

		private int collisionCount;

		private float transparencyRampVelocity;

		private float targetTransparency;

		private float transparency;

		private bool isSolid;

		public Coordinate2D GridCoords { get; set; }

		private void Awake()
		{
			transparency = 1f;
			transparencyRampVelocity = 0f;
			MakeSolid();
		}

		private void Update()
		{
			if (!isSolid)
			{
				transparency = Mathf.SmoothDamp(transparency, targetTransparency, ref transparencyRampVelocity, TransparencyRampTime);
				if (Mathf.Approximately(transparency, 1f))
				{
					MakeSolid();
				}
				else
				{
					Mesh.material.SetColor("_Color", Color.Lerp(TransparentColor, Color.white, transparency));
				}
				targetTransparency = 1f;
			}
		}

		private void MakeSolid()
		{
			Mesh.sharedMaterial = SolidMaterial;
			transparency = 1f;
			isSolid = true;
		}

		private void MakeTransparent()
		{
			Mesh.material = TransparentMaterial;
			isSolid = false;
		}

		public void OnTriggerStay(Collider collider)
		{
			if (isSolid)
			{
				MakeTransparent();
			}
			targetTransparency = 0f;
		}
	}
}
