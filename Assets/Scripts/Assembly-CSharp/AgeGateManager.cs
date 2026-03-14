using System;
using Disney.MobileNetwork;

public class AgeGateManager
{
	private static string PLAYER_PREF_SHOW_AGEGATE_KEY = "mobilenetwork.agegate.showAgeGate";

	private static string PLAYER_PREF_AGE_KEY = "mobilenetwork.agegate.age";

	private static int MINIMUM_COPPA_AGE = 13;

	private int m_age;

	public bool IsOfAge
	{
		get
		{
			if (m_age >= MINIMUM_COPPA_AGE)
			{
				return true;
			}
			return false;
		}
	}

	public AgeGateManager()
	{
		m_age = EncryptedPlayerPrefs.GetInt(PLAYER_PREF_AGE_KEY, -1);
	}

	public bool SetUsersAge(int age)
	{
		if (ValidateAge(age))
		{
			m_age = age;
			EncryptedPlayerPrefs.SetInt(PLAYER_PREF_SHOW_AGEGATE_KEY, 0);
			EncryptedPlayerPrefs.SetInt(PLAYER_PREF_AGE_KEY, age);
			return true;
		}
		return false;
	}

	public bool ValidateAge(int age)
	{
		if (age >= 0 && age <= 99)
		{
			return true;
		}
		return false;
	}

	public static bool ShouldShowAgeGate()
	{
		return Convert.ToBoolean(EncryptedPlayerPrefs.GetInt(PLAYER_PREF_SHOW_AGEGATE_KEY, 1));
	}
}
