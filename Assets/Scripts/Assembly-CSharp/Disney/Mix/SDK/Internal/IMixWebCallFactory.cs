using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public interface IMixWebCallFactory : IDisposable
	{
		string GuestControllerAccessToken { get; set; }

		event EventHandler<AbstractAuthenticationLostEventArgs> OnAuthenticationLost;

		IWebCall<GetUsersByDisplayNameRequest, GetUsersResponse> UsersByDisplayNamePost(GetUsersByDisplayNameRequest request);

		IWebCall<GetUsersByUserIdRequest, GetUsersResponse> UsersByUserIdPost(GetUsersByUserIdRequest request);

		IWebCall<ClearAlertsRequest, ClearAlertsResponse> AlertsClearPut(ClearAlertsRequest request);

		IWebCall<SetAvatarRequest, SetAvatarResponse> AvatarPut(SetAvatarRequest request);

		IWebCall<AddChatThreadRequest, AddChatThreadResponse> ChatThreadPut(AddChatThreadRequest request);

		IWebCall<GetChatThreadMessagesRequest, GetChatThreadMessagesResponse> ChatThreadPost(GetChatThreadMessagesRequest request);

		IWebCall<AddChatThreadGagMessageRequest, AddChatThreadGagMessageResponse> ChatThreadGagMessagePut(AddChatThreadGagMessageRequest request);

		IWebCall<AddChatThreadGameStateMessageRequest, AddChatThreadGameStateMessageResponse> ChatThreadGameStateMessagePut(AddChatThreadGameStateMessageRequest request);

		IWebCall<UpdateChatThreadGameStateMessageRequest, UpdateChatThreadGameStateMessageResponse> ChatThreadGameStateMessagePost(UpdateChatThreadGameStateMessageRequest request);

		IWebCall<GetChatThreadMessagesBySequenceRangesRequest, GetChatThreadMessagesResponse> ChatThreadMessagesBySequenceRangePost(GetChatThreadMessagesBySequenceRangesRequest request);

		IWebCall<GetChatThreadsRecentMessagesRequest, GetChatThreadMessagesResponse> ChatThreadMessagesRecentPost(GetChatThreadsRecentMessagesRequest request);

		IWebCall<GetChatThreadSequenceNumberRequest, GetChatThreadSequenceNumberResponse> ChatThreadMessagesSequenceNumberPost(GetChatThreadSequenceNumberRequest request);

		IWebCall<AddChatThreadNicknameRequest, AddChatThreadNicknameResponse> ChatThreadNicknamePut(AddChatThreadNicknameRequest request);

		IWebCall<RemoveChatThreadNicknameRequest, RemoveChatThreadNicknameResponse> ChatThreadNicknameDeletePost(RemoveChatThreadNicknameRequest request);

		IWebCall<AddChatThreadPhotoMessageRequest, AddChatThreadPhotoMessageResponse> ChatThreadPhotoMessagePut(AddChatThreadPhotoMessageRequest request);

		IWebCall<GetRecentChatThreadMessagesRequest, GetRecentChatThreadMessagesResponse> ChatThreadRecentMessagesPost(GetRecentChatThreadMessagesRequest request);

		IWebCall<GetSpecificChatThreadMessagesRequest, GetRecentChatThreadMessagesResponse> ChatThreadSpecificMessagesPost(GetSpecificChatThreadMessagesRequest request);

		IWebCall<AddChatThreadStickerMessageRequest, AddChatThreadStickerMessageResponse> ChatThreadStickerMessagePut(AddChatThreadStickerMessageRequest request);

		IWebCall<AddChatThreadTextMessageRequest, AddChatThreadTextMessageResponse> ChatThreadTextMessagePut(AddChatThreadTextMessageRequest request);

		IWebCall<AddChatThreadVideoMessageRequest, AddChatThreadVideoMessageResponse> ChatThreadVideoMessagePut(AddChatThreadVideoMessageRequest request);

		IWebCall<ClearMemberChatHistoryRequest, ClearMemberChatHistoryResponse> ChatThreadMembershipHistoryDeletePost(ClearMemberChatHistoryRequest request);

		IWebCall<ClearUnreadMessageCountRequest, ClearUnreadMessageCountResponse> ChatThreadMembershipUnreadMessageCountDeletePost(ClearUnreadMessageCountRequest request);

		IWebCall<AddChatThreadMembershipRequest, AddChatThreadMembershipResponse> ChatThreadMembershipPut(AddChatThreadMembershipRequest request);

		IWebCall<RemoveChatThreadMembershipRequest, RemoveChatThreadMembershipResponse> ChatThreadMembershipDeletePost(RemoveChatThreadMembershipRequest request);

		IWebCall<SetDisplayNameRequest, SetDisplayNameResponse> DisplaynamePut(SetDisplayNameRequest request);

		IWebCall<ValidateDisplayNamesRequest, ValidateDisplayNamesResponse> DisplaynameValidatePost(ValidateDisplayNamesRequest request);

		IWebCall<AddFriendshipRequest, AddFriendshipResponse> FriendshipPut(AddFriendshipRequest request);

		IWebCall<RemoveFriendshipRequest, RemoveFriendshipResponse> FriendshipDeletePost(RemoveFriendshipRequest request);

		IWebCall<BaseUserRequest, GetFriendshipRecommendationResponse> FriendshipRecommendPost(BaseUserRequest request);

		IWebCall<AddFriendshipInvitationRequest, AddFriendshipInvitationResponse> FriendshipInvitationPut(AddFriendshipInvitationRequest request);

		IWebCall<RemoveFriendshipInvitationRequest, RemoveFriendshipInvitationResponse> FriendshipInvitationDeletePost(RemoveFriendshipInvitationRequest request);

		IWebCall<BaseUserRequest, GetGeolocationResponse> GeolocationPost(BaseUserRequest request);

		IWebCall<BaseUserRequest, BaseResponse> IntegrationTestSupportModerationTempBanPut(BaseUserRequest request);

		IWebCall<OfficialAccountPublishPhotoTestRequest, BaseResponse> IntegrationTestSupportOfficialAccountPublishPhotoPost(OfficialAccountPublishPhotoTestRequest request);

		IWebCall<OfficialAccountPublishTextTestRequest, BaseResponse> IntegrationTestSupportOfficialAccountPublishTextPost(OfficialAccountPublishTextTestRequest request);

		IWebCall<OfficialAccountPublishVideoTestRequest, BaseResponse> IntegrationTestSupportOfficialAccountPublishVideoPost(OfficialAccountPublishVideoTestRequest request);

		IWebCall<BaseUserRequest, BaseResponse> IntegrationTestSupportPushNotificationMassSendPost(BaseUserRequest request);

		IWebCall<TriggerAlertRequest, BaseResponse> IntegrationTestSupportUserAlertPost(TriggerAlertRequest request);

		IWebCall<BaseUserRequest, BaseResponse> IntegrationTestSupportUserSessionExpirePost(BaseUserRequest request);

		IWebCall<SetLanguageRequest, BaseResponse> LanguagePreferencePost(SetLanguageRequest request);

		IWebCall<ReportPlayerRequest, BaseResponse> ModerationReportPlayerPut(ReportPlayerRequest request);

		IWebCall<ModerateTextRequest, ModerateTextResponse> ModerationTextPut(ModerateTextRequest request);

		IWebCall<AddNicknameRequest, AddNicknameResponse> NicknamePut(AddNicknameRequest request);

		IWebCall<RemoveNicknameRequest, RemoveNicknameResponse> NicknameDeletePost(RemoveNicknameRequest request);

		IWebCall<GetNotificationsRequest, GetNotificationsResponse> NotificationsPost(GetNotificationsRequest request);

		IWebCall<GetNotificationsSinceSequenceRequest, GetNotificationsResponse> NotificationsSinceSequencePost(GetNotificationsSinceSequenceRequest request);

		IWebCall<AddOfficialAccountFollowshipRequest, AddOfficialAccountFollowshipResponse> OfficialAccountAddPut(AddOfficialAccountFollowshipRequest request);

		IWebCall<BaseUserRequest, OfficialAccountsResponse> OfficialAccountAllPost(BaseUserRequest request);

		IWebCall<RemoveOfficialAccountFollowshipRequest, RemoveOfficialAccountFollowshipResponse> OfficialAccountRemovePost(RemoveOfficialAccountFollowshipRequest request);

		IWebCall<PilCheckRequest, PilCheckResponse> PilCheckPost(PilCheckRequest request);

		IWebCall<SetPresenceRequest, BaseResponse> PresencePut(SetPresenceRequest request);

		IWebCall<BaseUserRequest, PostPresenceResponse> PresencePost(BaseUserRequest request);

		IWebCall<TogglePushNotificationRequest, BaseResponse> PushNotificationsSettingPost(TogglePushNotificationRequest request);

		IWebCall<BaseUserRequest, BaseResponse> PushNotificationsSettingDeletePost(BaseUserRequest request);

		IWebCall<GetRegistrationTextRequest, GetRegistrationTextResponse> RegistrationTextPost(GetRegistrationTextRequest request);

		IWebCall<DisplayNameSearchRequest, DisplayNameSearchResponse> SearchDisplaynamePost(DisplayNameSearchRequest request);

		IWebCall<StartUserSessionRequest, StartUserSessionResponse> SessionUserPut(StartUserSessionRequest request);

		IWebCall<BaseUserRequest, BaseResponse> SessionUserDeletePost(BaseUserRequest request);

		IWebCall<GetStateRequest, GetStateResponse> StatePost(GetStateRequest request);

		IWebCall<RemoveFriendshipTrustRequest, RemoveFriendshipTrustResponse> FriendshipTrustDeletePost(RemoveFriendshipTrustRequest request);

		IWebCall<GetVideoUrlRequest, GetVideoUrlResponse> VideoUrlPost(GetVideoUrlRequest request);
	}
}
