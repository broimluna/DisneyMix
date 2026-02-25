using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class MatchCamera : MonoBehaviour
	{
		public Camera targetCamera;

		private Camera m_camera;

		private void Start()
		{
			m_camera = GetComponent<Camera>();
			MatchTargetCamera();
		}

		private void MatchTargetCamera()
		{
			if ((bool)targetCamera)
			{
				m_camera.projectionMatrix = targetCamera.projectionMatrix;
				m_camera.nearClipPlane = targetCamera.nearClipPlane;
				m_camera.farClipPlane = targetCamera.farClipPlane;
				m_camera.fieldOfView = targetCamera.fieldOfView;
				m_camera.rect = targetCamera.rect;
			}
		}
	}
}
