using System;
using System.Collections;
using System.Collections.Generic;
using Avatar;
using Avatar.DataTypes;
using Disney.Mix.SDK;
using Mix.Avatar;
using Mix.AvatarInternal;
using Mix.Data;
using Mix.Entitlements;
using Mix.Session;
using Mix.Tracking;
using Mix.User;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class AvatarEditorController : BaseController, IAvatarColorListener, ISpinnerListener, IMediaTray, IDisplayNamePanelCallback
	{
		public enum EDITOR_MODES
		{
			EDITOR = 0,
			CREATOR = 1
		}

		private const int MAX_NUM_RETRIES = 2;

		private const string avatarCreatedEvent = "UI/Registration/NextPage/AfterCreatingAvatar";

		private const string avatarSavedEvent = "MainApp/UI/SFX_2_SendDisney";

		private string lastCategory = string.Empty;

		private bool avatarCompositing;

		private bool selectionPending;

		private bool IgnoreColorAudio;

		private EDITOR_MODES mode = EDITOR_MODES.CREATOR;

		private string previousScreen = string.Empty;

		private IChatThread previousThread;

		public ClientAvatar avatarDna;

		private string item = "0";

		private int tintIndex;

		private bool showDisplayNamePanel;

		private int retryAttempts;

		public GameObject resetButton;

		public GameObject colorBar;

		public GameObject ProgressBar;

		public GameObject MixelDust;

		public GameObject removeButton;

		public Image backgroundImage;

		public Text categoryTitle;

		public Button randomButton;

		public Button doneButton;

		public AvatarObjectSpawner AvatarSpawner;

		public AvatarMediaTray mediaTray;

		public AvatarColorPicker ColorPicker;

		private bool startingUp = true;

		private float lockUi = -1f;

		public float SliderUpdateDelay = 0.2f;

		public Slider horizontalSlider;

		public Slider verticalSlider;

		private AvatarSpinner avatarSpinner;

		private IRegistrationProfile profileInfo;

		private AvatarFlags flags = AvatarFlags.WithoutCaching;

		private bool isDirty;

		private bool IsDirty
		{
			get
			{
				return isDirty;
			}
			set
			{
				isDirty = value;
				if (mode != EDITOR_MODES.CREATOR)
				{
					resetButton.SetActive(isDirty);
				}
			}
		}

		void IAvatarColorListener.OnColorClicked(int index)
		{
			if (!(lockUi >= 0f))
			{
				tintIndex = index;
				if (IgnoreColorAudio)
				{
					IgnoreColorAudio = false;
					return;
				}
				Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/AvatarCreateTopLevel");
				recompositeAvatar();
			}
		}

		private void Awake()
		{
			GameObject avatarObject = AvatarSpawner.Init();
			avatarSpinner = GetComponent<AvatarSpinner>();
			if (avatarSpinner == null)
			{
			}
			avatarSpinner.InitSpinner(avatarObject);
			avatarSpinner.SetListener(this);
			if (AvatarApi.ValidateAvatar(MonoSingleton<AvatarManager>.Instance.CurrentDna) && MonoSingleton<AvatarManager>.Instance.api.IsAvatarMultiplane(MonoSingleton<AvatarManager>.Instance.CurrentDna))
			{
				avatarDna = CloneAvatar(MonoSingleton<AvatarManager>.Instance.CurrentDna);
				if (avatarSpinner != null && avatarSpinner.Avatar != null)
				{
					avatarSpinner.Avatar.SetActive(false);
					avatarSpinner.LoadDnaOnAvatar(avatarDna, flags, HandleRegularAvatarLoaded);
				}
			}
			else if (avatarSpinner != null && avatarSpinner.Avatar != null)
			{
				avatarSpinner.Avatar.SetActive(false);
			}
			ProgressBar.GetComponent<Animator>().Play("ProgressBar_2-3");
		}

		private ClientAvatar CloneAvatar(IAvatar toClone)
		{
			ClientAvatar clientAvatar = new ClientAvatar();
			clientAvatar.Accessory = CloneProperty(toClone.Accessory);
			clientAvatar.Brow = CloneProperty(toClone.Brow);
			clientAvatar.Costume = CloneProperty(toClone.Costume);
			clientAvatar.Eyes = CloneProperty(toClone.Eyes);
			clientAvatar.Hair = CloneProperty(toClone.Hair);
			clientAvatar.Mouth = CloneProperty(toClone.Mouth);
			clientAvatar.Nose = CloneProperty(toClone.Nose);
			clientAvatar.Skin = CloneProperty(toClone.Skin);
			clientAvatar.Hat = CloneProperty(toClone.Hat);
			return clientAvatar;
		}

		private ClientAvatarProperty CloneProperty(IAvatarProperty toClone)
		{
			ClientAvatarProperty clientAvatarProperty = new ClientAvatarProperty();
			if (toClone == null)
			{
				return clientAvatarProperty;
			}
			clientAvatarProperty.SelectionKey = toClone.SelectionKey;
			clientAvatarProperty.TintIndex = toClone.TintIndex;
			clientAvatarProperty.XOffset = toClone.XOffset;
			clientAvatarProperty.YOffset = toClone.YOffset;
			return clientAvatarProperty;
		}

		public override void OnDataReceived(string aToken, object aData)
		{
			switch (aToken)
			{
			case "mode":
				mode = (EDITOR_MODES)(int)aData;
				doneButton.gameObject.SetActive(mode != EDITOR_MODES.CREATOR);
				break;
			case "return":
				previousScreen = (string)aData;
				break;
			case "returnThread:":
				previousThread = (IChatThread)aData;
				break;
			case "missingInfo":
				mode = EDITOR_MODES.CREATOR;
				profileInfo = (IRegistrationProfile)aData;
				break;
			case "missingDisplayName":
				showDisplayNamePanel = true;
				break;
			}
		}

		public override void OnUITransitionEnd()
		{
			if (mode == EDITOR_MODES.CREATOR)
			{
				Analytics.LogCreateAvatarPageView();
			}
			else
			{
				Analytics.LogAvatarEditorPageView();
			}
			if (!AvatarApi.ValidateAvatar(MonoSingleton<AvatarManager>.Instance.CurrentDna) || !MonoSingleton<AvatarManager>.Instance.api.IsAvatarMultiplane(MonoSingleton<AvatarManager>.Instance.CurrentDna))
			{
				avatarDna = CloneAvatar(MonoSingleton<AvatarManager>.Instance.GenerateRandomDna());
				avatarSpinner.SetState(AvatarSpinner.SpinState.SPINNING_UP);
				lockUi = 0f;
			}
			if (showDisplayNamePanel)
			{
				handleDisplayNameStatus();
			}
			backgroundImage.enabled = false;
			mediaTray.Init(this);
			mediaTray.categorySelector.Setup(mediaTray);
		}

		private void OnDestroy()
		{
		}

		public override void OnAndroidBackButtonClicked()
		{
			if (base.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AvatarEditorScreen_OnRegComplete"))
			{
				GameObject.Find("Overlay_AvatarEditorScreen/DoneBtn").GetComponent<Button>().onClick.Invoke();
				return;
			}
			if (!Singleton<PanelManager>.Instance.FindPanel(typeof(DisplayNamePanel)) && mode != EDITOR_MODES.CREATOR)
			{
				OnBackBtnClicked();
				return;
			}
			BasePanel panelWithId = Singleton<PanelManager>.Instance.GetPanelWithId(10);
			if (panelWithId != null)
			{
				panelWithId.ClosePanel();
				return;
			}
			GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC, true, 10);
			Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string>
			{
				{ genericPanel.TitleText, "customtokens.panels.leaving_app" },
				{ genericPanel.MessageText, null },
				{ genericPanel.ButtonOneText, "customtokens.login.logout_yes" },
				{ genericPanel.ButtonTwoText, "customtokens.login.logout_no" }
			});
			Singleton<PanelManager>.Instance.SetupButtons(new Dictionary<Button, Action> { 
			{
				genericPanel.ButtonOne,
				Application.Quit
			} });
		}

		public void OnStopSpinning()
		{
		}

		public void AtRest()
		{
		}

		public void AtMaxSpin()
		{
			retryAttempts = 0;
			LoadRandomAvatar();
			avatarSpinner.LoadDnaOnAvatar(avatarDna, flags, HandleRandomAvatarLoaded);
		}

		public void HandleRegularAvatarLoaded(bool success, string dnaSha)
		{
			HandleAvatarLoaded(success, dnaSha, false);
		}

		public void HandleRandomAvatarLoaded(bool success, string dnaSha)
		{
			HandleAvatarLoaded(success, dnaSha, true);
		}

		public void HandleAvatarLoaded(bool success, string dnaSha, bool random)
		{
			if (this.IsNullOrDisposed() || avatarSpinner.IsNullOrDisposed())
			{
				return;
			}
			if (success)
			{
				avatarSpinner.HitBrakes();
				if (avatarSpinner != null && avatarSpinner.Avatar != null)
				{
					avatarSpinner.Avatar.SetActive(true);
				}
				lockUi = -1f;
				avatarCompositing = false;
				if (startingUp)
				{
					startingUp = false;
					selectionPending = false;
				}
				else
				{
					setDefaultsUp();
				}
			}
			else if (retryAttempts < 2)
			{
				retryAttempts++;
				if (random)
				{
					LoadRandomAvatar();
					avatarSpinner.LoadDnaOnAvatar(avatarDna, flags, HandleRandomAvatarLoaded);
				}
				else
				{
					avatarSpinner.LoadDnaOnAvatar(avatarDna, flags, HandleRegularAvatarLoaded);
				}
			}
			else if (retryAttempts == 2)
			{
				retryAttempts++;
				avatarDna = DefaultAvatar.GetDefaultAvatar();
				avatarSpinner.LoadDnaOnAvatar(avatarDna, flags, HandleRegularAvatarLoaded);
			}
			else
			{
				MonoSingleton<AvatarManager>.Instance.ResetAvatarHead(avatarSpinner.Avatar);
				avatarSpinner.HitBrakes();
			}
		}

		private void Update()
		{
			Singleton<TechAnalytics>.Instance.TrackFPSOnAvatarEditor();
			if (lockUi >= 0f)
			{
				lockUi += Time.deltaTime;
				if (lockUi > 60f)
				{
					MonoSingleton<AvatarManager>.Instance.ResetAvatarHead(avatarSpinner.Avatar);
					avatarSpinner.HitBrakes();
					lockUi = -1f;
				}
			}
			if (selectionPending && !avatarCompositing)
			{
				recompositeAvatar();
				selectionPending = false;
			}
		}

		private IAvatarProperty GetPropertyFromString(string categoryName, ClientAvatar avatar)
		{
			switch (categoryName)
			{
			case "Accessory":
				return avatar.Accessory;
			case "Brow":
				return avatar.Brow;
			case "Eyes":
				return avatar.Eyes;
			case "Hair":
				return avatar.Hair;
			case "Mouth":
				return avatar.Mouth;
			case "Nose":
				return avatar.Nose;
			case "Skin":
				return avatar.Skin;
			case "Costume":
				return avatar.Costume;
			case "Hat":
				if (avatar.Hat == null)
				{
					return new ClientAvatarProperty("257", 0, 0.0, 0.0);
				}
				return avatar.Hat;
			default:
				return null;
			}
		}

		private void SetPropertyFromString(string categoryName, ClientAvatar avatar, IAvatarProperty newProp)
		{
			switch (categoryName)
			{
			case "Accessory":
				avatar.Accessory = newProp;
				break;
			case "Brow":
				avatar.Brow = newProp;
				break;
			case "Eyes":
				avatar.Eyes = newProp;
				break;
			case "Hair":
				avatar.Hair = newProp;
				break;
			case "Mouth":
				avatar.Mouth = newProp;
				break;
			case "Nose":
				avatar.Nose = newProp;
				break;
			case "Skin":
				avatar.Skin = newProp;
				break;
			case "Costume":
				avatar.Costume = newProp;
				break;
			case "Hat":
				avatar.Hat = newProp;
				break;
			}
		}

		private string GetSelectionKeyFromReferenceId(string refId)
		{
			Avatar_Multiplane avatarByReferenceId = Singleton<EntitlementsManager>.Instance.GetAvatarByReferenceId(refId);
			if (avatarByReferenceId != null)
			{
				return avatarByReferenceId.GetUid();
			}
			return string.Empty;
		}

		private string GetReferenceIdFromSelectionKey(string selectionKey)
		{
			Avatar_Multiplane avatarData = Singleton<EntitlementsManager>.Instance.GetAvatarData(selectionKey);
			if (avatarData != null)
			{
				return avatarData.GetReferenceId().ToString();
			}
			return string.Empty;
		}

		private void setDefaultsUp()
		{
			if (this.IsNullOrDisposed() || categoryTitle == null || removeButton == null || mediaTray == null || colorBar == null || ColorPicker == null || horizontalSlider == null || verticalSlider == null)
			{
				return;
			}
			categoryTitle.text = mediaTray.categorySelector.CurrentContent.locCategoryName;
			string categoryName = mediaTray.categorySelector.CurrentContent.categoryName;
			removeButton.GetComponent<Button>().interactable = categoryName != "Eyes";
			if (lastCategory != mediaTray.categorySelector.CurrentContent.locCategoryName)
			{
				Color[] colorsByCategoryName = AvatarColorTints.GetColorsByCategoryName(categoryName);
				if (!ColorPicker.HasInitialized())
				{
					ColorPicker.Init(this);
				}
				if (colorsByCategoryName != null && !categoryName.Equals("Accessory") && !categoryName.Equals("Hat"))
				{
					colorBar.gameObject.SetActive(true);
					ColorPicker.SetContent(categoryName);
					IgnoreColorAudio = true;
				}
				else
				{
					colorBar.gameObject.SetActive(false);
					IgnoreColorAudio = true;
				}
			}
			if (avatarDna == null)
			{
				tintIndex = 0;
				verticalSlider.value = 0.5f;
				horizontalSlider.value = 0.5f;
			}
			else
			{
				IAvatarProperty propertyFromString = GetPropertyFromString(categoryName, avatarDna);
				string selected;
				if (propertyFromString != null)
				{
					item = GetSelectionKeyFromReferenceId(propertyFromString.SelectionKey);
					tintIndex = propertyFromString.TintIndex;
					verticalSlider.value = (float)((propertyFromString.YOffset + 50.0) / 100.0);
					horizontalSlider.value = (float)((propertyFromString.XOffset + 50.0) / 100.0);
					selected = item;
				}
				else
				{
					item = mediaTray.categorySelector.CurrentContent.GetDefaultUid();
					tintIndex = 0;
					verticalSlider.value = 0.5f;
					horizontalSlider.value = 0.5f;
					selected = item;
				}
				mediaTray.SetSelected(selected);
				ColorPicker.SetSelected(tintIndex);
			}
			bool active = categoryName != "Nose" && categoryName != "Hair" && categoryName != "Skin" && categoryName != "Costume" && categoryName != "Hat";
			verticalSlider.gameObject.SetActive(active);
			bool active2 = categoryName == "Mouth";
			horizontalSlider.gameObject.SetActive(active2);
			lastCategory = categoryTitle.text;
		}

		public void RemoveItem()
		{
			if (!(lockUi >= 0f) && !string.IsNullOrEmpty(mediaTray.categorySelector.GetDisableId(0)))
			{
				item = mediaTray.categorySelector.GetDisableId(0);
				mediaTray.SetSelected(item);
				recompositeAvatar();
				setDefaultsUp();
			}
		}

		public void GenerateRandom()
		{
			if (lockUi < 0f)
			{
				OnRandomizeConfirmation();
			}
			Analytics.LogRandomizeAvatarAction(mode);
		}

		public void LoadRandomAvatar()
		{
			avatarDna = CloneAvatar(MonoSingleton<AvatarManager>.Instance.GenerateRandomDna());
			IsDirty = !AvatarApi.AreAvatarsEqual(avatarDna, MonoSingleton<AvatarManager>.Instance.CurrentDna);
		}

		public void OnHorizontalSliderChange()
		{
			if (!(lockUi >= 0f))
			{
				StartCoroutine(DelaySliderInput(horizontalSlider));
			}
		}

		public void OnVerticalSliderChange()
		{
			if (!(lockUi >= 0f))
			{
				StartCoroutine(DelaySliderInput(verticalSlider));
			}
		}

		public IEnumerator DelaySliderInput(Slider sliderToCheck)
		{
			float oldValue = sliderToCheck.value;
			yield return new WaitForSeconds(SliderUpdateDelay);
			if (Mathf.Abs(sliderToCheck.value - oldValue) <= float.Epsilon)
			{
				recompositeAvatar();
			}
		}

		public void OnEntitlementClicked(BaseContentData aEntitlement)
		{
			if (!(lockUi >= 0f))
			{
				if (aEntitlement.GetUid() == item)
				{
					RemoveItem();
					return;
				}
				item = aEntitlement.GetUid();
				mediaTray.SetSelected(item);
				Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/AvatarCreateMiddleLevel");
				recompositeAvatar();
				setDefaultsUp();
			}
		}

		public void OnContentHolderChanged()
		{
			Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/AvatarCreateBottomLevel");
			setDefaultsUp();
		}

		public void OnShowPreviewPanel(BaseContentData aEntitlement)
		{
		}

		public void OnHidePreviewPanel()
		{
		}

		private void recompositeAvatar()
		{
			if (!avatarCompositing && avatarDna != null && horizontalSlider != null && verticalSlider != null && avatarSpinner != null)
			{
				avatarCompositing = true;
				string categoryStr = mediaTray.categorySelector.CurrentContent.categoryName;
				IAvatarProperty propertyFromString = GetPropertyFromString(categoryStr, avatarDna);
				bool hasChanged = false;
				if (propertyFromString != null)
				{
					ClientAvatarProperty clientAvatarProperty = new ClientAvatarProperty();
					clientAvatarProperty.SelectionKey = GetReferenceIdFromSelectionKey(item);
					clientAvatarProperty.TintIndex = tintIndex;
					clientAvatarProperty.XOffset = horizontalSlider.value * 100f - 50f;
					clientAvatarProperty.YOffset = verticalSlider.value * 100f - 50f;
					hasChanged = item != propertyFromString.SelectionKey || tintIndex != propertyFromString.TintIndex;
					SetPropertyFromString(categoryStr, avatarDna, clientAvatarProperty);
				}
				avatarSpinner.LoadDnaOnAvatar(avatarDna, flags, delegate(bool success, string sha)
				{
					if (hasChanged)
					{
						avatarSpinner.PlayAnimationTrigger("*Category*Changed".Replace("*Category*", categoryStr));
					}
					HandleRegularAvatarLoaded(success, sha);
				});
				if (MonoSingleton<AvatarManager>.Instance == null || MonoSingleton<AvatarManager>.Instance.CurrentDna == null)
				{
					IsDirty = true;
				}
				else
				{
					IsDirty = !AvatarApi.AreAvatarsEqual(MonoSingleton<AvatarManager>.Instance.CurrentDna, avatarDna);
				}
			}
			else
			{
				selectionPending = true;
			}
		}

		public void ResetAvatarToDefault()
		{
			if (!(mediaTray == null) && !(horizontalSlider == null) && !(verticalSlider == null))
			{
				avatarDna = CloneAvatar(MonoSingleton<AvatarManager>.Instance.CurrentDna);
				IsDirty = false;
				IAvatarProperty propertyFromString = GetPropertyFromString(mediaTray.categorySelector.CurrentContent.categoryName, avatarDna);
				if (propertyFromString != null)
				{
					item = GetSelectionKeyFromReferenceId(propertyFromString.SelectionKey);
					tintIndex = propertyFromString.TintIndex;
					verticalSlider.value = (float)((propertyFromString.YOffset + 50.0) / 100.0);
					horizontalSlider.value = (float)((propertyFromString.XOffset + 50.0) / 100.0);
					mediaTray.SetSelected(item);
				}
				recompositeAvatar();
			}
		}

		public void OnDoneClicked()
		{
			if (IsDirty)
			{
				SaveAvatar();
			}
			if (mode == EDITOR_MODES.EDITOR)
			{
				GoToProfile();
			}
			else if (!MixSession.IsValidSession || profileInfo != null)
			{
				GoToRegistration();
			}
			else
			{
				handleDisplayNameStatus();
			}
			Singleton<SoundManager>.Instance.PlaySoundEvent((mode == EDITOR_MODES.EDITOR) ? "MainApp/UI/SFX_2_SendDisney" : "UI/Registration/NextPage/AfterCreatingAvatar");
		}

		private void handleDisplayNameStatus()
		{
			if (!(Singleton<PanelManager>.Instance.FindPanel(typeof(DisplayNamePanel)) != null))
			{
				base.gameObject.GetComponent<Animator>().enabled = true;
				if (!showDisplayNamePanel)
				{
					base.gameObject.GetComponent<Animator>().Play("AvatarEditorScreen_ToSubmitDisplayName");
				}
				DisplayNamePanel displayNamePanel = (DisplayNamePanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.DISPLAY_NAME_SUBMITTED);
				displayNamePanel.Init(this, MixSession.Session.LocalUser.RegistrationProfile);
			}
		}

		public void OnRandomizeConfirmation()
		{
			lockUi = 0f;
			avatarSpinner.SetState(AvatarSpinner.SpinState.SPINNING_UP);
		}

		public void OnBackBtnClicked()
		{
			if (mode == EDITOR_MODES.CREATOR)
			{
				GoToLogin();
			}
			else
			{
				GoToProfile();
			}
		}

		public void DnaLoadError(string swid)
		{
		}

		private void SaveAvatar()
		{
			Analytics.LogCreateAvatarSuccess(mode, avatarDna);
			MonoSingleton<AvatarManager>.Instance.setCurrentUsersDna(avatarDna);
			MonoSingleton<AvatarManager>.Instance.saveCurrentUsersDna();
		}

		private void GoToFatalError()
		{
			if (avatarSpinner != null && avatarSpinner.Avatar != null)
			{
				avatarSpinner.Avatar.SetActive(true);
			}
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Login/LoginScreen", new TransitionNone());
			navigationRequest.AddData("fatalError", true);
			navigationRequest.PopLastRequest = true;
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
			MonoSingleton<LoginManager>.Instance.Logout();
		}

		public void OnDisplayNameUpdated(DisplayNamePanel dispName)
		{
			ProgressBar.GetComponent<Animator>().Play("ProgressBar_3-4");
			base.gameObject.GetComponent<Animator>().enabled = true;
			base.gameObject.GetComponent<Animator>().Play("AvatarEditorScreen_OnRegComplete");
		}

		private void GoToProfile(ProfileController.PROFILE_MODES aMode = ProfileController.PROFILE_MODES.PROFILE)
		{
			if (avatarSpinner != null && avatarSpinner.Avatar != null)
			{
				avatarSpinner.Avatar.SetActive(true);
			}
			NavigationRequest navigationRequest;
			if (previousScreen == "Prefabs/Screens/ChatMix/ChatMixScreen")
			{
				navigationRequest = ChatHelper.NavigateToChatScreen(previousThread, new TransitionInLeft(), false, false);
				navigationRequest.PopLastRequest = true;
			}
			else
			{
				navigationRequest = new NavigationRequest("Prefabs/Screens/Profile/ProfileScreen", new TransitionAnimations());
				navigationRequest.AddData("mode", aMode);
				navigationRequest.PopLastRequest = true;
			}
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
		}

		private void GoToLogin()
		{
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Login/LoginScreen", new TransitionAnimations(false, "Intro", "ToLogin"));
			navigationRequest.PopLastRequest = true;
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
		}

		private void GoToRegistration()
		{
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/CreateAccount/CreateAccountScreen", new TransitionInLeft());
			if (profileInfo != null)
			{
				navigationRequest.AddData("missingInfo", profileInfo);
			}
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
		}

		public void GoToChat()
		{
			if (avatarSpinner != null && avatarSpinner.Avatar != null)
			{
				avatarSpinner.Avatar.SetActive(true);
			}
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Conversations/ConversationsScreen", new TransitionAnimations(false, "FromSoftLogin", "ToChat"));
			navigationRequest.PopLastRequest = true;
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
			LoginController.StopLoginMusic();
			Singleton<SoundManager>.Instance.PlaySoundEvent("UI/Registration/LetsGoButton");
		}

		private void ChangeParticlesToColor()
		{
			MixelDust.GetComponent<Animator>().Play("ParticleToColor");
		}
	}
}
