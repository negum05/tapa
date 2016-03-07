using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tapa
{
	// ラプラシアンフィルタ
	// http://hikipuro.hatenadiary.jp/entry/2015/08/11/060733
	class LaplacianFilter
	{
		// 引数で渡されたビットマップ画像にラプラシアンフィルタを適用します
		public static Bitmap Apply(Bitmap source)
		{
			// ビットマップ画像から全てのピクセルを抜き出す
			PixelManipulator s = PixelManipulator.LoadBitmap(source);
			PixelManipulator d = s.Clone();

			// フィルタを作成する
			float[] filter = _CreateLaplacianFilter();

			// 全てのピクセルを巡回する
			s.EachPixel((x, y) => {
				byte r = _Laplacian(filter, s.RangeR(x, y, 1));
				byte g = _Laplacian(filter, s.RangeG(x, y, 1));
				byte b = _Laplacian(filter, s.RangeB(x, y, 1));
				d.SetPixel(x, y, r, g, b);
			});

			// 新しいビットマップ画像を作成して、ピクセルをセットする
			return d.CreateBitmap();
		}

		// フィルタを作成する
		private static float[] _CreateLaplacianFilter()
		{
			float[] filter = new float[] {
				1,  1, 1,
				1, -8, 1,
				1,  1, 1
			};
			return filter;
		}

		private static byte _Laplacian(float[] filter, byte[,] pixels)
		{
			float color = 0;
			int size = pixels.GetLength(0);
			for (int y = 0; y < size; y++) {
				for (int x = 0; x < size; x++) {
					float p = pixels[x, y];
					p *= filter[x + y * size];
					color += p;
				}
			}
			color = Math.Min(255, color);
			color = Math.Max(0, color);
			return (byte)color;
		}
	}
}
