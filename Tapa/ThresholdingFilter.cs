using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tapa
{
	// http://hikipuro.hatenadiary.jp/entry/2015/08/10/085645
	// 2値化
	class ThresholdingFilter
	{
		// 引数で渡されたビットマップ画像を 2 値化します
		public static Bitmap Apply(Bitmap source, byte threshold = 128)
		{
			// ビットマップ画像から全てのピクセルを抜き出す
			PixelManipulator s = PixelManipulator.LoadBitmap(source);
			PixelManipulator d = s.Clone();

			// 全てのピクセルを巡回する
			s.EachPixel((x, y) => {
				// いったんグレースケールにする
				byte r = s.R(x, y);
				byte g = s.G(x, y);
				byte b = s.B(x, y);
				byte color = _GrayScale(r, g, b);

				// 2 値化する
				color = _Thresholding(color, threshold);
				d.SetPixel(x, y, color, color, color);
			});

			// 新しいビットマップ画像を作成して、ピクセルをセットする
			return d.CreateBitmap();
		}

		// グレースケール化する処理
		private static byte _GrayScale(byte r, byte g, byte b)
		{
			return (byte)(0.29891f * r + 0.58661f * g + 0.11448f * b);
		}

		// 2 値化する処理
		private static byte _Thresholding(byte color, byte threshold)
		{
			return color > threshold ? (byte)255 : (byte)0;
		}
	}
}
