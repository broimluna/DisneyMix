using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	[Serializable]
	public class PatternTemplate : ICloneable
	{
		private const string KEYWORD_PLATFORM = "platform";

		private const string KEYWORD_DECOY = "decoy";

		private const string KEYWORD_REFLECT_X = "reflect_x";

		private const string KEYWORD_REFLECT_Y = "reflect_y";

		private const string KEYWORD_ROTATE = "rotate";

		private const string KEYWORD_WEIGHT = "weight";

		private const string KEYWORD_OVERLAP = "overlap";

		private const string KEYWORD_INCLUDE = "include_after";

		private const string KEYWORD_EXCLUDE = "exclude_after";

		private const string KEYWORD_NEXT_PATTERN = "next_pattern";

		public List<PlatformInfo> PathPlatforms = new List<PlatformInfo>();

		public List<PlatformInfo> DecoyPlatforms = new List<PlatformInfo>();

		public bool CanReflectX { get; set; }

		public bool CanReflectY { get; set; }

		public bool CanRotate90 { get; set; }

		public float Overlap { get; set; }

		public int Weight { get; set; }

		public int IncludeAfter { get; set; }

		public int ExcludeAfter { get; set; }

		public string Name { get; set; }

		public float NextPatternTime { get; set; }

		public PatternTemplate()
		{
			CanReflectX = false;
			CanReflectY = false;
			Name = "Pattern";
		}

		public PatternTemplate(TextAsset patternFile)
		{
			LoadTemplateFromFile(patternFile);
		}

		public PatternTemplate(string patternString)
		{
			LoadPatternTemplateFromString(patternString, string.Empty);
		}

		public PatternTemplate(PatternData patternData)
		{
			Name = patternData.uid;
			Overlap = (float)patternData.overlap;
			CanReflectX = patternData.reflect_x;
			CanReflectY = patternData.reflect_y;
			CanRotate90 = patternData.rotate;
			Weight = patternData.weight;
			IncludeAfter = patternData.include_after;
			NextPatternTime = (float)patternData.next_pattern;
			PathPlatforms = new List<PlatformInfo>();
			DecoyPlatforms = new List<PlatformInfo>();
			foreach (PlatformData item in patternData.platform_config)
			{
				if (item.is_decoy)
				{
					DecoyPlatforms.Add(item.ToPlatformInfo());
				}
				else
				{
					PathPlatforms.Add(item.ToPlatformInfo());
				}
			}
		}

		public List<Coordinate2D> GetFootprint(Coordinate2D origin)
		{
			List<Coordinate2D> list = new List<Coordinate2D>(PathPlatforms.Count + DecoyPlatforms.Count);
			for (int i = 0; i < PathPlatforms.Count; i++)
			{
				list.Add(PathPlatforms[i].GridCoordinates + origin);
			}
			for (int j = 0; j < DecoyPlatforms.Count; j++)
			{
				list.Add(DecoyPlatforms[j].GridCoordinates + origin);
			}
			return list;
		}

		public string GetTemplateAsString()
		{
			string text = string.Empty;
			for (int i = 0; i < DecoyPlatforms.Count; i++)
			{
				if (DecoyPlatforms[i].PathOrder < 0)
				{
					text = text + "decoy " + DecoyPlatforms[i].GetSerialized() + Environment.NewLine;
				}
			}
			for (int j = 0; j < PathPlatforms.Count; j++)
			{
				text = text + "platform " + PathPlatforms[j].GetSerialized() + Environment.NewLine;
				for (int k = 0; k < DecoyPlatforms.Count; k++)
				{
					if (DecoyPlatforms[k].PathOrder == PathPlatforms[j].PathOrder)
					{
						text = text + "decoy " + DecoyPlatforms[k].GetSerialized() + Environment.NewLine;
					}
				}
			}
			string text2 = text;
			text = text2 + "overlap " + Overlap + Environment.NewLine;
			text2 = text;
			text = text2 + "reflect_x " + CanReflectX + Environment.NewLine;
			text2 = text;
			text = text2 + "reflect_y " + CanReflectY + Environment.NewLine;
			text2 = text;
			text = text2 + "rotate " + CanRotate90 + Environment.NewLine;
			if (Weight > 0)
			{
				text2 = text;
				text = text2 + "weight " + Weight + Environment.NewLine;
			}
			if (IncludeAfter > 0)
			{
				text2 = text;
				text = text2 + "include_after " + IncludeAfter + Environment.NewLine;
			}
			if (ExcludeAfter > 0)
			{
				text2 = text;
				text = text2 + "exclude_after " + ExcludeAfter + Environment.NewLine;
			}
			text2 = text;
			return text2 + "next_pattern " + NextPatternTime + Environment.NewLine;
		}

		private void LoadTemplateFromFile(TextAsset patternFile)
		{
			LoadPatternTemplateFromString(patternFile.text, patternFile.name);
		}

		private void LoadPatternTemplateFromString(string text, string patternName = "")
		{
			List<string> list = new List<string>(text.Split(new string[3] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
			Name = patternName;
			Weight = -1;
			int num = -1;
			try
			{
				for (int i = 0; i < list.Count; i++)
				{
					List<string> list2 = new List<string>(list[i].Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
					if (list2.Count <= 0)
					{
						continue;
					}
					switch (list2[0].ToLower())
					{
					case "platform":
					case "decoy":
					{
						Coordinate2D gridCoordinates = new Coordinate2D(int.Parse(list2[1]), int.Parse(list2[2]));
						PlatformType type = (PlatformType)(int)Enum.Parse(typeof(PlatformType), list2[4]);
						int rotation = int.Parse(list2[3]);
						float num2 = float.Parse(list2[5]);
						float exitTime = num2 + float.Parse(list2[6]);
						FenceInfo fenceInfo = new FenceInfo();
						if (list2.Count > 7)
						{
							fenceInfo.SetFromBits(list2[7]);
						}
						PlatformInfo platformInfo = new PlatformInfo();
						platformInfo.GridCoordinates = gridCoordinates;
						platformInfo.Type = type;
						platformInfo.EnterTime = num2;
						platformInfo.ExitTime = exitTime;
						platformInfo.Rotation = rotation;
						platformInfo.Fences = fenceInfo;
						if (list2[0].ToLower() == "platform")
						{
							num = (platformInfo.PathOrder = num + 1);
							PathPlatforms.Add(platformInfo);
						}
						else
						{
							platformInfo.PathOrder = num;
							DecoyPlatforms.Add(platformInfo);
						}
						break;
					}
					case "reflect_x":
						CanReflectX = bool.Parse(list2[1]);
						break;
					case "reflect_y":
						CanReflectY = bool.Parse(list2[1]);
						break;
					case "rotate":
						CanRotate90 = bool.Parse(list2[1]);
						break;
					case "overlap":
						Overlap = float.Parse(list2[1]);
						break;
					case "weight":
						Weight = int.Parse(list2[1]);
						break;
					case "include_after":
						IncludeAfter = int.Parse(list2[1]);
						break;
					case "exclude_after":
						ExcludeAfter = int.Parse(list2[1]);
						break;
					case "next_pattern":
						NextPatternTime = float.Parse(list2[1]);
						break;
					}
				}
			}
			catch (Exception exception)
			{
				Log.Exception("Invalid data in " + patternName, exception);
			}
		}

		public int GetWeightForPatternNumber(int patternNumber)
		{
			if (patternNumber > IncludeAfter && (ExcludeAfter <= 0 || patternNumber < ExcludeAfter))
			{
				return Weight;
			}
			return 0;
		}

		public static PatternTemplate CreateRotatedPattern(PatternTemplate originalPattern)
		{
			PatternTemplate patternTemplate = new PatternTemplate();
			for (int i = 0; i < originalPattern.PathPlatforms.Count; i++)
			{
				patternTemplate.PathPlatforms.Add(PlatformInfo.Rotate(originalPattern.PathPlatforms[i]));
			}
			for (int j = 0; j < originalPattern.DecoyPlatforms.Count; j++)
			{
				patternTemplate.DecoyPlatforms.Add(PlatformInfo.Rotate(originalPattern.DecoyPlatforms[j]));
			}
			patternTemplate.Overlap = originalPattern.Overlap;
			patternTemplate.Name = originalPattern.Name + " Rotated";
			patternTemplate.CanReflectX = originalPattern.CanReflectY;
			patternTemplate.CanReflectY = originalPattern.CanReflectX;
			patternTemplate.Weight = originalPattern.Weight;
			patternTemplate.IncludeAfter = originalPattern.IncludeAfter;
			patternTemplate.ExcludeAfter = originalPattern.ExcludeAfter;
			patternTemplate.CanRotate90 = false;
			patternTemplate.NextPatternTime = originalPattern.NextPatternTime;
			return patternTemplate;
		}

		public static PatternTemplate CreateReflectedPattern(PatternTemplate originalPattern, bool reflectX, bool reflectY)
		{
			PatternTemplate patternTemplate = new PatternTemplate();
			for (int i = 0; i < originalPattern.PathPlatforms.Count; i++)
			{
				patternTemplate.PathPlatforms.Add(PlatformInfo.Reflect(originalPattern.PathPlatforms[i], reflectX, reflectY));
			}
			for (int j = 0; j < originalPattern.DecoyPlatforms.Count; j++)
			{
				patternTemplate.DecoyPlatforms.Add(PlatformInfo.Reflect(originalPattern.DecoyPlatforms[j], reflectX, reflectY));
			}
			patternTemplate.Overlap = originalPattern.Overlap;
			patternTemplate.CanReflectX = originalPattern.CanReflectX;
			patternTemplate.CanReflectY = originalPattern.CanReflectY;
			patternTemplate.IncludeAfter = originalPattern.IncludeAfter;
			patternTemplate.ExcludeAfter = originalPattern.ExcludeAfter;
			patternTemplate.NextPatternTime = originalPattern.NextPatternTime;
			patternTemplate.Weight = originalPattern.Weight;
			if (reflectX)
			{
				patternTemplate.Name = originalPattern.Name + " Reflect X";
				patternTemplate.CanReflectX = false;
			}
			if (reflectY)
			{
				patternTemplate.Name = originalPattern.Name + " Reflect Y";
				patternTemplate.CanReflectY = false;
			}
			return patternTemplate;
		}

		public PatternData ToPatternData()
		{
			return new PatternData(this);
		}

		public string ToJson()
		{
			return JsonMapper.ToJson(ToPatternData());
		}

		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}
