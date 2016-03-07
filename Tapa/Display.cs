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
		public static string puzzlevan_path = @"C:\Users\Amano\OneDrive\zemi\puzzlevan\puzzlevan.exe";

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
				if (radio_hint.Checked) {
					if (tb_hintfile_path.TextLength == 0) {
						MessageBox.Show("ヒントを生成したいファイル名を指定してください。",
											"エラー",
											MessageBoxButtons.OK,
											MessageBoxIcon.Error);
						return;
					}
					if (!System.IO.File.Exists(Problem.prb_hintfile_path)) {
						MessageBox.Show("ヒントを生成したいファイルが存在しません。",
										"エラー",
										MessageBoxButtons.OK,
										MessageBoxIcon.Error);
						return;
					}
				}
				else {
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
				}

				Tapa.MAX_BOARD_ROW = int.Parse(comboBox1.Text);
				Tapa.MAX_BOARD_COL = int.Parse(comboBox2.Text);
				Tapa.BOX_SUM = Tapa.MAX_BOARD_ROW * Tapa.MAX_BOARD_COL;	// マス数

				string tmp = startMakeProblem.Text;
				startMakeProblem.Text = "問題生成中...";
				Enabled = false;
				startMakeProblem.Enabled = false;

				// 時間計測開始
				System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
				// 問題生成の処理
				if (!radio_random.Checked) {
					if (radio_hint.Checked) {
						Problem.hint_percent = int.Parse(comboBox3.Text);
						if (!Problem.manageMakingHintFromTxt()) {
							MessageBox.Show("指定された問題はこのプログラムでは解けませんでした。",
										"力及ばず",
										MessageBoxButtons.OK,
										MessageBoxIcon.Error);
							Problem.is_correct_txtformat = true;
						}
					}
					else { Problem.manageMakingProblemFromTxt(); }
					if (!Problem.is_correct_txtformat) {
						MessageBox.Show("指定されたDot画またはヒント対象ファイルがぱずぷれ形式ではない可能性があります。",
										"エラー",
										MessageBoxButtons.OK,
										MessageBoxIcon.Error);
						Problem.is_correct_txtformat = true;
					}
					if (!Problem.is_correct_tapa_dot) {
						MessageBox.Show("指定されたDot画がTapaの解答として正しくない可能性があります。",
										"エラー",
										MessageBoxButtons.OK,
										MessageBoxIcon.Error);
						Problem.is_correct_tapa_dot = true;
					}
					
				}
				else { Problem.manageMakingProblem(); }
				//時間計測終了
				sw.Stop();
				Console.WriteLine("問題生成にかかった時間 >> " + sw.Elapsed);

				// 遊ぶファイルを今回作ったものにする
				if (radio_hint.Checked) { tb_playfile_path.Text = Problem.playfile_path = Problem.ans_hintfile_path; }
				else { tb_playfile_path.Text = Problem.playfile_path = Problem.savefile_path; }
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
			radio_random_CheckedChanged_1(sender, e);
			radio_normal.Checked = true;

			// デフォルトの保存先（mainの初めに指定）
			tb_savefile_path.Text = Problem.default_path + @"tapa_prob.txt";
			tb_playfile_path.Text = Problem.default_path + @"tapa_prob.txt";
			Problem.savefile_path = tb_savefile_path.Text;
			Problem.playfile_path = tb_playfile_path.Text;
			// 行数の初期値(10)を設定
			comboBox1.SelectedIndex = comboBox1.Items.IndexOf("10");
			// 列数の初期値(10)を設定
			comboBox2.SelectedIndex = comboBox2.Items.IndexOf("10");
			// ヒント数の初期値(3)を設定
			comboBox3.SelectedIndex = comboBox3.Items.IndexOf("3");
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

		// 【puzzlevanで遊ぶ】
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
				System.Diagnostics.Process.Start(puzzlevan_path, Problem.playfile_path);
			// System.Diagnostics.Process.Start(@"D:\negum_d\OneDrive\zemi\puzzlevan\puzzlevan.exe", Problem.file_path);
			System.Threading.Thread.Sleep(500); //少し待つ

			// http://blog.kur.jp/entry/2009/12/05/activewin/
			int id;
			int i = 0;
			do {
				// アクティブなプロセスを取得
				IntPtr hWnd = GetForegroundWindow();
				GetWindowThreadProcessId(hWnd, out id);
				Console.WriteLine("アクティブなプロセス名 >> " + Process.GetProcessById(id).ProcessName.ToString());
				i++;
			} while (Process.GetProcessById(id).ProcessName != "puzzlevan" && i < 5);

			// F2 を送信（Puzzlevanを回答モードにするため）
			SendKeys.Send("{F2}");
			Console.WriteLine("F2送信");
		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{

		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			
		}

		// 【radio】ランダム問題生成
		private void radio_random_CheckedChanged_1(object sender, EventArgs e)
		{
			group_dot.Hide();
			group_hint.Hide();
			group_hint_num.Hide();
			button_pictset.Hide();

			group_play.Show();
			group_save.Show();
			group_rc.Show();

			startMakeProblem.Text = "問題生成";
		}

		// 【radio】Dot画から問題生成
		private void radio_dot_CheckedChanged_2(object sender, EventArgs e)
		{
			group_rc.Hide();
			group_hint.Hide();
			group_hint_num.Hide();

			button_pictset.Show();
			group_play.Show();
			group_save.Show();
			group_dot.Show();

			startMakeProblem.Text = "問題生成";
		}

		// 【radio】ヒント生成
		private void radio_hint_CheckedChanged(object sender, EventArgs e)
		{
			group_dot.Hide();
			group_play.Hide();
			group_save.Hide();
			group_rc.Hide();
			button_pictset.Hide();

			group_hint.Show();
			group_hint_num.Show();

			startMakeProblem.Text = "ヒントの生成";
		}

		private void groupBox1_Enter_3(object sender, EventArgs e)
		{

		}

		private void group_play_Enter(object sender, EventArgs e)
		{

		}

		private void group_save_Enter(object sender, EventArgs e)
		{

		}

		private void group_dot_Enter(object sender, EventArgs e)
		{

		}

		// 【ファイルの選択】ヒントを生成したいファイルを選択
		private void button1_Click_1(object sender, EventArgs e)
		{
			if (this.tb_hintfile_path.Text.Length == 0) {
				this.open_hintfile.FileName = @"*.txt";
				this.open_hintfile.InitialDirectory =
				  Environment.GetFolderPath(
					Environment.SpecialFolder.Desktop);
			}
			else {
				this.open_hintfile.FileName =
				  System.IO.Path.GetFileName(this.tb_hintfile_path.Text);
				this.open_hintfile.InitialDirectory =
				  System.IO.Path.GetDirectoryName(this.tb_hintfile_path.Text);
			}

			this.open_hintfile.DefaultExt = @"txt";
			this.open_hintfile.Filter = @"TEXT(*.txt)|*.txt|すべて(*.*)|*.*";
			this.open_hintfile.FilterIndex = 1;
			this.open_hintfile.Title = @"ヒントを出力したいファイルを選択";
			if (this.open_hintfile.ShowDialog() == DialogResult.OK) {
				this.tb_hintfile_path.Text = this.open_hintfile.FileName;
				Problem.prb_hintfile_path = this.open_hintfile.FileName;
				Console.WriteLine("ヒント対象ファイル >> " + Problem.prb_hintfile_path);
			}
		}

		// 【ファイルの選択】生成した問題の保存先を選択
		// http://codepanic.itigo.jp/cs/dialog_save.html
		private void sl_savefile_path_Click_1(object sender, EventArgs e)
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

		private void tb_savefile_path_TextChanged(object sender, EventArgs e)
		{

		}

		// 【ファイルを選択】遊ぶ問題を選択
		private void sl_playfile_path_Click(object sender, EventArgs e)
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



		private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
		{

		}

		private void open_hintfile_FileOk(object sender, CancelEventArgs e)
		{

		}

		private void openFileDialog_playfile_FileOk(object sender, CancelEventArgs e)
		{

		}

		// http://www.atmarkit.co.jp/fdotnet/chushin/introwinform_04/introwinform_04_02.html
		// 【ファイルの選択】dotファイル選択
		private void sl_dotfile_path_Click(object sender, EventArgs e)
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

		
		// 【combobox】最大行数
		private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			// 追加できないためGUIで設定
			//for (int i = 8; i <= 18; i++) {
			//	// コンボボックスへの項目の追加
			//	comboBox1.Items.Add(i.ToString());
			//}

			// 読み取り専用（テキストボックスは編集不可）にする
			comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		// 【combobox】最大列数
		private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			// 追加できないためGUIで設定
			//for (int i = 8; i <= 18; i++) {
			//	// コンボボックスへの項目の追加
			//	comboBox2.Items.Add(i.ToString());
			//}

			// 読み取り専用（テキストボックスは編集不可）にする
			comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 追加できないためGUIで設定
			//for (int i = 1; i <= 100; i++) {
			//	// コンボボックスへの項目の追加
			//	comboBox2.Items.Add(i.ToString());
			//}

			// 読み取り専用（テキストボックスは編集不可）にする
			comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		// 画像を選択
		private void button_pictset_Click(object sender, EventArgs e)
		{
			PictManagementForm p = new PictManagementForm();
			p.ShowDialog();

			p.Dispose();
		}

		// speed:normal 手法の制限の解除を行う
		private void radio_normal_CheckedChanged(object sender, EventArgs e)
		{
			Tapa.is_fast = false;
		}

		// speed:fast 手法の制限の解除を行わない
		private void radio_easy_CheckedChanged(object sender, EventArgs e)
		{
			Tapa.is_fast = true;
		}
	}
}
