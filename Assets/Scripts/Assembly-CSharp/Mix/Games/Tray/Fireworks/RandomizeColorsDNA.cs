using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class RandomizeColorsDNA : RandomizeColors
	{
		public override void SetColor()
		{
			float t;
			int whichColor;
			if (mFireworksGame != null)
			{
				t = GetComponentInParent<FireworksGame>().GetColorLerp(Colors.Length, out whichColor);
			}
			else
			{
				whichColor = Random.Range(0, Colors.Length);
				t = Random.Range(0f, 1f);
			}
			int num = (whichColor + 1) % Colors.Length;
			Color startColor = Color.Lerp(Colors[whichColor], Colors[num], t);
			AffectedSystems[0].startColor = startColor;
			AffectedSystems[2].startColor = startColor;
			whichColor = (whichColor + 2) % Colors.Length;
			num = (whichColor + 1) % Colors.Length;
			startColor = Color.Lerp(Colors[whichColor], Colors[num], t);
			AffectedSystems[1].startColor = startColor;
			AffectedSystems[3].startColor = startColor;
		}
	}
}
