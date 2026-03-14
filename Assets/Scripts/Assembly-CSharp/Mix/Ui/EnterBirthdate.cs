using System;
using System.Collections.Generic;
using Disney.Mix.SDK;
using Disney.MobileNetwork;
using Disney.Native;
using Mix.Connectivity;
using Mix.Localization;
using Mix.Session;
using Mix.User;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class EnterBirthdate : MonoBehaviour
	{
		private const float AGE_MOVEMENT_LOW_THRESHOLD = 9f;

		private const float DAY_OR_MONTH_MOVEMENT_LOW_THRESHOLD = 12f;

		private const float SCROLL_THRESHOLD_SCALE = 1.2f;

		private const int MONTH_WITH_31_DAYS = 31;

		private const int MONTH_WITH_30_DAYS = 30;

		private const int MONTH_LEAP_FEB = 29;

		private const int MONTH_REGULAR_FEB = 28;

		private const string ageScrollingEvent = "UI/Registration/AgeScrolling";

		private const string monthScrollingEvent = "UI/Registration/BirthdayScrolling/Month";

		private const string birthdayScrollingEvent = "UI/Registration/BirthdayScrolling/Day";

		public IAgeBand AgeBand;

		public Button EnterBirthdateButton;

		public Button BackButton;

		public GameObject DisplayNameGameObject;

		public GameObject DisplayNameErrorMessageGameObject;

		public GameObject ErrorMessageGameObject;

		public GameObject BackButtonGameObject;

		public GameObject DisabledMessageGameObject;

		public EnterBirthdateScroller AgeScroller;

		public EnterBirthdateScroller MonthScroller;

		public EnterBirthdateScroller DayScroller;

		public VerticalLayoutGroup AgeLayoutGroup;

		public VerticalLayoutGroup MonthLayoutGroup;

		public VerticalLayoutGroup DayLayoutGroup;

		private GameObject ageValue;

		private GameObject dayValue;

		private GameObject topBuffer;

		private GameObject bottomBuffer;

		private IEnterBirthdate caller;

		private bool errorDisplayName;

		private List<SpinnerItemObject> ageValues;

		private List<SpinnerItemObject> dayValues;

		private int month;

		private int day;

		private int year;

		private int age;

		private int currentMonthDays;

		private int yearsSince1900;

		private bool submittedBirthday;

		private bool inMissingInfoFlow;

		private IRegistrationProfile profileInfo;

		private IRegistrationConfiguration siteConfiguration;

		public int GetAge()
		{
			return age;
		}

		private void Start()
		{
			if (!PoisonFlowVariable.GetBirthdate().Equals(DateTime.MinValue))
			{
				AgeLayoutGroup.enabled = false;
				DayLayoutGroup.enabled = false;
				Canvas.ForceUpdateCanvases();
				AgeLayoutGroup.enabled = true;
				DayLayoutGroup.enabled = true;
			}
			EnterBirthdateScroller ageScroller = AgeScroller;
			ageScroller.OnDragStateChanged = (EnterBirthdateScroller.OnDragCallback)Delegate.Remove(ageScroller.OnDragStateChanged, new EnterBirthdateScroller.OnDragCallback(OnScrollerDragState));
			EnterBirthdateScroller ageScroller2 = AgeScroller;
			ageScroller2.OnDragStateChanged = (EnterBirthdateScroller.OnDragCallback)Delegate.Combine(ageScroller2.OnDragStateChanged, new EnterBirthdateScroller.OnDragCallback(OnScrollerDragState));
			EnterBirthdateScroller ageScroller3 = AgeScroller;
			ageScroller3.OnValueChanged = (EnterBirthdateScroller.OnValueChangedCallback)Delegate.Remove(ageScroller3.OnValueChanged, new EnterBirthdateScroller.OnValueChangedCallback(OnAgeChanged));
			EnterBirthdateScroller ageScroller4 = AgeScroller;
			ageScroller4.OnValueChanged = (EnterBirthdateScroller.OnValueChangedCallback)Delegate.Combine(ageScroller4.OnValueChanged, new EnterBirthdateScroller.OnValueChangedCallback(OnAgeChanged));
			EnterBirthdateScroller monthScroller = MonthScroller;
			monthScroller.OnDragStateChanged = (EnterBirthdateScroller.OnDragCallback)Delegate.Remove(monthScroller.OnDragStateChanged, new EnterBirthdateScroller.OnDragCallback(OnScrollerDragState));
			EnterBirthdateScroller monthScroller2 = MonthScroller;
			monthScroller2.OnDragStateChanged = (EnterBirthdateScroller.OnDragCallback)Delegate.Combine(monthScroller2.OnDragStateChanged, new EnterBirthdateScroller.OnDragCallback(OnScrollerDragState));
			EnterBirthdateScroller monthScroller3 = MonthScroller;
			monthScroller3.OnValueChanged = (EnterBirthdateScroller.OnValueChangedCallback)Delegate.Remove(monthScroller3.OnValueChanged, new EnterBirthdateScroller.OnValueChangedCallback(OnMonthChanged));
			EnterBirthdateScroller monthScroller4 = MonthScroller;
			monthScroller4.OnValueChanged = (EnterBirthdateScroller.OnValueChangedCallback)Delegate.Combine(monthScroller4.OnValueChanged, new EnterBirthdateScroller.OnValueChangedCallback(OnMonthChanged));
			EnterBirthdateScroller dayScroller = DayScroller;
			dayScroller.OnDragStateChanged = (EnterBirthdateScroller.OnDragCallback)Delegate.Remove(dayScroller.OnDragStateChanged, new EnterBirthdateScroller.OnDragCallback(OnScrollerDragState));
			EnterBirthdateScroller dayScroller2 = DayScroller;
			dayScroller2.OnDragStateChanged = (EnterBirthdateScroller.OnDragCallback)Delegate.Combine(dayScroller2.OnDragStateChanged, new EnterBirthdateScroller.OnDragCallback(OnScrollerDragState));
			EnterBirthdateScroller dayScroller3 = DayScroller;
			dayScroller3.OnValueChanged = (EnterBirthdateScroller.OnValueChangedCallback)Delegate.Remove(dayScroller3.OnValueChanged, new EnterBirthdateScroller.OnValueChangedCallback(OnDayChanged));
			EnterBirthdateScroller dayScroller4 = DayScroller;
			dayScroller4.OnValueChanged = (EnterBirthdateScroller.OnValueChangedCallback)Delegate.Combine(dayScroller4.OnValueChanged, new EnterBirthdateScroller.OnValueChangedCallback(OnDayChanged));
		}

		private void OnDestroy()
		{
		}

		public void Show(IEnterBirthdate aCaller, IRegistrationProfile aProfileInfo)
		{
			if (topBuffer == null)
			{
				topBuffer = Resources.Load<GameObject>("Prefabs/Ui/CreateAccount/TopBuffer");
			}
			if (bottomBuffer == null)
			{
				bottomBuffer = Resources.Load<GameObject>("Prefabs/Ui/CreateAccount/BottomBuffer");
			}
			if (ageValue == null)
			{
				ageValue = Resources.Load<GameObject>("Prefabs/Ui/CreateAccount/AgeBtn");
			}
			if (dayValue == null)
			{
				dayValue = Resources.Load<GameObject>("Prefabs/Ui/CreateAccount/DayBtn");
			}
			if (currentMonthDays == 0)
			{
				currentMonthDays = 31;
			}
			if (yearsSince1900 == 0)
			{
				yearsSince1900 = DateTime.Now.Year - 1900;
			}
			if (ageValues == null)
			{
				ageValues = initVerticalLayoutGroup(AgeLayoutGroup, ageValue, yearsSince1900, true);
			}
			if (dayValues == null)
			{
				dayValues = initVerticalLayoutGroup(DayLayoutGroup, dayValue, currentMonthDays);
			}
			caller = aCaller;
			profileInfo = aProfileInfo;
			inMissingInfoFlow = profileInfo != null;
			submittedBirthday = false;
			setUiInteractable(true);
			base.gameObject.SetActive(true);
			if (AgeBand == null)
			{
				RegistrationConfigurationGetter registrationConfigurationGetter = new RegistrationConfigurationGetter(MonoSingleton<LoginManager>.Instance.KeychainData, Log.MixLogger, MonoSingleton<LoginManager>.Instance.StorageDir, ExternalizedConstants.GuestControllerUrl, MixSession.GuestControllerSpoofedIP, ExternalizedConstants.GuestControllerClientId, MonoSingleton<LoginManager>.Instance.CoroutineManager, ExternalizedConstants.MixPlatformServicesUrl, ExternalizedConstants.MixPlatformServicesCellophane);
				string countryCode = caller.GetCountryCode();
				if (countryCode != null && countryCode != string.Empty)
				{
					registrationConfigurationGetter.Get(countryCode, delegate(IGetRegistrationConfigurationResult x)
					{
						OnSiteConfigResponse(false, x);
					});
				}
				else
				{
					registrationConfigurationGetter.Get(delegate(IGetRegistrationConfigurationResult x)
					{
						OnSiteConfigResponse(false, x);
					});
				}
			}
			if (!PoisonFlowVariable.GetBirthdate().Equals(DateTime.MinValue))
			{
				DateTime birthdate = PoisonFlowVariable.GetBirthdate();
				year = birthdate.Year;
				month = birthdate.Month;
				day = birthdate.Day;
				age = computeAge(birthdate);
			}
			else if (age <= 0)
			{
				age = 0;
				year = DateTime.Now.Year;
				month = 1;
				day = 1;
			}
			IAgeBand ageBandInfo = PoisonFlowVariable.GetAgeBandInfo();
			if ((ageBandInfo != null && ageBandInfo.AgeBandType == AgeBandType.Child) || (ageBandInfo == null && !PoisonFlowVariable.GetBirthdate().Equals(DateTime.MinValue)))
			{
				DisabledMessageGameObject.SetActive(true);
				AgeScroller.Enable(false);
				MonthScroller.Enable(false);
				DayScroller.Enable(false);
			}
			AgeScroller.Init(age, 0, yearsSince1900, 9f, "UI/Registration/AgeScrolling");
			MonthScroller.Init(month, 1, 12, 9f, "UI/Registration/BirthdayScrolling/Month");
			DayScroller.Init(day, 1, currentMonthDays, 9f, "UI/Registration/BirthdayScrolling/Day");
			if (inMissingInfoFlow)
			{
				BackButtonGameObject.SetActive(false);
				Analytics.LogPageView("missing_birthday");
			}
			else
			{
				BackButtonGameObject.SetActive(true);
				Analytics.LogAgeGatePageView();
			}
		}

		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		private void OnScrollerDragState(bool aIsDragging)
		{
			if (age == 0)
			{
				EnterBirthdateButton.interactable = false;
			}
			else
			{
				EnterBirthdateButton.interactable = !aIsDragging;
			}
		}

		private List<SpinnerItemObject> initVerticalLayoutGroup(VerticalLayoutGroup aGroup, GameObject aBaseObject, int aEnd, bool startAtZero = false)
		{
			List<SpinnerItemObject> list = new List<SpinnerItemObject>();
			new SpinnerItemObject(aGroup.transform, 0, topBuffer);
			int num = 0;
			if (startAtZero)
			{
				list.Add(new SpinnerItemObject(aGroup.transform, 1, aBaseObject, "0"));
				num++;
			}
			for (int i = 1; i <= aEnd; i++)
			{
				list.Add(new SpinnerItemObject(aGroup.transform, i + num, aBaseObject, i.ToString()));
			}
			new SpinnerItemObject(aGroup.transform, aEnd + num + 1, bottomBuffer);
			return list;
		}

		private void determineYear()
		{
			year = DateTime.Now.Year - age;
			if (DateTime.Now.Month < month)
			{
				year--;
			}
			else if (DateTime.Now.Month == month && DateTime.Now.Day < day)
			{
				year--;
			}
		}

		private void determineDays()
		{
			if (DayScroller.CurValue > currentMonthDays)
			{
				DayScroller.CurValue = currentMonthDays;
			}
			day = DayScroller.CurValue;
		}

		private void recalculateDaysForMonth()
		{
			int num = DateTime.DaysInMonth(year, month);
			if (currentMonthDays != num)
			{
				currentMonthDays = num;
				resizeDayList(currentMonthDays);
				DayScroller.MaxValue = currentMonthDays;
				determineDays();
			}
		}

		private void resizeDayList(int aNewCount)
		{
			switch (aNewCount)
			{
			case 28:
				dayValues[28].SetActive(false);
				dayValues[29].SetActive(false);
				dayValues[30].SetActive(false);
				break;
			case 29:
				dayValues[28].SetActive(true);
				dayValues[29].SetActive(false);
				dayValues[30].SetActive(false);
				break;
			case 30:
				dayValues[28].SetActive(true);
				dayValues[29].SetActive(true);
				dayValues[30].SetActive(false);
				break;
			case 31:
				dayValues[28].SetActive(true);
				dayValues[29].SetActive(true);
				dayValues[30].SetActive(true);
				break;
			}
		}

		private int computeAge(DateTime aBirthdate)
		{
			return (int)((float)DateTime.Now.Subtract(aBirthdate).Days / 365.25f);
		}

		public void OnBirthdaySubmit()
		{
			ErrorMessageGameObject.SetActive(false);
			setUiInteractable(false);
			submittedBirthday = true;
			if (ClientValidateBirthdayValues())
			{
				ServerValidateValues();
				return;
			}
			Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_1_Error");
			setUiInteractable(true);
			submittedBirthday = false;
		}

		public void OnNavigateBack()
		{
			if (caller != null)
			{
				caller.OnNavigateBack();
			}
		}

		public bool ClientValidateBirthdayValues()
		{
			bool flag = age <= 0 || age > yearsSince1900;
			bool flag2 = month < 1 || month > 12;
			bool flag3 = day < 1 || day > 31;
			if (!flag2 && !flag && day > DateTime.DaysInMonth(year, month))
			{
				flag3 = true;
			}
			toggleErrorFields(flag3, flag2, flag);
			return !flag3 && !flag2 && !flag;
		}

		private void toggleErrorFields(bool errorDay, bool errorMonth, bool errorYear)
		{
			if (errorMonth || errorDay || errorYear)
			{
				ErrorMessageGameObject.GetComponent<Text>().text = Singleton<Localizer>.Instance.getString("customtokens.register.error_birthday_invalid");
				ErrorMessageGameObject.SetActive(true);
				if (MonoSingleton<NativeAccessiblityManager>.Instance.IsEnabled)
				{
					AccessibilityManager.Instance.Speak(ErrorMessageGameObject.GetComponent<Text>().text);
				}
			}
			else
			{
				ErrorMessageGameObject.SetActive(false);
			}
		}

		private void ServerValidateValues()
		{
			if (siteConfiguration == null)
			{
				RegistrationConfigurationGetter registrationConfigurationGetter = new RegistrationConfigurationGetter(MonoSingleton<LoginManager>.Instance.KeychainData, Log.MixLogger, MonoSingleton<LoginManager>.Instance.StorageDir, ExternalizedConstants.GuestControllerUrl, MixSession.GuestControllerSpoofedIP, ExternalizedConstants.GuestControllerClientId, MonoSingleton<LoginManager>.Instance.CoroutineManager, ExternalizedConstants.MixPlatformServicesUrl, ExternalizedConstants.MixPlatformServicesCellophane);
				registrationConfigurationGetter.Get(delegate(IGetRegistrationConfigurationResult x)
				{
					OnSiteConfigResponse(true, x);
				});
				return;
			}
			siteConfiguration.GetRegistrationAgeBand(age, "en-US", delegate(IGetAgeBandResult ageBandResult)
			{
				if (ageBandResult.Success)
				{
					AgeBand = ageBandResult.AgeBand;
					checkAgeBand();
				}
				else
				{
					if (!MonoSingleton<ConnectionManager>.Instance.IsConnected)
					{
						caller.ShowErrorOverlay("customtokens.login.error_no_internet", string.Empty);
					}
					else
					{
						caller.ShowErrorOverlay("customtokens.global.generic_error", string.Empty);
					}
					Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_1_Error");
					setUiInteractable(true);
					submittedBirthday = false;
				}
			});
		}

		private void OnSiteConfigResponse(bool aFormSubmit, IGetRegistrationConfigurationResult aResult)
		{
			if (this.IsNullOrDisposed() || base.gameObject == null)
			{
				return;
			}
			if (!aResult.Success)
			{
				caller.ShowErrorOverlay("customtokens.register.error_birthday_check", string.Empty);
				if (aFormSubmit)
				{
					Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_1_Error");
					setUiInteractable(true);
					submittedBirthday = false;
				}
				return;
			}
			aResult.Configuration.GetRegistrationAgeBand(age, "en-US", delegate(IGetAgeBandResult ageBandResult)
			{
				if (ageBandResult.Success)
				{
					siteConfiguration = aResult.Configuration;
					AgeBand = ageBandResult.AgeBand;
					caller.SetCountryCode(AgeBand.CountryCode);
					if (aFormSubmit && submittedBirthday)
					{
						checkAgeBand();
					}
				}
				else if (aFormSubmit)
				{
					if (!MonoSingleton<ConnectionManager>.Instance.IsConnected)
					{
						caller.ShowErrorOverlay("customtokens.login.error_no_internet", string.Empty);
					}
					else
					{
						caller.ShowErrorOverlay("customtokens.global.generic_error", string.Empty);
					}
					Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_1_Error");
					setUiInteractable(true);
					submittedBirthday = false;
				}
			});
		}

		private void checkAgeBand()
		{
			if (AgeBand.AgeBandType == AgeBandType.Child)
			{
				PoisonFlowVariable.SetBirthdate(year, month, day, AgeBand);
				Analytics.LogBirthdaySubmissionFail(age);
			}
			else
			{
				Analytics.LogBirthdaySubmissionPass(age);
			}
			caller.OnBirthdateEntered(AgeBand, year, month, day);
		}

		private void setUiInteractable(bool aInteractable)
		{
			if (age == 0)
			{
				EnterBirthdateButton.interactable = false;
			}
			else
			{
				EnterBirthdateButton.interactable = aInteractable;
			}
			BackButton.interactable = aInteractable;
		}

		private void OnAgeChanged(int aNewValue)
		{
			age = aNewValue;
			determineYear();
			recalculateDaysForMonth();
		}

		private void OnMonthChanged(int aNewValue)
		{
			month = aNewValue;
			determineYear();
			recalculateDaysForMonth();
		}

		private void OnDayChanged(int aNewValue)
		{
			determineDays();
			determineYear();
		}
	}
}
