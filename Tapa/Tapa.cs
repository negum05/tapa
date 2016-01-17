using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Tapa
{
	static class Tapa
	{
		// 盤面の全てのマス
		public static List<List<Box>> box = new List<List<Box>>();
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
		public static bool DEBUG_PRINT_PROCESS = false;

		public static int NOW_STATE_PROCESS = 0;
		public static int STATE_ID_LIST_ONLY_ONE = 1;						// idリストが一意の時
		public static int STATE_CONFIRM_BOX_COLOR_FROM_ID_LIST = 2;			// idリストからマスが確定する時
		public static int STATE_AVOID_DUMPLING_AROUND_BLACK_BOX = 3;		// 黒マスの上下左右で団子マスを避ける時
		public static int STATE_ISOLATION_BLACK_BOXES_ONLY_EXTENDABLE = 4;	// 一繋がりの黒マス群の伸び代が一箇所のみの時

		public static int MAX_BOARD_ROW = 6;
		public static int MAX_BOARD_COL = 6;
		public static int BOX_SUM = MAX_BOARD_COL * MAX_BOARD_ROW;

		public static bool was_change_board;

		public static int cycle_num = 0;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{

			Problem.manageMakingProblem();
			Console.WriteLine("盤面:{0}*{1} = {2}", MAX_BOARD_ROW, MAX_BOARD_COL, (MAX_BOARD_ROW) * (MAX_BOARD_COL));
			Console.WriteLine("黒マスの数 >> " + Tapa.isolation_blackboxes_group_list[0].Count);
			return;


			if (args.Length == 0) {
				Console.WriteLine("Error:コマンドライン引き数が正しくありません。\n"
								+ "第一引数：入力ファイル\n第二引数：出力ファイル\n");
				Application.Exit();
			}

			Console.WriteLine("#######################入力ファイル名 >> " + args[0]);

			// 入力ファイルの読み込み
			Tapa.inputTapa(args[0]);
			// 準備：数字マスにidのリストを追加
			PatternAroundNumBox.preparePatternArroundNumBox();
			// 盤面の出力
			Console.WriteLine("入力盤面");
			Tapa.printBoard();
			Console.WriteLine();

			for (cycle_num = 1; cycle_num <= 30; cycle_num++) {
				was_change_board = false;
				// 数字マス周りのパターンを管理
				PatternAroundNumBox.managePatternAroundNumBox();
				Console.WriteLine("{0}回目：数字マス周りの処理後", cycle_num);
				Tapa.printBoard();
				Console.WriteLine();
				//Tapa.printCoordList(Tapa.edge_blackbox_coord_list);
				//Console.WriteLine();

				// 伸び代のある黒マスから、黒マスが伸びないかを見て、可能なら実際に伸ばす。
				Box.manageBlackBox();
				Console.WriteLine("{0}回目：黒マス関係の処理後", cycle_num);
				Tapa.printBoard();
				Console.WriteLine();
				//Tapa.printCoordList(Tapa.edge_blackbox_coord_list);
				//Console.WriteLine();
				if (!was_change_board) { break; }
			}

			// 未定マスが存在するなら、バックトラックを行う。
			if (Tapa.not_deployedbox_coord_list.Count > 0) {
				BackTrack backtrack = new BackTrack();
				backtrack.manageBackTrack();
				StateSave.setSavedState(BackTrack.correct_save_point);
				printBoard();
				Console.WriteLine("\n深さ >> " + BackTrack.min_depth);
			}

			//Console.WriteLine("notdeployedbox_list >> " + Tapa.not_deployedbox_coord_list.Count);
			//Console.WriteLine("numbox_coord_list >> " + Tapa.numbox_coord_list.Count);
			//Console.WriteLine("edge_blackbox_coordlist >> " + Tapa.edge_blackbox_coord_list.Count);
			//Console.WriteLine("isolation_blackboxes_group_list >> " + Tapa.isolation_blackboxes_group_list.Count);
			Console.WriteLine("盤面:{0}*{1} = {2}", Tapa.MAX_BOARD_ROW-2, Tapa.MAX_BOARD_COL-2, (Tapa.MAX_BOARD_ROW-2)*(Tapa.MAX_BOARD_COL-2));
			Console.WriteLine("黒マスの数 >> " + Tapa.isolation_blackboxes_group_list[0].Count);

			if (isCorrectAnswer()) { Console.WriteLine("正解！！"); }
			else { Console.WriteLine("不正解"); }

			return;

			/*
			// boxとsaveの参照確認用（一方の変化が他方に影響しないことを確認）
			StateSave test_state_save = new StateSave();
			StateSave.saveNowState(test_state_save);
			Console.WriteLine("######################");
			Console.WriteLine("box >> ");
			Tapa.printBoard();
			Console.WriteLine();
			Console.WriteLine("saved_box >> ");
			Tapa.printBoard(test_state_save.saved_box);
			Console.WriteLine();

			Console.WriteLine("box.[7][1] = BLACK>> ");
			Tapa.box[7][1].Color = Box.BLACK;
			Console.WriteLine("box >> ");
			Tapa.printBoard();
			Console.WriteLine();
			Console.WriteLine("saved_box >> ");
			Tapa.printBoard(test_state_save.saved_box);
			Console.WriteLine();

			Console.WriteLine("test_save[6][1].Color = Box.BLACK");
			test_state_save.saved_box[6][1].Color = Box.BLACK;
			Console.WriteLine("box >> ");
			Tapa.printBoard();
			Console.WriteLine();
			Console.WriteLine("saved_box >> ");
			Tapa.printBoard(test_state_save.saved_box);
			Console.WriteLine();

			Console.WriteLine("set");
			StateSave.setSavedState(test_state_save);
			Console.WriteLine("num_box >> ");
			Tapa.printBoard();
			Console.WriteLine();
			Console.WriteLine("saved_box >> ");
			Tapa.printBoard(test_state_save.saved_box);
			Console.WriteLine();

			Console.WriteLine("box[1][7].Color = Box.BLACK");
			box[1][7].Color = Box.BLACK;
			Console.WriteLine("box >> ");
			Tapa.printBoard();
			Console.WriteLine();
			Console.WriteLine("saved_box >> ");
			Tapa.printBoard(test_state_save.saved_box);
			Console.WriteLine();

			Console.WriteLine("set");
			StateSave.setSavedState(test_state_save);
			Console.WriteLine("num_box >> ");
			Tapa.printBoard();
			Console.WriteLine();
			Console.WriteLine("saved_box >> ");
			Tapa.printBoard(test_state_save.saved_box);
			Console.WriteLine();
			return;
			 * */



			// 数字マス周りのチェック
			// PatternAroundNumBox.checkPatternAroundNumBox();
			/*
			// ボタンをWindows風のスタイルにしてくれる
			Application.EnableVisualStyles();
			// falseにすることでパフォーマンスを優先する
			Application.SetCompatibleTextRenderingDefault(false);
			// Form1()が停止しない間常に動作
			Application.Run(new Display());
			 */
		}

		/*********************************
		 * 
		 * 入力ファイルの読み込み
		 * ## 場所はなぜかvisual studio 2013と同じディレクトリ ##
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

			MAX_BOARD_ROW = row_count + 2;		// 外周のマスも含んだ行数
			MAX_BOARD_COL = column_count + 2;	// 外周のマスも含んだ列数

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

			// 各セルのデータの取得
			for (int i = 1; i <= row_count; i++) {
				List<Box> tmp_box_list = new List<Box>();   // 行ごとのリストを格納する用
				for (int j = 1; j <= column_count; j++) {
					Box tmp_box = new Box();
					int tmp_num = 0;
					string st = "";
					Microsoft.Office.Interop.Excel.Range range = sheet.Cells[i, j];
					if (range.Value != null) {
						st = range.Value.ToString();  // セル(i,j)のデータを文字列で取得
					}
					else {
						Console.Write("Error: セル(" + i + "," + j + ")の読み込み中にエラー(中身がnull)\n");
						Application.Exit();
					}
					tmp_box.coord = new Coordinates(i, j);	// 座標の代入
					if (st.Equals("-")) {
						tmp_box.hasNum = false;
						not_deployedbox_coord_list.Add(new Coordinates(tmp_box.coord));		// 未定マスの座標Listに追加
					}
					else if (int.TryParse(st, out tmp_num)) { // tmp_num=(int)stが数字だった場合
						if (DEBUG) { Console.WriteLine("st >> " + st); }
						int origin_num = tmp_num;
						tmp_box.hasNum = true;
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
						tmp_box.box_num = tmp_num;
						// ####### (end) マスの数字を昇順に並べ替える
						numbox_coord_list.Add(new Coordinates(tmp_box.coord));		// 数字マスの座標Listに追加
					}
					else {
						Console.WriteLine("Error: セル(" + i + "," + j + ")の読み込み中にエラー(中身が数字でも'-'でもない)\n");
					}
					tmp_box_list.Add(new Box(tmp_box));
					tmp_box.clear();
				}
				box.Add(new List<Box>(tmp_box_list));  // 行毎にリストをboxに追加
				tmp_box_list.Clear();
			}

			// 盤面の外側に余分な白マスを生成
			makeOuterBox(row_count, column_count);
			//ワークブックを閉じる
			WorkBook.Close();
			//エクセルを閉じる
			ExcelApp.Quit();

			Box.during_make_inputbord = false;
		}

		/*********************************
		 * 
		 *   盤面の外側に1マスずつ白マスを配置
		 *   引数：（盤面の行数、盤面の列数）
		 *  
		 * *******************************/
		public static void makeOuterBox(int row_count, int column_count)
		{
			// ########## 盤面の外側にも1マスずつマスを配置
			Box tmp_box = new Box();
			List<Box> tmp_top_box_list = new List<Box>();	// 最上行に配置する行
			List<Box> tmp_bot_box_list = new List<Box>();	// 最下行に配置する行
			tmp_box.Color = Box.WHITE;						// 外側のマスは白色
			for (int i = 0; i <= column_count + 1; i++) {
				tmp_box.coord = new Coordinates(0, i);
				tmp_top_box_list.Add(new Box(tmp_box));      // 最上行に追加する空マスのリストに追加

				tmp_box.coord = new Coordinates(row_count + 1, i);
				tmp_bot_box_list.Add(new Box(tmp_box));      // 最下行に追加する空マスのリストに追加
			}
			Tapa.box.Insert(0, new List<Box>(tmp_top_box_list));    // 最上行に空マスのリストを追加
			Tapa.box.Add(new List<Box>(tmp_bot_box_list));          // 最下行に空マスのリストを追加
			for (int i = 1; i <= row_count; i++) {
				tmp_box.coord = new Coordinates(i, 0);					// 座標情報を訂正
				Tapa.box[i].Insert(0, new Box(tmp_box));			// 先頭に空マスを追加
				tmp_box.coord = new Coordinates(i, column_count + 1);	// 座標情報を訂正
				Tapa.box[i].Add(new Box(tmp_box));				// 末尾に空マスを追加
			}
		}

		/*********************************
		 * 
		 *   盤面の出力
		 *   引数
		 *   ll_box	: 出力するBoxの２次元リスト
		 *			　（nullならTapa.boxを出力）
		 *  
		 * *******************************/
		public static void printBoard(List<List<Box>> ll_box = null)
		{
			List<List<Box>> print_box;
			if (ll_box != null) { print_box = ll_box; }
			else { print_box = Tapa.box; }
			foreach (List<Box> tmp_box_list in print_box) {
				foreach (Box tmp_box in tmp_box_list) {
					tmp_box.printBoxNum();
				}
				Console.WriteLine();
			}
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
				tmp_coord.printCoordinates();
				Console.Write(" ");
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

		/*********************************
		 * 
		 *   今の処理をシェルに出力
		 *  
		 * *******************************/
		public static void printNowStateProcess()
		{
			switch (Tapa.NOW_STATE_PROCESS) {
				case 0:
					Console.Write("盤面生成");
					break;
				case 1:
					Console.Write("id_listが一意");
					break;
				case 2:
					Console.Write("id_listからマスの色が決定する");
					break;
				case 3:
					Console.Write("黒マスの上下左右を見て団子を回避");
					break;
				case 4:
					Console.Write("一繋がりの黒マスの伸び代が一意");
					break;
				default:
					Console.Write("Error: 塗られた理由が不明");
					break;
			}
		}


	}
}
