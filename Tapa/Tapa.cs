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
        // マスの2次元配列のようなリスト
        public static List<List<Box>> box = new List<List<Box>>();
        static bool DEBUG = true;

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
            // Excelでは（列、行）座標を用いているため、
            // ########### ここから（j、i）=（行、列）とする。 
            List<Box> tmp_box_list = new List<Box>();   // 行ごとのリストを格納する用
            Box tmp_box = new Box();
            for (int i = 1; i <= row_count; i++) {
                for (int j = 1; j <= column_count; j++) {
                    int tmp_num = 0;
                    string st = "";
                    Microsoft.Office.Interop.Excel.Range range = sheet.Cells[j, i];
                    if (range.Value != null) {
                        st = range.Value.ToString();  // セル(j,i)のデータを文字列で取得
                    }
                    else {
                        Console.Write("Error: セル(" + j + "," + i + ")の読み込み中にエラー(中身がnull)\n");
                        Application.Exit();
                    }
                    if (st.Equals("-")) {
                        tmp_box.has_num = false;
                    }
                    else if (int.TryParse(st, out tmp_num)) { // tmp_num=(int)stが数字だった場合
                        if (DEBUG) { Console.Write("st >> " + st + "\n"); }
                        tmp_box.has_num = true;
                        do {              // 数字を桁毎にリストに追加
                            tmp_box.box_num.Insert(0, tmp_num % 10);
                            tmp_num /= 10;
                        } while (tmp_num > 0);  // do-whileは0の場合を許可するため
                    }
                    else {
                        Console.WriteLine("Error: セル(" + j + "," + i + ")の読み込み中にエラー(中身が数字でも'-'でもない)\n", j, i);
                    }
                    tmp_box_list.Add(tmp_box.clone());
                    tmp_box.clear();
                }
                box.Add(new List<Box>(tmp_box_list));  // 行毎にリストをboxに追加
                tmp_box_list.Clear();
            }
            // ########## ここまで（j、i）=（行、列）とする。


            // ########## 盤面の外側にも1マスずつマスを配置
            tmp_box.my_color = 0;   // 外側のマスは白色
            for (int i = 0; i <= column_count + 1; i++) {
                tmp_box_list.Add(tmp_box.clone());      // 最上(下)行のマスのリストを生成
            }
            box.Insert(0, new List<Box>(tmp_box_list));    // 最上行に空マスのリストを追加
            box.Add(new List<Box>(tmp_box_list));          // 最下行に空マスのリストを追加
            for (int i = 0; i <= row_count + 1; i++) {
                box[i].Insert(0, tmp_box.clone());
                box[i].Add(tmp_box.clone());
            }
            // ##########

            for (int i = 0; i <= row_count+1; i++) {
                // Console.WriteLine("box[" + i + "].size >> " + box[i].Count());
                for (int j = 0; j <= column_count+1; j++) {
                    if (!box[i][j].has_num) {
                        if (box[i][j].my_color == 0) { Console.Write("**** "); }
                        else { Console.Write("---- "); }
                    }
                    else {
                        int count = 5;
                        foreach (int tmp in box[i][j].box_num) {
                            Console.Write(tmp);
                            count--;
                        }
                        while (count-- > 0) { Console.Write(" "); }
                    }
                }
                Console.Write("\n");
            }

            //ワークブックを閉じる
            WorkBook.Close();
            //エクセルを閉じる
            ExcelApp.Quit();
        }
    }
}
