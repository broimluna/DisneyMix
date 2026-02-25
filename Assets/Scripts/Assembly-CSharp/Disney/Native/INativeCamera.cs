using LitJson;

namespace Disney.Native
{
	public interface INativeCamera
	{
		void SendPhoto(JsonData aJsonData);

		void SendVideo(JsonData aJsonData);

		void CameraError(CameraError aError);
	}
}
