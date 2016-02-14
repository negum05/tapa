using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tapa
{
	class LaplacianFilter
	{
		// 引数で渡されたビットマップ画像にラプラシアンフィルタを適用します
		public static Bitmap Apply(Bitmap source)
		{
			// ビットマップ画像から全てのピクセルを抜き出す
			PixelManipulator s = PixelManipulator.LoadBitmap(source);
			PixelManipulator d = s.Clone();

			// カーネルを作成する
			float[] kernel = _CreateLaplacianKernel();

			// 全てのピクセルを巡回する
			s.EachPixel((x, y) => {
				byte r = _Laplacian(kernel, s.RangeR(x, y, 1));
				byte g = _Laplacian(kernel, s.RangeG(x, y, 1));
				byte b = _Laplacian(kernel, s.RangeB(x, y, 1));
				d.SetPixel(x, y, r, g, b);
			});

			// 新しいビットマップ画像を作成して、ピクセルをセットする
			return d.CreateBitmap();
		}

		// カーネルを作成する
		private static float[] _CreateLaplacianKernel()
		{
			float[] kernel = new float[] {
				1,  1, 1,
				1, -8, 1,
				1,  1, 1
			};
			return kernel;
		}

		private static byte _Laplacian(float[] kernel, byte[,] pixels)
		{
			float color = 0;
			int size = pixels.GetLength(0);
			for (int y = 0; y < size; y++) {
				for (int x = 0; x < size; x++) {
					float p = pixels[x, y];
					p *= kernel[x + y * size];
					color += p;
				}
			}
			color = Math.Min(255, color);
			color = Math.Max(0, color);
			return (byte)color;
		}
	}
}
