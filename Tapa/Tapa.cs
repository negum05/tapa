using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
// using Microsoft.Office.Interop.Excel;

namespace Tapa
{
	static class Tapa
	{
		// 盤面の全てのマス
		public static Box[][] box;
		// idの残っている数字マスの座標のリスト
		public static List<Coordinates> numbox_coord_list = new List<Coordinates>();
		// 未定マスの座標リスト
		public static List<Coordinates> not_deployedbox_coord_list = new List<Coordinates>();
		// 一繋がりの未定マス群の座標リスト
		public static List<List<Coordinates>> isolation_notdeployedboxes_group_list = new List<List<Coordinates>>();
		// 伸び代のある黒マスの座標リスト
		public static List<Coordinates> edge_blackbox_coord_list = new List<Coordinates>();
		// 一繋がりの黒マス群の座標リスト
		public static List<List<Coordinates>> isolation_blackboxes_group_list = new List<List<Coordinates>>();

		public static bool DEBUG = false;

		public static int MAX_BOARD_ROW = 30;
		public static int MAX_BOARD_COL = 30;
		public static int BOX_SUM = MAX_BOARD_COL * MAX_BOARD_ROW;

		public static bool was_change_board;

		public static int REPEAT_NUM = 40;


		// エクセルに書き込む用
		public static string file_name = "tapa";
		public static int processnum_kuromasu;
		public static int processnum_kakuteijogaiid;
		public static int processnum_dangoid;
		public static int processnum_korituid;
		public static int processnum_betu0id;
		public static int processnum_kakuteimasu;
		public static int processnum_numbox;

		public static int visittimes_kuromasu;
		public static int visittimes_kakuteijogaiid;
		public static int visittimes_dangoid;
		public static int visittimes_korituid;
		public static int visittimes_betu0id;
		public static int visittimes_kakuteimasu;

		public static TimeSpan sum_times_kuromasu;
		public static TimeSpan sum_times_kakuteijogaiid;
		public static TimeSpan sum_times_dangoid;
		public static TimeSpan sum_times_korituid;
		public static TimeSpan sum_times_betu0id;
		public static TimeSpan sum_times_kakuteimasu;
		//ストップウォッチを開始する
		public static System.Diagnostics.Stopwatch sw_csv = System.Diagnostics.Stopwatch.StartNew();
		public static bool is_count = false;


		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Problem.default_path = System.Environment.CurrentDirectory + @"\tapa_prob";
			Console.WriteLine("defaultpath >> " + Problem.default_path);
			// デフォルトディレクトリがなければ作成する
			if (!System.IO.Directory.Exists(Problem.default_path)) {
				Console.WriteLine("check");
				System.IO.Directory.CreateDirectory(Problem.default_path);
			}

			Problem.default_path += @"\";


			// ################# 問題生成用
			//// 時間計測開始
			//System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

			//Problem.manageMakingProblem();

			////時間計測終了
			//sw.Stop();
			//Console.WriteLine("問題生成にかかった時間 >> " + sw.Elapsed);
			//Console.ReadLine();
			//return;


			// ################# GUI用
			// ボタンをWindows風のスタイルにしてくれる
			System.Windows.Forms.Application.EnableVisualStyles();
			// falseにすることでパフォーマンスを優先する
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
			// Form1()が停止しない間常に動作
			System.Windows.Forms.Application.Run(new Display());


			// ################# csv作成用
			//MyCSVManagement.makeSampleData();
			//return;


			// ################# excelファイルから問題を読み込む用
			/*
			if (args.Length == 0) {
				Console.WriteLine("Error:コマンドライン引き数が正しくありません。\n"
								+ "第一引数：入力ファイル\n第二引数：出力ファイル\n");
			}

			Console.WriteLine("#######################入力ファイル名 >> " + args[0]);

			// 入力ファイルの読み込み
			Tapa.inputTapa(args[0]);
			// 準備：数字マスにidのリストを追加
			PatternAroundNumBox.preparePatternArroundNumBox();

			foreach (Coordinates co in Tapa.numbox_coord_list) {
				co.printCoordinates();
				Console.Write(Tapa.box[co.x][co.y].boxNum + ":");
				Tapa.box[co.x][co.y].printIdList();
			}

			// 盤面の出力
			Console.WriteLine("入力盤面");
			Tapa.printBoard();
			Console.WriteLine();

			is_count = true;
			Tapa.solveTapa(Tapa.REPEAT_NUM);
			is_count = false;

			Console.WriteLine("notdeployedbox_list >> " + Tapa.not_deployedbox_coord_list.Count);
			Console.WriteLine("numbox_coord_list >> " + Tapa.numbox_coord_list.Count);
			Console.WriteLine("edge_blackbox_coordlist >> " + Tapa.edge_blackbox_coord_list.Count);
			Console.WriteLine("isolation_blackboxes_group_list >> " + Tapa.isolation_blackboxes_group_list.Count);
			Console.WriteLine("盤面:{0}*{1} = {2}", MAX_BOARD_ROW, MAX_BOARD_COL, (MAX_BOARD_ROW) * (MAX_BOARD_COL));
			Console.WriteLine("黒マスの数 >> " + Tapa.isolation_blackboxes_group_list[0].Count);

			if (isCorrectAnswer()) { Console.WriteLine("正解！！"); }
			else { Console.WriteLine("不正解"); }
			return;
			*/

		}

