using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.DeviceDb;
using Mix.Native;
using Mix.Session;
using Mix.User;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class RegistrationController : BaseController, IAdultAccount, IChildAccount, CountryItem.ICountryListener, IDisneyIdLogin, IEmailSent, IEnterBirthdate, IForgotInfo, IMaseNrt, IMissingInfo, IErrorOverlay, IRulesOverlay, IPrivacyPracticesOverlay, ICountrySelectOverlay
	{
		public enum RegisterFlowOriginatingSource
		{
			MAIN_LOGIN = 0,
			DISNEY_ID_LOGIN = 1,
			ADULT_CREATE = 2,
			CHILD_CREATE = 3,
			SCREEN_SETTINGS = 4,
			PARENTAL_CONTROLS = 5
		}

		private const string soundPrefix = "UI/Registration/";

		private const string ageConfirmEvent = "NextPage/AfterConfirmAge";

		private const string termsViewedEvent = "NextPage/AfterTermsGuidelines";

		private const string accountCreatedEvent = "NextPage/AfterAccountCreated";

		private const string previousPageSoundEvent = "PreviousPage";

		public GameObject EnterBirthdatePanel;

		public GameObject ChildAccountPanel;

		public GameObject AdultAccountPanel;

		public GameObject MissingInfoPanel;

		public GameObject DisneyAccountLoginPanel;

		public GameObject AccountCreatedOverlay;

		public GameObject ForgotPanel;

		public GameObject MaseNrtPanel;

		public GameObject EmailSentPanel;

		public GameObject ProgressBar;

		public Animator ProgressBarAnimator;

		public Animator MixelDustAnimator;

		public GameObject CountrySelectPanel;

		public GameObject CountryItem;

		public ScrollView CountryScrollView;

		public Animator CountrySelectAnimator;

		private EnterBirthdate enterBirthdateController;

		private ChildAccount childAccountController;

		private AdultAccount adultAccountController;

		private DisneyIdLogin disneyIdLoginController;

		private MissingInfo missingInfoController;

		private ForgotInfo forgotInfoController;

		private MaseNrt maseNrtController;

		private EmailSent emailSentController;

		private GameObject privacyPracticesPanel;

		private GameObject countrySelectPanel;

		private IRegistrationProfile profileInfo;

		private int birthYear;

		private int birthMonth;

		private int birthDay;

		private IAgeBand ageBand;

		private string countryCode;

		private string countryName;

		private bool recover;

		private bool recoverTrueIfUsernameFalseIfPassword;

		private bool trueIfMaseFalseIfNrt;

		private RegisterFlowOriginatingSource originatingSource;

		private string username;

		private ILoginResult loginResult;

		private ILocalUser parent;

		private static Dictionary<string, string> countries = new Dictionary<string, string>
		{
			{ "Asia", "AA" },
			{ "Andorra", "AD" },
			{ "United Arab Emirates", "AE" },
			{ "Afghanistan", "AF" },
			{ "Antigua and Barbuda", "AG" },
			{ "Anguilla", "AI" },
			{ "Albania", "AL" },
			{ "Armenia", "AM" },
			{ "Netherlands Antilles", "AN" },
			{ "Angola", "AO" },
			{ "Antarctica", "AQ" },
			{ "Argentina", "AR" },
			{ "American Samoa", "AS" },
			{ "Austria", "AT" },
			{ "Australia", "AU" },
			{ "Aruba", "AW" },
			{ "Aland Islands", "AX" },
			{ "Azerbaijan", "AZ" },
			{ "Bosnia and Herzegowina", "BA" },
			{ "Barbados", "BB" },
			{ "Bangladesh", "BD" },
			{ "Belgium", "BE" },
			{ "Burkina Faso", "BF" },
			{ "Bulgaria", "BG" },
			{ "Bahrain", "BH" },
			{ "Burundi", "BI" },
			{ "Benin", "BJ" },
			{ "Saint Barthelemy", "BL" },
			{ "Bermuda", "BM" },
			{ "Brunei Darussalam", "BN" },
			{ "Bolivia", "BO" },
			{ "Bonaire, Sint Eustatius and Saba", "BQ" },
			{ "Brazil", "BR" },
			{ "Bahamas", "BS" },
			{ "Bhutan", "BT" },
			{ "Bouvet Island", "BV" },
			{ "Botswana", "BW" },
			{ "Belarus", "BY" },
			{ "Belize", "BZ" },
			{ "Canada", "CA" },
			{ "Cocos (Keeling) Islands", "CC" },
			{ "Democratic Republic of Congo", "CD" },
			{ "Central African Republic", "CF" },
			{ "Congo", "CG" },
			{ "Switzerland", "CH" },
			{ "Cote d'Ivoire", "CI" },
			{ "Cook Islands", "CK" },
			{ "Chile", "CL" },
			{ "Cameroon", "CM" },
			{ "China", "CN" },
			{ "Colombia", "CO" },
			{ "Costa Rica", "CR" },
			{ "Serbia and Montenegro", "CS" },
			{ "Cape Verde", "CV" },
			{ "Christmas Island", "CX" },
			{ "Cyprus", "CY" },
			{ "Czech Republic", "CZ" },
			{ "Germany", "DE" },
			{ "Djibouti", "DJ" },
			{ "Denmark", "DK" },
			{ "Dominica", "DM" },
			{ "Dominican Republic", "DO" },
			{ "Algeria", "DZ" },
			{ "Ecuador", "EC" },
			{ "Estonia", "EE" },
			{ "Egypt", "EG" },
			{ "Western Sahara", "EH" },
			{ "Eritrea", "ER" },
			{ "Spain", "ES" },
			{ "Ethiopia", "ET" },
			{ "Finland", "FI" },
			{ "Fiji", "FJ" },
			{ "Falkland Islands", "FK" },
			{ "Micronesia", "FM" },
			{ "Faroe Islands", "FO" },
			{ "France", "FR" },
			{ "Gabon", "GA" },
			{ "Grenada", "GD" },
			{ "Georgia", "GE" },
			{ "French Guiana", "GF" },
			{ "Guernsey", "GG" },
			{ "Ghana", "GH" },
			{ "Gibraltar", "GI" },
			{ "Greenland", "GL" },
			{ "Gambia", "GM" },
			{ "Guinea", "GN" },
			{ "Guadeloupe", "GP" },
			{ "Equatorial Guinea", "GQ" },
			{ "Greece", "GR" },
			{ "South Georgia", "GS" },
			{ "Guatemala", "GT" },
			{ "Guam", "GU" },
			{ "Guinea-Bissau", "GW" },
			{ "Guyana", "GY" },
			{ "Hong Kong", "HK" },
			{ "Heard and McDonald Islands", "HM" },
			{ "Honduras", "HN" },
			{ "Croatia", "HR" },
			{ "Haiti", "HT" },
			{ "Hungary", "HU" },
			{ "Indonesia", "ID" },
			{ "Ireland", "IE" },
			{ "Israel", "IL" },
			{ "Isle of Man", "IM" },
			{ "India", "IN" },
			{ "British Indian Ocean Territory", "IO" },
			{ "Iraq", "IQ" },
			{ "Iceland", "IS" },
			{ "Italy", "IT" },
			{ "Jersey", "JE" },
			{ "Jamaica", "JM" },
			{ "Jordan", "JO" },
			{ "Japan", "JP" },
			{ "Kenya", "KE" },
			{ "Kyrgystan", "KG" },
			{ "Cambodia", "KH" },
			{ "Kiribati", "KI" },
			{ "Comoros", "KM" },
			{ "Saint Kitts and Nevis", "KN" },
			{ "Korea, Republic of", "KR" },
			{ "Kuwait", "KW" },
			{ "Cayman Islands", "KY" },
			{ "Kazakhstan", "KZ" },
			{ "Laos", "LA" },
			{ "Lebanon", "LB" },
			{ "Saint Lucia", "LC" },
			{ "Liechtenstein", "LI" },
			{ "Sri Lanka", "LK" },
			{ "Liberia", "LR" },
			{ "Lesotho", "LS" },
			{ "Lithuania", "LT" },
			{ "Luxembourg", "LU" },
			{ "Latvia", "LV" },
			{ "Libya", "LY" },
			{ "Morocco", "MA" },
			{ "Monaco", "MC" },
			{ "Moldova, Republic of", "MD" },
			{ "Montenegro", "ME" },
			{ "Saint Martin (French part)", "MF" },
			{ "Madagascar", "MG" },
			{ "Marshall Islands", "MH" },
			{ "Macedonia", "MK" },
			{ "Mali", "ML" },
			{ "Myanmar", "MM" },
			{ "Mongolia", "MN" },
			{ "Macau", "MO" },
			{ "Northern Mariana Islands", "MP" },
			{ "Martinique", "MQ" },
			{ "Mauritania", "MR" },
			{ "Montserrat", "MS" },
			{ "Malta", "MT" },
			{ "Mauritius", "MU" },
			{ "Maldives", "MV" },
			{ "Malawi", "MW" },
			{ "Mexico", "MX" },
			{ "Malaysia", "MY" },
			{ "Mozambique", "MZ" },
			{ "Namibia", "NA" },
			{ "New Calendonia", "NC" },
			{ "Niger", "NE" },
			{ "Norfolk Island", "NF" },
			{ "Nigeria", "NG" },
			{ "Nicaragua", "NI" },
			{ "Netherlands", "NL" },
			{ "Norway", "NO" },
			{ "Nepal", "NP" },
			{ "Nauru", "NR" },
			{ "Niue", "NU" },
			{ "New Zealand", "NZ" },
			{ "Oman", "OM" },
			{ "Panama", "PA" },
			{ "Peru", "PE" },
			{ "French Polynesia", "PF" },
			{ "Papua New Guinea", "PG" },
			{ "Philippines", "PH" },
			{ "Pakistan", "PK" },
			{ "Poland", "PL" },
			{ "St. Pierre and Miquelon", "PM" },
			{ "Pitcairn", "PN" },
			{ "Puerto Rico", "PR" },
			{ "Palestine", "PS" },
			{ "Portugal", "PT" },
			{ "Palau", "PW" },
			{ "Paraguay", "PY" },
			{ "Qatar", "QA" },
			{ "Reunion", "RE" },
			{ "Romania", "RO" },
			{ "Serbia", "RS" },
			{ "Russian Federation", "RU" },
			{ "Rwanda", "RW" },
			{ "Saudi Arabia", "SA" },
			{ "Soloman Islands", "SB" },
			{ "Seychelles", "SC" },
			{ "Sweden", "SE" },
			{ "Singapore", "SG" },
			{ "St. Helena", "SH" },
			{ "Slovenia", "SI" },
			{ "Svalbard and Jan Mayen", "SJ" },
			{ "Slovakia", "SK" },
			{ "Sierra Leone", "SL" },
			{ "San Marino", "SM" },
			{ "Senegal", "SN" },
			{ "Somalia", "SO" },
			{ "Suriname", "SR" },
			{ "South Sudan", "SS" },
			{ "United Kingdom", "UK" },
			{ "Sao Tome and Principe", "ST" },
			{ "El Salvador", "SV" },
			{ "Sint Maarten (Dutch part)", "SX" },
			{ "Swaziland", "SZ" },
			{ "Turks and Caicos Islands", "TC" },
			{ "Chad", "TD" },
			{ "French Southern Territories", "TF" },
			{ "Togo", "TG" },
			{ "Thailand", "TH" },
			{ "Tajikistan", "TJ" },
			{ "Tokelau", "TK" },
			{ "Timor-Leste", "TL" },
			{ "Turkmenistan", "TM" },
			{ "Tunisia", "TN" },
			{ "Tonga", "TO" },
			{ "Turkey", "TR" },
			{ "Trinidad and Tobago", "TT" },
			{ "Tuvalu", "TV" },
			{ "Taiwan", "TW" },
			{ "Tanzania", "TZ" },
			{ "Ukraine", "UA" },
			{ "Uganda", "UG" },
			{ "Great Britain", "GB" },
			{ "United States Minor Outlying Islands", "UM" },
			{ "United States", "US" },
			{ "Uruguay", "UY" },
			{ "Uzbekistan", "UZ" },
			{ "Holy See (Vatican City State)", "VA" },
			{ "Saint Vincent and the Grenadines", "VC" },
			{ "Venezuela", "VE" },
			{ "Virgin Islands (British)", "VG" },
			{ "Virgin Islands (U.S.)", "VI" },
			{ "Viet Nam", "VN" },
			{ "Vanuatu", "VU" },
			{ "Wallis and Fatuna", "WF" },
			{ "Samoa", "WS" },
			{ "Kosovo", "XK" },
			{ "Yemen", "YE" },
			{ "Mayotte", "YT" },
			{ "Yugoslavia", "YU" },
			{ "South Africa", "ZA" },
			{ "Zambia", "ZM" },
			{ "Zimbabwe", "ZW" },
			{ "Burma", "BU" },
			{ "Curacao", "CW" },
			{ "France Metropolitan", "FX" },
			{ "Neutral Zone", "NT" },
			{ "USSR", "SU" },
			{ "East Timor", "TP" },
			{ "Zaire", "ZR" }
		};

		void ICountrySelectOverlay.ShowCountrySelectOverlay()
		{
			CountrySelectPanel.SetActive(true);
			CountryScrollView.Show();
		}

		void CountryItem.ICountryListener.OnCountryClicked(string aCountryCode, string aCountryName)
		{
			if (!(countryCode != aCountryCode))
			{
				return;
			}
			countryName = aCountryName;
			countryCode = countries[countryName];
			CountryScrollView.Hide();
			CountrySelectAnimator.Play("CountrySelect_PopOut", 0, 0f);
			RegistrationConfigurationGetter registrationConfigurationGetter = new RegistrationConfigurationGetter(MonoSingleton<LoginManager>.Instance.KeychainData, Log.MixLogger, MonoSingleton<LoginManager>.Instance.StorageDir, ExternalizedConstants.GuestControllerUrl, MixSession.GuestControllerSpoofedIP, ExternalizedConstants.GuestControllerClientId, MonoSingleton<LoginManager>.Instance.CoroutineManager, ExternalizedConstants.MixPlatformServicesUrl, ExternalizedConstants.MixPlatformServicesCellophane);
			if (countryCode != null && countryCode != string.Empty)
			{
				registrationConfigurationGetter.Get(countryCode, delegate(IGetRegistrationConfigurationResult x)
				{
					OnSiteConfigResponse(false, x);
				});
			}
		}

		void IErrorOverlay.ShowErrorOverlay(string aTitleLocToken, string aDescriptionLocToken)
		{
			GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
			genericPanel.ShowSimpleError(aTitleLocToken, aDescriptionLocToken);
		}

		void IRulesOverlay.ShowRulesOverlay()
		{
			if (Singleton<PanelManager>.Instance.ShowPanel(Panels.MIX_RULES, false) != null)
			{
				MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.Hide();
				Analytics.LogViewAppRules();
				PlaySound("PreviousPage");
			}
		}

		void IPrivacyPracticesOverlay.ShowPrivacyPracticesOverlay()
		{
			privacyPracticesPanel.SetActive(true);
		}

		void IEnterBirthdate.OnBirthdateEntered(IAgeBand aMyAgeBand, int aYear, int aMonth, int aDay)
		{
			if (aMyAgeBand != null)
			{
				birthDay = aDay;
				birthMonth = aMonth;
				birthYear = aYear;
				ageBand = aMyAgeBand;
			}
			if (ageBand.AgeBandType == AgeBandType.Child)
			{
				childAccountController.Show(birthYear, birthMonth, birthDay, ageBand, countryCode, countryName, this);
				childAccountController.GetComponent<Animator>().Play("SlideBackToFace", 0, 0f);
			}
			else
			{
				adultAccountController.Show(birthYear, birthMonth, birthDay, ageBand, countryCode, countryName, originatingSource == RegisterFlowOriginatingSource.PARENTAL_CONTROLS, this);
				adultAccountController.GetComponent<Animator>().Play("SlideBackToFace", 0, 0f);
			}
			enterBirthdateController.GetComponent<Animator>().Play("SlideFaceToTop");
			ProgressBarAnimator.Play("ProgressBar_1-2", 0, 0f);
			PlaySound("NextPage/AfterConfirmAge");
		}

		void IEnterBirthdate.OnNavigateBack()
		{
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Login/LoginScreen", new TransitionAnimations(false, "Intro", "ToLogin"));
			navigationRequest.PopLastRequest = true;
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
			PlaySound("PreviousPage");
		}

		void IChildAccount.OnAccountCreated()
		{
			childAccountController.HideKeyboard();
			childAccountController.GetComponent<Animator>().Play("SlideFaceToTop");
			AccountCreatedOverlay.SetActive(true);
			MixelDustAnimator.Play("ParticleToWhite");
			AccountCreatedOverlay.GetComponent<Animator>().Play("SlideBackToFace", 0, 0f);
			AccountCreatedOverlay.GetComponent<AnimationEvents>().OnAnimationEnd += GotoAvatar;
			PlaySound("NextPage/AfterAccountCreated");
			PoisonFlowVariable.SetBirthdate(0, 0, 0, null);
		}

		void IChildAccount.OnBackClicked()
		{
			childAccountController.gameObject.SetActive(false);
			childAccountController.HideKeyboard();
			childAccountController.GetComponent<Animator>().Play("SlideFaceToBack");
			enterBirthdateController.Show(this, null);
			enterBirthdateController.GetComponent<Animator>().Play("SlideTopToFace");
			ProgressBarAnimator.Play("ProgressBar_2-1");
			PlaySound("PreviousPage");
		}

		void IChildAccount.GoToDisneyIdLogin()
		{
			ProgressBarAnimator.Play("ProgressBar_Hidden");
			childAccountController.GetComponent<Animator>().Play("SlideFaceToTop");
			childAccountController.HideKeyboard();
			disneyIdLoginController.Show(birthYear, birthMonth, birthDay, this);
			disneyIdLoginController.GetComponent<Animator>().Play("SlideBackToFace");
		}

		void IAdultAccount.OnAccountCreated(ILocalUser aParent)
		{
			adultAccountController.HideKeyboard();
			if (originatingSource == RegisterFlowOriginatingSource.PARENTAL_CONTROLS)
			{
				NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Settings/SettingsScreen", new TransitionAnimations());
				navigationRequest.AddData("createAdultAccount", aParent);
				MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
				return;
			}
			adultAccountController.GetComponent<Animator>().Play("SlideFaceToTop");
			AccountCreatedOverlay.SetActive(true);
			MixelDustAnimator.Play("ParticleToWhite");
			AccountCreatedOverlay.GetComponent<Animator>().Play("SlideBackToFace", 0, 0f);
			AccountCreatedOverlay.GetComponent<AnimationEvents>().OnAnimationEnd += GotoAvatar;
			PlaySound("NextPage/AfterAccountCreated");
		}

		void IAdultAccount.OnBackClicked()
		{
			adultAccountController.gameObject.SetActive(false);
			adultAccountController.HideKeyboard();
			adultAccountController.GetComponent<Animator>().Play("SlideFaceToBack");
			enterBirthdateController.Show(this, null);
			enterBirthdateController.GetComponent<Animator>().Play("SlideTopToFace");
			ProgressBarAnimator.Play("ProgressBar_2-1");
			PlaySound("PreviousPage");
		}

		void IAdultAccount.GoToDisneyIdLogin()
		{
			ProgressBarAnimator.Play("ProgressBar_Hidden");
			adultAccountController.GetComponent<Animator>().Play("SlideFaceToTop");
			adultAccountController.HideKeyboard();
			disneyIdLoginController.Show(birthYear, birthMonth, birthDay, this);
			disneyIdLoginController.GetComponent<Animator>().Play("SlideBackToFace");
		}

		void IAdultAccount.HandleMaseAccount(string aEmail)
		{
			adultAccountController.HideKeyboard();
			originatingSource = RegisterFlowOriginatingSource.ADULT_CREATE;
			adultAccountController.GetComponent<Animator>().Play("SlideFaceToTop");
			maseNrtController.Show(aEmail, true, this);
			maseNrtController.GetComponent<Animator>().Play("SlideBackToFace");
			ProgressBarAnimator.Play("ProgressBar_Hidden");
		}

		void IAdultAccount.HandleNrtAccount(string aEmail)
		{
			adultAccountController.HideKeyboard();
			originatingSource = RegisterFlowOriginatingSource.ADULT_CREATE;
			adultAccountController.GetComponent<Animator>().Play("SlideFaceToTop");
			maseNrtController.Show(aEmail, false, this);
			maseNrtController.GetComponent<Animator>().Play("SlideBackToFace");
			ProgressBarAnimator.Play("ProgressBar_Hidden");
		}

		void IDisneyIdLogin.OnBackClicked()
		{
			disneyIdLoginController.gameObject.SetActive(false);
			ProgressBarAnimator.Play("ProgressBar_Show");
			disneyIdLoginController.HideKeyboard();
			disneyIdLoginController.GetComponent<Animator>().Play("SlideFaceToBack");
			if (ageBand.AgeBandType == AgeBandType.Child)
			{
				childAccountController.Show(birthYear, birthMonth, birthDay, ageBand, countryCode, countryName, this);
				childAccountController.GetComponent<Animator>().Play("SlideTopToFace");
			}
			else
			{
				adultAccountController.Show(birthYear, birthMonth, birthDay, ageBand, countryCode, countryName, originatingSource == RegisterFlowOriginatingSource.PARENTAL_CONTROLS, this);
				adultAccountController.GetComponent<Animator>().Play("SlideTopToFace");
			}
			PlaySound("PreviousPage");
		}

		void IDisneyIdLogin.OnInfoMissing(ILocalUser aLocalUser)
		{
			if (!this.IsNullOrDisposed() && !(base.gameObject == null))
			{
				disneyIdLoginController.HideKeyboard();
				missingInfoController.Show(aLocalUser, ageBand, this);
			}
		}

		void IDisneyIdLogin.OnValidAccount(string aNavgationConstPath, bool aShowPopUp)
		{
			disneyIdLoginController.HideKeyboard();
			switch (aNavgationConstPath)
			{
			case "Prefabs/Screens/AvatarEditor/AvatarEditorScreen":
				gotoAvatarCreator();
				break;
			case "Prefabs/Screens/Profile/ProfileScreen":
				gotoProfile(aShowPopUp);
				break;
			case "Prefabs/Screens/ChatMix/ChatMixScreen":
				gotoChat(aShowPopUp);
				break;
			default:
				gotoProfile();
				break;
			}
		}

		void IDisneyIdLogin.OnRecoveryClicked(bool aRecoverTrueIfUsernameFalseIfPassword)
		{
			disneyIdLoginController.HideKeyboard();
			disneyIdLoginController.GetComponent<Animator>().Play("SlideFaceToTop");
			originatingSource = RegisterFlowOriginatingSource.DISNEY_ID_LOGIN;
			forgotInfoController.Show(aRecoverTrueIfUsernameFalseIfPassword, this);
			forgotInfoController.GetComponent<Animator>().Play("SlideBackToFace");
		}

		void IDisneyIdLogin.HandleMaseAccount(string aEmail)
		{
			disneyIdLoginController.HideKeyboard();
			originatingSource = RegisterFlowOriginatingSource.DISNEY_ID_LOGIN;
			maseNrtController.Show(aEmail, true, this);
		}

		void IForgotInfo.OnBack()
		{
			forgotInfoController.Hide();
			if (originatingSource == RegisterFlowOriginatingSource.MAIN_LOGIN)
			{
				NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Login/LoginScreen", new TransitionAnimations(false, "Intro", "ToLogin_NoProgressBar"));
				navigationRequest.AddData("loginPrompt", true);
				MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
			}
			else if (originatingSource == RegisterFlowOriginatingSource.SCREEN_SETTINGS)
			{
				NavigationRequest navigationRequest2 = new NavigationRequest("Prefabs/Screens/Settings/SettingsScreen", new TransitionAnimations());
				navigationRequest2.AddData("fromScene", "Prefabs/Screens/Conversations/ConversationsScreen");
				MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest2);
			}
			else
			{
				forgotInfoController.GetComponent<Animator>().Play("SlideFaceToBack");
				disneyIdLoginController.Show(birthYear, birthMonth, birthDay, this);
				disneyIdLoginController.GetComponent<Animator>().Play("SlideTopToFace");
			}
			PlaySound("PreviousPage");
		}

		void IForgotInfo.OnEmailSent(DisneyIdEmailType aType)
		{
			forgotInfoController.GetComponent<Animator>().Play("SlideFaceToTop");
			emailSentController.Show(aType, this);
			emailSentController.GetComponent<Animator>().Play("SlideBackToFace");
		}

		void IMaseNrt.OnBack()
		{
			maseNrtController.Hide();
			if (originatingSource == RegisterFlowOriginatingSource.MAIN_LOGIN)
			{
				NavigationRequest aRequest = new NavigationRequest("Prefabs/Screens/Login/LoginScreen", new TransitionAnimations(false, "Intro", "ToLogin"));
				MonoSingleton<NavigationManager>.Instance.AddRequest(aRequest);
			}
			else if (originatingSource == RegisterFlowOriginatingSource.ADULT_CREATE)
			{
				adultAccountController.Show(birthYear, birthMonth, birthDay, ageBand, countryCode, countryName, originatingSource == RegisterFlowOriginatingSource.PARENTAL_CONTROLS, this);
				adultAccountController.GetComponent<Animator>().Play("SlideTopToFace");
				maseNrtController.GetComponent<Animator>().Play("SlideFaceToBack");
				ProgressBarAnimator.Play("ProgressBar_Show");
			}
			else
			{
				disneyIdLoginController.Show(birthYear, birthMonth, birthDay, this);
				disneyIdLoginController.GetComponent<Animator>().Play("SlideTopToFace");
				maseNrtController.GetComponent<Animator>().Play("SlideFaceToBack");
				ProgressBarAnimator.Play("ProgressBar_Show");
			}
			PlaySound("PreviousPage");
		}

		void IMaseNrt.OnEmailSent(DisneyIdEmailType aType)
		{
			maseNrtController.GetComponent<Animator>().Play("SlideFaceToTop");
			emailSentController.Show(aType, this);
			emailSentController.GetComponent<Animator>().Play("SlideBackToFace");
		}

		void IEmailSent.OnNext()
		{
			emailSentController.Hide();
			forgotInfoController.Hide();
			if (originatingSource == RegisterFlowOriginatingSource.MAIN_LOGIN)
			{
				NavigationRequest aRequest = new NavigationRequest("Prefabs/Screens/Login/LoginScreen", new TransitionAnimations(false, "Intro", "ToLogin"));
				MonoSingleton<NavigationManager>.Instance.AddRequest(aRequest);
			}
			else
			{
				emailSentController.GetComponent<Animator>().Play("SlideFaceToTop");
				disneyIdLoginController.Show(birthYear, birthMonth, birthDay, this);
				disneyIdLoginController.GetComponent<Animator>().Play("SlideBackToFace");
			}
		}

		void IMissingInfo.OnInfoUpdated()
		{
			missingInfoController.HideKeyboard();
			NavigationRequest navigationRequest = null;
			DisplayNameProposedStatus displayNameProposedStatus = MixSession.Session.LocalUser.RegistrationProfile.DisplayNameProposedStatus;
			switch (displayNameProposedStatus)
			{
			case DisplayNameProposedStatus.Rejected:
				navigationRequest = new NavigationRequest("Prefabs/Screens/Profile/ProfileScreen", new TransitionAnimations(false, "FromLogin"));
				navigationRequest.AddData("mode", ProfileController.PROFILE_MODES.INFO_UPDATE);
				break;
			case DisplayNameProposedStatus.None:
				navigationRequest = new NavigationRequest("Prefabs/Screens/AvatarEditor/AvatarEditorScreen", new TransitionAnimations(false, "ToDisplayNameFromHardLogin"));
				navigationRequest.AddData("mode", AvatarEditorController.EDITOR_MODES.CREATOR);
				navigationRequest.AddData("missingDisplayName", true);
				break;
			default:
			{
				string text = Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi.LoadUserValue("displayname.approved.seen");
				if (displayNameProposedStatus == DisplayNameProposedStatus.Accepted && !string.IsNullOrEmpty(text) && text == "0")
				{
					navigationRequest = new NavigationRequest("Prefabs/Screens/Profile/ProfileScreen", new TransitionAnimations(false, "FromLogin"));
					break;
				}
				navigationRequest = new NavigationRequest("Prefabs/Screens/Conversations/ConversationsScreen", new TransitionAnimations(false, "FromLogin"));
				if (!MonoSingleton<PushNotifications>.Instance.HaveShownPushPrePopup)
				{
					navigationRequest.AddData("regDone", true);
				}
				break;
			}
			}
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
		}

		public string GetCountryCode()
		{
			return countryCode;
		}

		public void SetCountryCode(string aCountryCode)
		{
			countryCode = aCountryCode;
			foreach (string key in countries.Keys)
			{
				if (countries[key] == countryCode)
				{
					countryName = key;
					break;
				}
			}
		}

		public void Start()
		{
			privacyPracticesPanel = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Ui/PrivacyPracticesPanel"));
			privacyPracticesPanel.SetActive(false);
			privacyPracticesPanel.transform.SetParent(base.transform.Find("Overlay_Holder"), false);
			populateCountryScrollView();
		}

		public override void OnDataReceived(string aToken, object aData)
		{
			if (aToken.Contains("missingInfo"))
			{
				loginResult = (ILoginResult)aData;
				profileInfo = loginResult.Session.LocalUser.RegistrationProfile;
				originatingSource = RegisterFlowOriginatingSource.MAIN_LOGIN;
			}
			else if (aToken.Contains("recover_password"))
			{
				recover = true;
				originatingSource = RegisterFlowOriginatingSource.SCREEN_SETTINGS;
				recoverTrueIfUsernameFalseIfPassword = false;
			}
			else if (aToken.Contains("recover_true_if_username_false_if_password"))
			{
				recover = true;
				originatingSource = RegisterFlowOriginatingSource.MAIN_LOGIN;
				recoverTrueIfUsernameFalseIfPassword = (bool)aData;
			}
			else if (aToken.Contains("MASE_username"))
			{
				trueIfMaseFalseIfNrt = true;
				username = (string)aData;
				originatingSource = RegisterFlowOriginatingSource.MAIN_LOGIN;
			}
			else if (aToken.Contains("NRT_username"))
			{
				username = (string)aData;
				originatingSource = RegisterFlowOriginatingSource.MAIN_LOGIN;
			}
			else if (aToken.Contains("createAdultAccount"))
			{
				originatingSource = RegisterFlowOriginatingSource.PARENTAL_CONTROLS;
			}
		}

		public override void OnUILoaded(NavigationRequest aNavigationRequest = null)
		{
			enterBirthdateController = EnterBirthdatePanel.GetComponent<EnterBirthdate>();
			childAccountController = ChildAccountPanel.GetComponent<ChildAccount>();
			adultAccountController = AdultAccountPanel.GetComponent<AdultAccount>();
			missingInfoController = MissingInfoPanel.GetComponent<MissingInfo>();
			disneyIdLoginController = DisneyAccountLoginPanel.GetComponent<DisneyIdLogin>();
			forgotInfoController = ForgotPanel.GetComponent<ForgotInfo>();
			maseNrtController = MaseNrtPanel.GetComponent<MaseNrt>();
			emailSentController = EmailSentPanel.GetComponent<EmailSent>();
			enterBirthdateController.Hide();
			childAccountController.Hide();
			adultAccountController.Hide();
			missingInfoController.Hide();
			disneyIdLoginController.Hide();
			forgotInfoController.Hide();
			maseNrtController.Hide();
			emailSentController.Hide();
			if (ageBand == null && !PoisonFlowVariable.GetBirthdate().Equals(DateTime.MinValue))
			{
				DateTime birthdate = PoisonFlowVariable.GetBirthdate();
				ageBand = PoisonFlowVariable.GetAgeBandInfo();
				birthYear = birthdate.Year;
				birthMonth = birthdate.Month;
				birthDay = birthdate.Day;
			}
		}

		public override void OnUITransitionEnd()
		{
			if (recover)
			{
				forgotInfoController.Show(recoverTrueIfUsernameFalseIfPassword, this);
				forgotInfoController.GetComponent<Animator>().Play("SlideBackToFace");
				ProgressBar.SetActive(false);
			}
			else if (!string.IsNullOrEmpty(username))
			{
				maseNrtController.Show(username, trueIfMaseFalseIfNrt, this);
				maseNrtController.GetComponent<Animator>().Play("SlideBackToFace");
			}
			else if (profileInfo == null)
			{
				enterBirthdateController.Show(this, null);
				enterBirthdateController.GetComponent<Animator>().Play("SlideBackToFace");
			}
			else if (!profileInfo.DateOfBirth.HasValue || profileInfo.DateOfBirth.Value.Equals(DateTime.MinValue))
			{
				enterBirthdateController.Show(this, profileInfo);
				enterBirthdateController.GetComponent<Animator>().Play("SlideBackToFace");
			}
			else if (MonoSingleton<LoginManager>.Instance.IsProfileMissingInfo() || loginResult is ILoginRequiresLegalMarketingUpdateResult)
			{
				missingInfoController.Show(MixSession.Session.LocalUser, ageBand, this, loginResult);
			}
		}

		public override void OnAndroidBackButtonClicked()
		{
			if (Singleton<PanelManager>.Instance.OnAndroidBackButton())
			{
				return;
			}
			if (IsPanelActive(emailSentController.gameObject))
			{
				if (emailSentController.GetEmailType().Equals(DisneyIdEmailType.UPGRADE_NRT))
				{
					NavigationRequest aRequest = new NavigationRequest("Prefabs/Screens/Login/LoginScreen", new TransitionAnimations(false, "Intro", "ToLogin"));
					MonoSingleton<NavigationManager>.Instance.AddRequest(aRequest);
				}
				else
				{
					emailSentController.Hide();
					forgotInfoController.ClearInput();
					forgotInfoController.GetComponent<Animator>().Play("SlideBackToFace");
				}
				return;
			}
			if (IsPanelActive(maseNrtController.gameObject))
			{
				maseNrtController.GoBackButton.onClick.Invoke();
				return;
			}
			if (IsPanelActive(CountrySelectPanel))
			{
				CountrySelectPanel.SetActive(false);
				return;
			}
			GameObject gameObject = GameObject.Find("PasswordRulesPanel");
			if (gameObject != null && gameObject.activeSelf)
			{
				gameObject.SetActive(false);
			}
			else if (AccountCreatedOverlay.activeSelf)
			{
				GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
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
			else if (privacyPracticesPanel.activeSelf)
			{
				privacyPracticesPanel.SetActive(false);
			}
			else if (!IsPanelActive(missingInfoController.gameObject))
			{
				if (IsPanelActive(disneyIdLoginController.gameObject))
				{
					disneyIdLoginController.BackButton.onClick.Invoke();
				}
				else if (IsPanelActive(forgotInfoController.gameObject))
				{
					forgotInfoController.BackButton.onClick.Invoke();
				}
				else if (IsPanelActive(childAccountController.gameObject))
				{
					childAccountController.BackButton.onClick.Invoke();
				}
				else if (IsPanelActive(adultAccountController.gameObject))
				{
					adultAccountController.BackButton.onClick.Invoke();
				}
				else
				{
					enterBirthdateController.BackButton.onClick.Invoke();
				}
			}
		}

		public void OnCountrySelectOverlayClose()
		{
			CountrySelectAnimator.Play("CountrySelect_PopOut");
		}

		private void OnSiteConfigResponse(bool aFormSubmit, IGetRegistrationConfigurationResult aResult)
		{
			if (!IsPanelActive(adultAccountController.gameObject) || Equals(null) || base.gameObject == null || !aResult.Success)
			{
				return;
			}
			aResult.Configuration.GetRegistrationAgeBand(enterBirthdateController.GetAge(), "en-US", delegate(IGetAgeBandResult ageBandResult)
			{
				if (IsPanelActive(adultAccountController.gameObject) && ageBandResult.Success)
				{
					ageBand = ageBandResult.AgeBand;
					if (ageBand.AgeBandType == AgeBandType.Child)
					{
						if (IsPanelActive(childAccountController.gameObject))
						{
							childAccountController.UpdateRegistrationStrings(countryCode, countryName, ageBand);
						}
						else
						{
							adultAccountController.Hide();
							childAccountController.Show(birthYear, birthMonth, birthDay, ageBand, countryCode, countryName, this);
						}
					}
					else if (IsPanelActive(adultAccountController.gameObject))
					{
						adultAccountController.UpdateRegistrationStrings(countryCode, countryName, ageBand);
					}
					else
					{
						childAccountController.Hide();
						adultAccountController.Show(birthYear, birthMonth, birthDay, ageBand, countryCode, countryName, originatingSource == RegisterFlowOriginatingSource.PARENTAL_CONTROLS, this);
					}
				}
			});
		}

		public void GotoAvatar()
		{
			gotoAvatarCreator();
		}

		private void populateCountryScrollView()
		{
			List<string> list = countries.Keys.ToList();
			list.Sort();
			foreach (string item in list)
			{
				CountryItem countryItem = new CountryItem(CountryItem, item, countries[item], this);
				countryItem.Id = CountryScrollView.Add(countryItem, false);
			}
		}

		private void gotoProfile(bool aShowAcceptedPopup = false)
		{
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Profile/ProfileScreen", new TransitionInLeft());
			if (!aShowAcceptedPopup)
			{
				navigationRequest.AddData("mode", ProfileController.PROFILE_MODES.INFO_UPDATE);
			}
			navigationRequest.PopLastRequest = true;
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
		}

		private void gotoAvatarCreator()
		{
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/AvatarEditor/AvatarEditorScreen", new TransitionAnimations(false, "FromCreateAccount", "ToAvatarEditor"));
			navigationRequest.AddData("mode", AvatarEditorController.EDITOR_MODES.CREATOR);
			navigationRequest.PopLastRequest = true;
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
			PlaySound("NextPage/AfterTermsGuidelines");
		}

		private void gotoChat(bool aShowNotificationPopup = false)
		{
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Conversations/ConversationsScreen", new TransitionNone());
			if (aShowNotificationPopup)
			{
				navigationRequest.AddData("regDone", true);
			}
			navigationRequest.PopLastRequest = true;
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
			LoginController.StopLoginMusic();
		}

		private bool IsPanelActive(GameObject aPanel)
		{
			return aPanel.activeSelf && Mathf.Approximately(aPanel.transform.localPosition.z, 0f);
		}

		private void PlaySound(string soundName)
		{
			Singleton<SoundManager>.Instance.PlaySoundEvent("UI/Registration/" + soundName);
		}
	}
}
