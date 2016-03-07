using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
// using AForge.Imaging.Filters;

namespace Tapa
{
	class PictToDotManagement
	{
		/*********************************
		 * 
		 * モザイク画からぱずぷれ形式に変換する
		 *
		 * 引数
		 *	dir_path	:	保存先ディレクトリ
		 *	file_name	:	保存ファイル名の頭
		 *	mozaic		:	変換対象のbitmap
		 *	size		:	モザイクのサイズ
		 *   
		 * *******************************/
		public static void makePzprFromMozaic(string savedir_path, string file_name, Bitmap mozaic, int size)
		{
			// 末尾に \ がなければ追加
			savedir_path += savedir_path[savedir_path.Length - 1].Equals('\\') ? "" : @"\";
			// 拡張子を削除したファイルパスを取得
			// http://jeanne.wankuma.com/tips/csharp/path/getfilenamewithoutextension.html
			string file_midpath = savedir_path + System.IO.Path.GetFileNameWithoutExtension(file_name);
			string mozaic_path = file_midpath + "_mozaic.txt";
			string connect_path = file_midpath + "_connect.txt";
			string deldump1_path = file_midpath + "_deldump1.txt";
			string deldump2_path = file_midpath + "_deldump2.txt";



			// 時間計測
			System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

			// ぱずぷれ形式に変換
			makeBoardFromDot(mozaic, size);
			Problem.generateTapaHintText(mozaic_path);
			Console.WriteLine("complete pzpr_mozaic " + sw.Elapsed);
			sw.Restart();

			// 孤立した黒マスを繋げる
			Problem p = new Problem();
			// p.readDotTapaTxt(tmp_dir + dot_name + "deldump1.txt");
			p.readDotTapaTxt(mozaic_path);

			connectIsoBlackBoxGroup();
			Problem.generateTapaHintText(connect_path);
			Console.WriteLine("complete pzpr_connect " + sw.Elapsed);
			sw.Restart();

			// 黒塗り部分を白にする
			avoidDumplingBoxes();
			Problem.generateTapaHintText(deldump1_path);
			Console.WriteLine("complete pzpr_deldump1 " + sw.Elapsed);
			sw.Restart();

			connectIsoBlackBoxGroup();

			// 黒マスの団子を削除
			removeDumpling();
			removeCutDumpling();
			Problem.generateTapaHintText(deldump2_path);
			Console.WriteLine("complete pzpr_deldump2 " + sw.Elapsed);
			sw.Reset();

		}
		/*********************************
		 * 
		 * 画像からモザイク化処理まで行う
		 *
		 * 引数
		 *	dir_path			:	オリジナル画像のあるディレクトリパス
		 *	file_name			:	オリジナル画像のファイル名
		 *	savedir_path		:	保存先ディレクトリパス
		 *	thr_binary			:	2値化のしきい値
		 *	mozaic_size			:	モザイクのサイズ
		 *	thr_mozaic_color	:	マスの色を決めるしきい値
		 *   
		 * *******************************/
		public static Bitmap makeDotFromPict(string dir_path, string file_name, string savedir_path
			, byte thr_binary = (byte)128, int mozaic_size = 6, int thr_mozaic_color = 50)
		{
			// 末尾に \ がなければ追加
			dir_path += dir_path[dir_path.Length - 1].Equals('\\') ? "" : @"\";
			savedir_path += savedir_path[savedir_path.Length - 1].Equals(@"\") ? "" : @"\";

			// 拡張子を削除した保存先ファイルパスを取得
			// http://jeanne.wankuma.com/tips/csharp/path/getfilenamewithoutextension.html
			string file_midpath = savedir_path + System.IO.Path.GetFileNameWithoutExtension(file_name);

			string gray_path = file_midpath + "_gray.jpg";
			string median_path = file_midpath + "_median.jpg";
			string binary_path = file_midpath + "_binary.jpg";
			string laplacian_path = file_midpath + "_laplacian.jpg";
			string moziac_path = file_midpath + "_mozaic.jpg";

			// オリジナル画像のビットマップを作成
			Bitmap bmp = new Bitmap(dir_path + file_name);

			// 時間計測開始
			System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

			// グレースケール化
			Bitmap gray = GrayScale.Apply(bmp);
			gray.Save(gray_path, ImageFormat.Jpeg);
			Console.WriteLine("complete gray " + sw.Elapsed);
			sw.Restart();

			// ノイズ除去
			//Bitmap median = MedianFilter.Apply(gray);
			//median.Save(median_path, ImageFormat.Jpeg);
			//Console.WriteLine("complete median " + sw.Elapsed);
			//sw.Restart();

			// 二値化処理
			Bitmap binary = ThresholdingFilter.Apply(gray, thr_binary);
			binary.Save(binary_path, ImageFormat.Jpeg);
			Console.WriteLine("complete binary " + sw.Elapsed);
			sw.Restart();

			// エッジ検出
			Bitmap laplacian = LaplacianFilter.Apply(binary);
			laplacian.Save(laplacian_path, ImageFormat.Jpeg);
			Console.WriteLine("complete laplacian " + sw.Elapsed);
			sw.Restart();

			// モザイク処理
			Bitmap mozaic = MozaicFilter.Apply(laplacian, thr_mozaic_color, mozaic_size);
			mozaic.Save(moziac_path, ImageFormat.Jpeg);
			Console.WriteLine("complete mozaic " + sw.Elapsed);
			sw.Reset();

			return mozaic;
		}


		/*********************************
		 * 
		 * 画像取得からPzpr形式までの全ての処理
		 *
		 * 引数
		 *	dir_path			:	オリジナル画像のあるディレクトリパス
		 *	file_name			:	オリジナル画像のファイル名
		 *	savedir_path		:	保存先ディレクトリパス
		 *	thr_binary			:	2値化のしきい値
		 *	mozaic_size			:	モザイクのサイズ
		 *	thr_mozaic_color	:	マスの色を決めるしきい値
		 *   
		 * *******************************/
		public static void makePzprFormPict(string dir_path, string file_name, string savedir_path
			, byte thr_binary = (byte)128, int mozaic_size = 6, int thr_mozaic_color = 50)
		{
			// 時間計測
			System.Diagnostics.Stopwatch sw_all = System.Diagnostics.Stopwatch.StartNew();

			// 画像取得からからエッジ検出まで
			Bitmap bmp = makeDotFromPict(dir_path, file_name, savedir_path
				, thr_binary, mozaic_size, thr_mozaic_color);

			// Bitmapからpzpr形式へ変換
			makePzprFromMozaic(savedir_path, file_name, bmp, mozaic_size);

			Console.WriteLine("complete all " + sw_all.Elapsed);
			sw_all.Reset();

			Console.ReadLine();
		}


		/*********************************
		* 
		* bitmapからぱずぷれ形式のtxtを生成
		*
		* 引数
		*	mozaic	:	変換対象のbitmap
		*	size	:	マスのサイズ
		*   
		* *******************************/
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
			Tapa.BOX_SUM = Tapa.MAX_BOARD_ROW * Tapa.MAX_BOARD_COL;

			Tapa.resetBoard();

			// 全てのピクセルを巡回する
			Box.during_make_inputbord = true;
			s.EachPixel((x, y) => {
				// 確認の終わったモザイクマスは飛ばす
				if (x % w != 0 || y % w != 0) {
					return;
				}
				// 2値画像なので、rのみのチェックでok
				Tapa.box[y / w][x / w].Color = (s.R(x, y) == 0) ? Box.BLACK : Box.NOCOLOR;
			});
			Box.during_make_inputbord = false;


		}

