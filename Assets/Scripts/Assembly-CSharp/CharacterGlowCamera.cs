using UnityEngine;

public class CharacterGlowCamera : MonoBehaviour
{
	public Camera glowCamera;

	public GameObject imageQuad;

	public void Awake()
	{
	}

	public void Mirror(Camera referenceCamera)
	{
		MirrorCameraSettings(referenceCamera, glowCamera);
	}

	private void MirrorCameraSettings(Camera fromCamera, Camera toCamera)
	{
		if (fromCamera != null && toCamera != null)
		{
			toCamera.transform.position = fromCamera.transform.position;
			toCamera.transform.rotation = fromCamera.transform.rotation;
			toCamera.orthographic = fromCamera.orthographic;
			toCamera.fieldOfView = fromCamera.fieldOfView;
			toCamera.orthographicSize = fromCamera.orthographicSize;
			toCamera.nearClipPlane = fromCamera.nearClipPlane;
			toCamera.farClipPlane = fromCamera.farClipPlane;
		}
	}
}
