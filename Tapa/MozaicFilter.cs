using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tapa
{
	// モザイク処理
	// http://hikipuro.hatenadiary.jp/entry/2015/08/10/093630
	class MozaicFilter
	{
		// 引数で渡されたビットマップ画像にモザイクフィルタを適用します
		public static Bitmap Apply(Bitmap source, float rate = 50f, int size = 6)
		{
			// ビットマップ画像から全てのピクセルを抜き出す
			PixelManipulator s = PixelManipulator.LoadBitmap(source);
			PixelManipulator d = s.Clone();
			rate /= 1000;

			// 範囲チェック
			if (size < 1) {
				size = 1;
			}
			if (size > 32) {
				size = 32;
			}
			int w = size * 2 + 1;
			Tapa.MAX_BOARD_ROW = s.height / w;		// 問題の行数
			Tapa.MAX_BOARD_COL = s.width / w;		// 問題の列数
			Tapa.BOX_SUM = Tapa.MAX_BOARD_ROW * Tapa.MAX_BOARD_COL;

			// 全てのピクセルを巡回する
			s.EachPixel((x, y) => {
				// 塗り終わったところを飛ばす
				if (x % w != 0 || y % w != 0) {
					return;
				}

				// モザイクに色を塗る
				byte r = _SMA(s.RangeR(x, y, size), rate);
				byte g = _SMA(s.RangeG(x, y, size), rate);
				byte b = _SMA(s.RangeB(x, y, size), rate);
				_Fill(d, x, y, size, r, g, b);
				// MozaicFilter._Fill(d, x, y, size, s.R(x,y), s.G(x,y), s.B(x,y));
			});

			// 新しいビットマップ画像を作成して、ピクセルをセットする
			return d.CreateBitmap();
		}

		//// 周囲のピクセルの平均値を出す処理
		// 線のピクセルの割合がrate以上あれば線の色にする
		private static byte _SMA(byte[,] pixels, float rate)
		{
			int color = 0;
			int size = pixels.GetLength(0);
			for (int y = 0; y < size; y++) {
				for (int x = 0; x < size; x++) {
					color += pixels[x, y] & (byte)00000001;
				}
			}

			return (color > size*size*rate) ? (byte)0 : (byte)255;
			//color /= size * size;
			//return (byte)color;
		}

		// 範囲を塗りつぶす処理
		private static void _Fill(PixelManipulator d, int dx, int dy, int size, byte r, byte g, byte b)
		{
			for (int y = -size; y <= size; y++) {
				for (int x = -size; x <= size; x++) {
					if (y == -size || y == size || x == -size || x == size) {
						d.SetPixel(dx + x, dy + y, 0, 255, 0);
					}
					else { d.SetPixel(dx + x, dy + y, r, g, b); }
				}
			}
		}
	}
}
