using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class AvatarSetter
	{
		public static void SetAvatar(AbstractLogger logger, INotificationQueue notificationQueue, IMixWebCallFactory mixWebCallFactory, IAvatar avatar, long avatarId, int slotId, Action successCallback, Action failureCallback)
		{
			try
			{
				Disney.Mix.SDK.Internal.MixDomain.Avatar avatar2 = new Disney.Mix.SDK.Internal.MixDomain.Avatar();
				avatar2.Accessory = CreateAvatarProperty(avatar.Accessory);
				avatar2.Brow = CreateAvatarProperty(avatar.Brow);
				avatar2.Costume = CreateAvatarProperty(avatar.Costume);
				avatar2.Eyes = CreateAvatarProperty(avatar.Eyes);
				avatar2.Hair = CreateAvatarProperty(avatar.Hair);
				avatar2.Mouth = CreateAvatarProperty(avatar.Mouth);
				avatar2.Nose = CreateAvatarProperty(avatar.Nose);
				avatar2.Skin = CreateAvatarProperty(avatar.Skin);
				avatar2.Hat = CreateAvatarProperty(avatar.Hat);
				avatar2.AvatarId = avatarId;
				avatar2.SlotId = slotId;
				Disney.Mix.SDK.Internal.MixDomain.Avatar avatar3 = avatar2;
				SetAvatarRequest setAvatarRequest = new SetAvatarRequest();
				setAvatarRequest.Avatar = avatar3;
				SetAvatarRequest request = setAvatarRequest;
				IWebCall<SetAvatarRequest, SetAvatarResponse> webCall = mixWebCallFactory.AvatarPut(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<SetAvatarResponse> e)
				{
					SetAvatarResponse response = e.Response;
					if (NotificationValidator.Validate(response.Notification))
					{
						notificationQueue.Dispatch(response.Notification, successCallback, failureCallback);
					}
					else
					{
						failureCallback();
					}
				};
				webCall.OnError += delegate
				{
					failureCallback();
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback();
			}
		}

		private static Disney.Mix.SDK.Internal.MixDomain.AvatarProperty CreateAvatarProperty(IAvatarProperty inProperty)
		{
			Disney.Mix.SDK.Internal.MixDomain.AvatarProperty avatarProperty = new Disney.Mix.SDK.Internal.MixDomain.AvatarProperty();
			avatarProperty.SelectionKey = inProperty.SelectionKey;
			avatarProperty.TintIndex = inProperty.TintIndex;
			avatarProperty.XOffset = inProperty.XOffset;
			avatarProperty.YOffset = inProperty.YOffset;
			return avatarProperty;
		}
	}
}