		/*********************************
		 * 
		 * 団子マスの大雑把な除去
		 * 輪郭を残すために使う
		 *   
		 * *******************************/
		private static void avoidDumplingBoxes()
		{
			// 団子黒マスの除去
			List<Coordinates> dump_list = new List<Coordinates>();
			for (int i = Tapa.MAX_BOARD_ROW; i >= 1; i--) {
				for (int j = Tapa.MAX_BOARD_COL; j >= 1; j--) {
					Box b = Tapa.box[i][j];
					// 周囲8マスに未定マスがなく、団子になっている黒マスを未定マスにする
					if (b.Color == Box.BLACK
						&& !Box.existWhatColorBoxAround8(b.coord, Box.NOCOLOR)
						&& !Box.checkNotDumplingBlackBoxAround(b.coord))
						dump_list.Add(b.coord);
				}
			}
			foreach (Coordinates co in dump_list) {
				Tapa.box[co.x][co.y].revision_color = Box.NOCOLOR;
			}
		}

		/*********************************
		 * 
		 * 一繋がりの黒マス群を全て繋げる
		 *   
		 * *******************************/
		private static void connectIsoBlackBoxGroup()
		{
			// 盤面の白マスを未定マスに変更する
			for (int i = 1; i <= Tapa.MAX_BOARD_ROW; i++) {
				for (int j = 1; j <= Tapa.MAX_BOARD_COL; j++) {
					if (Tapa.box[i][j].Color == Box.WHITE) {
						Box tmp = Tapa.box[i][j];
						tmp.revision_color = Box.NOCOLOR;
						Tapa.not_deployedbox_coord_list.Add(tmp.coord);
					}
				}
			}

			while (Tapa.isolation_blackboxes_group_list.Count != 1) {
				// 末尾のリストを取得（再構築された一繋がりの黒マスリストは末尾に追加される）
				List<Coordinates> bb_group_list
					= Tapa.isolation_blackboxes_group_list[Tapa.isolation_blackboxes_group_list.Count - 1];

				Coordinates pair1 = new Coordinates();
				Coordinates pair2 = new Coordinates();
				int distance = int.MaxValue;
				int tmp_distance = int.MaxValue;

				for (int i = bb_group_list.Count - 1; i >= 0; i--) {
					Coordinates co = bb_group_list[i];
					Coordinates tmp_co = Box.getCloseBlackBoxCoord(co, bb_group_list);
					tmp_distance = Box.getDistance(co, tmp_co);
					if (tmp_distance < distance) {
						distance = tmp_distance;
						pair1 = co;
						pair2 = tmp_co;
					}
				}
				// 黒マス同士を繋げる処理
				PictToDotManagement.connectBlackBox(pair1, pair2);
			}
		}

