using UnityEngine;

namespace Mix.Games.Tray.SecretWord
{
	public class TileColliderHelper : MonoBehaviour
	{
		private Collider m_bestTouchingSlotCollider;

		private Collider m_previousTouchingSlotCollider;

		private Transform _transform;

		public TileSlot GetBestTouchingSlot()
		{
			if (m_bestTouchingSlotCollider != null)
			{
				return m_bestTouchingSlotCollider.GetComponent<TileSlot>();
			}
			return null;
		}

		public TileSlot GetPreviousTouchingSlot()
		{
			if (m_previousTouchingSlotCollider != null)
			{
				return m_previousTouchingSlotCollider.GetComponent<TileSlot>();
			}
			return null;
		}

		private void Awake()
		{
			_transform = base.transform;
		}

		private void OnTriggerStay(Collider collider)
		{
			if (m_bestTouchingSlotCollider == null)
			{
				m_bestTouchingSlotCollider = collider;
			}
			else if (collider != m_bestTouchingSlotCollider)
			{
				Vector3 vector = _transform.position - m_bestTouchingSlotCollider.transform.position;
				if ((_transform.position - collider.transform.position).sqrMagnitude < vector.sqrMagnitude)
				{
					m_bestTouchingSlotCollider = collider;
				}
			}
		}

		private void OnTriggerExit(Collider collider)
		{
			if (collider == m_bestTouchingSlotCollider)
			{
				m_previousTouchingSlotCollider = collider;
				m_bestTouchingSlotCollider = null;
			}
		}
	}
}
