namespace ZXing.OneD.RSS.Expanded
{
	internal sealed class ExpandedPair
	{
		internal bool MayBeLast { get; private set; }

		internal DataCharacter LeftChar { get; private set; }

		internal DataCharacter RightChar { get; private set; }

		internal FinderPattern FinderPattern { get; private set; }

		public bool MustBeLast
		{
			get
			{
				return RightChar == null;
			}
		}

		internal ExpandedPair(DataCharacter leftChar, DataCharacter rightChar, FinderPattern finderPattern, bool mayBeLast)
		{
			LeftChar = leftChar;
			RightChar = rightChar;
			FinderPattern = finderPattern;
			MayBeLast = mayBeLast;
		}

		public override string ToString()
		{
			return string.Concat("[ ", LeftChar, " , ", RightChar, " : ", (FinderPattern != null) ? FinderPattern.Value.ToString() : "null", " ]");
		}

		public override bool Equals(object o)
		{
			if (!(o is ExpandedPair))
			{
				return false;
			}
			ExpandedPair expandedPair = (ExpandedPair)o;
			return EqualsOrNull(LeftChar, expandedPair.LeftChar) && EqualsOrNull(RightChar, expandedPair.RightChar) && EqualsOrNull(FinderPattern, expandedPair.FinderPattern);
		}

		private static bool EqualsOrNull(object o1, object o2)
		{
			return (o1 != null) ? o1.Equals(o2) : (o2 == null);
		}

		public override int GetHashCode()
		{
			return hashNotNull(LeftChar) ^ hashNotNull(RightChar) ^ hashNotNull(FinderPattern);
		}

		private static int hashNotNull(object o)
		{
			return (o != null) ? o.GetHashCode() : 0;
		}
	}
}