		/*********************************
		 * 
		 * 黒マス同士を繋げる
		 *   
		 * *******************************/
		private static void connectBlackBox(Coordinates co1, Coordinates co2)
		{
			// 距離(向きも考慮)
			int dist_x = co2.x - co1.x;
			int dist_y = co2.y - co1.y;
			// 向き
			int dir_x = dist_x == 0 ? 0 : dist_x / Math.Abs(dist_x);
			int dir_y = dist_y == 0 ? 0 : dist_y / Math.Abs(dist_y);
			// 歩幅
			int step_x, step_y;
			Coordinates direction = new Coordinates();
			Coordinates col_co;


			if (Math.Abs(dist_x) > Math.Abs(dist_y)) {
				// x方向、y方向でそれぞれ何マスずつ塗るか決める
				step_x = dist_y == 0 ? dist_x : dir_x * Math.Abs(dist_x / dist_y);
				step_y = dir_y;

				col_co = new Coordinates(co1);
				while (col_co.x != co2.x && col_co.y != co2.y) {
					int i;
					for (i = col_co.x; i != col_co.x + step_x && i != co2.x; i += dir_x) { // x方向から
						Tapa.box[i][col_co.y].connecting_color = Box.BLACK;
					}
					col_co = new Coordinates(i, col_co.y);
					Tapa.box[col_co.x][col_co.y].connecting_color = Box.BLACK;
					if (col_co.x == co2.x) { break; }

					for (i = col_co.y; i != col_co.y + step_y && i != co2.y; i += dir_y) {	// y方向
						Tapa.box[col_co.x][i].connecting_color = Box.BLACK;
					}
					col_co = new Coordinates(col_co.x, i);
					Tapa.box[col_co.x][col_co.y].connecting_color = Box.BLACK;
				}
			}
			else {
				// x方向、y方向でそれぞれ何マスずつ塗るか決める
				step_x = dir_x;
				step_y = dist_x == 0 ? dist_y : dir_y * Math.Abs(dist_y / dist_x);

				col_co = new Coordinates(co1);
				while (col_co.x != co2.x && col_co.y != co2.y) {
					int i;
					for (i = col_co.y; i != col_co.y + step_y && i != co2.y; i += dir_y) { // y方向から
						Tapa.box[col_co.x][i].connecting_color = Box.BLACK;
					}
					col_co = new Coordinates(col_co.x, i);
					Tapa.box[col_co.x][col_co.y].connecting_color = Box.BLACK;
					if (col_co.y == co2.y) { break; }

					for (i = col_co.x; i != col_co.x + step_x && i != co2.x; i += dir_x) {	// x方向
						Tapa.box[i][col_co.y].connecting_color = Box.BLACK;
					}
					col_co = new Coordinates(i, col_co.y);
					Tapa.box[col_co.x][col_co.y].connecting_color = Box.BLACK;
				}
			}
			// まだco2まで到達していない場合
			if (!col_co.Equals(co2)) {
				int x = col_co.x == co2.x ? 0 : dir_x;
				int y = col_co.y == co2.y ? 0 : dir_y;
				do {
					col_co = new Coordinates(col_co.x + x, col_co.y + y);
					Tapa.box[col_co.x][col_co.y].connecting_color = Box.BLACK;
				} while (!col_co.Equals(co2));
			}
		}

