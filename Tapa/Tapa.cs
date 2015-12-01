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
		// 数字マスの座標のリスト
		public static List<Coordinates> numbox_coord_list = new List<Coordinates>();
		// 未定マスの座標リスト
		public static List<Coordinates> not_deployedbox_coord_list = new List<Coordinates>();
		// 伸び代のある黒マスの座標リスト
		public static List<Coordinates> edge_blackbox_coord_list = new List<Coordinates>();
        public static bool DEBUG = false;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0) {
                Console.WriteLine("Error:コマンドライン引き数が正しくありません。\n"
                                + "第一引数：入力ファイル\n第二引数：出力ファイル\n");
                Application.Exit();
            }

            // 入力ファイルの読み込み
            Tapa.inputTapa(args[0]);
            // 盤面の出力
            Tapa.printBoard();
			Console.WriteLine();

			// 数字マスリストの出力
			//foreach (Coordinates tmp_co in numbox_coord_list) {
			//	Console.Write("(" + tmp_co.x + "," + tmp_co.y + ") >> ");
			//	box[tmp_co.x][tmp_co.y].printBoxNum();
			//	Console.Write("\n");
			//}

			// 未定マスリストの出力
			//foreach (Coordinates tmp_co in not_deployedbox_coord_list) {
			//	Console.Write("(" + tmp_co.x + "," + tmp_co.y + ")");
			//	Console.Write("\n");
			//}
			
			// 準備：数字マスにidのリストを追加
			PatternAroundNumBox.preparePatternArroundNumBox();
			//foreach (Coordinates tmp_co in numbox_coord_list) {
			//	box[tmp_co.x][tmp_co.y].printBoxNum();
			//	Console.Write("(" + tmp_co.x + "," + tmp_co.y + ") >> ");
			//	box[tmp_co.x][tmp_co.y].printIdList();
			//	Console.Write("\n");
			//}

			// 数字マス周りのパターンを管理
			PatternAroundNumBox.managePatternAroundNumBox();

			// 盤面の出力
			Tapa.printBoard();
			Console.WriteLine();

			// 伸び代のある黒マスから、黒マスが伸びないかを見て、可能なら実際に伸ばす。
			Box.extendBlackBox();

			// 盤面の出力
			Tapa.printBoard();
			
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
        static public void inputTapa(string in_filename)
        {
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
        }

        /*********************************
         * 
         *   盤面の外側に1マスずつ白マスを配置
         *   引数：（盤面の行数、盤面の列数）
         *  
         * *******************************/
        private static void makeOuterBox(int row_count, int column_count)
        {
            // ########## 盤面の外側にも1マスずつマスを配置
            Box tmp_box = new Box();
            List<Box> tmp_box_list = new List<Box>();
            tmp_box.Color = Box.WHITE;   // 外側のマスは白色
            for (int i = 0; i <= column_count+1; i++) {
                tmp_box_list.Add(new Box(tmp_box));      // 最上(下)行に追加する空マスのリストを生成
            }
            box.Insert(0, new List<Box>(tmp_box_list));    // 最上行に空マスのリストを追加
            box.Add(new List<Box>(tmp_box_list));          // 最下行に空マスのリストを追加
            for (int i = 1; i <= row_count; i++) {
                box[i].Insert(0, new Box(tmp_box));			// 先頭に空マスを追加
                box[i].Add(new Box(tmp_box));				// 末尾に空マスを追加
            }
        }

        /*********************************
         * 
         *   盤面をシェルに表示
         *  
         * *******************************/
        private static void printBoard()
        {
            foreach (List<Box> tmp_box_list in box) {
                foreach(Box tmp_box in tmp_box_list) {
                    tmp_box.printBoxNum();
                }
                Console.WriteLine();
            }
        }
    }
}