		/*********************************
		 * 
		 * 盤面のリセット
		 * 
		 * *******************************/
		public static void resetBoard()
		{
			box = new Box[Tapa.MAX_BOARD_ROW + 2][];
			// idの残っている数字マスの座標のリストを初期化
			numbox_coord_list.Clear();
			// 未定マスの座標リストを初期化
			not_deployedbox_coord_list.Clear();
			// 一繋がりの未定マス群の座標リストを初期化
			isolation_notdeployedboxes_group_list.Clear();
			// 伸び代のある黒マスの座標リストを初期化
			edge_blackbox_coord_list.Clear();
			// 一繋がりの黒マス群の座標リストを初期化
			isolation_blackboxes_group_list.Clear();

			Box.during_make_inputbord = true;
			for (int i = 0; i < Tapa.MAX_BOARD_ROW+2; i++) {
				Box[] tmp_boxarray = new Box[Tapa.MAX_BOARD_COL+2];
				for (int j = 0; j < Tapa.MAX_BOARD_COL+2; j++) {
					tmp_boxarray[j] = new Box();
					tmp_boxarray[j].coord = new Coordinates(i, j);

					if (i == 0 || i == Tapa.MAX_BOARD_ROW + 1 || j == 0 || j == Tapa.MAX_BOARD_COL + 1)
						tmp_boxarray[j].Color = Box.WHITE;
					else
						not_deployedbox_coord_list.Add(new Coordinates(tmp_boxarray[j].coord));
				}
				box[i] = tmp_boxarray;
			}
			Box.during_make_inputbord = false;
		}


		/*********************************
		 * 
		 * Tapaを解く(BuckTrack無し)
		 * 引数：
		 * cycle_num	: 手法の繰り返し上限回数
		 *  
		 * *******************************/
		public static void solveTapa(int cycle_num)
		{
			for (cycle_num = 1; cycle_num <= 30; cycle_num++) {
				was_change_board = false;
				// 数字マス周りのパターンを管理
				PatternAroundNumBox.managePatternAroundNumBox();

				// 伸び代のある黒マスから、黒マスが伸びないかを見て、可能なら実際に伸ばす。
				Box.manageBlackBox();

				if (!was_change_board) { break; }
			}
			Tapa.printBoard();
		}