		/*********************************
		 * 
		 * 切断点以外の団子マスを除去する
		 *   
		 * *******************************/
		private static void removeDumpling()
		{
			Problem p = new Problem();
			p.doTarjan(Tapa.isolation_blackboxes_group_list[0], Box.BLACK);

			for (int i = Tapa.isolation_blackboxes_group_list[0].Count - 1; i >= 0; i--) {
				Coordinates co = Tapa.isolation_blackboxes_group_list[0][i];
				if (!Box.checkNotDumplingBlackBoxAround(co)		// 自身の周りで団子マスができていて
					&& !Problem.cutpoint_list.Contains(co)) {	// かつ自身が切断点ではないなら
					Box b = Tapa.box[co.x][co.y];				// 自身を未定マスにする
					b.revision_color = Box.NOCOLOR;
					Tapa.isolation_blackboxes_group_list[0].RemoveAt(i);
					// 切断点を再度記録
					p.doTarjan(Tapa.isolation_blackboxes_group_list[0], Box.BLACK);
				}
			}
		}

		/*********************************
		 * 
		 * 切断点のみの団子マスに接している黒マス群のうち
		 * 最も短い黒マス群とそれに接している切断点を削除する
		 *   
		 * *******************************/
		private static void removeCutDumpling()
		{
			// 団子マス4つの保存用
			List<Coordinates> dump_coord;

			// 団子マスの座標を取得、団子マスがなければ終わり
			while ((dump_coord = Box.getDumpCoord()) != null) {
				
				// 切断点に接している切断点以外の黒マス保存用
				// Key	 : 切断点に接している黒マス
				// Value : 切断点
				Dictionary<Coordinates, Coordinates> adj_dict = new Dictionary<Coordinates, Coordinates>();
				foreach (Coordinates co in dump_coord) {
					// 切断点が接している黒マス座標を取得
					List<Coordinates> tmp_adj_coord = Box.getWhatColorBoxCoordListAround(co, Box.BLACK);
					// 接している黒マスのうち切断点の情報を除外
					foreach (Coordinates tmp_co in dump_coord) {
						tmp_adj_coord.Remove(tmp_co);
					}
					adj_dict[tmp_adj_coord[0]] = co;
				}
				// 盤面編集用
				StateSave edit = new StateSave();
				StateSave.saveNowState(edit);

				// 盤面editの団子マスdump_coordを未定マスにする
				foreach (Coordinates co in dump_coord) {
					edit.saved_box[co.x][co.y].revision_color = Box.NOCOLOR;
				}
				// edit盤面を生成
				// (一繋がりの黒マス群が複数生成される)
				StateSave.makeEditBoard(edit);

				// 黒マス群リストのうち、最少の黒マス群の参照を取得
				List<Coordinates> min_bblist = Box.getMinIsoBlackBoxListRef();

				// 切断点の黒マスの団子を黒く塗る
				foreach (Coordinates co in dump_coord) {
					edit.saved_box[co.x][co.y].revision_color = Box.BLACK;
				}

				// min_bblistとそれに接している切断点を未定マスにする
				foreach (Coordinates co in min_bblist) {
					edit.saved_box[co.x][co.y].revision_color = Box.NOCOLOR;
					if (!adj_dict.ContainsKey(co)) { continue; }
					edit.saved_box[adj_dict[co].x][adj_dict[co].y].revision_color = Box.NOCOLOR;
				}

				// edit盤面を生成
				// (団子マスを1つ除外できた盤面)
				StateSave.makeEditBoard(edit);
			}
		}



		///*********************************
		// * 
		// * 右と下の余白を削除する（プログラムが原因でできた余白の可能性があるため）
		// *   
		// * *******************************/
		//private static void deleteMargine(Bitmap source, int size){
		//	// ビットマップ画像から全てのピクセルを抜き出す
		//	PixelManipulator s = PixelManipulator.LoadBitmap(source);
		//	PixelManipulator d = s.Clone();

		//	int w = size * 2 + 1;
		//	bool is_bm = false;
		//	bool is_rm = false;

		//	for (int x = 0; x < s.width; x++) {
		////	}

		//}


	}
}
