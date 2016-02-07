namespace Tapa
{
    partial class Display
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.startMakeProblem = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.tb_playfile_path = new System.Windows.Forms.TextBox();
			this.sl_playfile_path = new System.Windows.Forms.Button();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radio_dot = new System.Windows.Forms.RadioButton();
			this.radio_random = new System.Windows.Forms.RadioButton();
			this.Dot画から = new System.Windows.Forms.ToolTip(this.components);
			this.ランダム = new System.Windows.Forms.ToolTip(this.components);
			this.sl_dotfile_path = new System.Windows.Forms.Button();
			this.tb_dotfile_path = new System.Windows.Forms.TextBox();
			this.openFileDialog_dotfile = new System.Windows.Forms.OpenFileDialog();
			this.ぱずぷれリンク = new System.Windows.Forms.ToolTip(this.components);
			this.openFileDialog_playfile = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.sl_savefile_path = new System.Windows.Forms.Button();
			this.tb_savefile_path = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// startMakeProblem
			// 
			this.startMakeProblem.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.startMakeProblem.Location = new System.Drawing.Point(155, 312);
			this.startMakeProblem.Name = "startMakeProblem";
			this.startMakeProblem.Size = new System.Drawing.Size(111, 38);
			this.startMakeProblem.TabIndex = 0;
			this.startMakeProblem.Text = "問題生成を開始";
			this.startMakeProblem.UseVisualStyleBackColor = true;
			this.startMakeProblem.Click += new System.EventHandler(this.button2_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 7);
			this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel1.Location = new System.Drawing.Point(12, 333);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(134, 17);
			this.linkLabel1.TabIndex = 1;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "ぱずぷれv3 へ移動する";
			this.ぱずぷれリンク.SetToolTip(this.linkLabel1, "http://pzv.jp");
			this.linkLabel1.UseCompatibleTextRendering = true;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.IntegralHeight = false;
			this.comboBox1.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18"});
			this.comboBox1.Location = new System.Drawing.Point(33, 276);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(44, 20);
			this.comboBox1.TabIndex = 2;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// comboBox2
			// 
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.IntegralHeight = false;
			this.comboBox2.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18"});
			this.comboBox2.Location = new System.Drawing.Point(83, 276);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(44, 20);
			this.comboBox2.TabIndex = 3;
			this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox1.Location = new System.Drawing.Point(33, 258);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(44, 12);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "行数";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.SystemColors.Control;
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox2.Location = new System.Drawing.Point(83, 258);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(44, 12);
			this.textBox2.TabIndex = 5;
			this.textBox2.Text = "列数";
			this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tb_playfile_path
			// 
			this.tb_playfile_path.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.tb_playfile_path.Cursor = System.Windows.Forms.Cursors.Default;
			this.tb_playfile_path.Location = new System.Drawing.Point(12, 123);
			this.tb_playfile_path.Name = "tb_playfile_path";
			this.tb_playfile_path.ReadOnly = true;
			this.tb_playfile_path.Size = new System.Drawing.Size(266, 19);
			this.tb_playfile_path.TabIndex = 6;
			this.tb_playfile_path.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
			// 
			// sl_playfile_path
			// 
			this.sl_playfile_path.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.sl_playfile_path.Location = new System.Drawing.Point(284, 121);
			this.sl_playfile_path.Name = "sl_playfile_path";
			this.sl_playfile_path.Size = new System.Drawing.Size(99, 23);
			this.sl_playfile_path.TabIndex = 7;
			this.sl_playfile_path.Text = "フォルダを選択";
			this.sl_playfile_path.UseVisualStyleBackColor = true;
			this.sl_playfile_path.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox4
			// 
			this.textBox4.BackColor = System.Drawing.SystemColors.Control;
			this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox4.Cursor = System.Windows.Forms.Cursors.Default;
			this.textBox4.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox4.Location = new System.Drawing.Point(50, 12);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(293, 24);
			this.textBox4.TabIndex = 8;
			this.textBox4.Text = "Tapaの問題を生成するだけ";
			// 
			// textBox5
			// 
			this.textBox5.BackColor = System.Drawing.SystemColors.Control;
			this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox5.Cursor = System.Windows.Forms.Cursors.Default;
			this.textBox5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox5.Location = new System.Drawing.Point(24, 105);
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new System.Drawing.Size(296, 12);
			this.textBox5.TabIndex = 9;
			this.textBox5.Text = "遊ぶ問題の選択";
			// 
			// textBox6
			// 
			this.textBox6.BackColor = System.Drawing.SystemColors.Control;
			this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox6.Cursor = System.Windows.Forms.Cursors.Default;
			this.textBox6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox6.Location = new System.Drawing.Point(24, 240);
			this.textBox6.Name = "textBox6";
			this.textBox6.ReadOnly = true;
			this.textBox6.Size = new System.Drawing.Size(296, 12);
			this.textBox6.TabIndex = 10;
			this.textBox6.Text = "生成する問題の行数と列数の選択";
			this.textBox6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(272, 312);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(111, 38);
			this.button2.TabIndex = 17;
			this.button2.Text = "Puzzlevanで遊ぶ";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radio_dot);
			this.groupBox1.Controls.Add(this.radio_random);
			this.groupBox1.Location = new System.Drawing.Point(80, 42);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(228, 42);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "問題生成の方法";
			// 
			// radio_dot
			// 
			this.radio_dot.AutoSize = true;
			this.radio_dot.Location = new System.Drawing.Point(144, 18);
			this.radio_dot.Name = "radio_dot";
			this.radio_dot.Size = new System.Drawing.Size(71, 16);
			this.radio_dot.TabIndex = 1;
			this.radio_dot.TabStop = true;
			this.radio_dot.Text = "Dot画から";
			this.Dot画から.SetToolTip(this.radio_dot, "ぱずぷれ形式のtxtで保存されたデータから問題を生成します");
			this.radio_dot.UseVisualStyleBackColor = true;
			this.radio_dot.CheckedChanged += new System.EventHandler(this.radio_dot_CheckedChanged);
			// 
			// radio_random
			// 
			this.radio_random.AutoSize = true;
			this.radio_random.Location = new System.Drawing.Point(7, 18);
			this.radio_random.Name = "radio_random";
			this.radio_random.Size = new System.Drawing.Size(59, 16);
			this.radio_random.TabIndex = 0;
			this.radio_random.TabStop = true;
			this.radio_random.Text = "ランダム";
			this.ランダム.SetToolTip(this.radio_random, "ランダムで問題を生成します");
			this.radio_random.UseVisualStyleBackColor = true;
			this.radio_random.CheckedChanged += new System.EventHandler(this.radio_random_CheckedChanged);
			// 
			// sl_dotfile_path
			// 
			this.sl_dotfile_path.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.sl_dotfile_path.Location = new System.Drawing.Point(284, 253);
			this.sl_dotfile_path.Name = "sl_dotfile_path";
			this.sl_dotfile_path.Size = new System.Drawing.Size(99, 23);
			this.sl_dotfile_path.TabIndex = 20;
			this.sl_dotfile_path.Text = "フォルダを選択";
			this.sl_dotfile_path.UseVisualStyleBackColor = true;
			this.sl_dotfile_path.Click += new System.EventHandler(this.button3_Click);
			// 
			// tb_dotfile_path
			// 
			this.tb_dotfile_path.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.tb_dotfile_path.Cursor = System.Windows.Forms.Cursors.Default;
			this.tb_dotfile_path.Location = new System.Drawing.Point(12, 255);
			this.tb_dotfile_path.Name = "tb_dotfile_path";
			this.tb_dotfile_path.ReadOnly = true;
			this.tb_dotfile_path.Size = new System.Drawing.Size(266, 19);
			this.tb_dotfile_path.TabIndex = 19;
			// 
			// openFileDialog_dotfile
			// 
			this.openFileDialog_dotfile.FileName = "openFileDialog1";
			this.openFileDialog_dotfile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// openFileDialog_playfile
			// 
			this.openFileDialog_playfile.FileName = "openFileDialog2";
			// 
			// textBox3
			// 
			this.textBox3.BackColor = System.Drawing.SystemColors.Control;
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3.Cursor = System.Windows.Forms.Cursors.Default;
			this.textBox3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox3.Location = new System.Drawing.Point(24, 170);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(296, 12);
			this.textBox3.TabIndex = 23;
			this.textBox3.Text = "生成した問題の保存先を選択";
			// 
			// sl_savefile_path
			// 
			this.sl_savefile_path.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.sl_savefile_path.Location = new System.Drawing.Point(284, 184);
			this.sl_savefile_path.Name = "sl_savefile_path";
			this.sl_savefile_path.Size = new System.Drawing.Size(99, 23);
			this.sl_savefile_path.TabIndex = 22;
			this.sl_savefile_path.Text = "フォルダを選択";
			this.sl_savefile_path.UseVisualStyleBackColor = true;
			this.sl_savefile_path.Click += new System.EventHandler(this.sl_savefile_path_Click);
			// 
			// tb_savefile_path
			// 
			this.tb_savefile_path.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.tb_savefile_path.Cursor = System.Windows.Forms.Cursors.Default;
			this.tb_savefile_path.Location = new System.Drawing.Point(12, 186);
			this.tb_savefile_path.Name = "tb_savefile_path";
			this.tb_savefile_path.ReadOnly = true;
			this.tb_savefile_path.Size = new System.Drawing.Size(266, 19);
			this.tb_savefile_path.TabIndex = 21;
			// 
			// Display
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 360);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.sl_savefile_path);
			this.Controls.Add(this.tb_savefile_path);
			this.Controls.Add(this.sl_dotfile_path);
			this.Controls.Add(this.tb_dotfile_path);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.sl_playfile_path);
			this.Controls.Add(this.tb_playfile_path);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.startMakeProblem);
			this.Name = "Display";
			this.Text = "Tapa_puzzle";
			this.Load += new System.EventHandler(this.Display_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button startMakeProblem;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox tb_playfile_path;
		private System.Windows.Forms.Button sl_playfile_path;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radio_dot;
		private System.Windows.Forms.ToolTip Dot画から;
		private System.Windows.Forms.RadioButton radio_random;
		private System.Windows.Forms.ToolTip ランダム;
		private System.Windows.Forms.Button sl_dotfile_path;
		private System.Windows.Forms.TextBox tb_dotfile_path;
		private System.Windows.Forms.OpenFileDialog openFileDialog_dotfile;
		private System.Windows.Forms.ToolTip ぱずぷれリンク;
		private System.Windows.Forms.OpenFileDialog openFileDialog_playfile;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Button sl_savefile_path;
		private System.Windows.Forms.TextBox tb_savefile_path;


    }
}

