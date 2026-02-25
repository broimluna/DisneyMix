using System.Collections.Generic;

namespace ZXing.OneD.RSS.Expanded
{
	internal sealed class ExpandedRow
	{
		internal List<ExpandedPair> Pairs { get; private set; }

		internal int RowNumber { get; private set; }

		internal bool IsReversed { get; private set; }

		internal ExpandedRow(List<ExpandedPair> pairs, int rowNumber, bool wasReversed)
		{
			Pairs = new List<ExpandedPair>(pairs);
			RowNumber = rowNumber;
			IsReversed = wasReversed;
		}

		internal bool IsEquivalent(List<ExpandedPair> otherPairs)
		{
			return Pairs.Equals(otherPairs);
		}

		public override string ToString()
		{
			return string.Concat("{ ", Pairs, " }");
		}

		public override bool Equals(object o)
		{
			if (!(o is ExpandedRow))
			{
				return false;
			}
			ExpandedRow expandedRow = (ExpandedRow)o;
			return Pairs.Equals(expandedRow.Pairs) && IsReversed == expandedRow.IsReversed;
		}

		public override int GetHashCode()
		{
			return Pairs.GetHashCode() ^ IsReversed.GetHashCode();
		}
	}
}
