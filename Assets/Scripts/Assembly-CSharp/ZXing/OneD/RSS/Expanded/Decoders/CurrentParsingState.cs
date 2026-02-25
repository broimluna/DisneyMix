namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal sealed class CurrentParsingState
	{
		private enum State
		{
			NUMERIC = 0,
			ALPHA = 1,
			ISO_IEC_646 = 2
		}

		private int position;

		private State encoding;

		internal CurrentParsingState()
		{
			position = 0;
			encoding = State.NUMERIC;
		}

		internal int getPosition()
		{
			return position;
		}

		internal void setPosition(int position)
		{
			this.position = position;
		}

		internal void incrementPosition(int delta)
		{
			position += delta;
		}

		internal bool isAlpha()
		{
			return encoding == State.ALPHA;
		}

		internal bool isNumeric()
		{
			return encoding == State.NUMERIC;
		}

		internal bool isIsoIec646()
		{
			return encoding == State.ISO_IEC_646;
		}

		internal void setNumeric()
		{
			encoding = State.NUMERIC;
		}

		internal void setAlpha()
		{
			encoding = State.ALPHA;
		}

		internal void setIsoIec646()
		{
			encoding = State.ISO_IEC_646;
		}
	}
}
