using UnityEngine;

namespace Mix.Assets.Worker
{
	public interface IByteToPNG
	{
		void OnByteToPNG(Texture2D aTexture2D, object aUserData);
	}
}
