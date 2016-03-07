namespace Tapa
{
	partial class PictManagementForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.bar_binary = new System.Windows.Forms.TrackBar();
			this.bar_mozaic_size = new System.Windows.Forms.TrackBar();
			this.bar_mozaic = new System.Windows.Forms.TrackBar();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_no = new System.Windows.Forms.Button();
			this.group_hint = new System.Windows.Forms.GroupBox();
			this.select_pict = new System.Windows.Forms.Button();
			this.tb_pictfile_path = new System.Windows.Forms.TextBox();
			this.open_pictfile = new System.Windows.Forms.OpenFileDialog();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.info = new System.Windows.Forms.ToolTip(this.components);
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bar_binary)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bar_mozaic_size)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bar_mozaic)).BeginInit();
			this.group_hint.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(12, 59);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(402, 201);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			// 
			// bar_binary
			// 
			this.bar_binary.Location = new System.Drawing.Point(12, 285);
			this.bar_binary.Name = "bar_binary";
			this.bar_binary.Size = new System.Drawing.Size(402, 45);
			this.bar_binary.TabIndex = 1;
			this.bar_binary.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// bar_mozaic_size
			// 
			this.bar_mozaic_size.Location = new System.Drawing.Point(12, 336);
			this.bar_mozaic_size.Name = "bar_mozaic_size";
			this.bar_mozaic_size.Size = new System.Drawing.Size(402, 45);
			this.bar_mozaic_size.TabIndex = 2;
			this.bar_mozaic_size.Scroll += new System.EventHandler(this.bar_mozaic_size_Scroll);
			// 
			// bar_mozaic
			// 
			this.bar_mozaic.Location = new System.Drawing.Point(12, 387);
			this.bar_mozaic.Name = "bar_mozaic";
			this.bar_mozaic.Size = new System.Drawing.Size(402, 45);
			this.bar_mozaic.TabIndex = 3;
			this.bar_mozaic.Scroll += new System.EventHandler(this.bar_mozaic_Scroll);
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(321, 431);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(93, 31);
			this.button_ok.TabIndex = 4;
			this.button_ok.Text = "決定";
			this.info.SetToolTip(this.button_ok, "編集を終了し、元の画面に戻ります。\r\n編集したファイルは1つのフォルダにまとめられ、選択した画像と同じディレクトリに保存されます。\r\nPuzzlevaで編集した場" +
        "合は、Puzzlevan上で保存してください。");
			this.button_ok.UseVisualStyleBackColor = true;
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_no
			// 
			this.button_no.Location = new System.Drawing.Point(222, 431);
			this.button_no.Name = "button_no";
			this.button_no.Size = new System.Drawing.Size(93, 31);
			this.button_no.TabIndex = 5;
			this.button_no.Text = "リセット";
			this.info.SetToolTip(this.button_no, "画像を始めの状態に戻します。");
			this.button_no.UseVisualStyleBackColor = true;
			this.button_no.Click += new System.EventHandler(this.button_no_Click);
			// 
			// group_hint
			// 
			this.group_hint.Controls.Add(this.select_pict);
			this.group_hint.Controls.Add(this.tb_pictfile_path);
			this.group_hint.Location = new System.Drawing.Point(12, 12);
			this.group_hint.Name = "group_hint";
			this.group_hint.Size = new System.Drawing.Size(402, 41);
			this.group_hint.TabIndex = 32;
			this.group_hint.TabStop = false;
			this.group_hint.Text = "問題を作成したい画像を選択";
			this.group_hint.Enter += new System.EventHandler(this.group_hint_Enter);
			// 
			// select_pict
			// 
			this.select_pict.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.select_pict.Location = new System.Drawing.Point(297, 12);
			this.select_pict.Name = "select_pict";
			this.select_pict.Size = new System.Drawing.Size(99, 23);
			this.select_pict.TabIndex = 30;
			this.select_pict.Text = "画像を選択";
			this.select_pict.UseVisualStyleBackColor = true;
			this.select_pict.Click += new System.EventHandler(this.button1_Click);
			// 
			// tb_pictfile_path
			// 
			this.tb_pictfile_path.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.tb_pictfile_path.Cursor = System.Windows.Forms.Cursors.Default;
			this.tb_pictfile_path.Location = new System.Drawing.Point(9, 14);
			this.tb_pictfile_path.Name = "tb_pictfile_path";
			this.tb_pictfile_path.ReadOnly = true;
			this.tb_pictfile_path.Size = new System.Drawing.Size(282, 19);
			this.tb_pictfile_path.TabIndex = 29;
			// 
			// open_pictfile
			// 
			this.open_pictfile.FileName = "open_pictfile";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(21, 274);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(143, 12);
			this.textBox1.TabIndex = 33;
			this.textBox1.Text = "2値化のしきい値（0~255）";
			this.info.SetToolTip(this.textBox1, "2値化（白黒化）する際のしきい値の設定。デフォルトは128。");
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.SystemColors.Control;
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Location = new System.Drawing.Point(21, 318);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(92, 12);
			this.textBox2.TabIndex = 34;
			this.textBox2.Text = "マスのサイズ(1~32)\r\n";
			this.info.SetToolTip(this.textBox2, "問題のマスのサイズの設定。デフォルトは6。");
			// 
			// textBox4
			// 
			this.textBox4.BackColor = System.Drawing.SystemColors.Control;
			this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox4.Location = new System.Drawing.Point(21, 369);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(161, 12);
			this.textBox4.TabIndex = 36;
			this.textBox4.Text = "マスの色を決めるしきい値(0~100)";
			this.info.SetToolTip(this.textBox4, "マスを黒マスにするか白マスにするかのしきい値の設定。デフォルトは50。");
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 431);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(101, 31);
			this.button1.TabIndex = 37;
			this.button1.Text = "Puzzlevanで編集";
			this.info.SetToolTip(this.button1, "PuzzlevanからDot画を開いて手作業で編集します。\r\n編集したいtxtファイルをPuzzlevanへドラッグアンドドロップしてください。\r\n編集したファイ" +
        "ルはPuzzlevan側で保存してください。");
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_3);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(349, 266);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(65, 20);
			this.button2.TabIndex = 39;
			this.button2.Text = "反映";
			this.info.SetToolTip(this.button2, "編集を終了し、元の画面に戻ります。\r\n編集したファイルは1つのフォルダにまとめられ、選択した画像と同じディレクトリに保存されます。\r\nPuzzlevaで編集した場" +
        "合は、Puzzlevan上で保存してください。");
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// PictManagementForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(426, 480);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.group_hint);
			this.Controls.Add(this.button_no);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this.bar_mozaic);
			this.Controls.Add(this.bar_mozaic_size);
			this.Controls.Add(this.bar_binary);
			this.Controls.Add(this.pictureBox1);
			this.Name = "PictManagementForm";
			this.Text = "PictManagement";
			this.Load += new System.EventHandler(this.PictManagement_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bar_binary)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bar_mozaic_size)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bar_mozaic)).EndInit();
			this.group_hint.ResumeLayout(false);
			this.group_hint.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TrackBar bar_binary;
		private System.Windows.Forms.TrackBar bar_mozaic_size;
		private System.Windows.Forms.TrackBar bar_mozaic;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Button button_no;
		private System.Windows.Forms.GroupBox group_hint;
		private System.Windows.Forms.Button select_pict;
		private System.Windows.Forms.TextBox tb_pictfile_path;
		private System.Windows.Forms.OpenFileDialog open_pictfile;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.ToolTip info;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}