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
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.Dot画から = new System.Windows.Forms.ToolTip(this.components);
			this.radio_dot = new System.Windows.Forms.RadioButton();
			this.ランダム = new System.Windows.Forms.ToolTip(this.components);
			this.radio_random = new System.Windows.Forms.RadioButton();
			this.openFileDialog_dotfile = new System.Windows.Forms.OpenFileDialog();
			this.ぱずぷれリンク = new System.Windows.Forms.ToolTip(this.components);
			this.openFileDialog_playfile = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.ヒント生成 = new System.Windows.Forms.ToolTip(this.components);
			this.radio_hint = new System.Windows.Forms.RadioButton();
			this.group_save = new System.Windows.Forms.GroupBox();
			this.sl_savefile_path = new System.Windows.Forms.Button();
			this.tb_savefile_path = new System.Windows.Forms.TextBox();
			this.group_play = new System.Windows.Forms.GroupBox();
			this.sl_playfile_path = new System.Windows.Forms.Button();
			this.tb_playfile_path = new System.Windows.Forms.TextBox();
			this.group_dot = new System.Windows.Forms.GroupBox();
			this.sl_dotfile_path = new System.Windows.Forms.Button();
			this.tb_dotfile_path = new System.Windows.Forms.TextBox();
			this.group_rc = new System.Windows.Forms.GroupBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.group_hint = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.tb_hintfile_path = new System.Windows.Forms.TextBox();
			this.open_hintfile = new System.Windows.Forms.OpenFileDialog();
			this.group_save.SuspendLayout();
			this.group_play.SuspendLayout();
			this.group_dot.SuspendLayout();
			this.group_rc.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.group_hint.SuspendLayout();
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
			// radio_dot
			// 
			this.radio_dot.AutoSize = true;
			this.radio_dot.Location = new System.Drawing.Point(129, 29);
			this.radio_dot.Name = "radio_dot";
			this.radio_dot.Size = new System.Drawing.Size(119, 16);
			this.radio_dot.TabIndex = 1;
			this.radio_dot.TabStop = true;
			this.radio_dot.Text = "Dot画から問題生成";
			this.Dot画から.SetToolTip(this.radio_dot, "ぱずぷれ形式のtxtファイルから問題を生成します");
			this.radio_dot.UseVisualStyleBackColor = true;
			this.radio_dot.CheckedChanged += new System.EventHandler(this.radio_dot_CheckedChanged_2);
			// 
			// radio_random
			// 
			this.radio_random.AutoSize = true;
			this.radio_random.Location = new System.Drawing.Point(12, 29);
			this.radio_random.Name = "radio_random";
			this.radio_random.Size = new System.Drawing.Size(107, 16);
			this.radio_random.TabIndex = 0;
			this.radio_random.TabStop = true;
			this.radio_random.Text = "ランダム問題生成";
			this.ランダム.SetToolTip(this.radio_random, "ランダムで問題を生成します");
			this.radio_random.UseVisualStyleBackColor = true;
			this.radio_random.CheckedChanged += new System.EventHandler(this.radio_random_CheckedChanged_1);
			// 
			// openFileDialog_dotfile
			// 
			this.openFileDialog_dotfile.FileName = "openFileDialog1";
			this.openFileDialog_dotfile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// openFileDialog_playfile
			// 
			this.openFileDialog_playfile.FileName = "openFileDialog2";
			this.openFileDialog_playfile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_playfile_FileOk);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
			// 
			// radio_hint
			// 
			this.radio_hint.AutoSize = true;
			this.radio_hint.Location = new System.Drawing.Point(263, 29);
			this.radio_hint.Name = "radio_hint";
			this.radio_hint.Size = new System.Drawing.Size(81, 16);
			this.radio_hint.TabIndex = 2;
			this.radio_hint.TabStop = true;
			this.radio_hint.Text = "ヒントを生成";
			this.ヒント生成.SetToolTip(this.radio_hint, "ぱずぷれ形式のtxtから問題のヒントを生成します");
			this.radio_hint.UseVisualStyleBackColor = true;
			this.radio_hint.CheckedChanged += new System.EventHandler(this.radio_hint_CheckedChanged);
			// 
			// group_save
			// 
			this.group_save.Controls.Add(this.sl_savefile_path);
			this.group_save.Controls.Add(this.tb_savefile_path);
			this.group_save.Location = new System.Drawing.Point(15, 170);
			this.group_save.Name = "group_save";
			this.group_save.Size = new System.Drawing.Size(368, 40);
			this.group_save.TabIndex = 29;
			this.group_save.TabStop = false;
			this.group_save.Text = "生成した問題の保存先を選択";
			this.group_save.Enter += new System.EventHandler(this.group_save_Enter);
			// 
			// sl_savefile_path
			// 
			this.sl_savefile_path.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.sl_savefile_path.Location = new System.Drawing.Point(263, 15);
			this.sl_savefile_path.Name = "sl_savefile_path";
			this.sl_savefile_path.Size = new System.Drawing.Size(99, 23);
			this.sl_savefile_path.TabIndex = 25;
			this.sl_savefile_path.Text = "フォルダを選択";
			this.sl_savefile_path.UseVisualStyleBackColor = true;
			this.sl_savefile_path.Click += new System.EventHandler(this.sl_savefile_path_Click_1);
			// 
			// tb_savefile_path
			// 
			this.tb_savefile_path.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.tb_savefile_path.Cursor = System.Windows.Forms.Cursors.Default;
			this.tb_savefile_path.Location = new System.Drawing.Point(9, 17);
			this.tb_savefile_path.Name = "tb_savefile_path";
			this.tb_savefile_path.ReadOnly = true;
			this.tb_savefile_path.Size = new System.Drawing.Size(242, 19);
			this.tb_savefile_path.TabIndex = 24;
			this.tb_savefile_path.TextChanged += new System.EventHandler(this.tb_savefile_path_TextChanged);
			// 
			// group_play
			// 
			this.group_play.Controls.Add(this.sl_playfile_path);
			this.group_play.Controls.Add(this.tb_playfile_path);
			this.group_play.Location = new System.Drawing.Point(15, 123);
			this.group_play.Name = "group_play";
			this.group_play.Size = new System.Drawing.Size(368, 40);
			this.group_play.TabIndex = 30;
			this.group_play.TabStop = false;
			this.group_play.Text = "遊ぶ問題を選択";
			this.group_play.Enter += new System.EventHandler(this.group_play_Enter);
			// 
			// sl_playfile_path
			// 
			this.sl_playfile_path.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.sl_playfile_path.Location = new System.Drawing.Point(263, 12);
			this.sl_playfile_path.Name = "sl_playfile_path";
			this.sl_playfile_path.Size = new System.Drawing.Size(99, 23);
			this.sl_playfile_path.TabIndex = 30;
			this.sl_playfile_path.Text = "フォルダを選択";
			this.sl_playfile_path.UseVisualStyleBackColor = true;
			this.sl_playfile_path.Click += new System.EventHandler(this.sl_playfile_path_Click);
			// 
			// tb_playfile_path
			// 
			this.tb_playfile_path.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.tb_playfile_path.Cursor = System.Windows.Forms.Cursors.Default;
			this.tb_playfile_path.Location = new System.Drawing.Point(9, 14);
			this.tb_playfile_path.Name = "tb_playfile_path";
			this.tb_playfile_path.ReadOnly = true;
			this.tb_playfile_path.Size = new System.Drawing.Size(242, 19);
			this.tb_playfile_path.TabIndex = 29;
			// 
			// group_dot
			// 
			this.group_dot.Controls.Add(this.sl_dotfile_path);
			this.group_dot.Controls.Add(this.tb_dotfile_path);
			this.group_dot.Location = new System.Drawing.Point(15, 216);
			this.group_dot.Name = "group_dot";
			this.group_dot.Size = new System.Drawing.Size(368, 40);
			this.group_dot.TabIndex = 27;
			this.group_dot.TabStop = false;
			this.group_dot.Text = "生成対象のDot画を選択";
			this.group_dot.Enter += new System.EventHandler(this.group_dot_Enter);
			// 
			// sl_dotfile_path
			// 
			this.sl_dotfile_path.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.sl_dotfile_path.Location = new System.Drawing.Point(263, 15);
			this.sl_dotfile_path.Name = "sl_dotfile_path";
			this.sl_dotfile_path.Size = new System.Drawing.Size(99, 23);
			this.sl_dotfile_path.TabIndex = 23;
			this.sl_dotfile_path.Text = "フォルダを選択";
			this.sl_dotfile_path.UseVisualStyleBackColor = true;
			this.sl_dotfile_path.Click += new System.EventHandler(this.sl_dotfile_path_Click);
			// 
			// tb_dotfile_path
			// 
			this.tb_dotfile_path.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.tb_dotfile_path.Cursor = System.Windows.Forms.Cursors.Default;
			this.tb_dotfile_path.Location = new System.Drawing.Point(9, 17);
			this.tb_dotfile_path.Name = "tb_dotfile_path";
			this.tb_dotfile_path.ReadOnly = true;
			this.tb_dotfile_path.Size = new System.Drawing.Size(242, 19);
			this.tb_dotfile_path.TabIndex = 22;
			// 
			// group_rc
			// 
			this.group_rc.Controls.Add(this.textBox2);
			this.group_rc.Controls.Add(this.textBox1);
			this.group_rc.Controls.Add(this.comboBox2);
			this.group_rc.Controls.Add(this.comboBox1);
			this.group_rc.Location = new System.Drawing.Point(15, 216);
			this.group_rc.Name = "group_rc";
			this.group_rc.Size = new System.Drawing.Size(368, 48);
			this.group_rc.TabIndex = 31;
			this.group_rc.TabStop = false;
			this.group_rc.Text = "行数と列数を選択";
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.SystemColors.Control;
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox2.Location = new System.Drawing.Point(207, 9);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(44, 12);
			this.textBox2.TabIndex = 9;
			this.textBox2.Text = "列数";
			this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox1.Location = new System.Drawing.Point(98, 9);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(44, 12);
			this.textBox1.TabIndex = 8;
			this.textBox1.Text = "行数";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
			this.comboBox2.Location = new System.Drawing.Point(207, 23);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(44, 20);
			this.comboBox2.TabIndex = 7;
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
			this.comboBox1.Location = new System.Drawing.Point(98, 23);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(44, 20);
			this.comboBox1.TabIndex = 6;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radio_hint);
			this.groupBox1.Controls.Add(this.radio_dot);
			this.groupBox1.Controls.Add(this.radio_random);
			this.groupBox1.Location = new System.Drawing.Point(15, 42);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(368, 59);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "モード";
			this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter_3);
			// 
			// group_hint
			// 
			this.group_hint.Controls.Add(this.button1);
			this.group_hint.Controls.Add(this.tb_hintfile_path);
			this.group_hint.Location = new System.Drawing.Point(15, 123);
			this.group_hint.Name = "group_hint";
			this.group_hint.Size = new System.Drawing.Size(368, 40);
			this.group_hint.TabIndex = 31;
			this.group_hint.TabStop = false;
			this.group_hint.Text = "ヒントを生成したいtxtファイルを選択";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button1.Location = new System.Drawing.Point(263, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(99, 23);
			this.button1.TabIndex = 30;
			this.button1.Text = "フォルダを選択";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// tb_hintfile_path
			// 
			this.tb_hintfile_path.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.tb_hintfile_path.Cursor = System.Windows.Forms.Cursors.Default;
			this.tb_hintfile_path.Location = new System.Drawing.Point(9, 14);
			this.tb_hintfile_path.Name = "tb_hintfile_path";
			this.tb_hintfile_path.ReadOnly = true;
			this.tb_hintfile_path.Size = new System.Drawing.Size(242, 19);
			this.tb_hintfile_path.TabIndex = 29;
			// 
			// open_hintfile
			// 
			this.open_hintfile.FileName = "open_hintfile";
			this.open_hintfile.FileOk += new System.ComponentModel.CancelEventHandler(this.open_hintfile_FileOk);
			// 
			// Display
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 360);
			this.Controls.Add(this.group_hint);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.group_rc);
			this.Controls.Add(this.group_dot);
			this.Controls.Add(this.group_play);
			this.Controls.Add(this.group_save);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.startMakeProblem);
			this.Name = "Display";
			this.Text = "Tapa_puzzle";
			this.Load += new System.EventHandler(this.Display_Load);
			this.group_save.ResumeLayout(false);
			this.group_save.PerformLayout();
			this.group_play.ResumeLayout(false);
			this.group_play.PerformLayout();
			this.group_dot.ResumeLayout(false);
			this.group_dot.PerformLayout();
			this.group_rc.ResumeLayout(false);
			this.group_rc.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.group_hint.ResumeLayout(false);
			this.group_hint.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button startMakeProblem;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ToolTip Dot画から;
		private System.Windows.Forms.ToolTip ランダム;
		private System.Windows.Forms.OpenFileDialog openFileDialog_dotfile;
		private System.Windows.Forms.ToolTip ぱずぷれリンク;
		private System.Windows.Forms.OpenFileDialog openFileDialog_playfile;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ToolTip ヒント生成;
		private System.Windows.Forms.GroupBox group_save;
		private System.Windows.Forms.Button sl_savefile_path;
		private System.Windows.Forms.TextBox tb_savefile_path;
		private System.Windows.Forms.GroupBox group_dot;
		private System.Windows.Forms.Button sl_dotfile_path;
		private System.Windows.Forms.TextBox tb_dotfile_path;
		private System.Windows.Forms.GroupBox group_play;
		private System.Windows.Forms.Button sl_playfile_path;
		private System.Windows.Forms.TextBox tb_playfile_path;
		private System.Windows.Forms.GroupBox group_rc;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radio_hint;
		private System.Windows.Forms.RadioButton radio_dot;
		private System.Windows.Forms.RadioButton radio_random;
		private System.Windows.Forms.GroupBox group_hint;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox tb_hintfile_path;
		private System.Windows.Forms.OpenFileDialog open_hintfile;


    }
}

