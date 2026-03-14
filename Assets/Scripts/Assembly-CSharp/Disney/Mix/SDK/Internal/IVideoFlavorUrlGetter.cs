using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public interface IVideoFlavorUrlGetter
	{
		void Get(string videoId, string videoFlavorId, IMixWebCallFactory mixWebCallFactory, Action<GetVideoUrlResponse> successCallback, Action failureCallback);
	}
}
