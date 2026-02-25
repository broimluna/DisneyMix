using System;
using System.Globalization;
using System.Text;
using UnityEngine;
using ZXing.Common;

namespace ZXing.Rendering
{
	public class SvgRenderer : IBarcodeRenderer<SvgRenderer.SvgImage>
	{
		public class SvgImage
		{
			private readonly StringBuilder content;

			public string Content
			{
				get
				{
					return content.ToString();
				}
				set
				{
					content.Length = 0;
					if (value != null)
					{
						content.Append(value);
					}
				}
			}

			public SvgImage()
			{
				content = new StringBuilder();
			}

			public SvgImage(string content)
			{
				this.content = new StringBuilder(content);
			}

			public override string ToString()
			{
				return content.ToString();
			}

			internal void AddHeader()
			{
				content.Append("<?xml version=\"1.0\" standalone=\"no\"?>");
				content.Append("<!-- Created with ZXing.Net (http://zxingnet.codeplex.com/) -->");
				content.Append("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");
			}

			internal void AddEnd()
			{
				content.Append("</svg>");
			}

			internal void AddTag(int displaysizeX, int displaysizeY, int viewboxSizeX, int viewboxSizeY, Color background, Color fill)
			{
				if (displaysizeX <= 0 || displaysizeY <= 0)
				{
					content.Append(string.Format("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.2\" baseProfile=\"tiny\" viewBox=\"0 0 {0} {1}\" viewport-fill=\"rgb({2})\" viewport-fill-opacity=\"{3}\" fill=\"rgb({4})\" fill-opacity=\"{5}\" {6}>", viewboxSizeX, viewboxSizeY, GetColorRgb(background), ConvertAlpha(background), GetColorRgb(fill), ConvertAlpha(fill), GetBackgroundStyle(background)));
				}
				else
				{
					content.Append(string.Format("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.2\" baseProfile=\"tiny\" viewBox=\"0 0 {0} {1}\" viewport-fill=\"rgb({2})\" viewport-fill-opacity=\"{3}\" fill=\"rgb({4})\" fill-opacity=\"{5}\" {6} width=\"{7}\" height=\"{8}\">", viewboxSizeX, viewboxSizeY, GetColorRgb(background), ConvertAlpha(background), GetColorRgb(fill), ConvertAlpha(fill), GetBackgroundStyle(background), displaysizeX, displaysizeY));
				}
			}

			internal void AddRec(int posX, int posY, int width, int height)
			{
				content.AppendFormat(CultureInfo.InvariantCulture, "<rect x=\"{0}\" y=\"{1}\" width=\"{2}\" height=\"{3}\"/>", posX, posY, width, height);
			}

			internal static double ConvertAlpha(Color32 alpha)
			{
				return Math.Round((double)(int)alpha.a / 255.0, 2);
			}

			internal static string GetBackgroundStyle(Color32 color)
			{
				double num = ConvertAlpha(color);
				return string.Format("style=\"background-color:rgb({0},{1},{2});background-color:rgba({3});\"", color.r, color.g, color.b, num);
			}

			internal static string GetColorRgb(Color32 color)
			{
				return color.r + "," + color.g + "," + color.b;
			}

			internal static string GetColorRgba(Color32 color)
			{
				double num = ConvertAlpha(color);
				return color.r + "," + color.g + "," + color.b + "," + num;
			}
		}

		public Color32 Foreground { get; set; }

		public Color32 Background { get; set; }

		public SvgRenderer()
		{
			Foreground = new Color(0f, 0f, 0f, 255f);
			Background = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		public SvgImage Render(BitMatrix matrix, BarcodeFormat format, string content)
		{
			return Render(matrix, format, content, null);
		}

		public SvgImage Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
		{
			SvgImage svgImage = new SvgImage();
			Create(svgImage, matrix, format, content, options);
			return svgImage;
		}

		private void Create(SvgImage image, BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
		{
			if (matrix != null)
			{
				int width = matrix.Width;
				int height = matrix.Height;
				image.AddHeader();
				image.AddTag(0, 0, 10 + width, 10 + height, Background, Foreground);
				AppendDarkCell(image, matrix, 5, 5);
				image.AddEnd();
			}
		}

		private static void AppendDarkCell(SvgImage image, BitMatrix matrix, int offsetX, int offSetY)
		{
			if (matrix == null)
			{
				return;
			}
			int width = matrix.Width;
			int height = matrix.Height;
			BitMatrix bitMatrix = new BitMatrix(width, height);
			bool flag = false;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < width; i++)
			{
				int endPosX;
				for (int j = 0; j < height; j++)
				{
					if (bitMatrix[i, j])
					{
						continue;
					}
					bitMatrix[i, j] = true;
					if (matrix[i, j])
					{
						if (!flag)
						{
							num = i;
							num2 = j;
							flag = true;
						}
					}
					else if (flag)
					{
						FindMaximumRectangle(matrix, bitMatrix, num, num2, j, out endPosX);
						image.AddRec(num + offsetX, num2 + offSetY, endPosX - num + 1, j - num2);
						flag = false;
					}
				}
				if (flag)
				{
					FindMaximumRectangle(matrix, bitMatrix, num, num2, height, out endPosX);
					image.AddRec(num + offsetX, num2 + offSetY, endPosX - num + 1, height - num2);
					flag = false;
				}
			}
		}

		private static void FindMaximumRectangle(BitMatrix matrix, BitMatrix processed, int startPosX, int startPosY, int endPosY, out int endPosX)
		{
			endPosX = startPosX + 1;
			for (int i = startPosX + 1; i < matrix.Width; i++)
			{
				for (int j = startPosY; j < endPosY; j++)
				{
					if (!matrix[i, j])
					{
						return;
					}
				}
				endPosX = i;
				for (int k = startPosY; k < endPosY; k++)
				{
					processed[i, k] = true;
				}
			}
		}
	}
}
