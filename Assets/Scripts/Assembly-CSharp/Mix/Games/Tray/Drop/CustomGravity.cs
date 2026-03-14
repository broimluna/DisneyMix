using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	[RequireComponent(typeof(Rigidbody))]
	public class CustomGravity : MonoBehaviour
	{
		[Range(0f, 20f)]
		public float GravityModifier;

		private Rigidbody myRigidbody;

		public Vector3 Gravity
		{
			get
			{
				return Physics.gravity * GravityModifier;
			}
		}

		private void Awake()
		{
			myRigidbody = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			myRigidbody.AddForce(Physics.gravity * GravityModifier * myRigidbody.mass, ForceMode.Acceleration);
		}
	}
}
