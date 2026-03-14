using UnityEngine;
using ZXing.Common;

namespace ZXing
{
	public interface IBarcodeWriter
	{
		BitMatrix Encode(string contents);

		Color32[] Write(string contents);

		Color32[] Write(BitMatrix matrix);
	}
}
