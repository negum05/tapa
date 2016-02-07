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
				Console.WriteLine("radio >> " + radio_dot.Checked);

				if (tb_savefile_path.TextLength == 0) {
					MessageBox.Show("保存先ファイルを指定してください。",
									"エラー",
									MessageBoxButtons.OK,
									MessageBoxIcon.Error);
					return;
				}
				if (radio_dot.Checked) {
					if (tb_dotfile_path.TextLength == 0) {
						MessageBox.Show("Dot画のファイル名を指定してください。",
											"エラー",
											MessageBoxButtons.OK,
											MessageBoxIcon.Error);
						return;
					}
					if (!System.IO.File.Exists(Problem.dotfile_path)) {
						MessageBox.Show("指定されたDot画のファイルが存在しません。",
										"エラー",
										MessageBoxButtons.OK,
										MessageBoxIcon.Error);
						return;
					}
				}

				Console.WriteLine("save >> " + Problem.savefile_path);
				Console.WriteLine("play >> " + Problem.playfile_path);
				Console.WriteLine("dot >> " + Problem.dotfile_path);

				Tapa.BOX_SUM = Tapa.MAX_BOARD_ROW * Tapa.MAX_BOARD_COL;	// マス数

				string tmp = startMakeProblem.Text;
				startMakeProblem.Text = "問題生成中...";
				Enabled = false;
				startMakeProblem.Enabled = false;

				// 時間計測開始
				System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
				// 問題生成の処理
				if (radio_dot.Checked) {
					Problem.manageMakingProblemFromTxt();
					if (!Problem.is_correct_txtformat) {
						MessageBox.Show("指定されたDot画がぱずぷれ形式ではない可能性があります。",
										"エラー",
										MessageBoxButtons.OK,
										MessageBoxIcon.Error);
						Problem.is_correct_txtformat = true;
					}
				}
				else { Problem.manageMakingProblem(); }
				//時間計測終了
				sw.Stop();
				Console.WriteLine("問題生成にかかった時間 >> " + sw.Elapsed);

				// 遊ぶファイルを今回作ったものにする
				tb_playfile_path.Text = Problem.playfile_path = Problem.savefile_path;
				startMakeProblem.Text = tmp;
				startMakeProblem.Enabled = true;
				Enabled = true;
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

		// Shownイベント・ハンドラ
		private void Form1_Shown(object sender, EventArgs e)
		{

		}

		private void Display_Load(object sender, EventArgs e)
		{
			radio_random.Checked = true;
			radio_random_CheckedChanged(sender, e);
			// デフォルトの保存先（mainの初めに指定）
			tb_savefile_path.Text = Problem.default_path + @"tapa_prob.txt";
			tb_playfile_path.Text = Problem.default_path + @"tapa_prob.txt";
			Problem.savefile_path = tb_savefile_path.Text;
			Problem.playfile_path = tb_playfile_path.Text;
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
		 *	【フォルダを選択】ボタン
		 *   
		 * *******************************/
		private void button1_Click(object sender, EventArgs e)
		{
			if (this.tb_playfile_path.Text.Length == 0) {
				this.openFileDialog_playfile.FileName = @"*.txt";
				this.openFileDialog_playfile.InitialDirectory =
				  Environment.GetFolderPath(
					Environment.SpecialFolder.Desktop);
			}
			else {
				this.openFileDialog_playfile.FileName =
				  System.IO.Path.GetFileName(this.tb_playfile_path.Text);
				this.openFileDialog_playfile.InitialDirectory =
				  System.IO.Path.GetDirectoryName(this.tb_playfile_path.Text);
			}

			this.openFileDialog_playfile.DefaultExt = @"txt";
			this.openFileDialog_playfile.Filter = @"TEXT(*.txt)|*.txt|すべて(*.*)|*.*";
			this.openFileDialog_playfile.FilterIndex = 1;
			this.openFileDialog_playfile.Title = @"遊ぶファイルを選択";
			if (this.openFileDialog_playfile.ShowDialog() == DialogResult.OK) {
				this.tb_playfile_path.Text = this.openFileDialog_playfile.FileName;
				Problem.playfile_path = this.openFileDialog_playfile.FileName;
				Console.WriteLine("遊ぶファイル >> " + Problem.playfile_path);
			}
		}

		private void textBox8_TextChanged(object sender, EventArgs e)
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
			if (tb_playfile_path.TextLength == 0) {
				MessageBox.Show("遊ぶファイルを選択してください。",
								"エラー",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}
			if (!System.IO.File.Exists(Problem.playfile_path)) {
				MessageBox.Show("選択されたファイルが存在しません。",
								"エラー",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}

			System.Diagnostics.Process p =
				System.Diagnostics.Process.Start(@"C:\Users\Amano\OneDrive\zemi\puzzlevan\puzzlevan.exe", Problem.playfile_path);
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
		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{

		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void radio_random_CheckedChanged(object sender, EventArgs e)
		{
			tb_dotfile_path.Hide();
			sl_dotfile_path.Hide();

			textBox6.Text = "生成する問題の行数と列数の選択";
			textBox1.Show();
			textBox2.Show();
			comboBox1.Show();
			comboBox2.Show();
		}

		private void radio_dot_CheckedChanged(object sender, EventArgs e)
		{
			// 行・列数選択を非表示
			textBox1.Hide();
			textBox2.Hide();
			comboBox1.Hide();
			comboBox2.Hide();

			textBox6.Text = "対象のDot画の選択";
			sl_dotfile_path.Show();
			tb_dotfile_path.Show();
		}

		// http://www.atmarkit.co.jp/fdotnet/chushin/introwinform_04/introwinform_04_02.html
		private void button3_Click(object sender, EventArgs e)
		{
			if (this.tb_dotfile_path.Text.Length == 0) {
				this.openFileDialog_dotfile.FileName = @"*.txt";
				this.openFileDialog_dotfile.InitialDirectory =
				  Environment.GetFolderPath(
					Environment.SpecialFolder.Desktop);
			}
			else {
				this.openFileDialog_dotfile.FileName =
				  System.IO.Path.GetFileName(this.tb_dotfile_path.Text);
				this.openFileDialog_dotfile.InitialDirectory =
				  System.IO.Path.GetDirectoryName(this.tb_dotfile_path.Text);
			}

			this.openFileDialog_dotfile.DefaultExt = @"txt";
			this.openFileDialog_dotfile.Filter = @"TEXT(*.txt)|*.txt|すべて(*.*)|*.*";
			this.openFileDialog_dotfile.FilterIndex = 1;
			this.openFileDialog_dotfile.Title = @"Dot画の選択";
			if (this.openFileDialog_dotfile.ShowDialog() == DialogResult.OK) {
				this.tb_dotfile_path.Text = this.openFileDialog_dotfile.FileName;
				Problem.dotfile_path = this.openFileDialog_dotfile.FileName;
				Console.WriteLine("dotファイル >> " + Problem.dotfile_path);
			}
		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{

		}

		// http://codepanic.itigo.jp/cs/dialog_save.html
		private void sl_savefile_path_Click(object sender, EventArgs e)
		{
			// ダイアログのタイトルを指定
			saveFileDialog1.Title = "生成した問題の保存先";
			// 初期フォルダを指定
			saveFileDialog1.InitialDirectory = System.Environment.CurrentDirectory + @"\tapa_prob\";
			// 初期ファイル名
			saveFileDialog1.FileName = "tapa_prob.txt";
			// ファイルが存在するかどうかチェックしない
			saveFileDialog1.CheckFileExists = false;
			// 拡張子が入力されなければ自動で付与するか否か
			saveFileDialog1.AddExtension = true;
			// ファイルが既に存在する場合警告するか否か
			saveFileDialog1.OverwritePrompt = true;
			// ファイルの種類、拡張子のフィルターを設定
			saveFileDialog1.Filter =
				"すべてのファイル(*.*)|*.*|" +
				"テキストファイル(*.txt)|*.txt";
			// 初期状態で選択されているフィルターは何番目か(インデックスは１から)
			saveFileDialog1.FilterIndex = 2;
			// ダイアログを表示
			DialogResult ret = saveFileDialog1.ShowDialog();
			// 【保存】ボタンで閉じられていれば、ファイル保存先を変更
			if (ret == DialogResult.OK) {
				tb_savefile_path.Text = saveFileDialog1.FileName;
				Problem.savefile_path = tb_savefile_path.Text;
				Console.WriteLine("ファイル保存先 >> " + Problem.savefile_path);
			}
		}

		private void textBox6_TextChanged(object sender, EventArgs e)
		{

		}

	}
}
