using Mix.Games.Data;

public class HighFiveResponse : MixGameResponse
{
	public int TotalHits { get; set; }

	public int MaxStreak { get; set; }

	public int TotalScore { get; set; }

	public int Rating { get; set; }
}
