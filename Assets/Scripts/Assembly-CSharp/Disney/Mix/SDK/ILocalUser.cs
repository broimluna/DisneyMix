using System;
using System.Collections;
using System.Collections.Generic;

namespace Disney.Mix.SDK
{
	public interface ILocalUser
	{
		IAvatar Avatar { get; }

		IDisplayName DisplayName { get; }

		string FirstName { get; }

		string Id { get; }

		string HashedId { get; }

		IEnumerable<IFriend> Friends { get; }

		IEnumerable<IIncomingFriendInvitation> IncomingFriendInvitations { get; }

		IEnumerable<IOutgoingFriendInvitation> OutgoingFriendInvitations { get; }

		IEnumerable<IOfficialAccount> Followships { get; }

		IEnumerable<IOfficialAccount> AllOfficialAccounts { get; }

		IEnumerable<IOneOnOneChatThread> OneOnOneChatThreads { get; }

		IEnumerable<IGroupChatThread> GroupChatThreads { get; }

		IEnumerable<IOfficialAccountChatThread> OfficialAccountChatThreads { get; }

		AgeBandType AgeBandType { get; }

		IRegistrationProfile RegistrationProfile { get; }

		IEnumerable<IAlert> Alerts { get; }

		event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged;

		event EventHandler<AbstractAddedToOneOnOneChatThreadEventArgs> OnAddedToOneOnOneChatThread;

		event EventHandler<AbstractAddedToGroupChatThreadEventArgs> OnAddedToGroupChatThread;

		event EventHandler<AbstractAddedToOfficialAccountThreadEventArgs> OnAddedToOfficialAccountChatThread;

		event EventHandler<AbstractReceivedOutgoingFriendInvitationEventArgs> OnReceivedOutgoingFriendInvitation;

		event EventHandler<AbstractReceivedIncomingFriendInvitationEventArgs> OnReceivedIncomingFriendInvitation;

		event EventHandler<AbstractUnfriendedEventArgs> OnUnfriended;

		event EventHandler<AbstractUntrustedEventArgs> OnUntrusted;

		event EventHandler<AbstractOfficialAccountFollowedEventArgs> OnOfficialAccountFollowed;

		event EventHandler<AbstractOfficialAccountUnfollowedEventArgs> OnOfficialAccountUnfollowed;

		event EventHandler<AbstractAlertsAddedEventArgs> OnAlertsAdded;

		event EventHandler<AbstractAlertsClearedEventArgs> OnAlertsCleared;

		event EventHandler<AbstractLegalMarketingUpdateRequiredEventArgs> OnLegalMarketingUpdateRequired;

		void FindUser(string displayName, Action<IFindUserResult> callback);

		IOutgoingFriendInvitation SendFriendInvitation(IUnidentifiedUser user, bool requestTrust, Action<ISendFriendInvitationResult> callback);

		IOutgoingFriendInvitation SendFriendInvitation(IRemoteChatMember user, bool requestTrust, Action<ISendFriendInvitationResult> callback);

		IOutgoingFriendInvitation SendFriendInvitation(IFriend user, bool requestTrust, Action<ISendFriendInvitationResult> callback);

		void AcceptFriendInvitation(IIncomingFriendInvitation invitation, bool acceptTrust, Action<IAcceptFriendInvitationResult> callback);

		void RejectFriendInvitation(IIncomingFriendInvitation invitation, Action<IRejectFriendInvitationResult> callback);

		void Unfriend(IFriend friend, Action<IUnfriendResult> callback);

		void Untrust(IFriend trustedUser, Action<IUntrustResult> callback);

		IUserNickname SetNickname(IFriend user, string nickname, Action<ISetUserNicknameResult> callback);

		void RemoveNickname(IFriend user, Action<IRemoveUserNicknameResult> callback);

		void GetRecommendedFriends(Action<IGetRecommendedFriendsResult> callback);

		void SetAvatar(IAvatar avatar, Action<ISetAvatarResult> callback);

		void CreateOneOnOneChatThread(IFriend member, Action<ICreateOneOnOneChatThreadResult> callback);

		void CreateGroupChatThread(IEnumerable<IFriend> members, Action<ICreateGroupChatThreadResult> callback);

		void GetRecentChatThreadMessages(Action<IGetRecentChatThreadMessagesResult> callback);

		void UpdateProfile(string firstName, string lastName, string displayName, string email, string parentEmail, DateTime? dateOfBirth, IEnumerable<KeyValuePair<IMarketingItem, bool>> marketingAgreements, IEnumerable<ILegalDocument> acceptedLegalDocuments, Action<IUpdateProfileResult> callback);

