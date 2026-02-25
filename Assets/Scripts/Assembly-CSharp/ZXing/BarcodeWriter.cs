using UnityEngine;

namespace ZXing
{
	public class BarcodeWriter : BarcodeWriterGeneric<Color32[]>, IBarcodeWriter
	{
		public BarcodeWriter()
		{
			base.Renderer = new Color32Renderer();
		}
	}
}
