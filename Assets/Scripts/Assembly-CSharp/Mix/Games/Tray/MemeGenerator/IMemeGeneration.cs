namespace Mix.Games.Tray.MemeGenerator
{
	public interface IMemeGeneration
	{
		MemeGeneratorController GetMemeGeneratorController();

		void SetMemeImage(string aImageUrl);

		void HideMemeImage();

		void SetMemeCaption(Captions aCaptions);

		void QuitMemeGeneration();
	}
}