		void RefreshProfile(Action<IRefreshProfileResult> callback);

		void SendParentalApprovalEmail(Action<ISendParentalApprovalEmailResult> callback);

		void SendVerificationEmail(Action<ISendVerificationEmailResult> callback);

		void ValidateDisplayName(string displayName, Action<IValidateDisplayNameResult> callback);

		void UpdateDisplayName(string displayName, Action<IUpdateDisplayNameResult> callback);

		void AddChatThreadMembers(IGroupChatThread chatThread, IEnumerable<IFriend> members, Action<IAddChatThreadMemberResult> callback);

		void RemoveChatThreadMember(IGroupChatThread chatThread, IRemoteChatMember member, Action<IRemoveChatThreadMemberResult> callback);

		void RemoveChatThreadMember(IGroupChatThread chatThread, IFriend member, Action<IRemoveChatThreadMemberResult> callback);

		void RemoveChatThreadMember(IGroupChatThread chatThread, ILocalUser member, Action<IRemoveChatThreadMemberResult> callback);

		void ModerateText(string text, bool isTrusted, Action<ITextModerationResult> callback);

		void ReportUser(IRemoteChatMember user, ReportUserReason reason, Action<IReportUserResult> callback);

		void ReportUser(IFriend user, ReportUserReason reason, Action<IReportUserResult> callback);

		void EnableAllPushNotifications(string token, PushNotificationService service, string provisionId, Action<IEnableAllPushNotificationsResult> callback);

		void EnableInvisiblePushNotifications(string token, PushNotificationService service, string provisionId, Action<IEnableInvisiblePushNotificationsResult> callback);

		void DisableAllPushNotifications(Action<IDisableAllPushNotificationsResult> callback);

		void DisableVisiblePushNotifications(Action<IDisableVisiblePushNotificationsResult> callback);

		IPushNotification ReceivePushNotification(IDictionary notification);

		void TemporarilyBanAccount(Action<ITemporarilyBanAccountResult> callback);

		void SendMassPushNotification(Action<ISendMassPushNotificationResult> callback);

		void SendAlert(int level, AlertType type, Action<ISendAlertResult> callback);

		void GetAllOfficialAccounts(Action<IGetAllOfficialAccountsResult> callback);

		void FollowOfficialAccount(IOfficialAccount account, Action<IFollowOfficialAccountResult> callback);

		void UnfollowOfficialAccount(IOfficialAccount account, Action<IUnfollowOfficialAccountResult> callback);

		void ClearAlerts(IEnumerable<IAlert> alerts, Action<IClearAlertsResult> callback);

		void GetAdultVerificationRequirements(Action<IGetAdultVerificationRequirementsResult> callback);

		void GetAdultVerificationStatus(Action<IGetAdultVerificationStatusResult> callback);

		void GetVerifyAdultForm(Action<IGetVerifyAdultFormResult> callback);

		void VerifyAdult(IVerifyAdultFormUnitedStates form, Action<IVerifyAdultResult> callback);

		void AnswerVerifyAdultQuiz(IVerifyAdultQuizAnswers answers, Action<IVerifyAdultResult> callback);

		void GetClaimableChildren(Action<IGetLinkedUsersResult> callback);

		void LinkChildAccount(ISession child, Action<ILinkChildResult> callback);

		void LinkClaimableChildAccounts(IEnumerable<ILinkedUser> children, Action<ILinkChildResult> callback);

		void GetLinkedChildren(Action<IGetLinkedUsersResult> callback);

		void GetLinkedGuardians(Action<IGetLinkedUsersResult> callback);

		void ValidateDisplayNames(IEnumerable<string> displayNames, Action<IValidateDisplayNamesResult> callback);

		void RequestTrustPermission(Action<IPermissionResult> callback);

		void RequestTrustPermissionForChild(ILinkedUser child, Action<IPermissionResult> callback);

		void ApproveChildTrustPermission(ISession child, ActivityApprovalStatus status, Action<IPermissionResult> callback);

		void ApproveChildTrustPermission(ILinkedUser child, ActivityApprovalStatus status, Action<IPermissionResult> callback);

		void GetChildTrustPermission(ISession child, Action<IPermissionResult> callback);

		void GetChildTrustPermission(ILinkedUser child, Action<IPermissionResult> callback);

		void SetLanguagePreference(string languageCode, Action<ISetLangaugePreferenceResult> callback);
	}
}
