using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging.Filters;

namespace Tapa
{
	class PictToDotManagement
	{
		public static void makeDotFromPict()
		{
			// Bitmap bmp = new Bitmap(@"C:\Users\Amano\OneDrive\zemi\dot_data\pikachu\pikachu_result.png");
			// Bitmap bmp = new Bitmap(@"C:\Users\Amano\OneDrive\zemi\0518\RichardPStanley.jpg");
			Bitmap bmp = new Bitmap(@"C:\Users\Amano\OneDrive\画像\壁紙\wide\4月は君の嘘.jpg");
			// Bitmap bmp = new Bitmap(@"C:\Users\Amano\OneDrive\test\konata.jpg");


			// グレースケール化
			Bitmap gray = new Grayscale(0.2125, 0.7154, 0.0721).Apply(bmp);
			gray.Save(@"C:\Users\Amano\OneDrive\zemi\0518\stile_gray.jpg", ImageFormat.Jpeg);

			// ノイズ除去
			//Bitmap median = MedianFilter.Apply(gray);
			//median.Save(@"C:\Users\Amano\OneDrive\zemi\0518\stile_median.jpg", ImageFormat.Jpeg);

			// 二値化処理
			//Bitmap binary = new Threshold().Apply(median);
			Bitmap binary = ThresholdingFilter.Apply(gray);
			binary.Save(@"C:\Users\Amano\OneDrive\zemi\0518\stile_binary.jpg", ImageFormat.Jpeg);

			// エッジ検出
			Bitmap laplacian = LaplacianFilter.Apply(binary);
			laplacian.Save(@"C:\Users\Amano\OneDrive\zemi\0518\stile_laplacian.jpg", ImageFormat.Jpeg);

			// モザイク処理
			Bitmap mozaic = MozaicFilter.Apply(laplacian, 6);
			mozaic.Save(@"C:\Users\Amano\OneDrive\zemi\0518\stile_mozaic.jpg", ImageFormat.Jpeg);

			// 黒塗り部分を白にして、ぱずぷれ形式に変換
			makeBoardFromDot(mozaic, 6);

		}

		public static void makeBoardFromDot(Bitmap mozaic, int size = 6)
		{
			// ビットマップ画像から全てのピクセルを抜き出す
			PixelManipulator s = PixelManipulator.LoadBitmap(mozaic);



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

			Tapa.resetBoard();

			// 全てのピクセルを巡回する
			int i = 0;
			Box.during_make_inputbord = true;
			s.EachPixel((x, y) => {
				// 確認の終わったモザイクマスは飛ばす
				if (x % w != 0 || y % w != 0) {
					return;
				}
				// 2値画像なので、rのみのチェックでok
				Tapa.box[i / (Tapa.MAX_BOARD_COL + 1)][i % (Tapa.MAX_BOARD_COL + 1)].Color
					= (s.R(x, y) == 0) ? Box.BLACK : Box.NOCOLOR;
				i++;
			});
			Box.during_make_inputbord = false;

			avoidDumplingBoxes();


			Problem.generateTapaHintText(@"C:\Users\Amano\OneDrive\test\konata_dot.txt");
		}

		private static void avoidDumplingBoxes()
		{
			// 団子黒マスの除去
			List<Coordinates> dump_list = new List<Coordinates>();
			for (int i = Tapa.MAX_BOARD_ROW; i >= 1; i--) {
				for (int j = Tapa.MAX_BOARD_COL; j >= 1; j--) {
					Box b = Tapa.box[i][j];
					// 四方に未定マスがなく、団子になっている黒マスを未定マスにする
					if (b.Color == Box.BLACK
						&& !Box.canExtendBlackBox(b.coord)
						&& !Box.checkNotDumplingBlackBoxAround(b.coord))
						dump_list.Add(b.coord);
				}
			}
			foreach (Coordinates co in dump_list) {
				Tapa.box[co.x][co.y].revision_color = Box.NOCOLOR;
			}
		}

	}
}
