using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;	// setforegraoundwindow
using System.Diagnostics;				// Process

namespace Tapa
{
	// parical:1つのクラスを複数クラスに分離する
	// 今回はDisplay.Designer.csとこれが分離した
	// Designer.csはWindowsフォームデザイナーで編集した内容のみが反映されるため
	// 直接コードを書き込んではいけない
	// 部分クラスの内どれか一つでも基底クラスとアクセス修飾子があれば
	// 残りの部分クラスではそれを省略できる

	public partial class Display : Form
	{

		public Display()
		{
			InitializeComponent();
		}
		/*********************************
		 * 
		 *	問題生成を始める 
		 *   
		 * *******************************/
		private void button2_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			try {
				if (Problem.directory_path.Length == 0) {
					MessageBox.Show("ディレクトリを指定してください。",
									"エラー",
									MessageBoxButtons.OK,
									MessageBoxIcon.Error);
					return;
				}
				if (textBox9.Text.Length == 0) {
					MessageBox.Show("ファイル名を指定してください。",
									"エラー",
									MessageBoxButtons.OK,
									MessageBoxIcon.Error);
					return;
				}

				string tmp = startMakeProblem.Text;
				startMakeProblem.Text = "問題生成中...";
				startMakeProblem.Enabled = false;

				// 時間計測開始
				System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

				// 問題生成の処理
				Problem.manageMakingProblem();

				//時間計測終了
				sw.Stop();
				Console.WriteLine("問題生成にかかった時間 >> " + sw.Elapsed);

				startMakeProblem.Text = tmp;
				startMakeProblem.Enabled = true;
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message, this.Text);
			}
			finally {
				this.Cursor = Cursors.Default;
			}
		}

		/*********************************
		 * 
		 *	ぱずぷれへのリンク 
		 *   
		 * *******************************/
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			//リンク先に移動したことにする
			linkLabel1.LinkVisited = true;
			//ブラウザで開く
			System.Diagnostics.Process.Start("http://pzv.jp/p.html?tapa");
		}

		private void Display_Load(object sender, EventArgs e)
		{
			// デフォルトのファイル名
			textBox9.Text = "tapa_puzzle";
			Problem.file_name = textBox9.Text + ".txt";
			// デフォルトの保存先
			textBox3.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			Problem.directory_path = textBox3.Text + @"\";
			// 行数の初期値(10)を設定
			comboBox1.SelectedIndex = comboBox1.Items.IndexOf("10");
			// 列数の初期値(10)を設定
			comboBox2.SelectedIndex = comboBox2.Items.IndexOf("10");
		}

		/*********************************
		 * 
		 *	最大行数
		 *   
		 * *******************************/
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 追加できないためGUIで設定
			//for (int i = 8; i <= 18; i++) {
			//	// コンボボックスへの項目の追加
			//	comboBox1.Items.Add(i.ToString());
			//}

			// 読み取り専用（テキストボックスは編集不可）にする
			comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			Tapa.MAX_BOARD_ROW = int.Parse(comboBox1.SelectedItem.ToString());	// 行数
		}

		/*********************************
		 * 
		 *	最大列数
		 *   
		 * *******************************/
		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 追加できないためGUIで設定
			//for (int i = 8; i <= 18; i++) {
			//	// コンボボックスへの項目の追加
			//	comboBox2.Items.Add(i.ToString());
			//}

			// 読み取り専用（テキストボックスは編集不可）にする
			comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
			Tapa.MAX_BOARD_COL = int.Parse(comboBox2.SelectedItem.ToString());	// 列数
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		/*********************************
		 * 
		 *	フォルダ選択
		 *   
		 * *******************************/
		private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
		{
			// ダイアログの説明
			folderBrowserDialog1.Description = "Tapaの問題を生成するフォルダを指定してください。\ntapa_puzzle.txtと同名のデータがあった場合上書きされます。";
			// 新しいフォルダボタンを表示する
			folderBrowserDialog1.ShowNewFolderButton = true;
			// ルートになるフォルダの指定
			folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
			// ダイアログを表示し、戻り値がOKならば選択したフォルダのパスを保存する。
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
				textBox3.Text = folderBrowserDialog1.SelectedPath;
				Problem.directory_path = folderBrowserDialog1.SelectedPath + @"\";
			}
		}

		/*********************************
		 * 
		 *	【フォルダを選択】ボタン
		 *   
		 * *******************************/
		private void button1_Click(object sender, EventArgs e)
		{
			folderBrowserDialog1_HelpRequest(sender, e);
		}

		private void textBox8_TextChanged(object sender, EventArgs e)
		{

		}

		/*********************************
		 * 
		 *	ファイル名入力フォーム
		 *   
		 * *******************************/
		private void textBox9_TextChanged(object sender, EventArgs e)
		{
			Problem.file_name = textBox9.Text + ".txt";
		}

		private void textBox10_TextChanged(object sender, EventArgs e)
		{

		}

		// http://www.atmarkit.co.jp/fdotnet/dotnettips/024w32api/w32api.html
		[DllImport("user32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll")]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		private void button2_Click_1(object sender, EventArgs e)
		{
			System.Diagnostics.Process p =
				System.Diagnostics.Process.Start(@"C:\Users\Amano\OneDrive\zemi\puzzlevan\puzzlevan.exe", Problem.file_path);
			// System.Diagnostics.Process.Start(@"D:\negum_d\OneDrive\zemi\puzzlevan\puzzlevan.exe", Problem.file_path);
			System.Threading.Thread.Sleep(500); //少し待つ

			// http://blog.kur.jp/entry/2009/12/05/activewin/
			int id;
			do {
				// アクティブなプロセスを取得
				IntPtr hWnd = GetForegroundWindow();
				GetWindowThreadProcessId(hWnd, out id);
				Console.WriteLine("アクティブなプロセス名 >> " + Process.GetProcessById(id).ProcessName.ToString());

			} while (Process.GetProcessById(id).ProcessName != "puzzlevan");

			// F2 を送信（回答モードにするため）
			System.Threading.Thread.Sleep(500); //少し待つ
			SendKeys.Send("{F2}");
			Console.WriteLine("F2送信");

			p.WaitForExit();
		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{

		}

	}
}
