using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tapa
{
	// http://hikipuro.hatenadiary.jp/entry/2015/08/10/054823
	// メディアンフィルター（ノイズ除去）を行う
	class MedianFilter
	{
		// 引数で渡されたビットマップ画像にメディアンフィルタを適用します
		public static Bitmap Apply(Bitmap source, int size = 3)
		{
			// ビットマップ画像から全てのピクセルを抜き出す
			PixelManipulator s = PixelManipulator.LoadBitmap(source);
			PixelManipulator d = s.Clone();

			// 範囲チェック
			if (size < 3) {
				size = 3;
			}
			if (size > 9) {
				size = 9;
			}
			size--;
			size /= 2;

			// 全てのピクセルを巡回する
			s.EachPixel((x, y) => {
				byte r = _Median(s.RangeR(x, y, size));
				byte g = _Median(s.RangeG(x, y, size));
				byte b = _Median(s.RangeB(x, y, size));
				d.SetPixel(x, y, r, g, b);
			});

			// 新しいビットマップ画像を作成して、ピクセルをセットする
			return d.CreateBitmap();
		}

		// ピクセル列の中央値を出す
		private static byte _Median(byte[,] pixels)
		{
			List<byte> colors = new List<byte>();
			int size = pixels.GetLength(0);
			for (int y = 0; y < size; y++) {
				for (int x = 0; x < size; x++) {
					colors.Add(pixels[x, y]);
				}
			}
			colors.Sort();
			int index = colors.Count / 2;
			return colors[index];
		}
	}
}
