using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class ChatMessageSender : IChatMessageSender
	{
		private static class RequestSender<TRequest, TResponse> where TRequest : BaseUserRequest where TResponse : BaseResponse, new()
		{
			public static void Send(IWebCall<TRequest, TResponse> request, Func<TResponse, bool> validator, Func<TResponse, IEnumerable<BaseNotification>> notificationGetter, INotificationQueue notificationQueue, AbstractLogger logger, Action<TResponse> successCallback, Action<string> failureCallback)
			{
				request.OnResponse += delegate(object sender, WebCallEventArgs<TResponse> e)
				{
					TResponse response = e.Response;
					if (validator(response))
					{
						IEnumerable<BaseNotification> notifications = notificationGetter(response);
						notificationQueue.Dispatch(notifications, delegate
						{
							successCallback(response);
						}, delegate
						{
							failureCallback("Couldn't dispatch");
						});
					}
					else
					{
						logger.Critical("Failed to validate response: " + JsonParser.ToJson(response));
						failureCallback(response.Status);
					}
				};
				request.OnError += delegate(object sender, WebCallErrorEventArgs e)
				{
					failureCallback(e.Status);
				};
				request.Execute();
			}
		}

		private readonly AbstractLogger logger;

		private readonly IRandom random;

		private readonly INotificationQueue notificationQueue;

		public ChatMessageSender(AbstractLogger logger, IRandom random, INotificationQueue notificationQueue)
		{
			this.logger = logger;
			this.random = random;
			this.notificationQueue = notificationQueue;
		}

		public void SendTextMessage(IMixWebCallFactory mixWebCallFactory, IInternalTextMessage textMessage, long chatThreadId, Action<AddChatThreadTextMessageNotification> successCallback, Action<string> failureCallback)
		{
			try
			{
				AddChatThreadTextMessageRequest addChatThreadTextMessageRequest = new AddChatThreadTextMessageRequest();
				addChatThreadTextMessageRequest.ChatThreadId = chatThreadId;
				addChatThreadTextMessageRequest.ClientChatMessageId = textMessage.LocalChatMessageId;
				addChatThreadTextMessageRequest.Text = textMessage.Text;
				AddChatThreadTextMessageRequest request = addChatThreadTextMessageRequest;
				IWebCall<AddChatThreadTextMessageRequest, AddChatThreadTextMessageResponse> request2 = mixWebCallFactory.ChatThreadTextMessagePut(request);
				RequestSender<AddChatThreadTextMessageRequest, AddChatThreadTextMessageResponse>.Send(request2, delegate(AddChatThreadTextMessageResponse r)
				{
					bool flag = NotificationValidator.Validate(r.Notification);
					if (flag)
					{
						textMessage.Text = r.Notification.Message.Text;
					}
					return flag;
				}, (AddChatThreadTextMessageResponse r) => new AddChatThreadTextMessageNotification[1] { r.Notification }, notificationQueue, logger, delegate(AddChatThreadTextMessageResponse response)
				{
					successCallback(response.Notification);
				}, failureCallback);
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback(null);
			}
		}

		public void ModerateTextMessage(IMixWebCallFactory mixWebCallFactory, string text, bool isTrusted, long chatThreadId, Action<ModerateTextResponse> successCallback, Action<string> failureCallback)
		{
			try
			{
				ModerateTextRequest moderateTextRequest = new ModerateTextRequest();
				moderateTextRequest.Text = text;
				moderateTextRequest.ModerationPolicy = ((!isTrusted) ? "UnTrusted" : "Trusted");
				moderateTextRequest.ChatThreadId = chatThreadId;
				ModerateTextRequest request = moderateTextRequest;
				IWebCall<ModerateTextRequest, ModerateTextResponse> request2 = mixWebCallFactory.ModerationTextPut(request);
				RequestSender<ModerateTextRequest, ModerateTextResponse>.Send(request2, delegate(ModerateTextResponse r)
				{
					bool? moderated = r.Moderated;
					return moderated.HasValue && r.Text != null;
				}, (ModerateTextResponse r) => Enumerable.Empty<BaseNotification>(), notificationQueue, logger, successCallback, failureCallback);
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback(null);
			}
		}

		public void SendStickerMessage(IMixWebCallFactory mixWebCallFactory, string contentId, long chatThreadId, long? localMessageId, Action<AddChatThreadStickerMessageNotification> successCallback, Action<string> failureCallback)
		{
			try
			{
				AddChatThreadStickerMessageRequest addChatThreadStickerMessageRequest = new AddChatThreadStickerMessageRequest();
				addChatThreadStickerMessageRequest.ChatThreadId = chatThreadId;
				addChatThreadStickerMessageRequest.ClientChatMessageId = localMessageId;
				addChatThreadStickerMessageRequest.ContentId = contentId;
				AddChatThreadStickerMessageRequest request = addChatThreadStickerMessageRequest;
				IWebCall<AddChatThreadStickerMessageRequest, AddChatThreadStickerMessageResponse> request2 = mixWebCallFactory.ChatThreadStickerMessagePut(request);
				RequestSender<AddChatThreadStickerMessageRequest, AddChatThreadStickerMessageResponse>.Send(request2, (AddChatThreadStickerMessageResponse r) => NotificationValidator.Validate(r.Notification), (AddChatThreadStickerMessageResponse r) => new AddChatThreadStickerMessageNotification[1] { r.Notification }, notificationQueue, logger, delegate(AddChatThreadStickerMessageResponse response)
				{
					successCallback(response.Notification);
				}, failureCallback);
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback(null);
			}
		}

		public void SendGagMessage(IMixWebCallFactory mixWebCallFactory, string contentId, string targetUserId, long chatThreadId, long? localMessageId, Action<AddChatThreadGagMessageNotification> successCallback, Action<string> failureCallback)
		{
			try
			{
				AddChatThreadGagMessageRequest addChatThreadGagMessageRequest = new AddChatThreadGagMessageRequest();
				addChatThreadGagMessageRequest.ChatThreadId = chatThreadId;
				addChatThreadGagMessageRequest.ClientChatMessageId = localMessageId;
				addChatThreadGagMessageRequest.ContentId = contentId;
				addChatThreadGagMessageRequest.TargetUserId = targetUserId;
				AddChatThreadGagMessageRequest request = addChatThreadGagMessageRequest;
				IWebCall<AddChatThreadGagMessageRequest, AddChatThreadGagMessageResponse> request2 = mixWebCallFactory.ChatThreadGagMessagePut(request);
				RequestSender<AddChatThreadGagMessageRequest, AddChatThreadGagMessageResponse>.Send(request2, (AddChatThreadGagMessageResponse r) => NotificationValidator.Validate(r.Notification), (AddChatThreadGagMessageResponse r) => new AddChatThreadGagMessageNotification[1] { r.Notification }, notificationQueue, logger, delegate(AddChatThreadGagMessageResponse response)
				{
					successCallback(response.Notification);
				}, failureCallback);
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback(null);
			}
		}

		public void SendPhotoMessage(IMixWebCallFactory mixWebCallFactory, string ugcMediaId, long chatThreadId, long? localMessageId, Action<AddChatThreadPhotoMessageNotification> successCallback, Action<string> failureCallback)
		{
			try
			{
				AddChatThreadPhotoMessageRequest addChatThreadPhotoMessageRequest = new AddChatThreadPhotoMessageRequest();
				addChatThreadPhotoMessageRequest.ChatThreadId = chatThreadId;
				addChatThreadPhotoMessageRequest.ClientChatMessageId = localMessageId;
				addChatThreadPhotoMessageRequest.UgcMediaId = ugcMediaId;
				AddChatThreadPhotoMessageRequest request = addChatThreadPhotoMessageRequest;
				IWebCall<AddChatThreadPhotoMessageRequest, AddChatThreadPhotoMessageResponse> request2 = mixWebCallFactory.ChatThreadPhotoMessagePut(request);
				RequestSender<AddChatThreadPhotoMessageRequest, AddChatThreadPhotoMessageResponse>.Send(request2, (AddChatThreadPhotoMessageResponse r) => NotificationValidator.Validate(r.Notification), (AddChatThreadPhotoMessageResponse r) => new AddChatThreadPhotoMessageNotification[1] { r.Notification }, notificationQueue, logger, delegate(AddChatThreadPhotoMessageResponse response)
				{
					successCallback(response.Notification);
				}, failureCallback);
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback(null);
			}
		}

		public void SendVideoMessage(IMixWebCallFactory mixWebCallFactory, string ugcMediaId, long chatThreadId, long? localMessageId, Action<AddChatThreadVideoMessageNotification> successCallback, Action<string> failureCallback)
		{
			try
			{
				AddChatThreadVideoMessageRequest addChatThreadVideoMessageRequest = new AddChatThreadVideoMessageRequest();
				addChatThreadVideoMessageRequest.ChatThreadId = chatThreadId;
				addChatThreadVideoMessageRequest.ClientChatMessageId = localMessageId;
				addChatThreadVideoMessageRequest.UgcMediaId = ugcMediaId;
				AddChatThreadVideoMessageRequest request = addChatThreadVideoMessageRequest;
				IWebCall<AddChatThreadVideoMessageRequest, AddChatThreadVideoMessageResponse> request2 = mixWebCallFactory.ChatThreadVideoMessagePut(request);
				RequestSender<AddChatThreadVideoMessageRequest, AddChatThreadVideoMessageResponse>.Send(request2, (AddChatThreadVideoMessageResponse r) => NotificationValidator.Validate(r.Notification), (AddChatThreadVideoMessageResponse r) => new AddChatThreadVideoMessageNotification[1] { r.Notification }, notificationQueue, logger, delegate(AddChatThreadVideoMessageResponse response)
				{
					successCallback(response.Notification);
				}, failureCallback);
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback(null);
			}
		}

		public void SendGameStateMessage(IMixWebCallFactory mixWebCallFactory, string gameName, Dictionary<string, object> payload, long chatThreadId, long? localMessageId, Action<AddChatThreadGameStateMessageNotification> successCallback, Action<string> failureCallback)
		{
			try
			{
				AddChatThreadGameStateMessageRequest addChatThreadGameStateMessageRequest = new AddChatThreadGameStateMessageRequest();
				addChatThreadGameStateMessageRequest.GameName = gameName;
				addChatThreadGameStateMessageRequest.ChatThreadId = chatThreadId;
				addChatThreadGameStateMessageRequest.ClientChatMessageId = localMessageId;
				addChatThreadGameStateMessageRequest.Payload = JsonParser.ToJson(payload);
				AddChatThreadGameStateMessageRequest request = addChatThreadGameStateMessageRequest;
				IWebCall<AddChatThreadGameStateMessageRequest, AddChatThreadGameStateMessageResponse> request2 = mixWebCallFactory.ChatThreadGameStateMessagePut(request);
				RequestSender<AddChatThreadGameStateMessageRequest, AddChatThreadGameStateMessageResponse>.Send(request2, (AddChatThreadGameStateMessageResponse r) => NotificationValidator.Validate(r.Notification), (AddChatThreadGameStateMessageResponse r) => new AddChatThreadGameStateMessageNotification[1] { r.Notification }, notificationQueue, logger, delegate(AddChatThreadGameStateMessageResponse response)
				{
					successCallback(response.Notification);
				}, failureCallback);
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback(null);
			}
		}

		public void UpdateGameStateMessage(IMixWebCallFactory mixWebCallFactory, long gameStateMessageId, Dictionary<string, object> payload, long chatThreadId, Action<UpdateChatThreadGameStateMessageResponse> successCallback, Action<string> failureCallback)
		{
			try
			{
				UpdateChatThreadGameStateMessageRequest updateChatThreadGameStateMessageRequest = new UpdateChatThreadGameStateMessageRequest();
				updateChatThreadGameStateMessageRequest.ChatThreadId = chatThreadId;
				updateChatThreadGameStateMessageRequest.GameStateMessageId = gameStateMessageId;
				updateChatThreadGameStateMessageRequest.Payload = JsonParser.ToJson(payload);
				updateChatThreadGameStateMessageRequest.ClientChatMessageId = random.NextLong();
				UpdateChatThreadGameStateMessageRequest request = updateChatThreadGameStateMessageRequest;
				IWebCall<UpdateChatThreadGameStateMessageRequest, UpdateChatThreadGameStateMessageResponse> request2 = mixWebCallFactory.ChatThreadGameStateMessagePost(request);
				RequestSender<UpdateChatThreadGameStateMessageRequest, UpdateChatThreadGameStateMessageResponse>.Send(request2, (UpdateChatThreadGameStateMessageResponse r) => NotificationValidator.Validate(r.UpdateGameStateNotification) && r.GameEventNotifications.All(NotificationValidator.Validate), (UpdateChatThreadGameStateMessageResponse r) => r.GameEventNotifications.Cast<BaseNotification>().Concat(new BaseNotification[1] { r.UpdateGameStateNotification }), notificationQueue, logger, successCallback, failureCallback);
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback(null);
			}
		}
	}
}