		/*********************************
		 * 
		 * 入力ファイルの読み込み
		 *  
		 * *******************************/
		public static void inputTapa(string in_filename)
		{
			Box.during_make_inputbord = true;

			Microsoft.Office.Interop.Excel.Application ExcelApp
				= new Microsoft.Office.Interop.Excel.Application();
			Console.Write("infilename >> " + in_filename + "\n");

			// エクセルの非表示
			ExcelApp.Visible = false;

			// エクセルファイルのオープンと
			// ワークブックの作成
			Microsoft.Office.Interop.Excel.Workbook WorkBook = ExcelApp.Workbooks.Open(in_filename);

			// 1シート目の選択
			Microsoft.Office.Interop.Excel.Worksheet sheet = WorkBook.Sheets[1];
			sheet.Select();

			// A1セルから見た下への連続データ数
			int row_count
				= sheet.get_Range("A1").End[Microsoft.Office.Interop.Excel.XlDirection.xlDown].Row;

			// A1セルから見た右への連続データ数
			int column_count
				= sheet.get_Range("A1").End[Microsoft.Office.Interop.Excel.XlDirection.xlToRight].Column;

			MAX_BOARD_ROW = row_count;		// 行数
			MAX_BOARD_COL = column_count;	// 列数
			BOX_SUM = MAX_BOARD_COL * MAX_BOARD_ROW;

			if (DEBUG) {
				Console.WriteLine("row_count >> " + row_count + "\ncolumn_count >> " + column_count + "\n");

				Microsoft.Office.Interop.Excel.Range tmp_range;
				for (int i = 1; i <= row_count; i++) {
					for (int j = 1; j <= column_count; j++) {
						tmp_range = sheet.Cells[i, j];
						if (tmp_range != null) {
							Console.Write(tmp_range.Value.ToString() + ' ');
						}
					}
					Console.Write("\n");
				}
			}

			resetBoard();
			// 各セルのデータの取得
			for (int i = 1; i <= row_count; i++) {
				for (int j = 1; j <= column_count; j++) {
					int tmp_num = 0;
					string st = "";

					Microsoft.Office.Interop.Excel.Range range = sheet.Cells[i, j];
					if (range.Value != null) {
						st = range.Value.ToString();  // セル(i,j)のデータを文字列で取得
					}
					else {
						Console.Write("Error: セル(" + i + "," + j + ")の読み込み中にエラー(中身がnull)\n");
					}

					if (st.Equals("-")) {
						Tapa.box[i][j].hasNum = false;
					}
					else if (int.TryParse(st, out tmp_num)) { // tmp_num=(int)stが数字だった場合
						int origin_num = tmp_num;
						Tapa.box[i][j].hasNum = true;
						
						// ####### (begin) マスの数字を昇順に並べ替える
						List<int> tmp_box_num_list = new List<int>();
						do {              // 数字を桁毎にリストに追加
							tmp_box_num_list.Insert(0, tmp_num % 10);
							tmp_num /= 10;
						} while (tmp_num > 0);  // do-whileは0の場合を許可するため
						tmp_box_num_list.Sort();	// 昇順にソート
						int digit_pow = (int)Math.Pow(10, origin_num.ToString().Length - 1);	// 10^(桁数-1)
						tmp_num = 0;
						foreach (int _num in tmp_box_num_list) {
							tmp_num += _num * digit_pow;
							digit_pow /= 10;
						}
						Tapa.box[i][j].boxNum = tmp_num;
						// ####### (end) マスの数字を昇順に並べ替える
						numbox_coord_list.Add(Tapa.box[i][j].coord);		// 数字マスの座標Listに追加
						Tapa.not_deployedbox_coord_list.Remove(Tapa.box[i][j].coord);	// 未定マスリストから除外
					}
					else {
						Console.WriteLine("Error: セル(" + i + "," + j + ")の読み込み中にエラー(中身が数字でも'-'でもない)\n");
					}
				}
			}

			//ワークブックを閉じる
			WorkBook.Close();
			//エクセルを閉じる
			ExcelApp.Quit();

			Box.during_make_inputbord = false;
		}

		/*********************************
		 * 
		 *   盤面の出力
		 *   引数
		 *   ll_box	: 出力するBoxの２次元リスト
		 *			　（nullならTapa.boxを出力）
		 *  
		 * *******************************/
		public static void printBoard(Box[][] ll_box = null)
		{
			Box[][] print_box;
			if (ll_box != null) { print_box = ll_box; }
			else { print_box = Tapa.box; }
			foreach (Box[] tmp_box_list in print_box) {
				foreach (Box tmp_box in tmp_box_list) {
					tmp_box.printBoxNum();
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		/*********************************
		 * 
		 *   Coordinates型の2次元リストを出力
		 *  
		 * *******************************/
		public static void printMultiCoordList(List<List<Coordinates>> multi_coord_list)
		{
			foreach (List<Coordinates> tmp_coord_list in multi_coord_list) {
				foreach (Coordinates tmp_coord in tmp_coord_list) {
					tmp_coord.printCoordinates();
				}
				Console.WriteLine();
			}
		}

		/*********************************
		 * 
		 *   Coordinates型のリストを出力
		 *  
		 * *******************************/
		public static void printCoordList(List<Coordinates> coord_list)
		{
			foreach (Coordinates tmp_coord in coord_list) {
				//tmp_coord.printCoordinates();
				//Console.Write(" ");

				tmp_coord.printCoordinates();
				Console.WriteLine(" id:" + Tapa.box[tmp_coord.x][tmp_coord.y].id_list.Count + " hint:" + Tapa.box[tmp_coord.x][tmp_coord.y].min_hint);
			}
			Console.WriteLine();
		}

		/*********************************
		 * 
		 *   盤面がTapaの解答として正しいか返す
		 *   true	:	正しい
		 *   false	:	正しくない
		 *  
		 * *******************************/
		public static bool isCorrectAnswer()
		{
			if (Tapa.not_deployedbox_coord_list.Count == 0			// 未定マスがない
				&& Tapa.numbox_coord_list.Count == 0				// idが一意に定まらなかった数字マスがない
				&& Tapa.isolation_blackboxes_group_list.Count == 1	// 黒マスが一繋がり
				&& Box.checkNotDumplingBlackBox()) {				// 黒マスの団子がない
				return true;
			}
			return false;
		}

	}
}
