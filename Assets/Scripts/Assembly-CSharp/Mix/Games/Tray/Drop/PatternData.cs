using System;
using System.Collections.Generic;
using LitJson;

namespace Mix.Games.Tray.Drop
{
	public class PatternData
	{
		public string uid { get; set; }

		public double overlap { get; set; }

		public bool reflect_x { get; set; }

		public bool reflect_y { get; set; }

		public bool rotate { get; set; }

		public int weight { get; set; }

		public int include_after { get; set; }

		public int exclude_after { get; set; }

		public double next_pattern { get; set; }

		public List<PlatformData> platform_config { get; set; }

		public string Name
		{
			get
			{
				return uid;
			}
		}

		public PatternData()
		{
		}

		public PatternData(PatternTemplate pattern)
		{
			uid = pattern.Name;
			overlap = pattern.Overlap;
			reflect_x = pattern.CanReflectX;
			reflect_y = pattern.CanReflectY;
			rotate = pattern.CanRotate90;
			weight = pattern.Weight;
			include_after = pattern.IncludeAfter;
			next_pattern = pattern.NextPatternTime;
			platform_config = new List<PlatformData>();
			foreach (PlatformInfo pathPlatform in pattern.PathPlatforms)
			{
				PlatformData item = new PlatformData(pathPlatform, false);
				platform_config.Add(item);
			}
			foreach (PlatformInfo decoyPlatform in pattern.DecoyPlatforms)
			{
				PlatformData item2 = new PlatformData(decoyPlatform, true);
				platform_config.Add(item2);
			}
		}

		public PatternData(List<string> values)
		{
			uid = values[0];
			overlap = double.Parse(values[1]);
			reflect_x = Convert.ToBoolean(values[2]);
			reflect_y = Convert.ToBoolean(values[3]);
			rotate = Convert.ToBoolean(values[4]);
			weight = Convert.ToInt16(values[5]);
			include_after = Convert.ToInt16(values[6]);
			next_pattern = double.Parse(values[7]);
			platform_config = JsonMapper.ToObject<List<PlatformData>>(values[8]);
		}

		public int GetWeightForPatternNumber(int patternNumber)
		{
			if (patternNumber > include_after)
			{
				return weight;
			}
			return 0;
		}

		public PatternTemplate ToPatternTemplate()
		{
			return new PatternTemplate(this);
		}

		public string GetPlatformConfigAsJson()
		{
			return JsonMapper.ToJson(platform_config);
		}

		public Dictionary<string, object> GetValues()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("uid", uid);
			dictionary.Add("overlap", overlap);
			dictionary.Add("reflect_x", reflect_x);
			dictionary.Add("reflect_y", reflect_y);
			dictionary.Add("rotate", rotate);
			dictionary.Add("weight", weight);
			dictionary.Add("include_after", include_after);
			dictionary.Add("next_pattern", next_pattern);
			dictionary.Add("platform_config", GetPlatformConfigAsJson());
			return dictionary;
		}
	}
}
