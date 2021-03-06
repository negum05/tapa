﻿using System;
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

			// しきい値の設定
			byte[] thr_array = new byte[256];
			int i;
			for (i = 0; i < threshold; i++) {
				thr_array[i] = (byte)0;
			}
			for (; i < 256; i++) {
				thr_array[i] = (byte)255;
			}

			// 全てのピクセルを巡回する
			s.EachPixel((x, y) => {
				// 2 値化する
				// グレースケール化されてることが前提なのでrgbは同じ値と仮定
				byte color = thr_array[s.R(x,y)];
				d.SetPixel(x, y, color, color, color);
			});

			// 新しいビットマップ画像を作成して、ピクセルをセットする
			return d.CreateBitmap();
		}		
	}
}
