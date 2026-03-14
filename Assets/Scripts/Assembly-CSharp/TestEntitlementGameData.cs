using System;
using System.IO;
using LitJson;
using Mix.Games.Data;

[Serializable]
public class TestEntitlementGameData : IEntitlementGameData
{
	private const string BASE_URL = "{0}/en_US/{0}_{1}";

	private const int DEFAULT_ORDER = 1000;

	private const int DEFAULT_POST_HEIGHT = 240;

	private const int DEFAULT_RESULTS_HEIGHT = 220;

	private const int DEFAULT_LOGO_HEIGHT = 128;

	private const int DEFAULT_THUMB_HEIGHT = 128;

	public string name { get; set; }

	public string uid { get; set; }

	public string thumb { get; set; }

	public string logo { get; set; }

	public string hd { get; set; }

	public string post { get; set; }

	public string result { get; set; }

	public string pause_img { get; set; }

	public string attempts { get; set; }

	public string base_url { get; set; }

	public string duration { get; set; }

	public int post_height { get; set; }

	public int logo_height { get; set; }

	public int results_height { get; set; }

	public int thumb_height { get; set; }

	public int order { get; set; }

	public TestEntitlementGameData()
	{
	}

	public TestEntitlementGameData(string aUnderscoreName, string aUid, bool aUnlimitedAttempts, int aOrder = 1000, int aPostHeight = 240, int aResultsHeight = 220)
	{
		name = aUnderscoreName;
		uid = aUid;
		SetAttempts(aUnlimitedAttempts);
		order = aOrder;
		post_height = aPostHeight;
		results_height = aResultsHeight;
		logo_height = 128;
		thumb_height = 128;
		thumb = MakeContentDataString(name, "thumb");
		logo = MakeContentDataString(name, "logo");
		hd = MakeContentDataString(name, string.Empty);
		post = MakeContentDataString(name, "postobject");
		result = MakeContentDataString(name, "resultsobject");
		pause_img = MakeContentDataString(name, "paused");
		base_url = string.Format("{0}/en_US", name);
	}

	public string ToJson()
	{
		StringWriter stringWriter = new StringWriter();
		JsonWriter jsonWriter = new JsonWriter(stringWriter);
		jsonWriter.Validate = false;
		jsonWriter.PrettyPrint = true;
		JsonMapper.ToJson(this, jsonWriter);
		return stringWriter.ToString();
	}

	public static TestEntitlementGameData FromJson(string aJson)
	{
		return JsonMapper.ToObject<TestEntitlementGameData>(aJson);
	}

	public void SetAttempts(bool aUnlimitedAttempts)
	{
		attempts = ((!aUnlimitedAttempts) ? "1" : "0");
	}

	private string MakeContentDataString(string aName, string aDataType)
	{
		return string.Format("{0}/en_US/{0}_{1}", aName, aDataType);
	}

	public string GetName()
	{
		return name;
	}

	public string GetHd()
	{
		return hd;
	}

	public string GetUid()
	{
		return uid;
	}

	public string GetLogo()
	{
		return logo;
	}

	public string GetPost()
	{
		return post;
	}

	public string GetResult()
	{
		return result;
	}

	public string GetPauseImage()
	{
		return pause_img;
	}

	public string GetThumbImage()
	{
		return thumb;
	}

	public string GetAttempts()
	{
		return attempts;
	}

	public string GetBaseUrl()
	{
		return base_url;
	}

	public string GetDuration()
	{
		return duration;
	}

	public int GetPostHeight()
	{
		return post_height;
	}

	public int GetLogoHeight()
	{
		return logo_height;
	}

	public int GetResultsHeight()
	{
		return results_height;
	}

	public int GetThumbHeight()
	{
		return thumb_height;
	}
}
