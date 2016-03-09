using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tapa
{
	public partial class PictManagementForm : Form
	{
		private readonly byte DEF_THR_BINARY = 128;			// 2値化のしきい値
		private readonly int DEF_MOZAIC_SIZE = 6;			// モザイクのサイズ
		private readonly int DEF_THR_MOZAIC_COLOR = 50;	// モザイクの色判定のしきい値

		private static string dir_path;		// オリジナル画像のあるディレクトリのパス
		private static string file_name;	// オリジナル画像のファイル名
		private static string savedir_path;	// 保存先のディレクトリパス

		public PictManagementForm()
		{
			InitializeComponent();
		}

		// ２値化のしきい値を表示
		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			info.SetToolTip(bar_binary, bar_binary.Value.ToString());
		}

		// マスのサイズを表示
		private void bar_mozaic_size_Scroll(object sender, EventArgs e)
		{
			info.SetToolTip(bar_mozaic_size, bar_mozaic_size.Value.ToString());
		}

		// マスの色を決めるピクセル数のしきい値を表示
		private void bar_mozaic_Scroll(object sender, EventArgs e)
		{
			info.SetToolTip(bar_mozaic, bar_mozaic.Value.ToString());
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}

		private void PictManagement_Load(object sender, EventArgs e)
		{
			// 画像を枠に収まるよう表示
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

			// ### binary
			// 最小値、最大値を設定
			bar_binary.Minimum = 0;
			bar_binary.Maximum = 255;

			// 初期値を設定
			bar_binary.Value = DEF_THR_BINARY;

			// 描画される目盛りの刻みを設定
			bar_binary.TickFrequency = 1;

			// スライダーをキーボードやマウス、
			// PageUp,Downキーで動かした場合の移動量設定
			bar_binary.SmallChange = 1;
			bar_binary.LargeChange = 10;

			// ### mozaic マスサイズ
			bar_mozaic_size.Minimum = 1;
			bar_mozaic_size.Maximum = 32;

			bar_mozaic_size.Value = DEF_MOZAIC_SIZE;

			bar_mozaic_size.TickFrequency = 1;

			bar_mozaic_size.SmallChange = 1;
			bar_mozaic_size.LargeChange = 4;

			// ### mozaic しきい値
			bar_mozaic.Minimum = 0;
			bar_mozaic.Maximum = 100;

			bar_mozaic.Value = DEF_THR_MOZAIC_COLOR;

			bar_mozaic.TickFrequency = 1;

			bar_mozaic.SmallChange = 1;
			bar_mozaic.LargeChange = 10;
		}


		private void group_hint_Enter(object sender, EventArgs e)
		{

		}

		// 画像選択
		private void button1_Click(object sender, EventArgs e)
		{
			if (this.tb_pictfile_path.Text.Length == 0) {
				this.open_pictfile.FileName = @"*.jpg";
				this.open_pictfile.InitialDirectory =
				  Environment.GetFolderPath(
					Environment.SpecialFolder.Desktop);
			}
			else {
				this.open_pictfile.FileName =
				  System.IO.Path.GetFileName(this.tb_pictfile_path.Text);
				this.open_pictfile.InitialDirectory =
				  System.IO.Path.GetDirectoryName(this.tb_pictfile_path.Text);
			}

			this.open_pictfile.DefaultExt = "画像ﾌｧｲﾙ(*.bmp,*.jpg,*.png,*.gif)|*.bmp;*.jpg;*.png;*.gif"; 
			//this.open_pictfile.Filter = @"JPEG(*.jpg)|*.jpg|"
			//							+ @"ビットマップファイル(*.bmp)|*.bmp|"
			//							+ @"GIFファイル(*.gif)|*.gif|"
			//							+ @"すべて(*.*)|*.*";

			this.open_pictfile.FilterIndex = 1;
			this.open_pictfile.Title = @"問題を生成したい画像を選択";
			if (this.open_pictfile.ShowDialog() == DialogResult.OK) {
				this.tb_pictfile_path.Text = this.open_pictfile.FileName;
				// オリジナル画像のディレクトリパス、ファイル名の取得
				PictManagementForm.file_name = System.IO.Path.GetFileName(this.tb_pictfile_path.Text);
				PictManagementForm.dir_path = System.IO.Path.GetDirectoryName(this.tb_pictfile_path.Text);
				// 保存先のディレクトリを作成、パスを取得
				PictManagementForm.savedir_path
					= FolderManagement.makeFolder(PictManagementForm.dir_path, PictManagementForm.file_name);
				pictureBox1.Image = PictToDotManagement.makeDotFromPict(
					PictManagementForm.dir_path, PictManagementForm.file_name, PictManagementForm.savedir_path);
			}
		}

		// 反映
		private void button2_Click(object sender, EventArgs e)
		{
			if (tb_pictfile_path.TextLength == 0) {
				MessageBox.Show("編集する画像ファイルを選択してください。",
								"エラー",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}
			if (!System.IO.File.Exists(tb_pictfile_path.Text)) {
				MessageBox.Show("選択されたファイルが存在しません。",
								"エラー",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}

			Enabled = false;
			pictureBox1.Image = PictToDotManagement.makeDotFromPict(
				PictManagementForm.dir_path,
				PictManagementForm.file_name,
				PictManagementForm.savedir_path,
				(byte)bar_binary.Value,
				(int)bar_mozaic_size.Value,
				(int)bar_mozaic.Value
				);
			//コントロールを再描画する。これがないと、新しい画像が表示されない。
			pictureBox1.Invalidate(); 
			Enabled = true;
		}

		// リセット
		private void button_no_Click(object sender, EventArgs e)
		{
			if (tb_pictfile_path.TextLength == 0) {	return; }

			Enabled = false;
			// サイズ、しきい値の初期化
			bar_binary.Value = DEF_THR_BINARY;
			bar_mozaic_size.Value = DEF_MOZAIC_SIZE;
			bar_mozaic.Value = DEF_THR_MOZAIC_COLOR;

			pictureBox1.Image = PictToDotManagement.makeDotFromPict(
					PictManagementForm.dir_path, PictManagementForm.file_name, PictManagementForm.savedir_path
					, DEF_THR_BINARY, DEF_MOZAIC_SIZE, DEF_THR_MOZAIC_COLOR);

			pictureBox1.Invalidate();
			Enabled = true;
		}

		private void button1_Click_2(object sender, EventArgs e)
		{

		}

		// 決定
		private void button_ok_Click(object sender, EventArgs e)
		{
			if (tb_pictfile_path.TextLength == 0) {
				MessageBox.Show("編集する画像ファイルを選択してください。",
								"エラー",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}
			if (!System.IO.File.Exists(tb_pictfile_path.Text)) {
				MessageBox.Show("選択されたファイルが存在しません。",
								"エラー",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}


			string tmp = button_ok.Text;
			button_ok.Text = "変換中...";
			Enabled = false;
			// ぱずぷれ形式に変換
			PictToDotManagement.makePzprFromMozaic(
				PictManagementForm.savedir_path,
				PictManagementForm.file_name,
				new Bitmap(pictureBox1.Image),
				(int)bar_mozaic_size.Value
				);
			button_ok.Text = tmp;
			Enabled = true;
		}

		// Puzlevanで編集
		private void button1_Click_3(object sender, EventArgs e)
		{
			if (tb_pictfile_path.TextLength == 0) {
				MessageBox.Show("編集する画像ファイルを選択してください。",
								"エラー",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}
			if (!System.IO.File.Exists(tb_pictfile_path.Text)) {
				MessageBox.Show("選択されたファイルが存在しません。",
								"エラー",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}

			// Puzzlevanの起動
			System.Diagnostics.Process p =
				System.Diagnostics.Process.Start(Display.puzzlevan_path);
			// フォルダを開く
			System.Diagnostics.Process.Start(savedir_path);
		}

		// PictureBox1のPaintイベントハンドラ
		// 画像上にマス数を出力(Imageオブジェクトには書き込まない)
		// http://dobon.net/vb/dotnet/graphics/pictureboximageanddrawimage.html
		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			if (tb_pictfile_path.TextLength == 0) { return; }

			//画像の上に文字列を描画する
			e.Graphics.DrawString(Tapa.MAX_BOARD_ROW + "*" + Tapa.MAX_BOARD_COL, this.Font, Brushes.Red, 1, 1);
			this.Invalidate();
		}

		
	}
}
